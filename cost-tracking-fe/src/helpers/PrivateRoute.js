import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import useAuth from "../components/Auth/AuthProvider";
import { decodeJwt } from 'jose';

const PrivateRoute = ({ children, elseContent }) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const navigate = useNavigate();
  const isLoggedInAuth = useAuth();

  const validateAccessToken = () => {
    const token = localStorage.getItem('accessToken'); 

    if (token) {
      try {
        
        const tmp = decodeJwt(token);
        console.log(tmp);
        if (tmp.exp * 1000 < Date.now()) {
          setIsLoggedIn(false);
          console.log("Session expired");
          return false;
        }
        localStorage.setItem('name', tmp.given_name);
        setIsLoggedIn(true);
        console.log("loggeed in true 1 ");
        return true;
      } catch (error) {
        setIsLoggedIn(false);
        console.log("loggeed in false 2 ");
      }
    } else {
      
      console.log("loggeed in false"); 
   }
   return false;
  };

  useEffect(() => {
      const isValidAccessToken = validateAccessToken();

      if (isValidAccessToken) {
        setIsLoggedIn(true);
      }
      else {
        navigate('/login');
      }
    
  }, []);

  return isLoggedIn ? children : elseContent; 
};

export default PrivateRoute;
