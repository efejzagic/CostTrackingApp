import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {
  Typography,
  Paper,
  Container,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Button,
} from '@mui/material';
import { Link, useNavigate, useParams } from 'react-router-dom';
import StyledPage from '../../components/Styled/StyledPage';
import LoadingCoomponent from '../../components/Loading/LoadingComponent';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';
import { toast } from 'react-toastify';

const InvoiceDetailsPage = () => {
  const { id } = useParams();
  const [invoice, setInvoice] = useState([]);
  const [isLoading, setIsLoading] = useState(true);


  const navigate = useNavigate();
  useEffect(() => {
    const fetchInvoiceDetails = async () => {
      try {
        const response = await axios.get(`http://localhost:8001/api/v/Invoice/${id}` , getConfigHeader());
        if(response.status===200)
        {
          setInvoice(response.data.data); 
          setIsLoading(false);
        }

      } catch (error) {
        setIsLoading(false);
        toast.error("Data fetch error");
        console.error('Error fetching invoice details:', error);
      }
    };

    fetchInvoiceDetails();
  }, [id]);


  


  return (
    <>
    <StyledPage>
    <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
    {isLoading ? (
      <LoadingCoomponent loading={isLoading} />
    ) : (
      <>
      {invoice  ? (
        <>
         <Paper elevation={3} style={{ padding: '2rem' }}>
            <Typography variant="h4">Income details</Typography>
            <Typography variant="h6">Income ID: {invoice.id}</Typography>
            <Typography variant="subtitle1">Incoem Date: {invoice.date}</Typography>
            <Typography variant="subtitle1">Due Date: {invoice.dueDate}</Typography>
            <Typography variant="subtitle1">Total Amount: {invoice.amount.toFixed(2)} KM</Typography>
         
         
          <Paper elevation={3} style={{ padding: '1rem' }}>
            <Typography variant="h6" gutterBottom>
              Income Items
            </Typography>
            <TableContainer>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell width={100}>Id</TableCell>
                    <TableCell width={200}>Amount</TableCell>
                    <TableCell width={200}>IncomeId</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {invoice.items.map((item) => (
                    <TableRow key={item.id}>
                      <TableCell>{item.id}</TableCell>
                      <TableCell>{item.amount} KM</TableCell>
                      <TableCell>{invoice.id}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Paper>
          </Paper>
        </>
      ) : (
       <LoadingCoomponent loading={isLoading}/>
      )}
      </>
      )}
      <Button component={Link} to="/invoice" variant="outlined" color="primary" style={{ marginTop: '1rem' }}>
        Back to income data
      </Button>
      
    </Container>
    </StyledPage>
    </>
  );
};

export default InvoiceDetailsPage;
