import axios from 'axios';

export const checkRoleInUserData = async (roleName) => {
  try {

    const accessToken = localStorage.getItem('accessToken');
    if (!accessToken) {
        // Handle the case where the access token is not found in local storage
        console.error('Access token not found in local storage');
        return false;
    }

    const config = {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      };

    const response = await axios.get('http://localhost:8001/api/Auth/UserData', config);
    // console.log("response",response.data);
    if(response.error) {
        return false;
    }
    const userData = response.data.data;
    // console.log(userData.roles);
    if (userData && userData.roles && userData.roles.includes(roleName)) {
      // console.log(" If Role", roleName)
      return true;
    } else {
      // console.log(" Else Role", roleName)
      return false;
    }
  } catch (error) {
    console.error('Error fetching user data:', error);
  }
};