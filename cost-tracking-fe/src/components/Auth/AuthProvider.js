import { useState, useEffect } from 'react';
import { decodeJwt } from 'jose';

function useAuth() {
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    // Check if a JWT token exists in local storage (or wherever you store your tokens)
    const token = localStorage.getItem('accessToken'); // Replace 'your_jwt_key' with your actual key

    if (token) {
      // Decode the token to check if it's valid
      try {
        decodeJwt(token);

        // Token is valid
        setIsLoggedIn(true);
        console.log("loggeed in true 1 ");
      } catch (error) {
        // Token is invalid or expired
        setIsLoggedIn(false);
        console.log("loggeed in false 2 ");
      }
    } else {
      // No token found
      console.log("loggeed in false"); 
   }
  }, []);

  console.log('isLoggedIn return ', isLoggedIn);
  return isLoggedIn;
}

export default useAuth;
