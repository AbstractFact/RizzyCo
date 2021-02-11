import React, { Component } from "react";

class AttackTerritorySelect extends Component {
  constructor() {
    super();

    this.territories = JSON.parse(localStorage.getItem("playerTerritories"));
    this.onChangeTerritory = this.onChangeTerritory.bind(this);

  }

  onChangeTerritory(event) {
    localStorage.addReinforcementsTerritory = parseInt(event.target.value);

  }

//   async getNeighbours(){
//     const res = await fetch("https://localhost:44348/api/Neighbour/GetTerritoryNeighbours/"+localStorage.addReinforcementsTerritory, { method: "GET"})
//         if (res.ok) {
//             var array = [];
//             const d = await res.json()
//             d.forEach(element => {
//                 var entry = {
//                     territoryID: element.territoryID,
//                     territoryName: element.territoryName,
//                     numArmies : element.numArmies
//                 };
//                 array.push(entry);
//             }); 
//             localStorage.setItem("playerTerritories", JSON.stringify(array)) 
//         } else {
//             console.log(res.message);
//         }  
//   }

  render() {
    return (
      <div>
        <div>
          <span>Select Territory: </span> 
          {this.territories && this.territories.length > 0 && (
            <div>
              <select onChange={this.onChangeTerritory}>
                {this.territories.map((m, index) => {
                  if(index===0)
                    localStorage.addReinforcementsTerritory = m.territoryID;
                  return <option key={m.territoryID} value={m.territoryID}>{m.territoryName}({m.numArmies})</option>;
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