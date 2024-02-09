import React, { useState, useEffect } from "react";
import {
  Button,
  Container,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Typography,
  Modal,
  Box
} from "@mui/material";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";
import axios from "axios";
import { useParams } from "react-router-dom";
import StyledPage from "../../components/Styled/StyledPage";
import { getConfigHeader } from "../../components/Auth/GetConfigHeader";
import { useDispatch, useSelector } from "react-redux";
import {
  selectArticleBySupplierId,
  selectSupplier,
  loadsuppliers
} from "../../state/suppliers";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  backgroundColor: "white",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4
};

const SupplierArticles = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const [open, setOpen] = useState(false);
  const [selectedItemId, setSelectedItemId] = useState(null);
  const dispatch = useDispatch();
  const articles = useSelector(selectArticleBySupplierId(id));
  const data = useSelector(selectSupplier(id));

  useEffect(() => {
    dispatch(loadsuppliers());
  }, []);

  const handleOpen = (id) => {
    setSelectedItemId(id);
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    setSelectedItemId(null);
  };

  const handleCreate = () => {
    navigate("/article/create");
  };

  //TODO: handle by article reducer
  const handleDelete = async () => {
    try {
      await axios.delete(
        `http://localhost:8001/api/v/Article/${selectedItemId}`,
        getConfigHeader()
      );
      handleClose();
      // fetchArticleData();
    } catch (error) {
      console.error("Error deleting data:", error);
    }
  };

  return (
    <>
      <StyledPage>
        <Container
          maxWidth="md"
          style={{
            marginTop: "2rem",
            display: "flex",
            flexDirection: "column",
            alignItems: "center"
          }}
        >
          <Typography
            variant="h4"
            gutterBottom
            style={{ alignSelf: "flex-start" }}
          >
            Articles for {data.name}
          </Typography>
          <Button
            onClick={handleCreate}
            variant="contained"
            color="primary"
            style={{ marginBottom: "1rem", alignSelf: "flex-start" }}
          >
            Create New
          </Button>
          <TableContainer
            component={Paper}
            style={{ overflowX: "auto", minWidth: 1200, alignSelf: "center" }}
          >
            <Table style={{ minWidth: 800 }}>
              <TableHead>
                <TableRow>
                  <TableCell width={100}>Id</TableCell>
                  <TableCell width={200}>Name</TableCell>
                  <TableCell width={200}>Quantity</TableCell>
                  <TableCell width={200}>Price</TableCell>
                  <TableCell width={200}>Description</TableCell>
                </TableRow>
              </TableHead>
              <TableBody>
                {articles.map((item) => (
                  <TableRow key={item.id}>
                    <TableCell>{item.id}</TableCell>
                    <TableCell>{item.name}</TableCell>
                    <TableCell>{item.quantity}</TableCell>
                    <TableCell>{item.price}</TableCell>
                    <TableCell>{item.description}</TableCell>

                    <TableCell>
                      <div style={{ display: "flex", gap: "0.5rem" }}>
                        <Button variant="outlined" color="primary" size="small">
                          <Link to={`/article/edit/${item.id}`}>Edit</Link>
                        </Button>
                        <Button
                          onClick={() => handleOpen(item.id)}
                          variant="outlined"
                          color="secondary"
                          size="small"
                        >
                          Delete
                        </Button>
                      </div>
                    </TableCell>
                  </TableRow>
                ))}
              </TableBody>
            </Table>
          </TableContainer>
        </Container>
        <Modal
          open={open}
          onClose={handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Delete Article
            </Typography>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              Are you sure you want to delete{" "}
              {articles.find((item) => item.id === selectedItemId)?.name}?
            </Typography>
            <Button
              onClick={handleDelete}
              variant="outlined"
              color="secondary"
              sx={{ mt: 2, mr: 2 }}
            >
              Delete
            </Button>
            <Button
              onClick={handleClose}
              variant="outlined"
              color="primary"
              sx={{ mt: 2 }}
            >
              Cancel
            </Button>
          </Box>
        </Modal>
      </StyledPage>
    </>
  );
};

export default SupplierArticles;
