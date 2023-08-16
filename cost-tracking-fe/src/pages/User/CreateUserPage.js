import React, { useState,useEffect } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import Nav from '../../components/Nav/Nav';
import { Link } from 'react-router-dom';
import { /* ... */ FormControl, InputLabel, Select, MenuItem } from '@mui/material';
import Box from '@mui/material/Box';
import OutlinedInput from '@mui/material/OutlinedInput';
import Chip from '@mui/material/Chip';

const ITEM_HEIGHT = 48;
const ITEM_PADDING_TOP = 8;
const MenuProps = {
  PaperProps: {
    style: {
      maxHeight: ITEM_HEIGHT * 4.5 + ITEM_PADDING_TOP,
      width: 250,
    },
  },
};


function getStyles(name, personName, theme) {
  return {
    fontWeight:
      personName.indexOf(name) === -1
        ? theme.typography.fontWeightRegular
        : theme.typography.fontWeightMedium,
  };
}

const CreateUserPage = () => {
  const [roles, setRoles] = useState([]);
  const [selectedRoles, setSelectedRoles] = useState([]);

  const [formData, setFormData] = useState({
    UserId: '',
    Username: '',
    Name: '',
    Surname: '',
    Email: '',
    Password: '',
    MultipleRoles: []
    // No need to initialize MultipleRoles, it will be an empty array by default
  });

  const [validationErrors, setValidationErrors] = useState({
    Email: false,
    Phone: false,
  });

  // const handleRoleChange = (event) => {
  //   setSelectedRoles(event.target.value);
  
  //   setFormData((prevData) => ({
  //     ...prevData,
  //     MultipleRoles: event.target.value.map(role => ({ Id: role.id, Name: role.name }))
  //   }));
  //   console.log('MultipleRoles in handleRoleChange:', formData.MultipleRoles);
  // };


  // const handleRoleChange = (event) => {
  //   const newSelectedRoles = event.target.value;
  
  //   setFormData((prevData) => ({
  //     ...prevData,
  //     MultipleRoles: newSelectedRoles.map(role => ({ Id: role.id, Name: role.name }))
  //   }));
  
  //   setSelectedRoles(newSelectedRoles);
  
  //   // Log the values for debugging
  //   console.log('Selected Roles:', newSelectedRoles);
  //   console.log('FormData:', formData);
  // };

  const handleRoleChange = (event) => {
    const newSelectedRoles = event.target.value;
    
    // Convert newSelectedRoles to the expected structure
    const roleObjects = newSelectedRoles.map(role => ({
      Id: role.id,
      Name: role.name,
      Description: role.description,
      Composite: role.composite,
      ClientRole: role.clientRole,
      ContainerId: role.containerId
    }));
  
    setFormData((prevData) => ({
      ...prevData,
      MultipleRoles: roleObjects
    }));
    
    setSelectedRoles(newSelectedRoles);
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

  // const handleRoleChange = (selectedOptions) => {
  //   setSelectedRoles(selectedOptions);
  // };

  // const handleSelectChange = (event, selectedOptions) => {
  //   const {
  //     target: { value },
  //   } = event;
  //   setSelectedRoles(
  //     // On autofill we get a stringified value.
  //     // typeof value === 'string' ? value.split(',') : value,
  //     selectedOptions
  //   );
  // };


  const fetchRoles = async () => {
    try {
        var token = localStorage.getItem('accessToken');
      const response = await axios.get('http://localhost:8001/api/Auth/roles' , {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      console.log("Roles " , response.data );
      setRoles(response.data);
    } catch (error) {
      console.error('Error fetching Construction Sites:', error);
    }
  };

  useEffect(() => {
    fetchRoles();
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
  
    event.preventDefault();
  
    try {
      var token = localStorage.getItem('accessToken');
      console.log("MUltiple roles log: " , formData.MultipleRoles);
      const response = await axios.post(
        'http://localhost:8001/api/Auth/CreateUser',
        {
          Model: formData
        },
        {
          headers: {
            Authorization: `Bearer ${token}`,
            'Content-Type': 'application/json' // Set the Content-Type header
          }
        }
      );

      if (response.data.statusCode === 200 ) {
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
  
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        New User
      </Typography>
      <Paper elevation={3} style={{ padding: '2rem' }}>
        <form onSubmit={handleSubmit}>
        <TextField
            label="Username"
            name="Username"
            fullWidth
            required
            value={formData.Username}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />
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
          <FormControl sx={{ m: 1, width: 300 }}>
        <InputLabel id="demo-multiple-chip-label">Roles</InputLabel>
        <Select
          labelId="demo-multiple-chip-label"
          id="demo-multiple-chip"
          multiple
          value={selectedRoles}
          onChange={handleRoleChange}
          input={<OutlinedInput id="select-multiple-chip" label="Roles" />}
          renderValue={(selected) => (
            <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
              {selected.map((value) => (
                <Chip key={value.id} label={value.name} />
              ))}
            </Box>
          )}
          MenuProps={MenuProps}
        >
          {roles.map((role) => (
            <MenuItem key={role.id} value={role}>
              {role.name}
            </MenuItem>
          ))}
        </Select>
      </FormControl>
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
            label="Password"
            name="Password"
            type="password"
            variant="outlined"
            margin="normal"
            fullWidth
            required
            value={formData.Password}
            onChange={handleInputChange}
            style={{ marginBottom: '1rem' }}
          />

{/* <Autocomplete
        multiple
        id="roles"
        options={roles} // Replace with your actual list of roles
        onChange={handleRoleChange}
        getOptionLabel={option => option.label} // Replace with your label property name
        renderInput={params => <TextField {...params} label="Roles" />}
      /> */}

 {/* <MultiSelectComponent
        options={roles} // Replace with your actual list of roles
        value={formData.MultipleRoles}
        onChange={handleRoleChange}
      /> */}

   {/* <FormControl fullWidth style={{ marginBottom: '1rem' }}>
  <InputLabel id="role-label">Role </InputLabel>
  <Select
    labelId="role-label"
    id="RoleId"
    name="RoleId"
    value={formData.RoleId}
    onChange={handleInputChange}
    required
    MenuProps={{
      style: { maxHeight: '400px' } // Adjust the maxHeight as needed
    }}
  >
    {roles.map(role => (
      <MenuItem key={role.id} value={role.id}>
        {role.name}
      </MenuItem>
    ))}
  </Select>
</FormControl> */}

                
          <Button type="submit" variant="contained" color="primary">
            Create
          </Button>
        </form>

       
      </Paper>
      <Button>   <Link to={`/users`}>Back to Users</Link> </Button>
    </Container>
    
    </>
  );
};

export default CreateUserPage;
