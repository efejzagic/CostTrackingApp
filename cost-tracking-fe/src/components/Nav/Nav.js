import React, { useState, useEffect } from "react";
import {
  Badge,
  Typography,
  AppBar,
  Button,
  ButtonGroup,
  CssBaseline,
  Toolbar,
  Drawer,
  List,
  ListItem,
  ListItemText,
  Divider,
  Box,
} from '@mui/material';
import { useNavigate } from "react-router-dom";
import ProfileIcon from "../../pages/Profile/ProfileIcon";
import logo from "../../static/img/cta_logo.png"
import { useCart } from "../../pages/Cart/CartContext";
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import Paper from "@mui/material/Paper"; // Import Paper component
import 'react-toastify/dist/ReactToastify.css';
import { toast } from 'react-toastify';
import axios from 'axios';
import { fetchUserRoles } from "../UserData/GetUserRoles";
import { getConfigHeader } from "../Auth/GetConfigHeader";

const buttonGroupStyle = {
  margin: '20px', // Adjust the margin as needed
};

const buttonStyle = {
  backgroundColor: '#007bff', // Customize the background color
  color: '#fff', // Text color
  '&:hover': {
    backgroundColor: '#0056b3', // Hover color
  },
};

const scrollableCartStyle = {
  width: '100%', // Adjust the width as needed
  height: '100%',
  overflowY: 'auto',
  display: 'flex',
  flexDirection: 'column',
};


const Nav = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(Boolean(localStorage.getItem("accessToken")));
  const navigate = useNavigate();
  const [cartOpen, setCartOpen] = useState(false);
  const { cart, clearCart } = useCart(); 
  const [hasRole, setHasRole] = useState(false);
  const [showMediaCard, setShowMediaCard] = useState(false);
  const[roles, setRoles] = useState([]);


  useEffect(() => {
    const accessToken = localStorage.getItem("accessToken");
    if (accessToken) {
      setIsLoggedIn(true);
      async function fetchRolesData() {
        const userRoles = await fetchUserRoles(); 
        setRoles(userRoles);
      }
      fetchRolesData();
      
    } else {
      setIsLoggedIn(false);
    }
  }, []);

  const toggleCart = () => {
    setCartOpen(!cartOpen);
  };

  const calculateTotal = () => {
    const totalAmount = cart.reduce((acc, item) => acc + item.quantity * item.price, 0);
    return totalAmount.toFixed(2); 
  };

  console.log("cart length: ",cart.length)
  const drawerHeight = cart.length > 0 ? `${cart.length * 250}px` : "200px"; 

  const orderItemsData = JSON.parse(localStorage.getItem('cart'));


  const HandleOrder = () => {
    const orderItems = orderItemsData.map((item) => ({
      articleId: item.id,
      articleName: item.name,
      quantity: item.quantity,
      pricePerItem: item.price,
      orderId: 0,
  }));

  const shippingDate = new Date();
  shippingDate.setUTCDate(shippingDate.getUTCDate() + 1);

  const createOrderDTO = {
    value: {
        orderDate: new Date().toISOString(),
        shippingDate: shippingDate.toISOString(),
        orderComplete: true,
        totalAmount: orderItems.reduce((total, item) => total + item.quantity * item.pricePerItem, 0),
        orderItems: orderItems,
    }
};

  
const isEmpty = Object.keys(orderItemsData).length === 0;
console.log("isEmpty" , isEmpty);
  if(isEmpty ) {

    toast.warning("Cart cannot be empty");
  }
  else {
  axios
      .post('http://localhost:8001/api/v/OrderExpense', createOrderDTO, getConfigHeader())
      .then((response) => {
          console.log('Order created successfully');
          var id = parseInt(response.data.message);
          localStorage.setItem('cart', JSON.stringify([]));
          toast.success('Order succesfull');
          clearCart();
          navigate(`/order/${id}/details`)
      })
      .catch((error) => {
          console.error('Error creating the order:', error);
      });
    }
  }



  return (
    <>
      <CssBaseline />
      <AppBar position="relative" />
      <Toolbar sx={{ justifyContent: "space-between" }}>
        <Typography variant="button" onClick={() => navigate("/")}>
          <img
            src={logo}
            alt="Cost Tracking App"
            style={{
              width: '100px',
              height: '100px',
              filter: 'blur(0)',
              opacity: 1,
              transition: 'filter 0.3s, opacity 0.3s',
            }}
            onMouseOver={(e) => {
              e.currentTarget.style.filter = 'blur(0.5px)';
              e.currentTarget.style.opacity = '0.8';
              e.currentTarget.style.cursor = 'pointer';
            }}
            onMouseOut={(e) => {
              e.currentTarget.style.filter = 'blur(0)';
              e.currentTarget.style.opacity = '1';
              e.currentTarget.style.cursor = 'auto';
            }}
          />
        </Typography>
        <div style={buttonGroupStyle}>
          <ButtonGroup
            variant="text"
            aria-label="text button group"
          >
            {roles.includes("ConstructionSite Manager") && (
            <Button
              onClick={() => navigate('/construction')}
              style={buttonStyle}
            >
              Construction
            </Button>
            )}
            {roles.includes("ConstructionSite Manager") && (  
            <Button
              onClick={() => navigate('/employee')}
              style={buttonStyle}
            >
              Employees
            </Button>
            )}

{roles.includes("Storage Manager") && (
          
            <Button
              onClick={() => navigate('/article')}
              style={buttonStyle}
            >
              Articles
            </Button>
)}
            {roles.includes("Storage Manager") && (
            <Button
              onClick={() => navigate('/supplier')}
              style={buttonStyle}
            >
              Suppliers
            </Button>
            )}
             {roles.includes("Equipment Manager") && (
            <Button
              onClick={() => navigate('/machinery')}
              style={buttonStyle}
            >
              Machinery
            </Button>
             )}
             {roles.includes("Equipment Manager") && (
              <Button
                onClick={() => navigate('/maintenance')}
                style={buttonStyle}
              >
                Maintenance
              </Button>
            )}
          </ButtonGroup>
        </div>
        <div style={{ display: 'flex', alignItems: 'center' }}>

        {roles.includes("Storage Manager") && (

        <Badge badgeContent={cart ? cart.length : 0} color="error">
        <ShoppingCartIcon onClick={toggleCart} style={{ cursor: "pointer" }} />

        </Badge>
        )}
        <ProfileIcon />
        </div>
      </Toolbar>

      <Drawer
        anchor="right"
        open={cartOpen}
        onClose={toggleCart}
        PaperProps={{
          sx: {
            width: "400px",
            padding: "20px",
            height: drawerHeight, 
            overflowY: "auto",
          },
        }}
      >
        <div>
          <List>
            {cart &&
              cart.map((item) => (
                <div key={item.id}>
                  <ListItem>
                    <ListItemText
                      primary={item.name}
                      secondary={
                        <>
                          Quantity: {item.quantity}
                          <br />
                          Price: {item.price} BAM
                          <br />
                         {}
                        </>
                      }
                    />
                  </ListItem>
                  <Divider />
                </div>
              ))}
          </List>
          <Box mt={2} px={2}> 
            <Typography variant="h6" gutterBottom>
              Total:  {calculateTotal()}  BAM
            </Typography>
            <Button style={{marginRight: '10px'}} variant="outlined" onClick={clearCart}>
              Clear Cart
            </Button>
            <Button onClick={HandleOrder}style={{marginLeft: '10px'}} variant="outlined">
              Order
            </Button>
          </Box>
        </div>
      </Drawer>
    </>
  );
};

export default Nav;
