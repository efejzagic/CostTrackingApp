import React from 'react';
import styled from '@mui/system/styled';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import Button from '@mui/material/Button';
import StyledPage from '../../components/Styled/StyledPage';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import axios from 'axios';
import { Link, Container, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Modal, Box } from '@mui/material';
import LoadingCoomponent from '../../components/Loading/LoadingComponent';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';


const StyledCard = styled(Card)`
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 40px;
  margin-bottom: 20px;
  border: 1px solid gray; /* Border style with 2x thickness */
  border-radius: 8px; /* Optional: Add border radius */
  max-width: 800px; /* Adjust the maximum width as needed */
  margin: 0 auto;
  opacity: 0.9;
`;

const LargeText = styled(Typography)`
  font-size: 3.3rem; /* Double the font size */
  font-weight: bold;
`;

const SmallText = styled(Typography)`
  font-size: 1rem;
  margin-top: 10px;
`;

const ButtonContainer = styled('div')`
  display: flex;
  justify-content: center; /* Center buttons horizontally */
  align-items: center; /* Center buttons vertically */
  margin-top: 20px;
  width: 100%;
`;

const boxStyle = {
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


function BalancePage() {
  const [data, setData] = useState([]); 
  const [invoiceData, setInvoiceData] = useState([]); 
  const [balanceData,setBalanceData] = useState(0.0); 
  const [totalExpense , setTotalExpense] = useState(0.0); 
  const [totalIncome , setTotalIncome] = useState(0.0); 


  const navigate = useNavigate();
  const[isLoading, setIsLoading] = useState(true);
  const [open, setOpen] = useState(false);
  const [selectedItemId, setSelectedItemId] = useState(null);

  const [selectedInvoiceItemId, setSelectedInvoiceItemId] = useState(null);
  const [openInvoice, setOpenInvoice] = useState(false);

  const fetchBalanceData = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Balance', getConfigHeader());
      setIsLoading(false);
      var fixedData = response.data.balance.toFixed(2);
      setBalanceData(fixedData);
      setTotalExpense(response.data.expenses.toFixed(2));
      setTotalIncome(response.data.incomes.toFixed(2));
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };
  const fetchExpenseData = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Expense?pageNumber=1&pageSize=5', getConfigHeader());
      setIsLoading(false);
      setData(response.data.data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const fetchInvoiceData = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Invoice?pageNumber=1&pageSize=5' , getConfigHeader());
      setInvoiceData(response.data.data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };


  useEffect(() => {
    setIsLoading(true);
    fetchBalanceData();
    fetchInvoiceData();
    fetchExpenseData();
  }, []); // Fetch data when component mounts


  const handleOpen = (id) => {
    setSelectedItemId(id);
    setOpen(true);
  };

  const handleOpenInvoice = (id) => {
    setSelectedInvoiceItemId(id);
    setOpenInvoice(true);
  };


  const handleClose = () => {
    setOpen(false);
    setSelectedItemId(null);
  };

  const handleCloseInvoice = () => {
    setOpenInvoice(false);
    setSelectedInvoiceItemId(null);
  };

  const handleCreate = () => {
    navigate('/expense/create');
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`http://localhost:8001/api/v/Expense/${selectedItemId}` , getConfigHeader());
      handleClose();
      fetchExpenseData(); // Refresh data after successful deletion
    } catch (error) {
      console.error('Error deleting data:', error);
      // Handle error scenario
    }
  };
  return (
    <>
    <div style={{ marginBottom: '200px' }}>

      <StyledPage >
        <div>
          <StyledCard style={{marginTop: '50px'}}>
            <LargeText>Balance: {balanceData} BAM</LargeText>
            <SmallText>TRN: 8023873739962112</SmallText>
            <SmallText>Expenses: {totalExpense} BAM</SmallText>
            <SmallText>Income: {totalIncome} BAM</SmallText>
          </StyledCard>
          <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
            
          {!isLoading ? (
          <>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
        Last 5 Expenses
        </Typography>
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
                  <TableCell>{item.amount} KM</TableCell>
                  <TableCell>{item.type}</TableCell>
                  <TableCell>{item.referenceId}</TableCell>
                  <TableCell>
                    {/* Conditional rendering */}
                    {item.constructionSiteId !== 0 && item.constructionSiteId!==null && (
                      <div>ConstructionSiteId: {item.constructionSiteId}</div>
                    )}
                    {item.machineryId !== 0 && item.machineryId!==null && (
                      <div>MachineryId: {item.machineryId}</div>
                    )}
                    {item.toolId !== 0 && item.toolId!==null && (
                      <div>ToolId: {item.toolId}</div>
                    )}
                    {item.maintenanceRecordId !== 0 && item.maintenanceRecordId!==null && (
                      <div>MaintenanceRecordId: {item.maintenanceRecordId}</div>
                    )}
                       {item.articleId !== 0 && item.articleId!==null && (
                      <div>ArticleId: {item.articleId}</div>
                    )}
                      {item.orderId !== 0 && item.orderId!==null && (
                      <div>Order Id: {item.orderId}</div>
                    )}
                  </TableCell>
                  <TableCell>
                    <div style={{ display: 'flex', gap: '0.5rem' }}>
                    <Button onClick={() => navigate(`/expense/${item.id}`)}  variant="outlined" color="primary" size="small">
                       Details
                      </Button>
                      <Button onClick={() => handleOpen(item.id)} variant="outlined" color="secondary" size="small">
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
        ) : (
          <LoadingCoomponent loading={isLoading}/>
        )}
        </Container>



        <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
          {!isLoading ? (
          <>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
        Last 5 Incomes
        </Typography>
        <TableContainer component={Paper} style={{ overflowX: 'auto', minWidth: 1200, alignSelf: 'center' }}>
          <Table style={{ minWidth: 800 }}>
            <TableHead>
              <TableRow>
              <TableCell width={100}>Id</TableCell>
                <TableCell width={200}>Date</TableCell>
                <TableCell width={200}>Due Date</TableCell>
                <TableCell width={200}>Amount</TableCell>
                <TableCell width={200}>Sub</TableCell>
                <TableCell width={200}>Actions</TableCell>

              </TableRow>
            </TableHead>
            <TableBody>
              {invoiceData.map((item) => (
                <TableRow key={item.id}>
                  <TableCell>{item.id}</TableCell>
                  <TableCell>{item.date}</TableCell>
                  <TableCell>{item.dueDate} </TableCell>
                  <TableCell>{item.amount} KM</TableCell>
                  <TableCell>
                    {/* Conditional rendering */}
                    {item.constructionSiteId !== 0 && item.constructionSiteId!==null && (
                      <div>ConstructionSiteId: {item.constructionSiteId}</div>
                    )}
                    {item.machineryId !== 0 && item.machineryId!==null && (
                      <div>MachineryId: {item.machineryId}</div>
                    )}
                    {item.toolId !== 0 && item.toolId!==null && (
                      <div>ToolId: {item.toolId}</div>
                    )}
                    {item.maintenanceRecordId !== 0 && item.maintenanceRecordId!==null && (
                      <div>MaintenanceRecordId: {item.maintenanceRecordId}</div>
                    )}
                       {item.articleId !== 0 && item.articleId!==null && (
                      <div>ArticleId: {item.articleId}</div>
                    )}
                     {item.orderId !== 0 && item.orderId!==null && (
                      <div>Order Id: {item.orderId}</div>
                    )}
                  </TableCell>
                  <TableCell>
                    <div style={{ display: 'flex', gap: '0.5rem' }}>
                    <Button onClick={() => navigate(`/invoice/${item.id}`)}  variant="outlined" color="primary" size="small">
                       Details
                      </Button>
                      <Button onClick={() => handleOpenInvoice(item.id)} variant="outlined" color="secondary" size="small">
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
        ) : (
          <Typography variant="body1" gutterBottom>
          Loading...
        </Typography>
        )}
        </Container>
          <ButtonContainer>
            <Button style={{marginRight: '10px'}} variant="outlined" color="primary">
              Create Transaction
            </Button>
            <Button style={{marginLeft: '10px'}} variant="outlined" color="primary">
              Lorem Ipsum
            </Button>
          </ButtonContainer>
        </div>
        <Modal
        open={open}
        onClose={handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={boxStyle}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Delete Expense
          </Typography>
          <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            Are you sure you want to delete expense with id {data.find((item) => item.id === selectedItemId)?.id}?
          </Typography>
          <Button onClick={handleDelete} variant="outlined" color="secondary" sx={{ mt: 2, mr: 2 }}>
            Delete
          </Button>
          <Button onClick={handleCloseInvoice} variant="outlined" color="primary" sx={{ mt: 2 }}>
            Cancel
          </Button>
        </Box>
      </Modal>

      <Modal
        open={openInvoice}
        onClose={handleCloseInvoice}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={boxStyle}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Delete Invoice
          </Typography>
          <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            Are you sure you want to delete invoice with id {invoiceData.find((item) => item.id === selectedItemId)?.id}?
          </Typography>
          <Button onClick={handleDelete} variant="outlined" color="secondary" sx={{ mt: 2, mr: 2 }}>
            Delete
          </Button>
          <Button onClick={handleCloseInvoice} variant="outlined" color="primary" sx={{ mt: 2 }}>
            Cancel
          </Button>
        </Box>
      </Modal>
      </StyledPage>
</div>
    </>
  );
}

export default BalancePage;
