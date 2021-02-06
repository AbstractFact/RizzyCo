import React, {useState, useRef, useEffect } from 'react';
import MsgList from './MsgList'
import Signup from "./Signup"
import Login from "./Login"
import Home from "./Home"
import './style/index.css'
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect
} from "react-router-dom";
//const LOCAL_STORAGE_KEY = 'todoApp.todos'

function App (){
 
  // // constructor(props) {
  // //   super(props);
  // //   this.state = {
  // //     hits: [],
  // //   };
  // // }

  // const CONTROLLER = 'https://localhost:44348/api/User'
  // const [message, setMessage] = useState([])
  // const msgNameRef = useRef()

  // // useEffect(() => {
  // //   const storedTodos = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY))
  // //   if (storedTodos) setTodos(storedTodos)
  // // }, [])

  // // useEffect(() => {
  // //   localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(todos))
  // // }, [todos])

  // // function toggleTodo(id) {
  // //   const newTodos = [...todos]
  // //   const todo = newTodos.find(todo => todo.id === id)
  // //   todo.complete = !todo.complete
  // //   setTodos(newTodos)
  // // }

  

  // // function handleClearTodos() {
  // //   const newTodos = todos.filter(todo => !todo.complete)
  // //   setTodos(newTodos)
  // // }
 
  // function componentDidMount() {

  // }

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
        </Switch>
    </Router>
  //   <>
  //     <MsgList msgs={message} />
  //     <p>{message}</p>
  //     <input ref={msgNameRef} type="text" />
  //     <button onClick={handleSend}>Send</button>
  //     {/* <button onClick={handleClearTodos}>Clear Complete</button>
  //     <div>{todos.filter(todo => !todo.complete).length} left to do</div> */}
  //   </>
   )
}

export default App;
