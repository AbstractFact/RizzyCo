import React, {useState, useRef, useEffect } from 'react';
import PlayerList from './PlayerList'
import DynamicSelect from "./DynamicSelect";

const LOCAL_STORAGE_KEY = 'home.players'
const LOCAL_STORAGE_KEY1 = 'home.maps'

function Home (){
    const [players, setPlayers] = useState([])
    const [maps, setMaps] = useState([])
    const playerUsernameRef = useRef()

    function handleSend() {
        window.location.href="/msg";
    }

    useEffect(() => {
        const storedPlayers = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY))
        if (storedPlayers) setPlayers(storedPlayers)
    }, [])

    useEffect(() => {
        localStorage.setItem(LOCAL_STORAGE_KEY, JSON.stringify(players))
    }, [players])

    useEffect(() => {
        const storedMaps = JSON.parse(localStorage.getItem(LOCAL_STORAGE_KEY1))
        if (storedMaps) setMaps(storedMaps)
    }, [])

    useEffect(() => {
        localStorage.setItem(LOCAL_STORAGE_KEY1, JSON.stringify(maps))
    }, [maps])

    function togglePlayer(id) {
        const newPlayers = [...players]
        const player = newPlayers.find(player => player.id === id)
        player.complete = !player.complete
        setPlayers(newPlayers)
    }

    function handleAddPlayer(e) {
        const username = playerUsernameRef.current.value
        if (username === '') return
        if (players.filter(player => player.complete===true).length===5)
        {
            alert("You can invite up to 5 players")
            return
        }
        if (players.filter(player => player.username===username).length!=0)
        {
            playerUsernameRef.current.value = null
            alert("Player is already invited")
            return
        }
        setPlayers(prevPlayers => {
        return [...prevPlayers, { id: username, username: username, complete: true}]
        })
        playerUsernameRef.current.value = null
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
        fetch("https://localhost:44348/api/User/CreateGame/"+localStorage.userID+"/"+localStorage.mapID, { method: "POST",
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify(usernames)
        }).then(res => {
            if (res.ok) {
                alert("Game created");
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

    function handleAddMap(map) {
        setMaps(prevMaps => {
        return [...prevMaps, { id: map.id, name: map.name}]
        })
    }

   return (
    <>
        <DynamicSelect maps={localStorage.getItem("allMaps")}/>
        <br />
        <label>Players:</label>
        <PlayerList players={players} togglePlayer={togglePlayer} />
        <input ref={playerUsernameRef} type="text" />
        <button onClick={handleAddPlayer}>Add Player</button>
        <button onClick={handleClearPlayers}>Clear Players</button>
        <br />
        <p>{players.filter(player => player.complete).length} players invited</p>
        <br />
        <button onClick={handleCreateGame}>Create game</button>
        <button onClick={handleSend}>Test</button>
    </>
   )
}

export default Home;
