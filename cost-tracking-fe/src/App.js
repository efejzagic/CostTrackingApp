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


function App() {



 return (
   <div>

       <BrowserRouter>

        <Routes>
        <Route path="/" element={<PrivateRoute><WelcomePage /> </PrivateRoute>} />

        {/* <Route path="/" element={<PrivateRoute element={<WelcomePage />} />} /> */}

           {/* <Route path="/" element={<WelcomePage />} /> */}
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
           <Route exact path="/expense" element={<ExpensePage/>} />
           <Route exact path="/expense/create" element={<CreateExpensePage/>} />

           <Route exact path="/article" element={<ArticlePage/>} />
           <Route exact path="/article/create" element={<CreateArticlePage/>} />
           <Route exact path="/supplier/:id/articles" element={<SupplierArticles/>} />
           <Route exact path="/profile" element={<ProfilePage/>} />

           <Route
             path="/secured"
             element={
               <PrivateRoute>
                 <SecuredPage />
               </PrivateRoute>
             }
           />
              <Route path="/unauthorized" element={<UnauthorizedPage />} /> {/* Add this route */}

           
         </Routes>
       </BrowserRouter>

       <ToastContainer />

   </div>
 );
}

export default App;