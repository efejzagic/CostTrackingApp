import React, { useState,useEffect } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { Link } from 'react-router-dom';
import { /* ... */ FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import StyledPage from '../../components/Styled/StyledPage';


const CreateArticlePage = () => {
  const [formData, setFormData] = useState({
    Name: '',
    Quantity: '',
    Price: 0,
    Description: '',
    SupplierId: 0,
  });

  const [suppliers, setSuppliers] = useState([]); 

  const fetchSuppliers = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Supplier');
      console.log("Suppliers: " , response.data.data );
      setSuppliers(response.data.data);
    } catch (error) {
      console.error('Error fetching Suppliers:', error);
    }
  };

  useEffect(() => {
    fetchSuppliers();
  }, []); // Fetch
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
      const response = await axios.post('http://localhost:8001/api/v/Article', {
        Value: formData
      });
      
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
        New Article
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
            label="Quantity"
            name="Quantity"
            fullWidth
            required
            value={formData.Quantity}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
           <TextField
            label="Price"
            name="Price"
            fullWidth
            required
            value={formData.Price}
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
       
   <FormControl fullWidth style={{ marginBottom: '1rem' }}>
  <InputLabel id="supplier-label">Supplier</InputLabel>
  <Select
    labelId="supplier-label"
    id="SupplierId"
    name="SupplierId"
    value={formData.SupplierId}
    onChange={handleInputChange}
    required
    MenuProps={{
      style: { maxHeight: '400px' } // Adjust the maxHeight as needed
    }}
  >
    {suppliers.map(supplier => (
      <MenuItem key={supplier.id} value={supplier.id}>
        {supplier.name}
      </MenuItem>
    ))}
  </Select>
</FormControl>

             
          <Button type="submit" variant="contained" color="primary">
            Create
          </Button>
        </form>

       
      </Paper>
      <Button>   <Link to={`/article`}>Back to Articles</Link> </Button>
    </Container>
    </StyledPage>
    </>
  );
};

export default CreateArticlePage;
