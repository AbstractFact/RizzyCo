import React, { Component } from "react";

class AttackTerritorySelect extends Component {
  constructor(props) {
    super(props);
    this.state = {
      attackTerritories : this.props.dataParentToChild,
      targetTerritories : []
    };

    this.onChangeAttackTerritory = this.onChangeAttackTerritory.bind(this);
    this.onChangeTargetTerritory = this.onChangeTargetTerritory.bind(this);
    this.getNeighbours = this.getNeighbours.bind(this);
    localStorage.attackFromTerritory = 0;
    localStorage.attackFromTerritoryName = "";
    localStorage.attackTargetTerritory = 0;
  }

  async onChangeAttackTerritory(event) {
    localStorage.attackFromTerritory = parseInt(event.target.value);
    localStorage.attackFromTerritoryName = event.target[event.target.selectedIndex].text;
    await this.getNeighbours();
    this.setState((state) => {
      return {targetTerritories : JSON.parse(localStorage.getItem("targetTerritories"))}
    });
    
  }

  onChangeTargetTerritory(event) {
    localStorage.attackTargetTerritory = parseInt(event.target.value);
  }

  async getNeighbours(){
    const res = await fetch("https://localhost:44348/api/Neighbour/GetTargetTerritories/"+(JSON.parse(localStorage.getItem("playerInfo"))).playerID + "/" + localStorage.attackFromTerritory + "/" + localStorage.gameID, { method: "GET"})
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
            localStorage.setItem("targetTerritories", JSON.stringify(array)) 
        } else {
            console.log(res.message);
        }  
  }

  render() {
    return (
      <div>
        <div>
          <span>Attack From: </span> 
          {this.state.attackTerritories && this.state.attackTerritories.length > 0 && (
            <div>
              <select onChange={this.onChangeAttackTerritory}>
              <option key = "default" value={0}>Select Territory</option>
                {JSON.parse(localStorage.getItem("playerTerritories")).filter(el=>el.numArmies >= 2).map((m, index) => {
                    return <option key={m.territoryID} value={m.territoryID}>{m.territoryName}</option>;
                })}
              </select>
              <select id="attackNumDice" disabled={(parseInt(localStorage.attackFromTerritory) === 0) ? true : null}>
                  <option key = "1" value={1} default>1</option>
                  <option key = "2" value={2}>2</option>
                  <option key = "3" value={3}>3</option>
              </select>
            </div>
          )}
          <span>Attack Target: </span> 
          {this.state.targetTerritories && (
            <div>
              <select onChange={this.onChangeTargetTerritory} disabled={(parseInt(localStorage.attackFromTerritory) === 0) ? true : null}>
              <option key = "default" value={0}>Select Territory</option>
                {
                  this.state.targetTerritories.map((m, index) => {
                  return <option key={m.territoryID} value={m.territoryID}>{m.territoryName}</option>;
                })}
              </select>
            </div>
          )}
        </div>
      </div>
    );
  }
}

export default AttackTerritorySelect;