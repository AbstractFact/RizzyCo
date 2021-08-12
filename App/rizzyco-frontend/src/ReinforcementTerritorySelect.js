import React, { Component } from "react";

class ReinforcementTerritorySelect extends Component {
  constructor() {
    super();

    this.territories = JSON.parse(localStorage.getItem("playerTerritories"));
    this.onChangeTerritory = this.onChangeTerritory.bind(this);
    localStorage.selectedAddArmieTerritory = 0;
  }

  onChangeTerritory(event) {
    localStorage.selectedAddArmieTerritory = parseInt(event.target.value);

  }

  render() {
    return (
      <div>
        <div>
          <span>Select Territory: </span> 
          {this.territories && this.territories.length > 0 && (
            <div>
              <select onChange={this.onChangeTerritory} defaultValue={{ label: "Select Territory", value: 0 }}>
              <option key = "default" value={0}>Select Territory</option>
                {
                  this.territories.map((m, index) => {
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

export default ReinforcementTerritorySelect;