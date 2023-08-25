import React, { useState } from 'react';
import { Button, TextField } from '@mui/material';
import { Container, Paper, Typography } from '@mui/material';
import { useNavigate  } from 'react-router-dom'; // Import useHistory from React Router
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import axios from 'axios';




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

      // const response = await fetch('http://localhost:8001/api/Auth/login', {
      //   method: 'POST',
      //   headers: {
      //     'Content-Type': 'application/json',
      //   },
      //   body: JSON.stringify({ username, password }),
      // });

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
        
        console.log("Response: ", response)
        const data = response.data;
        // console.log("Data: " , data.data);
        localStorage.setItem('accessToken', data.accessToken);
        localStorage.setItem('username', data.username)
        setIsLoading(true);
        toast.success('Login succesfull');

        navigate('/'); // Save token in local storage
      } else {
        console.error('Login failed');
        toast.error(' - Login failed'); // Display the toast alert

      }
    } catch (error) {
      console.error('Error:', error);
      toast.error('Error - Login failed'); // Display the toast alert

    }
  };

  return (

    
    <Container style={styles.container}>

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
  </Container>

  );
}

export default Login;
