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
import { useNavigate } from 'react-router-dom';
import StyledPage from '../components/Styled/StyledPage';
import {fetchUserRoles} from '../components/UserData/GetUserRoles';
import { useDispatch, useSelector } from "react-redux";
import { loadBugs, getUnresolvedBugs, resolveBug, } from "../state/bugs";
import { loadsuppliers, selectSuppliers } from '../state/suppliers';

function Homepage() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const suppliers = useSelector(selectSuppliers);
  const [name, setName] = useState('');
  const [roles, setRoles] = useState([]);
  const [showMediaCard, setShowMediaCard] = useState(false);
  
  async function CheckRole(roleName) {
    console.log(roleName, await checkRoleInUserData(roleName));
    return await checkRoleInUserData(roleName);
  }
  useEffect(() => {
    dispatch(loadsuppliers());
  }, []);

  useEffect(() => {
    console.log({suppliers});

  }, [suppliers]);
  useEffect(() => {

    async function fetchRolesData() {
      const userRoles = await fetchUserRoles(); 
      setRoles(userRoles);
    }
    fetchRolesData();
    
    setName(localStorage.getItem('name'));
  }, []);

  return (
    <>
        <>
      <StyledPage>
        <main style={{ paddingBottom: '4rem' }}>
          <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
            <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
              Hello {name}
            </Typography>
          </Container>
          <div className="card-grid" style={{ marginTop: '4rem' }}>
            <MediaCard
              title="Users"
              media={<Icon component={PersonAddIcon} fontSize="large" />}
              text="This section provides a list of all registered users in the system. You can view a list of users, create new user profiles, and edit existing user profiles. It's a central hub for managing user accounts within your application."
              route='/users'
            />
            {roles.includes('Finance') && (
              <MediaCard
                title="Income"
                media={<Icon component={ReceiptIcon} fontSize="large" />}
                text="The Income card allows you to track and manage sources of income. You can record various types of income, such as salaries, bonuses, or other sources, and track income amounts and frequency. It helps users monitor their earnings."
                route='/invoice'
              />
            )}
            {roles.includes('Finance') && (
              <MediaCard
                title="Expenses"
                media={<Icon component={PaidIcon} fontSize="large" />}
                text="This section helps you keep track of your expenses and spending. Users can log and categorize their expenses, making it easier to budget and manage finances. It typically includes features for adding expenses, setting categories, and analyzing spending patterns."
                route='/expense'
              />
            )}
            {roles.includes('Storage Manager') && (
              <MediaCard
                title="Order"
                media={<Icon component={CurrencyExchangeIcon} fontSize="large" />}
                text="The Order card is used to manage and track orders or purchases. Users can create new orders and view order histories. It's used to handle customer orders and keep track of product sales."
                route='/order'
              />
            )}
            {roles.includes('Finance') && (
              <MediaCard
                title="Balance"
                media={<Icon component={AccountBalanceWalletIcon} fontSize="large" />}
                text="The Balance card displays the financial status and net balance of an organization. It provides a snapshot of the current financial situation, showing the total income, total expenses, and the resulting balance. This card is essential for maintaining financial health."
                route='/balance'
              />
            )}
          </div>
        </main>
      </StyledPage>
    </>
    </>
  );
}

export default Homepage;
