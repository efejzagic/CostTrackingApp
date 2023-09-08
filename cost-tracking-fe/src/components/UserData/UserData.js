import React, { useEffect, useState } from 'react';
import axios from 'axios';

const UserData = () => {
  const [userData, setUserData] = useState({});
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    // Make an Axios GET request to fetch user data
    axios.get('http://localhost:8001/api/Auth/UserData')
      .then((response) => {
        setUserData(response.data.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching user data:', error);
        setLoading(false);
      });
  }, []);

  if (loading) {
    return <p>Loading...</p>;
  }

  return (
    <div>
      <h2>User Data</h2>
      <pre>{JSON.stringify(userData, null, 2)}</pre>
    </div>
  );
};

export default UserData;
