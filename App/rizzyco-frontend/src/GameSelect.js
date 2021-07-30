import React, { Component } from "react";

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
                creationDate: element.creationDate,
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
  
  async onContinueGame() {
    console.log("pozvala se");
  }

  render() {
    return (
      <div>
        <div>
          <h2>Game History: </h2> 
          {this.state.games && (
            <div>
                {
                  this.state.games.map((m, index) => {
                  return <div>
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
                            {return <div><label style={{color: m.playerColor}}>{m.username}</label><br/></div>})}
                            <br/><br/>
                            <button onClick={this.onContinueGame}>Continue Game</button> 
                            <br/><br/><br/>
                         </div>;
                })}
            </div>
          )}
        </div>
      </div>
    );
  }
}

export default GameSelect;