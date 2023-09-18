import React, { useState, useEffect } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import { Link } from 'react-router-dom';
import { DemoContainer } from '@mui/x-date-pickers/internals/demo';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import * as dayjs from 'dayjs';
import 'dayjs/plugin/utc';
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import weekOfYear from 'dayjs/plugin/weekOfYear';
import StyledPage from '../../components/Styled/StyledPage';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';

var utc = require('dayjs/plugin/utc')

dayjs.extend(utc)

dayjs.extend(weekOfYear);

const CreateMaintenancePage = () => {
  const [formData, setFormData] = useState({
    Name: '',
    Date:  null,
    Description: '',
    Price: 0,
    MachineryId: 0,
    ToolId: 0,
    Technician: '',
    Status: '',
    Category: '',
    SubCategory: ''
    });

  const [newItem, setNewItem] = useState({
      description: '',
      amount: 0,
      MaintenanceId: 0,
    });
  const [validationErrors, setValidationErrors] = useState({
    Email: false,
    Phone: false,
  });
  const [subCategories, setSubCategories] = useState([]); // Added subCategories state

  const handleDateChange = (dateField, dateValue) => {
    setFormData((prevData) => ({
      ...prevData,
      [dateField]: dayjs.utc(dateValue),
    }));
  };

  const handleInputChange = (event) => {
    const { name, value } = event.target;
    setFormData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };



  const handleNewItemChange = (event) => {
    const { name, value } = event.target;
    setNewItem({
      ...newItem,
      [name]: value,
    });
  };

  const handleAddItem = () => {
    if (newItem.description || newItem.amount > 0) {
      setFormData((prevFormData) => ({
        ...prevFormData,
        items: [...prevFormData.items, newItem],
      }));
      setNewItem({
        description: '',
        amount: 0,
        MaintenanceId: 0,
      });
    }
  };
  const handleSubCategoryChange = (event) => {
    const subCategoryId = event.target.value;
  const { Category } = formData;

  setFormData((prevFormData) => ({
    ...prevFormData,
    SubCategory: subCategoryId,
    [`${Category}Id`]: subCategoryId, // Set corresponding Id field based on Category
    MachineryId: Category === 'Machinery' ? subCategoryId : 0,
    ToolId: Category === 'Tool' ? subCategoryId : 0,
  }));

    console.log(`value: Machinery`, formData.MachineryId);
    console.log(`value: Tool`, formData.ToolId);
  };

  const handleDeleteItem = (index) => {
    setFormData((prevFormData) => {
      const updatedItems = [...prevFormData.items];
      updatedItems.splice(index, 1);
      return {
        ...prevFormData,
        items: updatedItems,
      };
    });
  };
  const handleCategoryChange = (event) => {
    setFormData((prevFormData) => ({
      ...prevFormData,
      Category: event.target.value,
    }));
  };

  useEffect(() => {
    // Fetch sub-categories based on selected category

    if (formData.Category) {
      console.log("form data category" , formData.Category);
    
      axios
        .get(`http://localhost:8001/api/v/${formData.Category}` , getConfigHeader())
        .then((response) => {
          console.log(response.data.data);
          setSubCategories(response.data.data);
        })
        .catch((error) => {
          console.error('Error fetching sub-categories:', error);
        });
   
    }
  }, [formData.Category])


  const handleSubmit = async (event) => {
    event.preventDefault();
    console.log("formData" , formData);

    const formattedDate = formData.Date.toISOString();
    const data = {
      Name: formData.Name,
      Timestamp: formattedDate,
      Description: formData.Description,
      Technician: formData.Technician,
      Status: formData.Status,      
      Price: formData.Price,
      MachineryId: formData.MachineryId,
      ToolId: formData.ToolId,
    };
    console.log("Data: " , data);
    try {
      const response = await axios.post('http://localhost:8001/api/v/MaintenanceRecord', 
      {Value: data}, 
      getConfigHeader()
      );
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
      // Handle success (e.g., show success message, redirect, etc.)
      console.log('Maintenance created successfully', response.data);
    } catch (error) {
      // Handle error (e.g., show error message)
      console.error('Error creating Maintenance', error);
      toast.error("Fail");
    }
  };
  return (

    <>
    <StyledPage>
    
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        New Maintenance
      </Typography>
      <Paper elevation={3} style={{ padding: '2rem' }}>
        <form onSubmit={handleSubmit}>
        
        <TextField
           style={{ marginTop: '20px' }}
            label="Name"
            name="Name"
            value={formData.Name}
            onChange={handleInputChange}
          />
           <TextField
           style={{ marginTop: '20px' }}
            label="Description"
            name="Description"
            value={formData.Description}
            onChange={handleInputChange}
          />
           <TextField
           style={{ marginTop: '20px' }}
            label="Price"
            type='number'
            name="Price"
            value={formData.Price}
            onChange={handleInputChange}
          />

<TextField
           style={{ marginTop: '20px' }}
            label="Technican"
            name="Technician"
            value={formData.Technician}
            onChange={handleInputChange}
          />
           <TextField
           style={{ marginTop: '20px' }}
            label="Status"
            name="Status"
            value={formData.Status}
            onChange={handleInputChange}
          />

        <LocalizationProvider dateAdapter={AdapterDayjs}>


        <div style={{ marginTop: '20px' }}>
    <DatePicker
      label="Date"
      value={formData.Date}
      onChange={(newDate) => handleDateChange('Date', newDate)}
      renderInput={(params) => <TextField {...params} />}
    />
  </div>

            </LocalizationProvider>
      
        <div>
          <Select
            label="Category"
            name="category"
            value={formData.Category}
            onChange={handleCategoryChange}
          >
            <MenuItem value="Machinery">Machinery</MenuItem>
            <MenuItem value="Tool">Tool</MenuItem>
          
          </Select>

          <Select
            label="SubCategory"
            name="subCategory"
            value={formData.SubCategory}
            onChange={handleSubCategoryChange}
          >
            <MenuItem value="">Select SubCategory</MenuItem>
            {subCategories.map((subCategory) => (
              <MenuItem key={subCategory.id} value={subCategory.id}>
                {subCategory.title || subCategory.name}
              </MenuItem>
            ))}
          </Select>
        </div>
          <Button type="submit" variant="contained" color="primary">
            Create
          </Button>
        </form>

       
      </Paper>
      <Button>   <Link to={`/Maintenance`}>Back to Maintenances</Link> </Button>
    </Container>
    </StyledPage>
    
    </>
  );
};

export default CreateMaintenancePage;
