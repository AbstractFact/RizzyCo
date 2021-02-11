import React, { Component } from "react";
import ReinforcementTerritorySelect from "./ReinforcementTerritorySelect";
import AttackTerritorySelect from "./AttackTerritorySelect";
import TargetTerritorySelect from "./TargetTerritorySelect";

export default class Game extends Component {
  constructor() {
    super();

    this.state = JSON.parse(localStorage.getItem("playerInfo"));
    
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
        <button>Add armie</button>
        <br />
        <br />
        <h4>ATTACK</h4>
        <AttackTerritorySelect />
        <TargetTerritorySelect />
        <button>Attack</button>
        <br />
        <br />
        <h4>DEFENSE</h4>
        <label>Territory</label>
        <br />
        <button onClick={this.getPlayerInfo}>Deffend</button>
        <br />
        <br />
        <button >END MOVE</button>  
    </>
    );
  }
}

