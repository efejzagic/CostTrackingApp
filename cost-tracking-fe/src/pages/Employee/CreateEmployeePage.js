import React, { useState,useEffect } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { Link } from 'react-router-dom';
import { /* ... */ FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import StyledPage from '../../components/Styled/StyledPage';

const CreateEmployeePage = () => {
  const [formData, setFormData] = useState({
    Name: '',
    Surname: '',
    Address: '',
    City: '',
    Country: '',
    ConstructionSiteId: '',
    HourlyRate: '',
    HoursOfWork: '',
    Salary: '',
  });

  const [constructionSites, setConstructionSites] = useState([]); // To store fetched Construction Site data

  const fetchConstructionSites = async () => {
    try {
      const response = await axios.get('http://localhost:8001/api/v/ConstructionSite');
      console.log("Construction Sites: " , response.data.data );
      setConstructionSites(response.data.data);
    } catch (error) {
      console.error('Error fetching Construction Sites:', error);
    }
  };

  useEffect(() => {
    fetchConstructionSites();
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
      const response = await axios.post('http://localhost:8001/api/v/Employee', {
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
        New Employee
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
            label="Surname"
            name="Surname"
            fullWidth
            required
            value={formData.Surname}
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

        {/* <TextField
            label="ConstructionSiteId"
            name="ConstructionSiteId"
            fullWidth
            required
            value={formData.ConstructionSiteId}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          /> */}
   <FormControl fullWidth style={{ marginBottom: '1rem' }}>
  <InputLabel id="construction-site-label">Construction Site</InputLabel>
  <Select
    labelId="construction-site-label"
    id="ConstructionSiteId"
    name="ConstructionSiteId"
    value={formData.ConstructionSiteId}
    onChange={handleInputChange}
    required
    MenuProps={{
      style: { maxHeight: '400px' } // Adjust the maxHeight as needed
    }}
  >
    {constructionSites.map(site => (
      <MenuItem key={site.id} value={site.id}>
        {site.title}
      </MenuItem>
    ))}
  </Select>
</FormControl>

                  <TextField
            label="HourlyRate"
            name="HourlyRate"
            fullWidth
            required
            value={formData.HourlyRate}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
                  <TextField
            label="HoursOfWork"
            name="HoursOfWork"
            fullWidth
            required
            value={formData.HoursOfWork}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
                  <TextField
            label="Salary"
            name="Salary"
            fullWidth
            required
            value={formData.Salary}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
          <Button type="submit" variant="contained" color="primary">
            Create
          </Button>
        </form>

       
      </Paper>
      <Button>   <Link to={`/employee`}>Back to Employee Data</Link> </Button>
    </Container>
    </StyledPage>
    </>
  );
};

export default CreateEmployeePage;
