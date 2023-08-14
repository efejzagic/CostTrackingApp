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

function App() {
 return (
   <div>
       <BrowserRouter>
         <Routes>
           <Route exact path="/" element={<WelcomePage />} />
           <Route exact path="/login" element={<LoginPage />} />
           <Route exact path="/users" element={<AdminUsers />} />
           <Route exact path="/supplier" element={<SupplierPage />} />
           <Route exact path="/supplier/create" element={<CreateSupplierPage/>} />
           <Route exact path="/supplier/edit/:id" element={<EditSupplierPage/>} />
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