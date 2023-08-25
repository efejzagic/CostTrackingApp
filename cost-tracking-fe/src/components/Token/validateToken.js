const jwt = require('jsonwebtoken');

const validateToken = (token) => {
  const publicKey = 'YOUR_KEYCLOAK_PUBLIC_KEY'; // Replace with your Keycloak public key
  const options = {
    algorithms: ['RS256'], // Algorithm used by Keycloak for signing tokens
  };

  try {
    const decodedToken = jwt.verify(token, publicKey, options);
    return decodedToken;
  } catch (error) {
    throw new Error('Invalid token');
  }
};

const accessToken = localStorage.getItem('accessToken'); // Replace with the access token to validate
try {
  const decodedToken = validateToken(accessToken);
  console.log('Valid token:', decodedToken);
} catch (error) {
  console.error('Token validation error:', error.message);
}