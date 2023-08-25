import * as React from 'react';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import CardMedia from '@mui/material/CardMedia';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { useNavigate } from 'react-router-dom';


export default function MediaCard(props) {
  const { title, media, text, route } = props;
  const navigate = useNavigate ();
    console.log("Route: ", {route});

    const handleButton = () => {
        navigate(route);
      };
  return (
    <Card sx={{ maxWidth: 345 }}>
      <CardMedia
        sx={{ height: 140, display: 'flex', alignItems: 'center', justifyContent: 'center' }}
      >
        {media}
        </CardMedia>
      <CardContent>
        <Typography gutterBottom variant="h5" component="div">
          {title}
        </Typography>
        <Typography variant="body2" color="text.secondary">
          {text}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small" onClick={handleButton}>GO</Button>
      </CardActions>
    </Card>
  );
}
