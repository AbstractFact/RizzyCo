import React, { Component } from "react";

class DynamicSelect extends Component {
  constructor() {
    super();
    this.onChangeMap = this.onChangeMap.bind(this);
  }

  onChangeMap(event) {
    console.log(event.target.value);
  }

  render() {
    const { maps } = this.props
    return (
      <div>
        <div>
          <span>Select map</span> :
          {maps && maps.length > 0 && (
            <div>
              <select onChange={this.onChangeMap}>
                {(JSON.parse(maps)).map((m, index) => {
                  return <option key={m.id}>{m.name}</option>;
                })}
              </select>
            </div>
          )}
        </div>
      </div>
    );
  }
}

export default DynamicSelect;