import { useEffect, useState } from "react";
import { Book } from "../types/library/books";
import { ApiService } from "../services/api.service";
import { StyledEngineProvider } from "@mui/material";
import SortedTable from "./sorted-table/SortedTable";

export default function Dashboard() {
  const [books, setBooks] = useState<Book[]>([]);

  useEffect(() => {
    ApiService.getAllBooks().then((booksResponse) => {
      setBooks(booksResponse);
    });
  }, []);

  return (
    <StyledEngineProvider injectFirst>
      <SortedTable rows={books} />
    </StyledEngineProvider>
  );
}
