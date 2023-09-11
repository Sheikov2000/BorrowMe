import React, { useEffect, useState } from "react";
import { Spinner } from "reactstrap";
import { onLoginStatusChange, me } from "./modules/authManager";
import firebase from "firebase";
import ItemCard from "./components/itemCard/ItemCard";
import { BrowserRouter } from "react-router-dom";
import NavBar from "./components/NavBar";
import { ApplicationViews } from "./views/ApplicationViews";
import { createContext } from "react";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";


export let userContext = null;

function App() {
  const [isLoggedIn, setIsLoggedIn] = useState(null);
  const [userProfile, setUserProfile] = useState(null);

useEffect(() => {
  userContext = createContext(userProfile);
}, [userProfile]);
  
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
    // Until we know whether is logged in or not, just show a spinner
    return <Spinner className="app-spinner dark" />;
  }

  return (
    <div className="App">
      <BrowserRouter>
      <userContext.Provider value={userProfile}>
        <ApplicationViews isLoggedIn={isLoggedIn} />
      </userContext.Provider>
      </BrowserRouter>
    </div>
  );
}

export default App;