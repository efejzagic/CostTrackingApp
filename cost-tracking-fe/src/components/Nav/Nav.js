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
          style={{ width: '100px', height: '100px' }} // Set your desired width and height
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
