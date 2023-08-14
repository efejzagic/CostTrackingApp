import * as React from 'react';
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import Nav from '../components/Nav/Nav';
import { Button } from '@mui/material';
import CardActions from '@mui/material/CardActions';
import CardMedia from '@mui/material/CardMedia';

export default function AdminUsersPage() {
  return (
    <>
    <Nav/>
    <Typography variant='h4' align='center' color='textPrimary' gutterBottom> Admin Users Page</Typography>
    <Typography variant='body2' align='center' color='textPrimary' gutterBottom>Sve akcije koje su vezane za korisnike sistema sa strane admina</Typography>
    
    <Box justifyContent="center" width='auto' sx={{ display: 'flex' }}>
  
      <Card sx={{ marginRight: '16px' }}>
      <CardMedia
        sx={{ height: 140 }}
        image='https://upload.wikimedia.org/wikipedia/commons/thumb/a/a7/React-icon.svg/1200px-React-icon.svg.png'
        title="green iguana"
      />
        <CardContent>
            <Typography gutterBottom  sx={{ fontSize: 14 }} component='div'> 
                Kreiraj novog korisnika
            </Typography>
            <Typography variant='body2' color='text.secondary'>
                Kreirajte novog korisnika sa rolama
            </Typography>
            <CardActions>
                <Button size="small">Kreiraj</Button>
            </CardActions>
        </CardContent>
      </Card>
      <Card >
        <CardContent>
            <Typography gutterBottom  sx={{ fontSize: 14 }} component='div'> 
                Kreiraj novog korisnika
            </Typography>
            <Typography variant='body2' color='text.secondary'>
                Kreirajte novog korisnika sa rolama
            </Typography>
            <CardActions>
                <Button size="small">Kreiraj</Button>
            </CardActions>
        </CardContent>
      </Card>
    </Box>
    </>
  );
}