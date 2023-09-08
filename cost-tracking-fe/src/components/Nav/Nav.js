import React, { useState, useEffect } from "react";
import { Typography, AppBar, Button, ButtonGroup, CssBaseline, Toolbar } from "@mui/material";
import { useNavigate } from "react-router-dom";

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

  const handleLogin = () => {
    // Simulate a successful login by setting a fake access token
    navigate('/login');
  };

  const handleLogout = () => {
    // Simulate a logout by removing the access token
    localStorage.removeItem("accessToken");
    setIsLoggedIn(false);
    navigate("/login"); // Redirect to the login page after logout
  };

  return (
    <>
      <CssBaseline />
      <AppBar position="relative" />
      <Toolbar sx={{ justifyContent: "space-between" }}>
        <Typography variant="button" onClick={() => navigate("/")}>
          Cost Tracking App
        </Typography>
        <ButtonGroup variant="text" aria-label="text button group">
          <Button onClick={() => navigate("/construction")}>Construction</Button>
          <Button onClick={() => navigate("/employee")}>Employees</Button>
          <Button onClick={() => navigate("/article")}>Articles</Button>
          <Button onClick={() => navigate("/supplier")}>Suppliers</Button>
          {isLoggedIn ? (
            <>
              <Typography variant="button">Hi</Typography>
              <Button onClick={handleLogout}>Logout</Button>
            </>
          ) : (
            <Button type="button" className="text-blue-800" onClick={handleLogin}>
              Login
            </Button>
          )}
        </ButtonGroup>
      </Toolbar>
    </>
  );
};

export default Nav;
