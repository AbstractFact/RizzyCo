import React, { Component } from "react";
import ReinforcementTerritorySelect from "./ReinforcementTerritorySelect";
import AttackTerritorySelect from "./AttackTerritorySelect";
import TargetTerritorySelect from "./TargetTerritorySelect";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';


export default class Game extends Component {
  constructor() {
    super();

    this.state = JSON.parse(localStorage.getItem("playerInfo"));
    this.connection = null;
    this.sendJoinGameMessage = this.sendJoinGameMessage.bind(this);
    this.onAddArmie = this.onAddArmie.bind(this);
    this.onAttack = this.onAttack.bind(this);
    this.onDeffend = this.onDeffend.bind(this);    
  }

  async sendJoinGameMessage () {
    if (this.connection.connectionStarted) {
        try {
            await this.connection.invoke('JoinGameGroup', localStorage.lobbyID, localStorage.gameID);
        }
        catch(e) {
            console.log(e);
        }
    }
    else {
        alert('No connection to server yet.');
    }
  }
  
  async componentDidMount(){
    this.connection = new HubConnectionBuilder()
          .withUrl('https://localhost:44348/RizzyCoHub')
          .configureLogging(LogLevel.Debug)
          .build();
    await this.connection.start()
    .then(async result => 
    {
      console.info('SignalR Connected');
      await this.sendJoinGameMessage();
      this.connection.on('PlayerAddArmie', message => {
        console.log(message);
      
      });
    })
    .catch(err => console.error('SignalR Connection Error: ', err));
  }
   
  async onAddArmie(){
    const res =  await fetch("https://localhost:44348/api/PlayerTerritory/AddArmie/"+localStorage.gameID+"/"+(JSON.parse(localStorage.getItem("playerInfo"))).playerID+"/"+localStorage.selectedAddArmieTerritory, { method: "POST"}); 
    if (res.ok) {
      alert("Armie added!");
    }
    else {
        this.setState({
            errors: { message: res.message }
        });
    }
  }

  onAttack(){

  }

  onDeffend(){

  }



  render() {
    return (
        <>
        <br />
        <br />
        <h1>WELCOME TO THE RISK GAME</h1>
        <br />
        <br />
        <label>Player color: </label>
        <label>{this.state.playerColor}</label>
        <br />
        <br />
        <label>Mission: </label>
        <label>{this.state.mission}</label>
        <br />
        <br />
        <label>Available armies: </label>
        <label>{this.state.availableArmies}</label>
        <br />
        <br />
        <label>On turn: </label>
        <label>Player</label>
        <br />
        <br />
        <h4>REINFORCEMENT</h4>
        <ReinforcementTerritorySelect />
        <button onClick={this.onAddArmie}>Add armie</button>
        <br />
        <br />
        <h4>ATTACK</h4>
        <AttackTerritorySelect />
        <TargetTerritorySelect />
        <button onClick={this.onAttack}>Attack</button>
        <br />
        <br />
        <h4>DEFENSE</h4>
        <label>Territory</label>
        <br />
        <button onClick={this.onDeffend}>Deffend</button>
        <br />
        <br />
        <button >END MOVE</button>  
    </>
    );
  }
}
