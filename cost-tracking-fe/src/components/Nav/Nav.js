import React, { useState, useEffect } from "react";
import { Typography, AppBar, Button, ButtonGroup, CssBaseline, Toolbar } from "@mui/material";
import { useNavigate } from "react-router-dom";
import ProfileIcon from "../../pages/Profile/ProfileIcon";


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
          Cost Tracking App
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
