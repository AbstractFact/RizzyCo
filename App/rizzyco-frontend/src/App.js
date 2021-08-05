import React from 'react';
import Signup from "./Signup"
import Login from "./Login"
import Lobby from "./Lobby"
import Home from "./Home"
import Game from "./Game"
import WaitingLobby from "./WaitingLobby"
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
        <Route exact path="/" component={()=>(<Redirect to="/login" />)}>
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
        <Route path="/game">
          <Game />
        </Route>
        <Route path="/waitingLobby">
          <WaitingLobby />
        </Route>
        </Switch>
    </Router>
   )
}

export default App;
