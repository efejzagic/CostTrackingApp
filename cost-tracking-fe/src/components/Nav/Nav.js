import React from "react";
import { Typography, Paper,  AppBar,Table,TableHead, TableRow, TableBody, TableCell, TableContainer, TablePagination, Card, CardActions, CardContent, CardMedia, CssBaseline, Grid, Toolbar, Container} from '@mui/material';
import Button from '@mui/material/Button';
import ButtonGroup from '@mui/material/ButtonGroup';
import PrivateRoute from '../../helpers/PrivateRoute';
import { useNavigate  } from 'react-router-dom'; // Import useHistory from React Router
import LogoutButton from "../Logout/LogoutButton";



const Nav=()=> {
  const navigate = useNavigate (); // Initialize useHistory
  const handleLogin = () => {
    // You can perform any login-related actions here if needed
  
    // After handling login logic, navigate to the "/test" route
    navigate('/login');
    };
  return (
    <>
      <CssBaseline />
      <AppBar position='relative'/>
        <Toolbar sx={{ justifyContent: "space-between" }}>
          <Typography variant='button' onClick={() => navigate('/')}> Cost Tracking App</Typography>
          <ButtonGroup variant="text" aria-label="text button group">
            <div className="hover:text-gray-200">
            <Button onClick={() => navigate('/construction')}>Construction</Button>
            <Button onClick={() => navigate('/employee')}>Employees</Button>

            <Button onClick={() => navigate('/article')}>Articles</Button>
              <Button onClick={() => navigate('/supplier')}>Suppliers</Button>
            <PrivateRoute elseContent={<Button    type="button"
                     className="text-blue-800"
                     onClick={handleLogin}>Login</Button>}>
        {/* This content will only be displayed if the user is logged in */}
        <Typography variant="button">Hi</Typography>
        <LogoutButton  ></LogoutButton>
      </PrivateRoute>
      
                
               </div>
        </ButtonGroup>
        </Toolbar>
       
    </>
  );
}

export default Nav;
