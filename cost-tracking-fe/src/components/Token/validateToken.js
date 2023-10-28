const jwt = require('jsonwebtoken');

const validateToken = (token) => {
  const publicKey = 'YOUR_KEYCLOAK_PUBLIC_KEY'; 
  const options = {
    algorithms: ['RS256'], 
  };

  try {
    const decodedToken = jwt.verify(token, publicKey, options);
    return decodedToken;
  } catch (error) {
    throw new Error('Invalid token');
  }
};

const accessToken = localStorage.getItem('accessToken');
try {
  const decodedToken = validateToken(accessToken);
  console.log('Valid token:', decodedToken);
} catch (error) {
  console.error('Token validation error:', error.message);
}