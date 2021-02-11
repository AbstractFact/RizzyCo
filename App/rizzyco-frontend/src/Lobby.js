import React, {useState, useEffect } from 'react';
import PlayerList from './PlayerList'
import DynamicSelect from "./DynamicSelect";

import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';

const LOCAL_STORAGE_KEY = 'home.players'

function Lobby (){

    if(!localStorage.token)
    {
        localStorage.setItem("redirect", window.location.href);
        window.location.href="/login";
    }
    else
        localStorage.setItem("redirect", "");

    if(!localStorage.lobbyID)
            localStorage.lobbyID=window.location.href;

    const [ connection, setConnection ] = useState(null);
    const [players, setPlayers] = useState([])

    useEffect(() => {
        const storedPlayers = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY))
        if (storedPlayers) setPlayers(storedPlayers)
    }, [])

    useEffect(() => {
        localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(players))
    }, [players])

    useEffect(() => {
        const newConnection = new HubConnectionBuilder()
            .withUrl('https://localhost:44348/RizzyCoHub')
            .configureLogging(LogLevel.Debug)
            .build()

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(async result =>  {
                    console.log('Connected!');
                    await sendLobbyMessage(localStorage.lobbyID, localStorage.username);
                    connection.on('ReceiveLobbyPlayerAdd', message => {
                        setPlayers([]);
                        message.forEach(element => {
                            handleAddPlayer(element);
                        });
                        
                    });

                    connection.on('ReceiveGameStarted', message => {
                        localStorage.gameID=message;
                        console.log(localStorage.gameID);
                        window.location.href="/game";
                    });

                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    const sendLobbyMessage = async (lobbyID, username) => {
        console.log("uso u send msg");
        const msg = {
            lobbyID: lobbyID,
            username: username
        };
        
        if (connection.connectionStarted) {
            try {
                await connection.send('JoinLobbyGroup', msg);
            }
            catch(e) {
                console.log(e);
            }
        }
        else {
            alert('No connection to server yet.');
        }
    }

    const sendCreateGameMessage = async (lobbyID, gameID) => {
        console.log("uso u send msg");
        const msg = {
            lobbyID: lobbyID,
            gameID: gameID
        };
        
        if (connection.connectionStarted) {
            try {
                await connection.send('JoinGameGroup', msg);
            }
            catch(e) {
                console.log(e);
            }
        }
        else {
            alert('No connection to server yet.');
        }
    }

    function togglePlayer(id) {
        const newPlayers = [...players]
        const player = newPlayers.find(player => player.id === id)
        player.complete = !player.complete
        setPlayers(newPlayers)
    }

    const handleAddPlayer = (addUsername) =>{
        const username = addUsername
        if (username === '') return
        if (players.filter(player => player.complete===true).length===5)
        {
            alert("Party is full.")
            window.location.href="/home"
            return
        }
        if (players.filter(player => player.username===username).length!==0)
        {
            return
        }
        setPlayers(prevPlayers => {
        return [...prevPlayers, { id: username, username: username, complete: true}]
        })
    }

    function handleClearPlayers() {
        const newPlayers = players.filter(player => !player.complete)
        setPlayers(newPlayers)
    }

   
    function handleCreateGame() {
        const usernames=new Array()
        players.filter(player => player.complete===true).forEach(element => {
           usernames.push(element.username);
        });

        var msg = {
            "users" :usernames,
            "creatorID" : parseInt(localStorage.userID),
            "mapID" : parseInt(localStorage.mapID),
            "lobbyID" : localStorage.lobbyID
        }

        fetch("https://localhost:44348/api/User/CreateGame", { method: "POST",
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify(msg)
        }).then(res => {
            if (res.ok) {
                res.json().then(async result=>{
                    await sendCreateGameMessage(localStorage.lobbyID, result);
                    alert("Game created");
                });

            } else {
                this.setState({
                    errors: { message: res.message }
                });
            }
        })
        .catch(err => {
            console.log("Create game error: ", err);
        });
    }

    if(!localStorage.token)
    {
        localStorage.setItem("redirect", window.location.href);
        window.location.href="/login";
        return ;
    }
    else
    {
        localStorage.setItem("redirect", "");
        if(!localStorage.lobbyID)
                localStorage.lobbyID=window.location.href;
        return (
            <>
                <DynamicSelect maps={localStorage.getItem("allMaps")}/>
                <br />
                <label>Players:</label>
                <PlayerList players={players} togglePlayer={togglePlayer} />
                <button onClick={handleClearPlayers}>Clear Players</button>
                <br />
                <p>{players.filter(player => player.complete).length} players invited</p>
                <br />
                <button onClick={handleCreateGame}>Create game</button>
                <br />
                <br />
                <a href={localStorage.lobbyID}>{localStorage.lobbyID}</a>
            </>
            )
    }
       
}

export default Lobby;
