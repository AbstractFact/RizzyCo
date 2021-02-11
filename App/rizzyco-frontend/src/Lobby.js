import React, {useState, useEffect } from 'react';
import PlayerList from './PlayerList'
import MapSelect from "./MapSelect";

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

                    connection.on('ReceiveGameStarted', async message => {
                        localStorage.gameID=message;
                        await getPlayer();
                        await getPlayerTerritories();
                        await getAllTerritories();
                        window.location.href="/game";
                    });

                })
                .catch(e => console.log('Connection failed: ', e));
        }
    }, [connection]);

    const sendLobbyMessage = async (lobbyID, username) => {
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
                    localStorage.gameID = result;
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

    const getPlayer = async function getPlayerInfo (){
        const res =  await fetch("https://localhost:44348/api/Player/GetPlayerInfo/"+localStorage.gameID+"/"+localStorage.userID, { method: "GET"}); 
        if (res.ok) {
            const result = await res.json();

            var entity = {
                playerID:result.playerID,
                playerColor:result.playerColor,
                mission:result.mission,
                availableArmies:result.availableArmies
                }; 
            localStorage.setItem("playerInfo", JSON.stringify(entity)) 
        }
        else {
            this.setState({
                errors: { message: res.message }
            });
        }
    }

    const getPlayerTerritories = async function getPlayerTerritories(){
        const res = await fetch("https://localhost:44348/api/PlayerTerritory/GetPlayerTerritories/"+(JSON.parse(localStorage.getItem("playerInfo"))).playerID, { method: "GET"})
        if (res.ok) {
            var array = [];
            const d = await res.json()
            d.forEach(element => {
                var entry = {
                    territoryID: element.territoryID,
                    territoryName: element.territoryName,
                    numArmies : element.numArmies
                };
                array.push(entry);
            }); 
            localStorage.setItem("playerTerritories", JSON.stringify(array)) 
        } else {
            console.log(res.message);
        }  
    }

    const getAllTerritories = async function getAllTerritories(){
        const res = await fetch("https://localhost:44348/api/Game/GetGameTerritories/"+localStorage.gameID, { method: "GET"})
        if (res.ok) {
            var array = [];
            const d = await res.json()
            d.forEach(element => {
                var entry = {
                    territoryID: element.territoryID,
                    territoryName: element.territoryName,
                    numArmies : element.numArmies
                };
                array.push(entry);
            }); 
            localStorage.setItem("allTerritories", JSON.stringify(array)) 
        } else {
            console.log(res.message);
        }  
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
                <MapSelect maps={localStorage.getItem("allMaps")}/>
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
                <label>{localStorage.lobbyID}</label>
            </>
            )
    }
       
}

export default Lobby;
