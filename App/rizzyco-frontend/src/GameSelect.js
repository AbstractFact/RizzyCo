import React, { Component } from "react";
import "./style/style.css";

class GameSelect extends Component {
  constructor() {
    super();

    this.state = {
      games : []
    };

    this.onContinueGame = this.onContinueGame.bind(this);
    this.getUserGames = this.getUserGames.bind(this);
    
  }

  async componentDidMount() {  
    await this.getUserGames();
  }

  async getUserGames(){
    const res = await fetch("https://localhost:44348/api/Player/GetUserGames/"+localStorage.userID, { method: "GET"})
    if (res.ok) {
        var array = [];
        const d = await res.json()
        d.forEach(element => {
            var entry = {
                gameID : element.gameID,
                creationDate: element.creationDate,
                finished: element.finished,
                mapID: element.mapID,
                participants: element.participants
            };
            array.push(entry);
        }); 
        localStorage.setItem("userGames", JSON.stringify(array));
        this.setState((state) => {
            return {games : JSON.parse(localStorage.getItem("userGames"))}
          });
    } else {
        console.log(res.message);
    }  
  }
  
  async onContinueGame(gameID, mapID) {
    localStorage.waitingLobbyID = gameID;
    localStorage.gameID = gameID;
    localStorage.mapID = mapID;
    window.location.href="/waitingLobby";
  }

  render() {
    return (
        <div className="gameSelectDiv">
          <h2 >Game History: </h2> 
          {this.state.games && (
            <div>
                {
                  this.state.games.map((m, index) => {
                  return <div key={m.gameID} className="homeGameDiv">
                            <h4>Game: {index+1}</h4>
                            <label>Started on: </label>
                            {new Intl.DateTimeFormat("en-GB", {
                                year: "numeric",
                                month: "2-digit",
                                day: "2-digit",
                                hour: '2-digit',
                                minute: '2-digit'
                              }).format(new Date(m.creationDate))}
                            <br/> 
                            <p>Participants:</p>
                            {m.participants.map((m, index) => 
                            {return <div key={m.username}><label style={{color: m.playerColor}}>{m.username}</label><br/></div>})}
                            <br/>
                            <button className="continueGameHomeBtn" onClick={() => this.onContinueGame(m.gameID, m.mapID)} disabled={m.finished ? true : null}>Continue Game</button> 
                            <br/>
                         </div>;
                })}
            </div>
          )}
        </div>
    );
  }
}

export default GameSelect;