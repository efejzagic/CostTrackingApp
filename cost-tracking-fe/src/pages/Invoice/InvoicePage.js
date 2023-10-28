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
  backgroundColor: 'white', 
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const InvoicePage = () => {
  const [data, setData] = useState([]);
  const navigate = useNavigate();
  const [isLoading, setIsLoading] = useState(true);


  const [totalAmount,setTotalAmount] = useState(0.0);


  const [open, setOpen] = useState(false);
  const [selectedItemId, setSelectedItemId] = useState(null);

  const fetchInvoiceData = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Invoice' , getConfigHeader());
      setData(response.data.data);
      setIsLoading(false);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const fetchInvoiceTotalAmountData = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Invoice/totalAmount' , getConfigHeader());
      setTotalAmount(response.data);
    } catch (error) {
      console.error('Error fetching data:', error);
      if (error.response.status === 401) {
        console.log("Unauthorized access");
        navigate('/unauthorized');
      }
    }
  };


  useEffect(() => {
    fetchInvoiceData();
    fetchInvoiceTotalAmountData();
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
    navigate('/Invoice/create');
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`http://localhost:8001/api/v/Invoice/${selectedItemId}`, getConfigHeader());
      handleClose();
      fetchInvoiceData(); 
    } catch (error) {
      console.error('Error deleting data:', error);
    }
  };

  return (
    <>
    <StyledPage>

      <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
        {isLoading ? (
          <LoadingCoomponent loading={isLoading}/>
        ): (
          <>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
          Income data
        </Typography>
        <Button onClick={handleCreate} variant="contained" color="primary" style={{ marginBottom: '1rem', alignSelf: 'flex-start' }}>
          Create new Income
        </Button>
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
              {data.map((item) => (
                <TableRow key={item.id}>
                  <TableCell>{item.id}</TableCell>
                  <TableCell>{item.date}</TableCell>
                  <TableCell>{item.dueDate} </TableCell>
                  <TableCell>{item.amount} KM </TableCell>
                  <TableCell>                   
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
                        <Link to={`/Invoice/${item.id}/details`}>Details</Link>
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
            Delete Invoice
          </Typography>
          <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            Are you sure you want to delete {data.find((item) => item.id === selectedItemId)?.name}?
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

export default InvoicePage;
