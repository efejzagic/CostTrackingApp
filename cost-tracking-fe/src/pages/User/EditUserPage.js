import React, { useState, useEffect } from 'react';
import { Button, Container, Paper, TextField, Typography } from '@mui/material';
import axios from 'axios';
import { toast } from 'react-toastify';
import { useParams } from 'react-router-dom';
import StyledPage from '../../components/Styled/StyledPage';
import { getConfigHeader } from '../../components/Auth/GetConfigHeader';


const EditUserPage = () => {
    const { id } = useParams();
  const [formData, setFormData] = useState({
    UserId: id, // Include the user ID in the form data
    Username: '',
    Name: '',
    Surname: '',
    Email: '',
    Password: '', // Include this field if you allow password changes
    MultipleRoles: [],
  });

  const [validationErrors, setValidationErrors] = useState({
    Email: false,
    // Add more validation errors as needed
  });

  const fetchUserData = async () => {
    try {
      const token = localStorage.getItem('accessToken');
      const response = await axios.get(
        `http://localhost:8001/api/Auth/GetUserById/${id}`,
       getConfigHeader()
      );

      console.log("user response: " , response);
      if (response.status === 200) {
        const userData = response.data.data;
        setFormData({
          ...formData,
          Username: userData.username,
          Name: userData.firstName,
          Surname: userData.lastName,
          Email: userData.email,
          // Populate other fields as needed
        });
      } else {
        // Handle the failure scenario
        console.log('Failed to fetch user data');
      }
    } catch (error) {
      console.error('Error:', error);
    }
  };

  useEffect(() => {
    fetchUserData();
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
      const token = localStorage.getItem('accessToken');
      const response = await axios.put(
        `http://localhost:8001/api/Auth/UpdateUser/${id}`,
        {
          Model: formData,
        },
        getConfigHeader()
      );

      if (response.data.statusCode === 200) {
        console.log('PUT request successful');
        console.log('Response data:', response.data);
        toast.success('User updated successfully');
      } else {
        console.log('PUT request failed');
        console.log('Response data:', response.data);
        toast.error('Failed to update user');
      }
    } catch (error) {
      console.error('Error:', error);
      toast.error('Failed to update user');
    }
  };

  return (

    <>
    <StyledPage>
    <Container maxWidth="md" style={{ marginTop: '2rem' }}>
      <Typography variant="h5" gutterBottom>
        Edit User
      </Typography>
      <Paper elevation={3} style={{ padding: '2rem' }}>
        <form onSubmit={handleSubmit}>
          {/* Add fields for editing user data here */}
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
            onChange={handleInputChange}
            error={validationErrors.Email}
            helperText={validationErrors.Email ? 'Invalid email format' : ''}
            style={{ marginBottom: '1rem' }}
          />
          {/* Include other fields as needed */}
          <Button type="submit" variant="contained" color="primary">
            Save Changes
          </Button>
        </form>
      </Paper>
    </Container>
    </StyledPage>
    </>
  );
};

export default EditUserPage;
