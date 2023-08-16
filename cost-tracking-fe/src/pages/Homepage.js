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


function Homepage() {

  return (
    <>
      
      <main>
      <Container maxWidth="md" style={{ marginTop: '2rem', display: 'flex', flexDirection: 'column', alignItems: 'center'  }}>
        <Typography variant="h4" gutterBottom style={{ alignSelf: 'flex-start' }}>
          Homepage admin
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
        route='/construction'
      />
       <MediaCard
        title="Create new expense"
        media={<Icon component={PaidIcon} fontSize="large" />}
        text="Lorem ipsum dolor sit amet."
        route='/construction/create'
      />
       <MediaCard
        title="Transactions"
        media={<Icon component={CurrencyExchangeIcon} fontSize="large" />}
        text="Lorem ipsum dolor sit amet."
        route='/construction/create'
      />
       <MediaCard
        title="Finance"
        media={<Icon component={AccountBalanceWalletIcon} fontSize="large" />}
        text="Lorem ipsum dolor sit amet."
        route='/construction/create'
      />
      
      </div>

      </main>
    </>
  );
}

export default Homepage;
