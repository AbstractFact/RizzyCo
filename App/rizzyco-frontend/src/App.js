import React from 'react';
import Signup from "./Signup"
import Login from "./Login"
import Home from "./Home"
import FetchRabbitMQMassages from "./FetchRabbitMQMassages"
import './style/index.css'
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Redirect
} from "react-router-dom";
import { createStore, applyMiddleware, combineReducers, bindActionCreators } from 'redux';
import { Provider, connect }  from 'react-redux';
import { actionCreators, reducer } from './RabbitMQ';

const ConnectedRoot = connect(
  (state) => ({
    state: state.rabbitMQMessages
  }),
  (dispatch) => ({
    dispatch: bindActionCreators(actionCreators, dispatch)
  })
)(FetchRabbitMQMassages);


// const reducer = combineReducers(reducer);
const store = createStore(reducer);
function App (){
   return (
    <Router>
        <Switch>
        <Route exact path="/" component={()=>(<Redirect to="/signup" />)}>
        </Route>
        <Route path="/home">
          <Home />
        </Route>
        <Route path="/login">
          <Login />
        </Route>
        <Route path="/signup">
          <Signup />
        </Route>
        <Route path="/msg">
          {/* <Provider store={store}>
            <ConnectedRoot />
          </Provider> */}
          <FetchRabbitMQMassages store={store}/>
        </Route>
        </Switch>
    </Router>
   )
}

export default App;
