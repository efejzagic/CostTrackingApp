import React, { useEffect, useState } from "react";

const PrivateRoute = ({ children, elseContent }) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  const validateAccessToken = (token) => {
    // Implement your logic to validate the access token here
    // Return true if valid, false otherwise
    if (token.trim().length === 0) return false;

    return true; // Placeholder
  };

  useEffect(() => {
    // Check if the access token exists in local storage
    const accessToken = localStorage.getItem('accessToken');
    if (accessToken) {
      // You would need to implement a function to validate the access token
      const isValidAccessToken = validateAccessToken(accessToken);

      if (isValidAccessToken) {
        setIsLoggedIn(true);
      }
    }
  }, []);

  return isLoggedIn ? children : elseContent; // Render children if logged in, else render elseContent
};

export default PrivateRoute;
