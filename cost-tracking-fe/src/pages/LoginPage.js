import React, { useState } from 'react';
import { Button, TextField } from '@mui/material';
import { Container, Paper, Typography } from '@mui/material';
import { useNavigate  } from 'react-router-dom';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import axios from 'axios';
import StyledPageNoNav from '../components/Styled/StyledPageNoNav';
import { css } from "@emotion/react";
import { RingLoader } from "react-spinners";
import LoadingCoomponent from '../components/Loading/LoadingComponent';

const styles = {
  container: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    minHeight: '100vh',
  },
  paper: {
    padding: '20px',
    maxWidth: '400px',
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  form: {
    width: '100%',
    marginTop: '20px',
  },
  submitButton: {
    marginTop: '10px',
  },
};


function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [isLoading, setIsLoading] = useState(false);


  const navigate = useNavigate ();
  
  const handleLogin = async () => {

  
    try {
      setIsLoading(true); 

      const response = await axios.post('http://localhost:8001/api/Auth/login', 
        {
          username,
          password
        },
       {
        headers: {
          'Content-Type': 'application/json',
        },
      });


      if (response.status === 200) {
        setIsLoading(false);
        console.log("Response: ", response)
        const data = response.data;
        localStorage.setItem('accessToken', data.accessToken);
        localStorage.setItem('username', data.username)
        setIsLoading(false);
        toast.success('Login succesfull');

        navigate('/'); 
      } else {
        console.error('Login failed');
        toast.error(' - Login failed'); 
        setIsLoading(false);
      }
    } catch (error) {
      console.error('Error:', error);
      setIsLoading(false);
      toast.error('Error - Login failed');

    }
  };

  return (

    <>
   
    <StyledPageNoNav>
    <Container style={styles.container}>
    {isLoading ? ( 
         <LoadingCoomponent loading={isLoading}/>
        ) : (
    <Paper elevation={3} style={styles.paper}>
      
      <Typography variant="h4">Login</Typography>
      <form style={styles.form}>
        <TextField
          label="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          variant="outlined"
          margin="normal"
          required
          fullWidth
        />
        <TextField
          label="Password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          variant="outlined"
          margin="normal"
          required
          fullWidth
        />
        <Button onClick={handleLogin}
          style={styles.submitButton}
          variant="contained"
          color="primary"
          fullWidth
        >
          Login
        </Button >
      </form>
    </Paper>
        )}
  </Container>
  </StyledPageNoNav>
        
  </>
  );
}

export default Login;
