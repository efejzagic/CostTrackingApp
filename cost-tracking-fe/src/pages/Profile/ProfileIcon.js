import React, { useState } from 'react';
import { IconButton, Menu, MenuItem, ListItemIcon, ListItemText } from '@mui/material';
import { AccountCircle, Logout, Login } from '@mui/icons-material';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';


const ProfileIcon = ( ) => {
  const [anchorEl, setAnchorEl] = useState(null);
const navigate = useNavigate();


  const handleMenuOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
  };

  const handleLogin = () => {
    navigate('/login');
  };

  const handleLogout = () => {
    localStorage.removeItem("accessToken");
    navigate("/login"); 
  };
  return (
    <div>
      <IconButton
        onClick={handleMenuOpen}
        size="large"
        color="inherit"
        aria-label="profile-icon"
        edge="end"
      >
        <AccountCircle />
      </IconButton>
      <Menu
        anchorEl={anchorEl}
        open={Boolean(anchorEl)}
        onClose={handleMenuClose}
        anchorOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
        transformOrigin={{
          vertical: 'top',
          horizontal: 'right',
        }}
      >
          <MenuItem onClick={handleLogout}>
            <ListItemIcon>
              <Logout fontSize="small" />
            </ListItemIcon>
            <ListItemText primary="Log Out" />
          </MenuItem>
        
        <MenuItem  component={Link} to="/profile" onClick={handleMenuClose}>
          <ListItemIcon>
            <AccountCircle fontSize="small" />
          </ListItemIcon>
          <ListItemText primary="Manage Account" />
        </MenuItem>
      </Menu>
    </div>
  );
};

export default ProfileIcon;
