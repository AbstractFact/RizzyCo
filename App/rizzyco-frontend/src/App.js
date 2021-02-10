import React from 'react';
import Signup from "./Signup"
import Login from "./Login"
import Lobby from "./Lobby"
import Home from "./Home"
import Chat from "./Chat"
import './style/index.css'
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect
} from "react-router-dom";

function App (){
   return (
    <Router>
        <Switch>
        <Route exact path="/" component={()=>(<Redirect to="/signup" />)}>
        </Route>
        <Route path="/home">
          <Home />
        </Route>
        <Route path="/lobby">
          <Lobby />
        </Route>
        <Route path="/login">
          <Login />
        </Route>
        <Route path="/signup">
          <Signup />
        </Route>
        <Route path="/msg"> 
          <Chat />
        </Route>
        </Switch>
    </Router>
   )
}

export default App;
