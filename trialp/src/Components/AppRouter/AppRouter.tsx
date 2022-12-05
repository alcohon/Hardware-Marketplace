import React from 'react';
import {
    Route,
    Router,
    Link,
    BrowserRouter,
    Routes,
    Outlet,
    Navigate
} from "react-router-dom";

import Login from './../../Components/Auth/Login/Login';
import Register from './../../Components/Auth/Register/Register';
import Profile from './../../Components/Auth/Profile/Profile';
import { Home } from './../../Components/Home/Home';
import Layout from '../Layout/Layout';
import NotFound from '../NotFound/NotFound';
import Products from '../Products/Products';
import Product from '../Product/Product';
import { useSelector } from 'react-redux';
import Cart from '../Cart/Cart';


function RequireAuth({ children, redirectTo }: any) {
    const { isLoggedIn } = useSelector((state: any) => state.auth);
    return isLoggedIn ? children : <Navigate to={redirectTo} />;
}

function AppRouter() {
  return (
      <Routes>
          <Route path="/" element={<Layout />}>
              <Route index element={<Home />} />
              <Route path="/login" element={<Login/>} />
              <Route path="/register" element={<Register />} />
              
              <Route path="/profile" element={
                  <RequireAuth redirectTo="/">
                      <Profile />
                  </RequireAuth>
              }/>

              <Route path="/cart" element={
                  <RequireAuth redirectTo="/">
                      <Cart />
                  </RequireAuth>
              }/>

              <Route path="/products" element={<Products />} />
              <Route path="/products/:subsubcategory" element={<Products />} />
              <Route path="/product/:key" element={<Product />} />
              <Route path='*' element={<NotFound />} />
          </Route>
      </Routes>
  );
}

export default AppRouter;