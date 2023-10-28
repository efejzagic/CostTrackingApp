import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  Container,
  Paper,
  Typography,
  TableContainer,
  Table,
  TableHead,
  TableBody,
  TableRow,
  TableCell,
} from '@mui/material';
import { useParams } from 'react-router-dom';
import StyledPage from '../../components/Styled/StyledPage';
import LoadingCoomponent from '../../components/Loading/LoadingComponent';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';

const OrderDetailsPage = () => {
    const { id } = useParams();
    const [order, setOrder] = useState(null);
    const [isLoading, setIsLoading] = useState(true);
  
    useEffect(() => {
      axios
        .get(`http://localhost:8001/api/v/Order/${id}` , getConfigHeader())
        .then((response) => {
            console.log(response.data.data);
          setOrder(response.data.data);
          setIsLoading(false); 
        })
        .catch((error) => {
          setIsLoading(false);
          console.error('Error fetching order details:', error);
        });
    }, [id]);
  

    return (
        <><StyledPage>
        <Container maxWidth="md" style={{ marginTop: '2rem' }}>
          {isLoading ? (
            <LoadingCoomponent loading={isLoading} />
          ) : 
          (
          <Paper elevation={3} style={{ padding: '2rem' }}>
            <Typography variant="h4">Order Details</Typography>
            <Typography variant="h6">Order ID: {order.id}</Typography>
            <Typography variant="subtitle1">Order Date: {order.orderDate}</Typography>
            <Typography variant="subtitle1">Shipping Date: {order.shippingDate}</Typography>
            <Typography variant="subtitle1">Total Amount: {order.totalAmount.toFixed(2)} KM</Typography>
    
            <TableContainer component={Paper} style={{ marginTop: '2rem' }}>
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
                      <TableCell>{(item.quantity * item.pricePerItem).toFixed(2)} KM</TableCell>
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