import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router } from "react-router-dom";
import { Spinner } from 'reactstrap';
import Header from "./components/Header";
import ApplicationViews from "./components/ApplicationViews";
import { onLoginStatusChange } from "./modules/authManager";
import { me } from "./modules/authManager"

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
    return <Spinner className="app-spinner dark" />;
  }

  return (
    <Router>
      <Header isLoggedIn={isLoggedIn} profile={userProfile} />
      <ApplicationViews isLoggedIn={isLoggedIn} profile={userProfile} />
    </Router>
  );
}


export default App;

// import React, { useEffect, useState } from "react";
// import { BrowserRouter as Router } from "react-router-dom";
// import { Spinner } from "reactstrap";
// import { getUserDetails, onLoginStatusChange } from "./authManager";

// import ApplicationViews from "./components/ApplicationViews";
// function App() {
//   const [isLoggedIn, setIsLoggedIn] = useState(null);
//   useEffect(() => {
//     onLoginStatusChange(setIsLoggedIn);
//   }, []);
//   if (isLoggedIn === null) {
//     return <Spinner className="app-spinner dark" />;
//   }
//   return (
//     <Router>
//       {/* <Header isLoggedIn={isLoggedIn} /> */}
//       <ApplicationViews isLoggedIn={isLoggedIn} />
//     </Router>
//   );
// }
// export default App;