import React from "react";
import { Typography, Paper,  AppBar,Table,TableHead, TableRow, TableBody, TableCell, TableContainer, TablePagination, Card, CardActions, CardContent, CardMedia, CssBaseline, Grid, Toolbar, Container} from '@mui/material';
import Button from '@mui/material/Button';
import ButtonGroup from '@mui/material/ButtonGroup';
import PrivateRoute from '../../helpers/PrivateRoute';
import { useNavigate  } from 'react-router-dom'; // Import useHistory from React Router
import LogoutButton from "../Logout/LogoutButton";



const Nav=()=> {
  console.log("Private route" , PrivateRoute);
  const navigate = useNavigate (); // Initialize useHistory
  const handleLogin = () => {
    // You can perform any login-related actions here if needed
  
    // After handling login logic, navigate to the "/test" route
    navigate('/test');
    };
  return (
    <>
      <CssBaseline />
      <AppBar position='relative'/>
        <Toolbar sx={{ justifyContent: "space-between" }}>
          <Typography variant='button'> Cost Tracking App</Typography>
          <ButtonGroup variant="text" aria-label="text button group">
            <div className="hover:text-gray-200">
            <PrivateRoute elseContent={<Button    type="button"
                     className="text-blue-800"
                     onClick={handleLogin}>Login</Button>}>
        {/* This content will only be displayed if the user is logged in */}
        <Typography variant="button">Hi {localStorage.getItem('username')}</Typography>
        <LogoutButton  type="button"
                     className="text-blue-800"></LogoutButton>
      </PrivateRoute>
      
                
               </div>
        </ButtonGroup>
        </Toolbar>
       
    </>
  );
}

export default Nav;
