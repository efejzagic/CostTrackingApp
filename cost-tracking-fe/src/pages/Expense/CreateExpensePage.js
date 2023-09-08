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
import Nav from '../../components/Nav/Nav';

var utc = require('dayjs/plugin/utc')

dayjs.extend(utc)

dayjs.extend(weekOfYear);

const CreateExpensePage = () => {
  const [formData, setFormData] = useState({
    Date:  null,
    Description: '',
    Amount: 0,
    ReferenceId: 0,
    Type: '',
    ConstructionSiteId: 0,
    MachineryId: 0,
    ToolId: 0,
    MaintenanceRecordId: 0,
    ArticleId: 0,
    Category: '',
    SubCategory: ''
    });

  const [newItem, setNewItem] = useState({
      description: '',
      amount: 0,
      expenseId: 0,
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

  

  const handleAddItem = () => {
    if (newItem.description || newItem.amount > 0) {
      setFormData((prevFormData) => ({
        ...prevFormData,
        items: [...prevFormData.items, newItem],
      }));
    }
  };
  const handleSubCategoryChange = (event) => {
    const subCategoryId = event.target.value;
  const { Category } = formData;

  setFormData((prevFormData) => ({
    ...prevFormData,
    SubCategory: subCategoryId,
    [`${Category}Id`]: subCategoryId, // Set corresponding Id field based on Category
    ConstructionSiteId: Category === 'ConstructionSite' ? subCategoryId : 0,
    MachineryId: Category === 'Machinery' ? subCategoryId : 0,
    ToolId: Category === 'Tool' ? subCategoryId : 0,
    MaintenanceRecordId: Category === 'MaintenanceRecord' ? subCategoryId : 0,
    ArticleId: Category === 'Article' ? subCategoryId : 0,
  }));

    console.log(`value: CS`, formData.ConstructionSiteId);
    console.log(`value: Machinery`, formData.MachineryId);
    console.log(`value: Tool`, formData.ToolId);
    console.log(`value: Article`, formData.ArticleId);
    console.log(`value: MR`, formData.MaintenanceRecordId);
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

  const handleTypeChange = (event) => {
    setFormData((prevFormData) => ({
      ...prevFormData,
      Type: event.target.value,
    }));
  };

  useEffect(() => {
    // Fetch sub-categories based on selected category

    if (formData.Category) {
      console.log("form data category" , formData.Category);
    
      axios
        .get(`http://localhost:8001/api/v/${formData.Category}`)
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
      Date: formattedDate,
      Description: formData.Description,
      Amount: formData.Amount,
      ReferenceId: formData.ReferenceId,
      Type: parseInt(formData.Type, 10),
      ConstructionSiteId: formData.ConstructionSiteId,
      MachineryId: formData.MachineryId,
      ToolId: formData.ToolId,
      MaintenanceRecordId: formData.MaintenanceRecordId,
      ArticleId: formData.ArticleId,
    };
    console.log("Data: " , data);
    try {
      const response = await axios.post('http://localhost:8001/api/v/Expense', 
      {Value: data}
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
      console.log('expense created successfully', response.data);
    } catch (error) {
      // Handle error (e.g., show error message)
      console.error('Error creating expense', error);
      toast.error("Fail");
    }
  };
  return (

    <>
    <Nav/>
    
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        New Expense
      </Typography>
      <Paper elevation={3} style={{ padding: '2rem' }}>
        <form onSubmit={handleSubmit}>
        <LocalizationProvider dateAdapter={AdapterDayjs}>

        <DatePicker
          label="Date"
          value={formData.Date}
          onChange={(newDate) => handleDateChange('Date', newDate)}
          renderInput={(params) => <TextField {...params} />}
        />

            </LocalizationProvider>

            <div style={{ display: 'flex', alignItems: 'center' }}>
          <TextField
           style={{ marginTop: '20px' }}
            label="Description"
            name="Description"
            value={formData.Description}
            onChange={handleInputChange}
          />
          <TextField
           style={{ marginTop: '20px' }}
            label="Amount"
            type="number"
            name="Amount"
            value={formData.Amount}
            onChange={handleInputChange}
          />
           <TextField
           style={{ marginTop: '20px' }}
            label="Reference Id"
            type="number"
            name="ReferenceId"
            value={formData.ReferenceId}
            onChange={handleInputChange}
          />

          </div>
          <div>

          <Select
            label="Type"
            name="Type"
            value={formData.Type}
            onChange={handleTypeChange}
          >
            <MenuItem value="0">Salary</MenuItem>
            <MenuItem value="1">Maintenance</MenuItem>
            <MenuItem value="2">Material</MenuItem>

          </Select>


          </div>
        <div>
          <Select
            label="Category"
            name="category"
            value={formData.Category}
            onChange={handleCategoryChange}
          >
            <MenuItem value="ConstructionSite">Construction Site</MenuItem>
            <MenuItem value="Machinery">Machinery</MenuItem>
            <MenuItem value="Tool">Tool</MenuItem>
            <MenuItem value="MaintenanceRecord">Maintenance Record</MenuItem>
            <MenuItem value="Article">Article</MenuItem>

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
      <Button>   <Link to={`/expense`}>Back to Expenses</Link> </Button>
    </Container>
    
    </>
  );
};

export default CreateExpensePage;
