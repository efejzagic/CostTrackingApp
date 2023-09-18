import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import WelcomePage from "./pages/Homepage";
import SecuredPage from "./pages/Securedpage";
import LoginPage from "./pages/LoginPage"
import { ToastContainer } from 'react-toastify';
import AdminUsers from './pages/AdminUsers'
import SupplierPage from "./pages/Supplier/SupplierPage";
import CreateSupplierPage from "./pages/Supplier/CreateSupplierPage";
import EditSupplierPage from "./pages/Supplier/EditSupplierPage";
import CSPage from "./pages/ConstructionSite/CSPage";
import CreateCSPage from "./pages/ConstructionSite/CreateCSPage";
import EditCSPage from "./pages/ConstructionSite/EditCSPage";
import EmployeePage from "./pages/Employee/EmployeePage";
import CreateEmployeePage from "./pages/Employee/CreateEmployeePage";
import EditEmployeePage from "./pages/Employee/EditEmployeePage";
import UsersPage from "./pages/User/UsersPage";
import CreateUserPage from "./pages/User/CreateUserPage";
import CSEmployeesPage from "./pages/ConstructionSite/CSEmployees";
import InvoicePage from "./pages/Invoice/InvoicePage";
import CreateInvoicePage from "./pages/Invoice/CreateInvoicePage";
import ArticlePage from "./pages/Article/ArticlePage";
import CreateArticlePage from "./pages/Article/CreateArticlePage";
import UnauthorizedPage from "./components/Unauthorized/UnauthorizedPage";
import SupplierArticles from "./pages/Supplier/SupplierArticles";
import ExpensePage from "./pages/Expense/ExpensePage";
import CreateExpensePage from "./pages/Expense/CreateExpensePage";
import PrivateRoute from "./helpers/PrivateRoute";
import ProfilePage from "./pages/Profile/ProfilePage";
import EditUserPage from "./pages/User/EditUserPage";
import EditArticlePage from "./pages/Article/EditArticlePage";
import { createTheme, ThemeProvider } from '@mui/material/styles';
import ExpenseDetailsPage from "./pages/Expense/ExpenseDetailsPage";
import CSExpensesPage from "./pages/ConstructionSite/CSExpensesPage";
import CSExpensePageId from "./pages/ConstructionSite/CSExpensePageId";
import BalancePage from "./pages/Balance/BalancePage";
import OrderPage from "./pages/Order/OrderPage";
import { CartProvider } from "./pages/Cart/CartContext";
import OrderDetailsPage from "./pages/Order/OrderDetails";
import OrderHistoryPage from "./pages/Order/OrderHistoryPage";
import InvoiceDetailsPage from "./pages/Invoice/InvoiceDetailsPage";
import MaintenancePage from "./pages/Maintenance/MaintenancePage";
import CreateMaintenancePage from "./pages/Maintenance/CreateMaintenancePage";
import ForbiddenPage from "./components/Forbidden/ForbiddenPage";
import MachineryPage from "./pages/Machinery/MachineryPage";
import CreateMachineryPage from "./pages/Machinery/CreateArticlePage";

function App() {


  const theme = createTheme({
    typography: {
      fontFamily: 'Play, sans-serif',
      fontSize: 14,
    },
  });

  
 return (



   <div>
    <CartProvider>

  <ThemeProvider  theme={theme}>

       <BrowserRouter>

        <Routes>
        <Route path="/" element={<PrivateRoute><WelcomePage /> </PrivateRoute>} />

        {/* <Route path="/" element={<PrivateRoute element={<WelcomePage />} />} /> */}

           {/* <Route path="/" element={<WelcomePage />} /> */}
           <Route exact path="/login" element={<LoginPage />} />  
           <Route exact path="/users" element={<PrivateRoute><UsersPage /></PrivateRoute>} />
           <Route exact path="/users/create" element={<PrivateRoute><CreateUserPage /></PrivateRoute>} />
           <Route exact path="/users/edit/:id" element={<PrivateRoute><EditUserPage /></PrivateRoute>} />
           <Route exact path="/supplier" element={<PrivateRoute><SupplierPage /></PrivateRoute>} />
           <Route exact path="/supplier/create" element={<PrivateRoute><CreateSupplierPage/></PrivateRoute>} />
           <Route exact path="/supplier/edit/:id" element={<PrivateRoute><EditSupplierPage/></PrivateRoute>} />
           <Route exact path="/construction" element={<PrivateRoute><CSPage /></PrivateRoute>} />

           <Route exact path="/construction/expenses" element={<PrivateRoute><CSExpensesPage/></PrivateRoute>} />
           <Route exact path="/construction/create" element={<PrivateRoute><CreateCSPage/></PrivateRoute>} />
           <Route exact path="/construction/edit/:id" element={<PrivateRoute><EditCSPage/></PrivateRoute>} />
           <Route exact path="/construction/:id/employees" element={<PrivateRoute><CSEmployeesPage/></PrivateRoute>} />
           <Route exact path="/construction/:id/expenses" element={<PrivateRoute><CSExpensePageId/></PrivateRoute>} />

           <Route exact path="/employee" element={<PrivateRoute><EmployeePage /></PrivateRoute>} />
           <Route exact path="/employee/create" element={<PrivateRoute><CreateEmployeePage/></PrivateRoute>} />
           <Route exact path="/employee/edit/:id" element={<PrivateRoute><EditEmployeePage/></PrivateRoute>} />
           <Route exact path="/invoice" element={<PrivateRoute><InvoicePage/></PrivateRoute>} />
           
           <Route exact path="/invoice/create" element={<PrivateRoute><CreateInvoicePage/></PrivateRoute>} />
           <Route exact path="/invoice/:id/details" element={<PrivateRoute><InvoiceDetailsPage/></PrivateRoute>} />
           <Route exact path="/expense" element={<PrivateRoute><ExpensePage/></PrivateRoute>} />
           <Route exact path="/expense/create" element={<PrivateRoute><CreateExpensePage/></PrivateRoute>} />
           <Route exact path="/expense/:id" element={<PrivateRoute><ExpenseDetailsPage/></PrivateRoute>} />

           <Route exact path="/expense/create" element={<PrivateRoute><CreateExpensePage/></PrivateRoute>} />

           <Route exact path="/article" element={<PrivateRoute><ArticlePage/></PrivateRoute>} />
           <Route exact path="/article/create" element={<PrivateRoute><CreateArticlePage/></PrivateRoute>} />
           <Route exact path="/article/edit/:id" element={<PrivateRoute><EditArticlePage/></PrivateRoute>} />

           <Route exact path="/supplier/:id/articles" element={<SupplierArticles/>} />
           <Route exact path="/profile" element={<PrivateRoute><ProfilePage/></PrivateRoute>} />

           <Route exact path="/balance" element={<BalancePage/>} />
           <Route exact path="/order" element={<OrderPage/>} />
           <Route exact path="/order/:id/details" element={<OrderDetailsPage/>} />
           <Route exact path="/order/history" element={<OrderHistoryPage/>} />


           <Route exact path="/maintenance" element={<MaintenancePage/>} />
           <Route exact path="/maintenance/create" element={<CreateMaintenancePage/>} />
           <Route exact path="/machinery" element={<MachineryPage/>} />
           <Route exact path="/machinery/create" element={<CreateMachineryPage/>} />


           <Route
             path="/secured"
             element={
               <PrivateRoute>
                 <SecuredPage />
               </PrivateRoute>
             }
           />
              <Route path="/unauthorized" element={<UnauthorizedPage />} /> 
              <Route path="/forbidden" element={<ForbiddenPage />} /> 

           
         </Routes>
       </BrowserRouter>

       <ToastContainer />
  </ThemeProvider >
  </CartProvider>
   </div>
 );
}

export default App;