import React, { useState,useEffect } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import Nav from '../../components/Nav/Nav';
import { Link } from 'react-router-dom';
import { /* ... */ FormControl, InputLabel, Select, MenuItem } from '@mui/material';


const CreateUserPage = () => {
    const [roles, setRoles] = useState([]);
    const [selectedRole, setSelectedRole] = useState('');

  const [formData, setFormData] = useState({
    UserId: '',
    Username: '',
    Name: '',
    Surname: '',
    Email: '',
    Password: ''
    // RoleId: ''
  });

  const [validationErrors, setValidationErrors] = useState({
    Email: false,
    Phone: false,
  });

  const handleEmailChange = (event) => {
    const email = event.target.value;
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    setValidationErrors((prevErrors) => ({
      ...prevErrors,
      Email: !emailRegex.test(email),
    }));
    handleInputChange(event);
  };


//   const fetchRoles = async () => {
//     try {
//         var token = localStorage.getItem('accessToken');
//       const response = await axios.get('http://localhost:8001/api/Auth/roles' , {
//         headers: {
//           Authorization: `Bearer ${token}`,
//         },
//       });
//       console.log("Roles " , response.data );
//       setRoles(response.data);
//     } catch (error) {
//       console.error('Error fetching Construction Sites:', error);
//     }
//   };

//   useEffect(() => {
//     fetchRoles();
//   }, []); // Fetch
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
        var token = localStorage.getItem('accessToken');
        const response = await axios.post('http://localhost:8001/api/Auth/CreateUser', {
            Model: formData
        }, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        });
      
      if (response.data.status === 200 ) {
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
    <Nav/>
    
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
