import React, { useState, useEffect } from "react";
import { Typography, AppBar, Button, ButtonGroup, CssBaseline, Toolbar } from "@mui/material";
import { useNavigate } from "react-router-dom";
import ProfileIcon from "../../pages/Profile/ProfileIcon";
import logo from "../../static/img/cta_logo.png"

const Nav = () => {
  const [isLoggedIn, setIsLoggedIn] = useState(Boolean(localStorage.getItem("accessToken")));
  const navigate = useNavigate();

  useEffect(() => {
    const accessToken = localStorage.getItem("accessToken");
    if (accessToken) {
      setIsLoggedIn(true);
    } else {
      setIsLoggedIn(false);
    }
  }, []);



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
      filter: 'blur(0)',  // Adjust the blur value as needed
      opacity: 1,        // Adjust the opacity value (0.0 to 1.0)
      transition: 'filter 0.3s, opacity 0.3s', // Add a smooth transition effect
    }}
    onMouseOver={(e) => {
      e.currentTarget.style.filter = 'blur(0.5px)'; // Remove the blur on hover
      e.currentTarget.style.opacity = '0.8';      // Set opacity to 1 on hover
      e.currentTarget.style.cursor = 'pointer'; // Change cursor to pointer on hover
    }}
    onMouseOut={(e) => {
      e.currentTarget.style.filter = 'blur(0)'; // Reapply the blur on mouse out
      e.currentTarget.style.opacity = '1';      // Set opacity back to 0.8 on mouse out
      e.currentTarget.style.cursor = 'auto';      // Reset cursor to default on mouse out
    }}
  />
</Typography>

        <ButtonGroup variant="text" aria-label="text button group">
          <Button onClick={() => navigate("/construction")}>Construction</Button>
          <Button onClick={() => navigate("/employee")}>Employees</Button>
          <Button onClick={() => navigate("/article")}>Articles</Button>
          <Button onClick={() => navigate("/supplier")}>Suppliers</Button>
        </ButtonGroup>
        <ProfileIcon />
      </Toolbar>
    </>
  );
};

export default Nav;
