import * as React from 'react';
import '../App.css';
import 'react-toastify/dist/ReactToastify.css';
import MediaCard from '../components/Cards/MediaCard';
import '../components/Cards/CardGrid.css'
import { Container, Typography } from '@mui/material';
import { Icon } from '@mui/material';
import PersonAddIcon from '@mui/icons-material/PersonAdd';
import ReceiptIcon from '@mui/icons-material/Receipt';
import PaidIcon from '@mui/icons-material/Paid';
import CurrencyExchangeIcon from '@mui/icons-material/CurrencyExchange';
import AccountBalanceWalletIcon from '@mui/icons-material/AccountBalanceWallet';
import { checkRoleInUserData } from '../components/UserData/RoleChecker';
import { useState, useEffect } from 'react';
import Nav from '../components/Nav/Nav';
import useAuth from '../components/Auth/AuthProvider';
import { useNavigate } from 'react-router-dom';
import styled from 'styled-components';
import background from '../static/img/backround_image.jpg'

const StyledPage = styled.div`
background-image: url(${background});
background-size: cover;
  background-position: center;
  height: 100vh; /* Adjust the height as needed */
  position: relative;

  /* Add the light breeze blue wave effect */
  &::before {
    content: '';
    position: absolute;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background: linear-gradient(to bottom, #b3e0ff 0%, #5cacee 100%);
    opacity: 0; /* Adjust the opacity as needed */
  }
`;

function Homepage() {
  // const isLoggedIn = useAuth();
  const navigate = useNavigate();

  const [name, setName] = useState('');
  const [hasRole, setHasRole] = useState(false);
  const [showMediaCard, setShowMediaCard] = useState(false);

  useEffect(() => {
    async function fetchData() {
      const roleName = 'Finance'; // Replace with the role you want to check
      const result = await checkRoleInUserData(roleName);
      setHasRole(result);
      setShowMediaCard(result); // Set showMediaCard based on the role result
    }
    // if(!isLoggedIn) {
    //   console.log("rediredct to login");
    //   navigate('/login');
    // }
    setName(localStorage.getItem('name'));
    fetchData();
  }, []);


  

  return (
    <>
          <StyledPage>

             <Nav/>

      <main>


      <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center'  }}>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
          Hello {name}
        </Typography>
        </Container>
      <div className="card-grid " style={{ marginTop: '4rem'}}>
      <MediaCard
        title="Create new users"
        media={<Icon component={PersonAddIcon} fontSize="large" />}
        text="Lorem ipsum dolor sit amet."
        route='/users'
      />
  <MediaCard
        title="Create new Invoice"
        media={<Icon component={ReceiptIcon} fontSize="large" />}
        text="Lorem ipsum dolor sit amet."
        route='/invoice'
      />
       <MediaCard
        title="Create new expense"
        media={<Icon component={PaidIcon} fontSize="large" />}
        text="Lorem ipsum dolor sit amet."
        route='/expense'
      />
       <MediaCard
        title="Transactions"
        media={<Icon component={CurrencyExchangeIcon} fontSize="large" />}
        text="Lorem ipsum dolor sit amet."
        route='/construction/create'
      />

      {showMediaCard && (
        <MediaCard
          title="Finance"
          media={<Icon component={AccountBalanceWalletIcon} fontSize="large" />}
          text="Lorem ipsum dolor sit amet."
          route="/construction/create"
        />
      )}
      </div>

      </main>
      </StyledPage>

    </>
  );
}

export default Homepage;
