import React from "react";
import { Navigate, Outlet, Route, Routes } from "react-router-dom";
import { HomePage } from "../screens/HomePage";
import { AllItems } from "../screens/AllItems";
import { MyItems } from "../screens/MyItems";
import { CreateItem } from "../screens/CreateItem";
import { EditItem } from "../screens/EditItem";
import { ItemDetails } from "../screens/ItemDetails";
import Login from "../components/auth/Login";
import Register from "../components/auth/Register";

export const ApplicationViews = ({ isLoggedIn }) => {
  return (
    <Routes>
      <Route path="/">
        <Route
          index
          element={isLoggedIn ? <HomePage /> : <Navigate to="/login" />}
        />
        <Route path="login" element={<Login />} />
        <Route path="register" element={<Register />} />
        <Route
          path="/AllItems"
          element={isLoggedIn ? <AllItems /> : <Navigate to="/login" />}
        />
        <Route path="/MyItems">
          <Route
            index
            element={isLoggedIn ? <MyItems /> : <Navigate to="/login" />}
          />
          <Route
            path="Add"
            element={isLoggedIn ? <CreateItem /> : <Navigate to="/login" />}
          />
          <Route
            path="Edit/:id"
            element={isLoggedIn ? <EditItem /> : <Navigate to="/login" />}
          />
          <Route
            path=":id"
            element={isLoggedIn ? <ItemDetails /> : <Navigate to="/login" />}
          />
        </Route>
      </Route>
    </Routes>
  );
};









