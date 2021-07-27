import { render } from "@testing-library/react";
import React, { Component } from "react";

class TargetTerritorySelect extends Component {
  constructor() {
    super();

    this.state = {
      count: 0
    };

    this.territories = JSON.parse(localStorage.getItem("targetTerritories"));
    this.onChangeTerritory = this.onChangeTerritory.bind(this);
    this.test = this.test.bind(this);

    document.addEventListener('attackFromTerritorySelected', function() {
      this.test();
    });
  }

  test ()
  {
    this.territories = JSON.parse(localStorage.getItem("targetTerritories"));
    this.setState((state) => {
      // Important: read `state` instead of `this.state` when updating.
      return {count: state.count + 1}
    });
    console.log("proslo");
  }

  componentDidMount()
  {
    this.test();
  }
  onChangeTerritory(event) {
    localStorage.attackTerritory = parseInt(event.target.value);

  }

  render() {
    localStorage.attackTerritory = 0;
    return (
      <div>
        <div>
          <span>Target Territory: </span> 
          {this.territories && (
            <div>
              <select onChange={this.onChangeTerritory} disabled={(parseInt(localStorage.selectedAddArmieTerritory) === 0) ? true : null}>
              <option key = "default" value={0}>Select Territory</option>
                {
                  this.territories.map((m, index) => {
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