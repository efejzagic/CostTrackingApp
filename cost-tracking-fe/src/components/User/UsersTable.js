import React, { useEffect, useState } from 'react';
import axios from 'axios';
import { Button, Container, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography, Modal, Box } from '@mui/material';
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';

const UserTable = () => {
  const [users, setUsers] = useState([]);
  const navigate = useNavigate();



  const handleOpen = (id) => {
  };

  const handleCreate = () => {
    navigate('/users/create');
  };
  useEffect(() => {
    const fetchUsers = async () => {
      try {
        const token = localStorage.getItem('accessToken');
        const response = await axios.get('http://localhost:8001/api/v/Account/all-users', {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        });
        console.log("Response", response);
        if (response.status === 200) {
            setUsers(response.data);
        }
        else {
            console.log("else");
            navigate('/login');
        }
      } catch (error) {
        
        console.error('Error fetching users:', error);
        if (error.response.status === 401) {
          console.log("Unauthorized access");
          // Redirect to unauthorized page or handle the unauthorized access scenario
          navigate('/unauthorized');
        }
        else {
          navigate('/login');
        }
      }
    };

    fetchUsers();
  }, []);

  return (
      <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
          Users
        </Typography>
        <Button onClick={handleCreate} variant="contained" color="primary" style={{ marginBottom: '1rem', alignSelf: 'flex-start' }}>
          Create New User
        </Button>
        <TableContainer component={Paper} style={{ overflowX: 'auto', minWidth: 1200, alignSelf: 'center' }}>
          <Table style={{ minWidth: 800 }}>
            <TableHead>
              <TableRow>
                <TableCell width={100}>Id</TableCell>
                <TableCell width={200}>Username</TableCell>
                <TableCell width={200}>First Name</TableCell>
                <TableCell width={200}>Last Name</TableCell>                
                <TableCell width={200}>Email</TableCell>
                </TableRow>
            </TableHead>
            <TableBody>
              {users.map((item) => (
                <TableRow key={item.id}>
                  <TableCell>{item.id}</TableCell>
                  <TableCell>{item.username}</TableCell>
                  <TableCell>{item.firstName}</TableCell>
                  <TableCell>{item.lastName}</TableCell>
                  <TableCell>{item.email}</TableCell>
                  <TableCell>
                    <div style={{ display: 'flex', gap: '0.5rem' }}>
                      <Button variant="outlined" color="primary" size="small">
                        <Link to={`/users/edit/${item.id}`}>Edit</Link>
                      </Button>
                      <Button onClick={() => handleOpen(item.id)} variant="outlined" color="secondary" size="small">
                        Delete
                      </Button>
                      
                    </div>
                  </TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      </Container>
  );
};

export default UserTable;
