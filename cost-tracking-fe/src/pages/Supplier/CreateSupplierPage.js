import React, { useState } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { Link } from 'react-router-dom';
import StyledPage from '../../components/Styled/StyledPage';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';

const CreatePage = () => {
  const [formData, setFormData] = useState({
    Name: '',
    Address: '',
    City: '',
    Country: '',
    Email: '',
    Phone: '',
  });

  const [validationErrors, setValidationErrors] = useState({
    Email: false,
    Phone: false,
  });

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleEmailChange = (event) => {
    const email = event.target.value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    setValidationErrors((prevErrors) => ({
      ...prevErrors,
      Email: !emailRegex.test(email),
    }));
    handleInputChange(event);
  };

  const handlePhoneChange = (event) => {
    const phone = event.target.value;
    const phoneRegex = /^\d{9}$/;
    setValidationErrors((prevErrors) => ({
      ...prevErrors,
      Phone: !phoneRegex.test(phone),
    }));
    handleInputChange(event);
  };

  const handleSubmit = async (event) => {
    event.preventDefault();
  
    try {
      const response = await axios.post('http://localhost:8001/api/v/Supplier', {
        Value: formData
      }, getConfigHeader());
      
      if (response.status === 200) {
        console.log('POST request successful');
        console.log('Response data:', response.data);
        // Reset the form data or navigate to another page if needed
        toast.success("Success");
      } else {
        console.log('POST request failed');
        console.log('Response data:', response.data);
        toast.error("Fail");
        // Handle the failure scenario
      }
    } catch (error) {
      console.error('Error:', error);
      toast.error("Fail");
      // Handle the error scenario
    }
  };
  return (

    <>
    <StyledPage>
    
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        New Supplier
      </Typography>
      <Paper elevation={3} style={{ padding: '2rem' }}>
        <form onSubmit={handleSubmit}>
          <TextField
            label="Name"
            name="Name"
            fullWidth
            required
            value={formData.Name}
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
          <TextField
            label="Email"
            name="Email"
            type="email"
            fullWidth
            required
            value={formData.Email}
            onChange={handleEmailChange}
            error={validationErrors.Email}
            helperText={validationErrors.Email ? 'Invalid email format' : ''}
            style={{ marginBottom: '1rem' }}
          />
          <TextField
            label="Phone"
            name="Phone"
            fullWidth
            required
            value={formData.Phone}
            onChange={handlePhoneChange}
            error={validationErrors.Phone}
            helperText={validationErrors.Phone ? 'Invalid phone format (10 digits)' : ''}
            style={{ marginBottom: '1rem' }}
          />
          <Button type="submit" variant="contained" color="primary">
            Create
          </Button>
        </form>

       
      </Paper>
      <Button>   <Link to={`/supplier`}>Back to Suppliers Data</Link> </Button>
    </Container>
    </StyledPage>
    </>
  );
};

export default CreatePage;
