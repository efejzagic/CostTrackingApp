import axios from 'axios';

export const fetchUserRoles = async () => {
  try {

    const accessToken = localStorage.getItem('accessToken');
    if (!accessToken) {
        console.error('Access token not found in local storage');
        return false;
    }

    const config = {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      };

    const response = await axios.get('http://localhost:8001/api/Auth/UserData', config);
    if(response.error) {
        return [];
    }
    const userData = response.data.data;
    if (userData && userData.roles) {
      return userData.roles;
    } else {
      return [];
    }
  } catch (error) {
    console.error('Error fetching user data:', error);
    return [];
  }
};