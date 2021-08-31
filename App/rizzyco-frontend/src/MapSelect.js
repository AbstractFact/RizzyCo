import React, { Component } from "react";

class MapSelect extends Component {
  constructor() {
    super();
    this.onChangeMap = this.onChangeMap.bind(this);
  }

  onChangeMap(event) {
    localStorage.mapID = parseInt(event.target.value);
  }

  render() {
    const { maps } = this.props
    return (
        <div className="mapDiv">
          <span>Select map: </span> 
          {maps && maps.length > 0 && (
            <div>
              <select onChange={this.onChangeMap}>
                {(JSON.parse(maps)).map((m, index) => {
                  if(index===0)
                    localStorage.mapID = m.id;
                  return <option key={m.id} value={m.id}>{m.name}</option>;
                })}
              </select>
            </div>
          )}
        </div>
    );
  }
}

export default MapSelect;