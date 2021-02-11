import React, { Component } from "react";

class TargetTerritorySelect extends Component {
  constructor() {
    super();

    this.territories = JSON.parse(localStorage.getItem("allTerritories"));
    this.onChangeTerritory = this.onChangeTerritory.bind(this);

  }

  onChangeTerritory(event) {
    localStorage.addArmieToTerritory = parseInt(event.target.value);

  }

  render() {
    return (
      <div>
        <div>
          <span>Target Territory: </span> 
          {this.territories && this.territories.length > 0 && (
            <div>
              <select onChange={this.onChangeTerritory}>
                {this.territories.map((m, index) => {
                  if(index===0)
                    localStorage.addArmieToTerritory = m.territoryID;
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

export default TargetTerritorySelect;