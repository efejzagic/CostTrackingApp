import axios from 'axios';

export const userData = async (roleName) => {
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
    console.log("response",response.data);
    if(response.error) {
        return false;
    }
    const userData = response.data.data;
    console.log(userData.roles);
   
    return userData;
  } catch (error) {
    console.error('Error fetching user data:', error);
  }
};