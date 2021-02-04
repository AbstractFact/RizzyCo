import React, {useState, useRef, useEffect } from 'react';
import MsgList from './MsgList'

//const LOCAL_STORAGE_KEY = 'todoApp.todos'

function App (){
  // constructor(props) {
  //   super(props);
  //   this.state = {
  //     hits: [],
  //   };
  // }

  const CONTROLLER = 'https://localhost:44348/api/User'
  const [message, setMessage] = useState([])
  const msgNameRef = useRef()

  // useEffect(() => {
  //   const storedTodos = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY))
  //   if (storedTodos) setTodos(storedTodos)
  // }, [])

  // useEffect(() => {
  //   localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(todos))
  // }, [todos])

  // function toggleTodo(id) {
  //   const newTodos = [...todos]
  //   const todo = newTodos.find(todo => todo.id === id)
  //   todo.complete = !todo.complete
  //   setTodos(newTodos)
  // }

  function handleSend() {

    // const msg = msgNameRef.current.value
    // if (msg === '') return
    
    fetch(CONTROLLER + "/SendInvitation", {method:"POST", 
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify({ 
        "id": 14,
        "username": "Test",
        "password": "maremare",
        "email": "mare@gmail.com",
        "role": "User",
        "token": null })
    })
    .then(response => response.json())
    .then(data => this.setMessage(data))

    // setMessage(prevMessages => {
    //   return [...prevMessages, { msg: msg}]
    // })
    // msgNameRef.current.value = null
  }

  // function handleClearTodos() {
  //   const newTodos = todos.filter(todo => !todo.complete)
  //   setTodos(newTodos)
  // }
 
  function componentDidMount() {

  }

  return (
    <>
      <MsgList msgs={message} />
      <p>{message}</p>
      <input ref={msgNameRef} type="text" />
      <button onClick={handleSend}>Send</button>
      {/* <button onClick={handleClearTodos}>Clear Complete</button>
      <div>{todos.filter(todo => !todo.complete).length} left to do</div> */}
    </>
  )
}

export default App;
