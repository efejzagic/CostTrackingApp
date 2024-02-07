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
import StyledPage from "../../components/Styled/StyledPage";
import LoadingCoomponent from "../../components/Loading/LoadingComponent";
import { useDispatch, useSelector } from "react-redux";
import {
  selectSuppliers,
  loadsuppliers,
  selectSuppliersLoading,
  deleteSupplier
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

const SupplierPage = () => {
  const navigate = useNavigate();
  const isLoading = useSelector(selectSuppliersLoading);
  const [open, setOpen] = useState(false);
  const [selectedItemId, setSelectedItemId] = useState(null);
  const dispatch = useDispatch();
  const data = useSelector(selectSuppliers);

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
    navigate("/supplier/create");
  };

  const handleDelete = async () => {
    try {
      dispatch(deleteSupplier(selectedItemId));
      handleClose();
      dispatch(loadsuppliers());
    } catch (error) {
      console.error("Error deleting data:", error);
    }
  };

  const handleFormatDateTime = (dateTime) => {
    const date = new Date(dateTime);
    return `${date.getDate()}/${date.getMonth() + 1}/${date.getFullYear()} ${date.getHours()}:${date.getMinutes()}:${date.getSeconds()}`;
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
            Supplier Data
          </Typography>
          <Button
            onClick={handleCreate}
            variant="contained"
            color="primary"
            style={{ marginBottom: "1rem", alignSelf: "flex-start" }}
          >
            Create New
          </Button>
          {isLoading ? (
            <LoadingCoomponent loading={isLoading} />
          ) : (
            <TableContainer
              component={Paper}
              style={{ overflowX: "auto", minWidth: 1200, alignSelf: "center" }}
            >
              <Table style={{ minWidth: 800 }}>
                <TableHead>
                  <TableRow>
                    <TableCell width={100}>Id</TableCell>
                    <TableCell width={200}>Name</TableCell>
                    <TableCell width={200}>Address</TableCell>
                    <TableCell width={200}>Country</TableCell>
                    <TableCell width={200}>Email</TableCell>
                    <TableCell width={200}>Phone</TableCell>
                    <TableCell width={200}>DateCreated</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {data &&
                    data.map((item) => (
                      <TableRow key={item.id}>
                        <TableCell>{item.id}</TableCell>
                        <TableCell>{item.name}</TableCell>
                        <TableCell>
                          {item.address} {item.city}
                        </TableCell>
                        <TableCell>{item.country}</TableCell>
                        <TableCell>{item.email}</TableCell>
                        <TableCell>{item.phone}</TableCell>
                        <TableCell>
                          {handleFormatDateTime(item.dateCreated)}
                        </TableCell>
                        <TableCell>
                          <div style={{ display: "flex", gap: "0.5rem" }}>
                            <Button
                              variant="outlined"
                              color="primary"
                              size="small"
                            >
                              <Link to={`/supplier/${item.id}/articles`}>
                                Articles
                              </Link>
                            </Button>
                            <Button
                              variant="outlined"
                              color="primary"
                              size="small"
                            >
                              <Link to={`/supplier/edit/${item.id}`}>Edit</Link>
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
          )}
        </Container>
        <Modal
          open={open}
          onClose={handleClose}
          aria-labelledby="modal-modal-title"
          aria-describedby="modal-modal-description"
        >
          <Box sx={style}>
            <Typography id="modal-modal-title" variant="h6" component="h2">
              Delete Supplier
            </Typography>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              Are you sure you want to delete{" "}
              {data && data.find((item) => item.id === selectedItemId)?.name}?
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

export default SupplierPage;
