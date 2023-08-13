import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Nav from "./components/Nav/Nav";
import WelcomePage from "./pages/Homepage";
import SecuredPage from "./pages/Securedpage";
import PrivateRoute from "./helpers/PrivateRoute";
import LoginPage from "./pages/LoginPage"
import { ToastContainer } from 'react-toastify';

function App() {
 return (
   <div>
       <BrowserRouter>
         <Routes>
           <Route exact path="/" element={<WelcomePage />} />
           <Route exact path="/test" element={<LoginPage />} />
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