export const getConfigHeader = () => {
    const accessToken = localStorage.getItem('accessToken');
    if (!accessToken) {
        console.error('Access token not found in local storage');
        return false;
    }
    return {
        headers: {
          Authorization: `Bearer ${accessToken}`,
        },
      };
  }