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
import { Link, useParams } from 'react-router-dom';
import StyledPage from '../../components/Styled/StyledPage';
import LoadingCoomponent from '../../components/Loading/LoadingComponent';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';

const ExpenseDetailsPage = () => {
  const { id } = useParams();
  const [expense, setExpense] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  useEffect(() => {
    const fetchExpenseDetails = async () => {
      try {
        const response = await axios.get(`http://localhost:8001/api/v/Expense/${id}` , getConfigHeader());
        setExpense(response.data.data); // Assuming the response data matches your Expense model
        setIsLoading(false);
      } catch (error) {
        console.error('Error fetching expense details:', error);
      }
    };

    fetchExpenseDetails();
  }, [id]);

  return (
    <>
    <StyledPage>
    <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
        Expense Details
      </Typography>
      {expense ? (
        <>
          {/* Expense Details Table */}
          <Paper elevation={3} style={{ padding: '1rem', marginBottom: '1rem' }}>
            <Typography variant="h6" gutterBottom>
              Data for expense: 
            </Typography>
            <TableContainer>
              <Table>
                <TableBody>
                  <TableRow>
                    <TableCell width={100}>Id</TableCell>
                    <TableCell width={200}>Date</TableCell>
                    <TableCell width={200}>Description</TableCell>
                    <TableCell width={200}>Amount</TableCell>
                    <TableCell width={200}>ReferenceId</TableCell>
                  </TableRow>
                  <TableRow>
                    <TableCell>{expense.id}</TableCell>
                    <TableCell>{expense.date}</TableCell>
                    <TableCell>{expense.description}</TableCell>
                    <TableCell>{expense.amount} KM</TableCell>
                    <TableCell>{expense.referenceId}</TableCell>
                  </TableRow>
                </TableBody>
              </Table>
            </TableContainer>
          </Paper>

          {/* Expense Items Table */}
          <Paper elevation={3} style={{ padding: '1rem' }}>
            <Typography variant="h6" gutterBottom>
              Expense Items
            </Typography>
            <TableContainer>
              <Table>
                <TableHead>
                  <TableRow>
                    <TableCell width={100}>Id</TableCell>
                    <TableCell width={200}>Description</TableCell>
                    <TableCell width={200}>Amount</TableCell>
                    <TableCell width={200}>ExpenseId</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {expense.items.map((item) => (
                    <TableRow key={item.id}>
                      <TableCell>{item.id}</TableCell>
                      <TableCell>{item.description}</TableCell>
                      <TableCell>{item.amount} KM</TableCell>
                      <TableCell>{item.expenseId}</TableCell>
                    </TableRow>
                  ))}
                </TableBody>
              </Table>
            </TableContainer>
          </Paper>
        </>
      ) : (
       <LoadingCoomponent loading={isLoading} />
      )}
      <Button component={Link} to="/expense" variant="outlined" color="primary" style={{ marginTop: '1rem' }}>
        Back to Expenses
      </Button>
    </Container>
    </StyledPage>
    </>
  );
};

export default ExpenseDetailsPage;
