import React from 'react';
import { Typography, Container, Paper } from '@mui/material';
import Nav from '../Nav/Nav';
import StyledPage from '../Styled/StyledPage';
const UnauthorizedPage = () => {
  return (
    <>
    <StyledPage>
    <Container maxWidth="sm">
      <Paper elevation={3} style={{ padding: '20px', textAlign: 'center' }}>
        <Typography variant="h4" gutterBottom>
          Unauthorized 401
        </Typography>
        <Typography variant="body1">
          You do not have permission to access this page.
        </Typography>
      </Paper>
    </Container>
    </StyledPage>
    </>
  );
};

export default UnauthorizedPage;