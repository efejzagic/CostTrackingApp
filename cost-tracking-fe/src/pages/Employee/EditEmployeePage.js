import React, { useState } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import Nav from '../../components/Nav/Nav';
import { useParams } from 'react-router-dom';
import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import { /* ... */ FormControl, InputLabel, Select, MenuItem } from '@mui/material';

const EditEmployeePage = () => {
    const { id } = useParams();


  const [formData, setFormData] = useState({
    Id: id,
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

  useEffect(() => {
    const fetchEmployeeData = async () => {
        try {
            const response = await axios.get(`http://localhost:8001/api/v/Employee/${id}`);
            const apiEmployeeData = response.data.data; 
            const mapperEmployeeData = {
                Name: apiEmployeeData.name,
                Surname: apiEmployeeData.surname,
                Address: apiEmployeeData.address,
                City: apiEmployeeData.city,
                Country: apiEmployeeData.country,
                ConstructionSiteId: apiEmployeeData.constructionSiteId,
                HourlyRate: apiEmployeeData.hourlyRate,
                HoursOfWork: apiEmployeeData.hoursOfWork,
                Salary: apiEmployeeData.salary,
            };
            setFormData((prevFormData) => ({
                ...prevFormData,
                ...mapperEmployeeData,
            }));
        } catch (error) {
            console.error('Error fetching data:', error);
        }
    };

    fetchEmployeeData();
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
      const response = await axios.put('http://localhost:8001/api/v/Employee', {
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

  return (

    <>
    
    <Nav/>
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        Edit Employee
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
            Edit
          </Button>
        </form>
      </Paper>
      <Button>   <Link to={`/employee`}>Back to Employee Data</Link> </Button>
    </Container>
    </>
  );
};

export default EditEmployeePage;
