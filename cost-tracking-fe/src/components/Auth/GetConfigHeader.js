export const getConfigHeader = () => {
    const accessToken = localStorage.getItem('accessToken');
    if (!accessToken) {
        // Handle the case where the access token is not found in local storage
        console.error('Access token not found in local storage');
        return false;
    }
    return {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      };
  }