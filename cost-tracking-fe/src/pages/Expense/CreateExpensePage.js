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

const CreateExpensePage = () => {
  const [formData, setFormData] = useState({
    Date:  null,
    Amount: 0,
    items: [],
    ExpenseDescription: '',
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
  const [subCategories, setSubCategories] = useState([]); 

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
        expenseId: 0,
      });
    }
  };
  const handleSubCategoryChange = (event) => {
    const subCategoryId = event.target.value;
  const { Category } = formData;

  setFormData((prevFormData) => ({
    ...prevFormData,
    SubCategory: subCategoryId,
    [`${Category}Id`]: subCategoryId, 
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

  useEffect(() => {
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
      Date: formattedDate,
      Amount: formData.Amount,
      Description: formData.ExpenseDescription,
      Items: formData.items,
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
      , getConfigHeader());
      if (response.status === 200) {
        console.log('POST request successful');
        console.log('Response data:', response.data);
        toast.success("Success");
      } else {
        console.log('POST request failed');
        console.log('Response data:', response.data);
        toast.error("Fail");
      }
      console.log('Expense created successfully', response.data);
    } catch (error) {
      console.error('Error creating expense', error);
      toast.error("Fail");
    }
  };
  return (

    <>
    <StyledPage>
    
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
            name="description"
            value={newItem.description}
            onChange={handleNewItemChange}
          />
          <TextField
           style={{ marginTop: '20px' }}
            label="Amount"
            type="number"
            name="amount"
            value={newItem.amount}
            onChange={handleNewItemChange}
          />
          <Button   variant="outlined" color="primary" onClick={handleAddItem}>+</Button>
        </div>
        <div style={{ marginTop: '20px' }}>
          {formData.items.map((item, index) => (
            <div key={index} style={{ marginBottom: '10px', display: 'flex', alignItems: 'center' }}>
              <TextField
                label={`Description ${index + 1}`}
                name={`description_${index}`}
                value={item.description}
                disabled
              />
              <TextField
                label={`Amount ${index + 1}`}
                type="number"
                name={`amount_${index}`}
                value={item.amount}
                disabled
              />
              <Button
                variant="outlined"
                color="secondary"
                onClick={() => handleDeleteItem(index)}
                style={{ marginLeft: '10px' }}
              >
                -
              </Button>
            </div>
          ))}
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
<div>   <TextField
           style={{ marginTop: '20px' }}
            label="Expense Description"
            name="ExpenseDescription"
            value={formData.ExpenseDescription}
            onChange={handleInputChange}
          /></div>
       
        </div>
     
          <Button type="submit" variant="contained" color="primary">
            Create
          </Button>
        </form>

       
      </Paper>
      <Button variant="outlined" color="primary">   <Link to={`/expense`}>Back to Expenses</Link> </Button>
    </Container>
    </StyledPage>
    
    </>
  );
};

export default CreateExpensePage;
