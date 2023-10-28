import React, { useState } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import StyledPage from '../../components/Styled/StyledPage';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';

const EditSupplierPage = () => {
    const { id } = useParams();


  const [formData, setFormData] = useState({
    Id: id,
    Name: '',
    Address: '',
    City: '',
    Country: '',
    Email: '',
    Phone: '',
  });

  useEffect(() => {
    const fetchSupplierData = async () => {
        try {
            const response = await axios.get(`http://localhost:8001/api/v/Supplier/${id}` , getConfigHeader());
            const apiSupplierData = response.data.data; 
            const mappedSupplierData = {
                Name: apiSupplierData.name,
                Address: apiSupplierData.address,
                City: apiSupplierData.city,
                Country: apiSupplierData.country,
                Email: apiSupplierData.email,
                Phone: apiSupplierData.phone
            };
            setFormData((prevFormData) => ({
                ...prevFormData,
                ...mappedSupplierData,
            }));
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    fetchSupplierData();
}, [id]);


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
      const response = await axios.put('http://localhost:8001/api/v/Supplier', {
        Value: formData
      }, getConfigHeader());
      
      if (response.status === 200) {
        console.log('PUT request successful');
        console.log('Response data:', response.data);
        toast.success("Success");
      } else {
        console.log('PUT request failed');
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
        Edit Supplier
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
            Edit
          </Button>
        </form>
      </Paper>
      <Button>   <Link to={`/supplier`}>Back to Supplier Data</Link> </Button>

    </Container>
    </StyledPage>
    </>
  );
};

export default EditSupplierPage;
