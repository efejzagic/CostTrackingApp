import React, { useState,useEffect } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { Link, useNavigate } from 'react-router-dom';
import { /* ... */ FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import StyledPage from '../../components/Styled/StyledPage';


const CreateMachineryPage = () => {
  const [formData, setFormData] = useState({
    Name: '',
    Description: '',
    ProductionYear: 0,
    Location: '',
    ConstructionSiteId: '',

  });
  const navigate = useNavigate();

  const [constructionSite, setConstructionSite] = useState([]); 

  const getConfigHeader = () => {
    const accessToken = localStorage.getItem('accessToken');
    if (!accessToken) {
        console.error('Access token not found in local storage');
        return false;
    }
    return {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      };
  }

  const fetchSuppliers = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/ConstructionSite', getConfigHeader());
      if(response.status === 200) {
      setConstructionSite(response.data.data);
      }
      else if(response.status === 401) {
        navigate('/unauthorized')
      }
      else if(response.status === 403) {
        navigate('/forbidden')
      }
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
      const response = await axios.post('http://localhost:8001/api/v/Machinery', {
        Value: formData
      } , getConfigHeader()) ;
      
      if (response.status === 200) {
        console.log('POST request successful');
        console.log('Response data:', response.data);
        toast.success("Success");
      }
      
      else {
        console.log('POST request failed');
        console.log('Response data:', response.data);
        toast.error("Fail");
      }
    } catch (error) {
      console.error('Error:', error);
      toast.error("Fail");
      navigate('/login');
    }
  };
  return (

    <>
    <StyledPage>
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        New Machinery
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
            label="Description"
            name="Description"
            fullWidth
            required
            value={formData.Description}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
       
       <TextField
  label="Production Year"
  variant="outlined"
  fullWidth
  type="datetime-local" 
  name="ProductionYear"
  value={formData.ProductionYear}
  onChange={handleInputChange}
  inputProps={{ step: 1 }} 
  style={{ marginBottom: '1rem' }}
/>
      <TextField
        label="Location"
        variant="outlined"
        fullWidth
        name="Location"
        value={formData.Location}
        onChange={handleInputChange}
      />
     
   <FormControl fullWidth style={{ marginBottom: '1rem' }}>
  <InputLabel id="cs-label">Construction Site</InputLabel>
  <Select
    labelId="cs-label"
    id="ConstructionSiteId"
    name="ConstructionSiteId"
    value={formData.ConstructionSiteId}
    onChange={handleInputChange}
    required
    MenuProps={{
      style: { maxHeight: '400px' } 
    }}
  >
    {constructionSite.map(constructionSite => (
      <MenuItem key={constructionSite.id} value={constructionSite.id}>
        {constructionSite.title} ({constructionSite.city})
      </MenuItem>
    ))}
  </Select>
</FormControl>

             
          <Button type="submit" variant="contained" color="primary">
            Create
          </Button>
        </form>

       
      </Paper>
      <Button>   <Link to={`/machinery`}>Back to Machinery data</Link> </Button>
    </Container>
    </StyledPage>
    </>
  );
};

export default CreateMachineryPage;
