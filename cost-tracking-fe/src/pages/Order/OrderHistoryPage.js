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
import StyledPage from "../../components/Styled/StyledPage";
import LoadingCoomponent from "../../components/Loading/LoadingComponent";
import { getConfigHeader } from "../../components/Auth/GetConfigHeader";
import { useDispatch, useSelector } from "react-redux";
import {
  deleteOrder,
  loadOrders,
  selectOrders,
  selectOrdersLoading
} from "../../state/orders";

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

const OrderHistoryPage = () => {
  const data = useSelector(selectOrders);
  const [open, setOpen] = useState(false);
  const [selectedItemId, setSelectedItemId] = useState(null);
  const isLoading = useSelector(selectOrdersLoading);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  useEffect(() => {
    dispatch(loadOrders());
  }, []);

  const handleOpen = (id) => {
    setSelectedItemId(id);
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    setSelectedItemId(null);
  };

  const handleDelete = async () => {
    try {
      dispatch(deleteOrder(selectedItemId));
      handleClose();
      // fetchOrderData();
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
          {isLoading ? (
            <LoadingCoomponent loading={isLoading} />
          ) : (
            <>
              <Typography
                variant="h4"
                gutterBottom
                style={{ alignSelf: "flex-start" }}
              >
                Order History
              </Typography>
              <div
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                  width: "100%"
                }}
              >
                <div>
                  <Button
                    variant="contained"
                    onClick={() => navigate("/order")}
                    color="primary"
                    style={{ marginBottom: "1rem", marginLeft: "1rem" }}
                  >
                    Order
                  </Button>
                </div>
              </div>
              <TableContainer
                component={Paper}
                style={{
                  overflowX: "auto",
                  minWidth: 1200,
                  alignSelf: "center"
                }}
              >
                <Table style={{ minWidth: 800 }}>
                  <TableHead>
                    <TableRow>
                      <TableCell width={100}>Id</TableCell>
                      <TableCell width={200}>Order Date</TableCell>
                      <TableCell width={200}>Shipping Date</TableCell>
                      <TableCell width={200}>OrderComplete</TableCell>
                      <TableCell width={200}>TotalAmount</TableCell>
                      <TableCell width={200}>Actions</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {data &&
                      data.map((item) => (
                        <TableRow key={item.id}>
                          <TableCell>{item.id}</TableCell>
                          <TableCell>
                            {handleFormatDateTime(item.orderDate)}
                          </TableCell>
                          <TableCell>
                            {handleFormatDateTime(item.shippingDate)}
                          </TableCell>
                          <TableCell>
                            {item.orderComplete ? "Yes" : "No"}
                          </TableCell>
                          <TableCell>{item.totalAmount} KM</TableCell>

                          <TableCell>
                            <div style={{ display: "flex", gap: "0.5rem" }}>
                              <Button
                                onClick={() =>
                                  navigate(`/order/${item.id}/details`)
                                }
                                variant="outlined"
                                color="secondary"
                                size="small"
                              >
                                Details
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
            </>
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
              Delete Order
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

export default OrderHistoryPage;
