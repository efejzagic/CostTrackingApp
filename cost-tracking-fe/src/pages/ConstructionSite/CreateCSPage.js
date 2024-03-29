import React, { useState } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { Link } from 'react-router-dom';
import StyledPage from '../../components/Styled/StyledPage';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';

const CreateCSPage = () => {
  const [formData, setFormData] = useState({
    Title: '',
    Description: '',
    Address: '',
    City: '',
    Country: '',
  });


  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };



  const handleSubmit = async (event) => {
    event.preventDefault();
  
    try {
      const response = await axios.post('http://localhost:8001/api/v/ConstructionSite', {
        Value: formData
      }, getConfigHeader());
      
      if (response.status === 200) {
        console.log('POST request successful');
        console.log('Response data:', response.data);
        toast.success("Success");
      } else {
        console.log('POST request failed');
        console.log('Response data:', response.data);
        toast.error("Fail");
      }
    } catch (error) {
      console.error('Error:', error);
      toast.error("Fail");
    }
  };
  return (

    <>
    <StyledPage>
    
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        New Construction Site
      </Typography>
      <Paper elevation={3} style={{ padding: '2rem' }}>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Title"
            name="Title"
            fullWidth
            required
            value={formData.Title}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
             <TextField
            label="Description"
            name="Description"
            fullWidth
            required
            value={formData.Description}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
           <TextField
            label="Address"
            name="Address"
            fullWidth
            required
            value={formData.Address}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
          <TextField
            label="City"
            name="City"
            fullWidth
            required
            value={formData.City}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
          <TextField
            label="Country"
            name="Country"
            fullWidth
            required
            value={formData.Country}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
          <Button type="submit" variant="contained" color="primary">
            Create
          </Button>
        </form>

       
      </Paper>
      <Button>   <Link to={`/construction`}>Back to Construction Data</Link> </Button>
    </Container>
    </StyledPage>
    </>
  );
};

export default CreateCSPage;
