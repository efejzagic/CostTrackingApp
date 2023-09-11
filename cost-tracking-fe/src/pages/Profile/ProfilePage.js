import React, { useState, useEffect } from 'react';
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
import StyledPage from '../../components/Styled/StyledPage';

const ProfilePage = () => {
  const [userDataState, setUserDataState] = useState({});
  const [name, setName] = useState('');
  const [surname, setSurname] = useState('');
  const [email, setEmail] = useState('');
  

  const handleEditDialogOpen = () => {
    window.location.href = 'https://lemur-5.cloud-iam.com/auth/realms/cost-tracking-app/account/#/personal-info';

  };


  const handlePasswordDialogOpen = () => {
    window.location.href = 'https://lemur-5.cloud-iam.com/auth/realms/cost-tracking-app/account/#/security/signingin';
  };
  const handleDeviceOpen = () => {
    window.location.href = 'https://lemur-5.cloud-iam.com/auth/realms/cost-tracking-app/account/#/security/device-activity';
  };

  useEffect(() => {
    setUserDataFromToken();
  }, []);

  const setUserDataFromToken = () => {
    const token = localStorage.getItem('accessToken');
    const data = decodeJwt(token);
    setUserDataState(data);
    setName(data.given_name || '');
    setSurname(data.family_name || '');
    setEmail(data.email || '');
  };

  return (
    <>
    <StyledPage>
      <Grid container justifyContent="center" style={{ marginTop: '50px' }}>
      <Grid item xs={12} sm={8} md={6}>
        <Paper elevation={3} style={{ padding: '20px' }}>
          <Avatar
            alt="User Profile"
            src="/path-to-profile-image.jpg"
            sx={{ width: 100, height: 100, margin: '0 auto' }}
          />
          <Typography variant="h4" align="center">
            {userDataState.given_name}  {userDataState.family_name}
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

          <Button
            variant="outlined"
            color="primary"
            style={{ display: 'block', margin: '20px auto' }}
            onClick={handleDeviceOpen}
          >
            See Devices
          </Button>
          <Typography variant="body1" align="center" style={{ marginTop: '20px' }}>
            About me: Lorem ipsum dolor sit amet, consectetur adipiscing elit.
            Nulla ultricies purus eu quam consectetur, eget elementum odio volutpat.
          </Typography>
        </Paper>
      </Grid>

      
      </Grid>
      </StyledPage>
    </>
  );
};

export default ProfilePage;
