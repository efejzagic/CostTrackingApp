import React from 'react';
import { Typography, Container, Paper } from '@mui/material';
import Nav from '../Nav/Nav';
const UnauthorizedPage = () => {
  return (
    <>
    <Nav/>
    <Container maxWidth="sm">
      <Paper elevation={3} style={{ padding: '20px', textAlign: 'center' }}>
        <Typography variant="h4" gutterBottom>
          Unauthorized
        </Typography>
        <Typography variant="body1">
          You do not have permission to access this page.
        </Typography>
      </Paper>
    </Container>
    </>
  );
};

export default UnauthorizedPage;