import * as React from 'react';
import '../App.css';
import { Typography, Paper,  AppBar,Table,TableHead, TableRow, TableBody, TableCell, TableContainer, TablePagination, Card, CardActions, CardContent, CardMedia, CssBaseline, Grid, Toolbar, Container} from '@mui/material';
import Nav from '../components/Nav/Nav'
import { useState, useEffect } from 'react';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import GetSuppliers from '../components/Supplier/GetSuppliers'

function Homepage() {
  const data = GetSuppliers();
 
  return (
    <>
      <Nav/>
        <main>
          <div>
            <Container maxWidth='sm'>
              <Typography variant='h3' align='center' color='textPrimary' gutterBottom>
                Supplier Table
              </Typography>
              <Typography variant='h6' align='center' color='textSecondary' gutterBottom>
                Axios fetch test for Supplier API
              </Typography>
              <TableContainer component={Paper}>
      <Table sx={{ minWidth: 500 }} aria-label="simple table">
        <TableHead>
          <TableRow>
            <TableCell>Id </TableCell>
            <TableCell align="center">Name</TableCell>
            <TableCell align="center">Email</TableCell>
            <TableCell align="center">Phone</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {data.map((row) => (
            <TableRow
              key={row.name}
              sx={{ '&:last-child td, &:last-child th': { border: 0 } }}
            >
              <TableCell component="th" scope="row">
                {row.id}
              </TableCell>
              <TableCell align="right">{row.name}</TableCell>
              <TableCell align="right">{row.email}</TableCell>
              <TableCell align="right">{row.phone}</TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
   
            </Container>
          </div>
        </main>
    </>
  );
}

export default Homepage;
