import React, { useState, useEffect } from "react";
import axios from "axios";
import {
  Container,
  Paper,
  Typography,
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell
} from "@mui/material";
import { useParams } from "react-router-dom";
import StyledPage from "../../components/Styled/StyledPage";
import LoadingCoomponent from "../../components/Loading/LoadingComponent";
import { useDispatch, useSelector } from "react-redux";
import {
  loadOrders,
  selectOrdersLoading,
  selectOrder
} from "../../state/orders";

const OrderDetailsPage = () => {
  const { id } = useParams();
  const order = useSelector(selectOrder(id));
  const isLoading = useSelector(selectOrdersLoading);
  const dispatch = useDispatch();
  useEffect(() => {
    dispatch(loadOrders());
  }, [id]);

  return (
    <>
      <StyledPage>
        <Container maxWidth="md" style={{ marginTop: "2rem" }}>
          {isLoading ? (
            <LoadingCoomponent loading={isLoading} />
          ) : (
            <Paper elevation={3} style={{ padding: "2rem" }}>
              <Typography variant="h4">Order Details</Typography>
              <Typography variant="h6">Order ID: {order.id}</Typography>
              <Typography variant="subtitle1">
                Order Date: {order.orderDate}
              </Typography>
              <Typography variant="subtitle1">
                Shipping Date: {order.shippingDate}
              </Typography>
              <Typography variant="subtitle1">
                Total Amount: {order.totalAmount.toFixed(2)} KM
              </Typography>

              <TableContainer component={Paper} style={{ marginTop: "2rem" }}>
                <Table>
                  <TableHead>
                    <TableRow>
                      <TableCell>Article Name</TableCell>
                      <TableCell>Quantity</TableCell>
                      <TableCell>Price Per Item</TableCell>
                      <TableCell>Subtotal</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {order.orderItems.map((item) => (
                      <TableRow key={item.articleId}>
                        <TableCell>{item.articleName}</TableCell>
                        <TableCell>{item.quantity}</TableCell>
                        <TableCell>{item.pricePerItem.toFixed(2)} KM</TableCell>
                        <TableCell>
                          {(item.quantity * item.pricePerItem).toFixed(2)} KM
                        </TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </TableContainer>
            </Paper>
          )}
        </Container>
      </StyledPage>
    </>
  );
};

export default OrderDetailsPage;
