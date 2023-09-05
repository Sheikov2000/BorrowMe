import React, { useEffect, useState } from "react";
import { Spinner } from "reactstrap";
import { onLoginStatusChange, me } from "./modules/authManager";
import firebase from "firebase";
import ItemCard from "./components/itemCard/ItemCard";
import { BrowserRouter } from "react-router-dom";
import NavBar from "./components/NavBar";
import { ApplicationViews } from "./views/ApplicationViews";

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(null);
  const [userProfile, setUserProfile] = useState(null);

  useEffect(() => {
    onLoginStatusChange(setIsLoggedIn);
  }, []);

  useEffect(() => {
    if (isLoggedIn) {
      me().then(setUserProfile);
    } else {
      setUserProfile(null);
    }
  }, [isLoggedIn]);

  if (isLoggedIn === null) {
    // Until we know whether or not the user is logged in or not, just show a spinner
    return <Spinner className="app-spinner dark" />;
  }

  return (
    <div className="App">
      <BrowserRouter>
      <ApplicationViews isLoggedIn={isLoggedIn}/>
      </BrowserRouter>
    </div>
  );
}

export default App;