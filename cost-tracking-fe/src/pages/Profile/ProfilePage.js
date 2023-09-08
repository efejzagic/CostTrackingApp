import React, { useState,useEffect } from 'react';
import {
  Avatar,
  Typography,
  Paper,
  Grid,
  IconButton,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
} from '@mui/material';
import { Edit as EditIcon } from '@mui/icons-material';
import axios from 'axios';
import { decodeJwt } from 'jose';

const ProfilePage = () => {
    const [userDataState, setUserDataState] = useState([]);
  const [isEditDialogOpen, setIsEditDialogOpen] = useState(false);
  const [isPasswordDialogOpen, setIsPasswordDialogOpen] = useState(false);
  const [name, setName] = useState('John Doe');
  const [username, setUsername] = useState('johndoe123');
  const [email, setEmail] = useState('johndoe@example.com');
  const [currentPassword, setCurrentPassword] = useState('');
  const [newPassword, setNewPassword] = useState('');


  const handleEditDialogOpen = () => {
    setIsEditDialogOpen(true);
  };

  const handleEditDialogClose = () => {
    setIsEditDialogOpen(false);
  };

  const handlePasswordDialogOpen = () => {
    setIsPasswordDialogOpen(true);
  };

  const handlePasswordDialogClose = () => {
    setIsPasswordDialogOpen(false);
  };

  const handleEditProfile = () => {
    // Add logic to update user's name, username, and email here
    console.log('Updating profile...');
    handleEditDialogClose();
  };

  const handlePasswordChange = () => {
    // Add logic to change the password here
    console.log('Changing password...');
    handlePasswordDialogClose();
  };

  useEffect(() => {
    setUserDataFromToken();
  }, []);

  const setUserDataFromToken = () => {
    const token = localStorage.getItem('accessToken');
    const data = decodeJwt(token);
    console.log(data.name);
    setUserDataState(data)
  }

  const fetchUserData = async () => {

  try {

    const accessToken = localStorage.getItem('accessToken');
    if (!accessToken) {
        // Handle the case where the access token is not found in local storage
        console.error('Access token not found in local storage');
        return false;
    }

    const config = {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      };

    const response = await axios.get('http://localhost:8001/api/Auth/UserData', config);
    console.log("response",response);
    if(response.error) {
        console.error(response.error);
    }
    const userData = response.data.data;
    setUserDataState(userData);
    console.log(userData);

  } catch (error) {
    console.error('Error fetching user data:', error);
  }
};

  return (
    <Grid container justifyContent="center" style={{ marginTop: '50px' }}>
      <Grid item xs={12} sm={8} md={6}>
        <Paper elevation={3} style={{ padding: '20px' }}>
          <Avatar
            alt="User Profile"
            src="/path-to-profile-image.jpg"
            sx={{ width: 100, height: 100, margin: '0 auto' }}
          />
          <Typography variant="h4" align="center">
            {userDataState.name}
          </Typography>
          <Typography variant="subtitle1" align="center">
            {userDataState.preferred_username}
          </Typography>
          <Typography variant="subtitle2" align="center" color="textSecondary">
            Email: {userDataState.email}
          </Typography>
          <IconButton
            aria-label="Edit Profile"
            color="primary"
            style={{ display: 'block', margin: '0 auto' }}
            onClick={handleEditDialogOpen}
          >
            <EditIcon />
          </IconButton>
          <Button
            variant="outlined"
            color="primary"
            style={{ display: 'block', margin: '20px auto' }}
            onClick={handlePasswordDialogOpen}
          >
            Change Password
          </Button>
          <Typography variant="body1" align="center" style={{ marginTop: '20px' }}>
            About me: Lorem ipsum dolor sit amet, consectetur adipiscing elit.
            Nulla ultricies purus eu quam consectetur, eget elementum odio volutpat.
          </Typography>
        </Paper>
      </Grid>

      {/* Edit Profile Dialog */}
      <Dialog open={isEditDialogOpen} onClose={handleEditDialogClose}>
        <DialogTitle>Edit Profile</DialogTitle>
        <DialogContent>
          <TextField
            margin="dense"
            id="name"
            label="Name"
            fullWidth
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <TextField
            margin="dense"
            id="username"
            label="Username"
            fullWidth
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />
          <TextField
            margin="dense"
            id="email"
            label="Email"
            fullWidth
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handleEditDialogClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handleEditProfile} color="primary">
            Save Changes
          </Button>
        </DialogActions>
      </Dialog>

      {/* Password Change Dialog */}
      <Dialog open={isPasswordDialogOpen} onClose={handlePasswordDialogClose}>
        <DialogTitle>Password Change</DialogTitle>
        <DialogContent>
          <TextField
            autoFocus
            margin="dense"
            id="current-password"
            label="Current Password"
            type="password"
            fullWidth
            value={currentPassword}
            onChange={(e) => setCurrentPassword(e.target.value)}
          />
          <TextField
            margin="dense"
            id="new-password"
            label="New Password"
            type="password"
            fullWidth
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
          />
        </DialogContent>
        <DialogActions>
          <Button onClick={handlePasswordDialogClose} color="primary">
            Cancel
          </Button>
          <Button onClick={handlePasswordChange} color="primary">
            Change Password
          </Button>
        </DialogActions>
      </Dialog>
    </Grid>
  );
};

export default ProfilePage;
