import { Modal, Box, Typography, Autocomplete, TextField } from "@mui/material";
import { mostPopular100Books } from "../../consts/consts";
import { CSSProperties, useEffect, useState } from "react";
import { Book } from "../../types/library/books";
import { ApiService } from "../../services/api.service";
import SortedTable from "../sorted-table/SortedTable";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 1000,
  bgcolor: "background.paper",
  border: "2px solid #000",
  color: "black",
  p: 4,
};

const searchboxStyle: CSSProperties = {
  marginBottom: "20px",
};

export default function AddBookModal(props: {
  open: boolean;
  onClose: () => void;
}) {
  const [searchQuery, setSearchQuery] = useState<string | null>(null);
  const [foundRows, setFoundRows] = useState<Book[]>([]);

  useEffect(() => {
    if (searchQuery === null) {
      return;
    }

    ApiService.searchForBooks(searchQuery).then((booksResponse) => {
      setFoundRows(booksResponse);
      console.log(booksResponse);
    });
  }, [searchQuery]);

  return (
    <Modal
      open={props.open}
      onClose={props.onClose}
      aria-labelledby="modal-modal-title"
      aria-describedby="modal-modal-description"
    >
      <Box sx={style}>
        <Autocomplete
          sx={searchboxStyle}
          freeSolo
          options={mostPopular100Books}
          onChange={(_, value) => setSearchQuery(value)}
          renderInput={(params) => <TextField {...params} inputMode="text" />}
        />
        <SortedTable rows={foundRows} />
      </Box>
    </Modal>
  );
}
