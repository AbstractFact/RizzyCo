import React from 'react';
import { v4 as uuidv4 } from 'uuid';
import GameSelect from "./GameSelect";

function Home (){
    function handleCreateLobby() {
        localStorage.lobbyID = "http://localhost:3000/lobby/"+uuidv4();
        window.location.href="/lobby";
    }

    function handleLogOut() {
        localStorage.clear();
        localStorage.setItem("redirect", null);
        window.location.href="/login";
    }

   return (
    <>
        <br />
        <button onClick={handleCreateLobby}>Create lobby</button>
        <label style={{float: "right"}}>{localStorage.username}</label>
        <br />
        <button style={{float: "right"}} onClick={handleLogOut}>Log out</button>
        <br />
        <br />
        <GameSelect />
    </>
   )
}

export default Home;
