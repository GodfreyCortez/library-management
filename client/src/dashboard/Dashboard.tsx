import { useEffect, useState, CSSProperties } from "react";
import { Book } from "../types/library/books";
import { ApiService } from "../services/api.service";
import { Button, StyledEngineProvider } from "@mui/material";
import SortedTable from "./sorted-table/SortedTable";
import AddBookModal from "./add-book-modal/AddBookModal";

const buttonStyle: CSSProperties = {
  position: "absolute",
  top: "5%",
  right: "5%",
  border: "2px solid #000",
  color: "black",
};

export default function Dashboard() {
  const [books, setBooks] = useState<Book[]>([]);

  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);

  useEffect(() => {
    ApiService.getAllBooks().then((booksResponse) => {
      setBooks(booksResponse);
    });
  }, [open]);

  const handleAddBooks = (selected: string[]) => {
    const addBookPromises = selected.map((bookId) =>
      ApiService.addBook(bookId)
    );

    Promise.all(addBookPromises).then(() => {
      setOpen(false);
    });
  };

  const handleDelete = (selected: string[]) => {
    /**
     * After deleting all the books,
     * then refresh with the updated list from the DB
     */
    const deletePromises = selected.map((bookId) =>
      ApiService.deleteBook(bookId)
    );

    Promise.all(deletePromises)
      .then(() => {
        return ApiService.getAllBooks();
      })
      .then((booksResponse) => {
        setBooks(booksResponse);
      });
  };

  return (
    <StyledEngineProvider injectFirst>
      <Button style={buttonStyle} variant="contained" onClick={handleOpen}>
        Add Book
      </Button>
      <AddBookModal
        open={open}
        onClose={handleClose}
        onAddBooks={handleAddBooks}
      />
      <SortedTable
        rows={books}
        tooltipActionType={"Delete"}
        tooltipAction={handleDelete}
      />
    </StyledEngineProvider>
  );
}
