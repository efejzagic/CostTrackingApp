import React from 'react';
import axios from 'axios';
import { Button } from '@mui/material';

const LogoutButton = () => {
  const handleLogout = async () => {
    try {
      await axios.post('http://localhost:8001/api/v/Account/invalidate-token', {
        sessionID: localStorage.getItem('accessToken') 
      });

    } catch (error) {
      console.error('Error during logout:', error);
    }
  };

  return (
    <Button type="button"
    className="text-blue-800" onClick={handleLogout}>Logout</Button>
  );
};

export default LogoutButton;