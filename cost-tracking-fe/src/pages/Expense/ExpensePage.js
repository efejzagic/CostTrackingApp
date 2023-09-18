import React, { useState, useEffect } from 'react';
import { Button, Container, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, Modal, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import axios from 'axios';
import StyledPage from '../../components/Styled/StyledPage';
import LoadingCoomponent from '../../components/Loading/LoadingComponent';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';


const style = {
  position: 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 400,
  backgroundColor: 'white', // Change to your preferred background color
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const ExpensePage = () => {
  const [data, setData] = useState([]); 
  const navigate = useNavigate();
  const[isLoading, setIsLoading] = useState(true);

  const [totalAmount,setTotalAmount] = useState(0.0);


  const [open, setOpen] = useState(false);
  const [selectedItemId, setSelectedItemId] = useState(null);

  const fetchExpenseData = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Expense' , getConfigHeader());
      setIsLoading(false);
      setData(response.data.data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const fetchExpenseTotalAmountData = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Expense/totalAmount' , getConfigHeader());
      setTotalAmount(1);
    } catch (error) {
      console.error('Error fetching data:', error);
      if (error.response.status === 401) {
        console.log("Unauthorized access");
        // Redirect to unauthorized page or handle the unauthorized access scenario
        navigate('/unauthorized');
      }
    }
  };

  useEffect(() => {
    setIsLoading(true);
    fetchExpenseData();
    fetchExpenseTotalAmountData();
  }, []); // Fetch data when component mounts

  const handleOpen = (id) => {
    setSelectedItemId(id);
    setOpen(true);
  };

  const handleClose = () => {
    setOpen(false);
    setSelectedItemId(null);
  };

  const handleCreate = () => {
    navigate('/expense/create');
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`http://localhost:8001/api/v/Expense/${selectedItemId}`, getConfigHeader());
      handleClose();
      fetchExpenseData(); // Refresh data after successful deletion
    } catch (error) {
      console.error('Error deleting data:', error);
      // Handle error scenario
    }
  };

  return (
    <>
    <StyledPage>

      <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
        Expenses
        </Typography>
        <Button onClick={handleCreate} variant="contained" color="primary" style={{ marginBottom: '1rem', alignSelf: 'flex-start' }}>
          Create new Expense
        </Button>

        {!isLoading ? (
          <>
        <TableContainer component={Paper} style={{ overflowX: 'auto', minWidth: 1200, alignSelf: 'center' }}>
          <Table style={{ minWidth: 800 }}>
            <TableHead>
              <TableRow>
                <TableCell width={100}>Id</TableCell>
                <TableCell width={200}>Date</TableCell>
                <TableCell width={200}>Description</TableCell>
                <TableCell width={200}>Amount</TableCell>
                <TableCell width={200}>Type</TableCell>
                <TableCell width={200}>ReferenceId</TableCell>
                <TableCell width={200}>Sub</TableCell>
                <TableCell width={200}>Actions</TableCell>

              </TableRow>
            </TableHead>
            <TableBody>
              {data.map((item) => (
                <TableRow key={item.id}>
                  <TableCell>{item.id}</TableCell>
                  <TableCell>{item.date}</TableCell>
                  <TableCell>{item.description} </TableCell>
                  <TableCell>{item.amount}KM</TableCell>
                  <TableCell>{item.type}</TableCell>
                  <TableCell>{item.referenceId}</TableCell>
                  <TableCell>
                    {/* Conditional rendering */}
                    {item.constructionSiteId !== 0 && item.constructionSiteId!==null &&(
                      <div>ConstructionSiteId: {item.constructionSiteId}</div>
                    )}
                    {item.machineryId !== 0 && item.machineryId!==null &&(
                      <div>MachineryId: {item.machineryId}</div>
                    )}
                    {item.toolId !== 0 && item.toolId!==null &&(
                      <div>ToolId: {item.toolId}</div>
                    )}
                    {item.maintenanceRecordId !== 0 && item.maintenanceRecordId!==null && (
                      <div>MaintenanceRecordId: {item.maintenanceRecordId}</div>
                    )}
                       {item.articleId !== 0 && item.articleId!==null &&(
                      <div>ArticleId: {item.articleId}</div>
                    )}
                    {item.orderId !== 0 && item.orderId!==null && (
                      <div>OrderId: {item.orderId}</div>
                    )}
                  </TableCell>
                  <TableCell>
                    <div style={{ display: 'flex', gap: '0.5rem' }}>
                    <Button variant="outlined" color="primary" size="small">
                        <Link to={`/expense/${item.id}`}>Details</Link>
                      </Button>
                      <Button onClick={() => handleOpen(item.id)} variant="outlined" color="secondary" size="small">
                        Delete
                      </Button>
                      
                    </div>
                  </TableCell>
                </TableRow>
              ))}

              <TableRow >
              <TableCell colSpan={3} /> {/* Empty cells to align with amount column */}
              <TableCell>{totalAmount.toFixed(2)} KM</TableCell>
                </TableRow>
            </TableBody>
          </Table>
        </TableContainer>
        </>
        ) : (
          <LoadingCoomponent loading={isLoading} />
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
            Delete Expense
          </Typography>
          <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            Are you sure you want to delete expense with id {data.find((item) => item.id === selectedItemId)?.id}?
          </Typography>
          <Button onClick={handleDelete} variant="outlined" color="secondary" sx={{ mt: 2, mr: 2 }}>
            Delete
          </Button>
          <Button onClick={handleClose} variant="outlined" color="primary" sx={{ mt: 2 }}>
            Cancel
          </Button>
        </Box>
      </Modal>
      </StyledPage>
    </>
  );
};

export default ExpensePage;
