import React, { useState } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { /* ... */ FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import StyledPage from '../../components/Styled/StyledPage';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';

const EditArticlePage = () => {
    const { id } = useParams();


  const [formData, setFormData] = useState({
    Id: id,
    Name: '',
    Quantity: 0,
    Price: 0.0,
    Description: '',
    SupplierId: ''
  });

  useEffect(() => {
    const fetchArticleData = async () => {
        try {
            const response = await axios.get(`http://localhost:8001/api/v/Article/${id}`, getConfigHeader());
            const apiArticleData = response.data.data; 
            console.log("article data: " , response.data.data);
            const mapperArticleData = {
                Name: apiArticleData.name,
                Quantity: apiArticleData.quantity,
                Price: apiArticleData.price,
                Description: apiArticleData.description,
                SupplierId: apiArticleData.supplierId
            };
            setFormData((prevFormData) => ({
                ...prevFormData,
                ...mapperArticleData,
            }));
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    fetchArticleData();
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
      const response = await axios.put('http://localhost:8001/api/v/Article', {
        Value: formData
      }, getConfigHeader());
      
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
  const [suppliers, setSuppliers] = useState([]); 

  const fetchSuppliers = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/Supplier', getConfigHeader());
      console.log("Suppliers: " , response.data.data );
      setSuppliers(response.data.data);
    } catch (error) {
      console.error('Error fetching Suppliers:', error);
    }
  };

  useEffect(() => {
    fetchSuppliers();
  }, []); // Fetch

  return (

    <>
    <StyledPage>
    
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        Edit Article
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
            type='number'
            fullWidth
            required
            value={formData.Quantity}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
           <TextField
            label="Price"
            name="Price"
            type='number'
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
    value={formData.SupplierId ?? ''}
    onChange={handleInputChange}
    required
    MenuProps={{
      style: { maxHeight: '400px' } // Adjust the maxHeight as needed
    }}
  >
    {suppliers.map(site => (
      <MenuItem key={site.id} value={site.id}>
        {site.name}
      </MenuItem>
    ))}
  </Select>
</FormControl>
           
          <Button type="submit" variant="contained" color="primary">
            Edit
          </Button>
        </form>
      </Paper>
      <Button>   <Link to={`/article`}>Back to Articles</Link> </Button>
    </Container>
    </StyledPage>
    </>
  );
};

export default EditArticlePage;
