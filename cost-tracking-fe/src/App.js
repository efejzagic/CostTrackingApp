import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Nav from "./components/Nav/Nav";
import WelcomePage from "./pages/Homepage";
import SecuredPage from "./pages/Securedpage";
import PrivateRoute from "./helpers/PrivateRoute";
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

function App() {

 return (
   <div>
    
       <BrowserRouter>
       <Nav/>
        <Routes>
          
           <Route path="/" element={<WelcomePage />} />
           <Route exact path="/login" element={<LoginPage />} />  
           <Route exact path="/users" element={<UsersPage />} />
           <Route exact path="/users/create" element={<CreateUserPage />} />
           <Route exact path="/supplier" element={<SupplierPage />} />
           <Route exact path="/supplier/create" element={<CreateSupplierPage/>} />
           <Route exact path="/supplier/edit/:id" element={<EditSupplierPage/>} />
           <Route exact path="/construction" element={<CSPage />} />
           <Route exact path="/construction/create" element={<CreateCSPage/>} />
           <Route exact path="/construction/edit/:id" element={<EditCSPage/>} />
           <Route exact path="/construction/:id/employees" element={<CSEmployeesPage/>} />
           <Route exact path="/employee" element={<EmployeePage />} />
           <Route exact path="/employee/create" element={<CreateEmployeePage/>} />
           <Route exact path="/employee/edit/:id" element={<EditEmployeePage/>} />
           <Route exact path="/invoice" element={<InvoicePage/>} />
           <Route exact path="/invoice/create" element={<CreateInvoicePage/>} />
           <Route
             path="/secured"
             element={
               <PrivateRoute>
                 <SecuredPage />
               </PrivateRoute>
             }
           />
           
         </Routes>
       </BrowserRouter>
       <ToastContainer />

   </div>
 );
}

export default App;