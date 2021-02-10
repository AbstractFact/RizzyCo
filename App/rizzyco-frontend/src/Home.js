import React from 'react';
import { v4 as uuidv4 } from 'uuid';

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
        <label>Active games:</label>
        {/* <GameList games={games} toggleGame={toggleGame} /> */}
    </>
   )
}

export default Home;
