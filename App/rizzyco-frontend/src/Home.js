import React from 'react';
import { v4 as uuidv4 } from 'uuid';
import GameSelect from "./GameSelect";

function Home (){
    function handleCreateLobby() {
        localStorage.lobbyID = "http://localhost:3000/lobby/"+uuidv4();
        window.location.href="/lobby";
    }

   return (
    <>
        <br />
        <button onClick={handleCreateLobby}>Create lobby</button>
        <br />
        <br />
        <GameSelect />
    </>
   )
}

export default Home;
