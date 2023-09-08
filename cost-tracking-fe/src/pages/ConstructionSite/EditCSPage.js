import React, { useState } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import Nav from '../../components/Nav/Nav';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { Link } from 'react-router-dom';

const EditCSPage = () => {
    const { id } = useParams();


  const [formData, setFormData] = useState({
    Id: id,
    Title: '',
    Description: '',
    Address: '',
    City: '',
    Country: '',
  });

  useEffect(() => {
    const fetchCSData = async () => {
        try {
            const response = await axios.get(`http://localhost:8001/api/v/ConstructionSite/${id}`);
            const apiCSData = response.data.data; 
            const mapperCSData = {
                Title: apiCSData.title,
                Description: apiCSData.description,
                Address: apiCSData.address,
                City: apiCSData.city,
                Country: apiCSData.country,
            };
            setFormData((prevFormData) => ({
                ...prevFormData,
                ...mapperCSData,
            }));
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    fetchCSData();
}, [id]);


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
      const response = await axios.put('http://localhost:8001/api/v/ConstructionSite', {
        Value: formData
      });
      
      if (response.status === 200) {
        console.log('PUT request successful');
        console.log('Response data:', response.data);
        // Reset the form data or navigate to another page if needed
        toast.success("Success");
      } else {
        console.log('PUT request failed');
        console.log('Response data:', response.data);
        toast.error("Fail t");
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
    <Nav/>
    
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        Edit Construction Site
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
            Edit
          </Button>
        </form>
      </Paper>
      <Button>   <Link to={`/construction`}>Back to Construction Data</Link> </Button>
    </Container>
    </>
  );
};

export default EditCSPage;
