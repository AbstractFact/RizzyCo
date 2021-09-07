import React from 'react';
import { v4 as uuidv4 } from 'uuid';
import GameSelect from "./GameSelect";
import "./style/style.css";

function Home (){
    function handleCreateLobby() {
        localStorage.lobbyID = "http://localhost:3000/lobby/"+uuidv4();
        window.location.href="/lobby";
    }

    function handleLogOut() {
        localStorage.clear();
        window.location.href="/login";
    }

    return (
        <>
        <div className="homeContainer"> 
            <div className="navDiv">
                <div>
                    <a href="/home"><img className="logoImg" src="http://127.0.0.1:10000/devstoreaccount1/rizzyco-container/Logo.png"alt="Home"/></a>
                </div>
                <div className="logoutDiv">
                    <label>{localStorage.username}</label>
                    <button className="logoutBtn" onClick={handleLogOut} >Log out</button>
                    <br />
                </div>
            </div>
            <button className="createLobbyBtn" onClick={handleCreateLobby}>Create lobby</button>
            <GameSelect />
        </div>
        </>
    )
}

export default Home;
