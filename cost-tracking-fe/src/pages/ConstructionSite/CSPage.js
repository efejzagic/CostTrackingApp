import React, { useState, useEffect } from 'react';
import { Button, Container, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, Modal, Box } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';
import axios from 'axios';
import StyledPage from '../../components/Styled/StyledPage';
import LoadingCoomponent from '../../components/Loading/LoadingComponent';
import { toast } from 'react-toastify';
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

const CSPage = () => {
  const [data, setData] = useState([]);
  const navigate = useNavigate();

  const [open, setOpen] = useState(false);
  const [selectedItemId, setSelectedItemId] = useState(null);
  const [isLoading, setIsLoading] = useState(true);
  
  const fetchCSData = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/ConstructionSite', getConfigHeader());
      console.log(response);
      if(response.status === 200) {
        setData(response.data.data);
      }
      else if(response.status === 401) {
        navigate('/unauthorized');
      }
      else if(response.status === 403) {
        navigate('/forbidden');
      }
      setIsLoading(false);
    } catch (error) {
      console.error('Error fetching data:', error);
      setIsLoading(false);
      if (error.response.status === 401) {
        console.log("Unauthorized access");
        navigate('/unauthorized');
      }
      if (error.response.status === 403) {
        console.log("Unauthorized access");
        navigate('/forbidden');
      }
      else {
        toast.error("Data fetch error");
      }
    }
  };

  useEffect(() => {
    fetchCSData();
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
    navigate('/construction/create');
  };

  const handleDelete = async () => {
    try {
      await axios.delete(`http://localhost:8001/api/v/ConstructionSite/${selectedItemId}` , getConfigHeader());
      handleClose();
      fetchCSData(); 
    } catch (error) {
      console.error('Error deleting data:', error);
    }
  };

  return (
    <>
      <StyledPage>
      <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
        {isLoading ? (
          <LoadingCoomponent loading={isLoading} />
        ) : (
          <>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
          Construction Site Data
        </Typography>
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', width: '100%' }}>
            <div>
              <Button onClick={handleCreate} variant="contained" color="primary" style={{ marginBottom: '1rem', alignSelf: 'flex-start' }}>
                Create New
              </Button>
            </div>
            <div>
              <Button variant="contained" color="primary" style={{ marginBottom: '1rem', marginLeft: '1rem' }}>
                See Incomes
              </Button>
              <Button variant="contained" onClick={() => navigate('/construction/expenses')} color="primary" style={{ marginBottom: '1rem', marginLeft: '1rem' }}>
                
                See Expenses
                
              </Button>
            </div>
          </div>

        <TableContainer component={Paper} style={{ overflowX: 'auto', minWidth: 1200, alignSelf: 'center' }}>
          <Table style={{ minWidth: 800 }}>
            <TableHead>
              <TableRow>
                <TableCell width={100}>Id</TableCell>
                <TableCell width={200}>Title</TableCell>
                <TableCell width={200}>Description</TableCell>
                <TableCell width={200}>Address</TableCell>
                <TableCell width={200}>Country</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
              {data.map((item) => (
                <TableRow key={item.id}>
                  <TableCell>{item.id}</TableCell>
                  <TableCell>{item.title}</TableCell>
                  <TableCell>{item.description}</TableCell>
                  <TableCell>{item.address} {item.city}</TableCell>
                  <TableCell>{item.country}</TableCell>
                  <TableCell>
                    <div style={{ display: 'flex', gap: '0.5rem' }}>
                    <Button variant="outlined" color="primary" size="small">
                        <Link to={`/construction/${item.id}/employees`}>Employees</Link>
                      </Button>
                      <Button variant="outlined" color="primary" size="small">
                        <Link to={`/construction/${item.id}/expenses`}>Expenses</Link>
                      </Button>
                      <Button variant="outlined" color="primary" size="small">
                        <Link to={`/construction/edit/${item.id}`}>Edit</Link>
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
            Delete Construction Site
          </Typography>
          <Typography id="modal-modal-description" sx={{ mt: 2 }}>
            Are you sure you want to delete {data.find((item) => item.id === selectedItemId)?.title}?
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

export default CSPage;
