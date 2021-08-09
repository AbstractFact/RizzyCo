import React, { Component } from "react";
import ReinforcementTerritorySelect from "./ReinforcementTerritorySelect";
import AttackTerritorySelect from "./AttackTerritorySelect";
import { HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
import "./style/style.css";


export default class Game extends Component {
  constructor() {
    super();

    this.state = 
    {
      playerInfo:JSON.parse(localStorage.getItem("playerInfo")),
      allTerritories:JSON.parse(localStorage.getItem("allTerritories"))
    }
    this.connection = null;
    this.sendJoinGameMessage = this.sendJoinGameMessage.bind(this);
    this.onAddArmie = this.onAddArmie.bind(this);
    this.onAttack = this.onAttack.bind(this);
    this.onDeffend = this.onDeffend.bind(this);  
    this.handleLogOut = this.handleLogOut.bind(this);
    this.handleGameStopped = this.handleGameStopped.bind(this); 
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
        var territories = JSON.parse(localStorage.getItem("allTerritories"));
        territories.forEach(el => {if (el.territoryID===message.territoryID) {el.numArmies=message.numArmies; return;}});
        localStorage.setItem("allTerritories", JSON.stringify(territories));
        this.setState(
            {
              playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
              allTerritories:JSON.parse(localStorage.getItem("allTerritories"))
            });
        JSON.parse(localStorage.getItem("allTerritories")).forEach(element => {
          let el = document.getElementById(element.territoryID);
          el.style.backgroundColor=element.playerColor;
          el.querySelector('span').innerHTML=element.numArmies;
        });
      });

      this.connection.on('PlayerLeft', async message => {
        alert(message + " left the game");
        await this.handleGameStopped();
        window.location.href="/home";
      
      });
    })
    .catch(err => console.error('SignalR Connection Error: ', err));

    JSON.parse(localStorage.getItem("allTerritories")).forEach(element => {
      let el = document.getElementById(element.territoryID);
      el.style.backgroundColor=element.playerColor;
      el.querySelector('span').innerHTML=element.numArmies;
    });
  }
   
  async onAddArmie(){
    if(parseInt(localStorage.selectedAddArmieTerritory) === 0)
    {
      alert("Please select territory.");
      return;
    }
    const res =  await fetch("https://localhost:44348/api/PlayerTerritory/AddArmie/"+localStorage.gameID+"/"+(JSON.parse(localStorage.getItem("playerInfo"))).playerID+"/"+localStorage.selectedAddArmieTerritory, { method: "POST"}); 
    if (res.ok) {
      alert("Armie added!");

      var tmp = JSON.parse(localStorage.getItem("playerInfo"));
      tmp.availableArmies=tmp.availableArmies-1;
      localStorage.setItem("playerInfo", JSON.stringify(tmp));
      this.setState( 
        {
          playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
          allTerritories:JSON.parse(localStorage.getItem("allTerritories"))
        }
      );
      
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



  async handleGameStopped() {
    if (this.connection.connectionStarted) {
        try {
            await this.connection.send('LeaveInterruptedGameGroup', parseInt(localStorage.gameID));
        }
        catch(e) {
            console.log(e);
        }
    }
    else {
        alert('No connection to server yet.');
    }
}

  async handleLogOut() {
    if (this.connection.connectionStarted) {
        try {
            await this.connection.send('LeaveGameGroup', parseInt(localStorage.gameID), localStorage.username);
        }
        catch(e) {
            console.log(e);
        }
    }
    else {
        alert('No connection to server yet.');
    }
    localStorage.clear();
    localStorage.setItem("redirect", null);
    window.location.href="/login";
}

  render() {
    return (
        <>
        <div>
          <br />
          <div className = "logOutDiv">
          <label>{localStorage.username}</label>
          <br />
          <button onClick={this.handleLogOut}>Log out</button>
          <br />
          </div>
          <h1>WELCOME TO THE RISK GAME</h1>
          <br />
          <br />
          <div className="HUDDiv">
          <div>
            <h4>GAME INFO</h4>
            <label>Participants: </label>
            <div>
            { JSON.parse(localStorage.getItem("gameParticipants")).map((m, index) => 
            {return <div key={m.username}><label style={{color: m.playerColor, textDecorationLine: m.onTurn ? "underline" : "none"}}>{m.username}</label> </div>})}
            </div>
            <br />
            <label>My Mission: </label>
            <label>{this.state.playerInfo.mission}</label>
            <br />
            <br />
            <label>Available armies: </label>
            <label>{this.state.playerInfo.availableArmies}</label>
          </div>
          <div>
            <h4>REINFORCEMENT</h4>
            <ReinforcementTerritorySelect />
            <button onClick={this.onAddArmie}>Add armie</button>
          </div>
          <div>
            <h4>ATTACK</h4>
            <AttackTerritorySelect />
            <button onClick={this.onAttack}>Attack</button>
          </div>
          <div>
            <h4>DEFENSE</h4>
            <label>Territory</label>
            <br/>
            <button onClick={this.onDeffend}>Deffend</button>
          </div>
          <div>
            <button >END MOVE</button> 
          </div>
          </div>
        </div>
        <div className="wrapper">
            <div id = "34" className="box" style={{position:"absolute", left:"8%", top:"15%"}}><span></span></div>
            <div id = "35" className="box" style={{position:"absolute", left:"20%", top:"14%"}}><span></span></div>
            <div id = "37" className="box" style={{position:"absolute", left:"15%", top:"23%"}}><span></span></div>
            <div id = "40" className="box" style={{position:"absolute", left:"15%", top:"35%"}}><span></span></div>
            <div id = "42" className="box" style={{position:"absolute", left:"18%", top:"47%"}}><span></span></div>
            <div id = "41" className="box" style={{position:"absolute", left:"23%", top:"39%"}}><span></span></div>
            <div id = "38" className="box" style={{position:"absolute", left:"20%", top:"25%"}}><span></span></div>
            <div id = "39" className="box" style={{position:"absolute", left:"27%", top:"25%"}}><span></span></div>
            <div id = "36" className="box" style={{position:"absolute", left:"34%", top:"12%"}}><span></span></div>
            <div id = "43" className="box" style={{position:"absolute", left:"21%", top:"56%"}}><span></span></div>
            <div id = "45" className="box" style={{position:"absolute", left:"25%", top:"65%"}}><span></span></div>
            <div id = "44" className="box" style={{position:"absolute", left:"31%", top:"65%"}}><span></span></div>
            <div id = "46" className="box" style={{position:"absolute", left:"24%", top:"80%"}}><span></span></div>
            <div id = "9" className="box" style={{position:"absolute", left:"41.7%", top:"21%"}}><span></span></div>
            <div id = "11" className="box" style={{position:"absolute", left:"42%", top:"31%"}}><span></span></div>
            <div id = "14" className="box" style={{position:"absolute", left:"42%", top:"46%"}}><span></span></div>
            <div id = "10" className="box" style={{position:"absolute", left:"49%", top:"23%"}}><span></span></div>
            <div id = "12" className="box" style={{position:"absolute", left:"47%", top:"34%"}}><span></span></div>
            <div id = "15" className="box" style={{position:"absolute", left:"51%", top:"44%"}}><span></span></div>
            <div id = "13" className="box" style={{position:"absolute", left:"57%", top:"28%"}}><span></span></div>
            <div id = "17" className="box" style={{position:"absolute", left:"52%", top:"56%"}}><span></span></div>
            <div id = "16" className="box" style={{position:"absolute", left:"45%", top:"55%"}}><span></span></div>
            <div id = "18" className="box" style={{position:"absolute", left:"55%", top:"72%"}}><span></span></div>
            <div id = "19" className="box" style={{position:"absolute", left:"59%", top:"71%"}}><span></span></div>
            <div id = "20" className="box" style={{position:"absolute", left:"55%", top:"85%"}}><span></span></div>
            <div id = "21" className="box" style={{position:"absolute", left:"61%", top:"81%"}}><span></span></div>
            <div id = "28" className="box" style={{position:"absolute", left:"65%", top:"36%"}}><span></span></div>
            <div id = "31" className="box" style={{position:"absolute", left:"60%", top:"51%"}}><span></span></div>
            <div id = "32" className="box" style={{position:"absolute", left:"72%", top:"51%"}}><span></span></div>
            <div id = "33" className="box" style={{position:"absolute", left:"80%", top:"55%"}}><span></span></div>
            <div id = "29" className="box" style={{position:"absolute", left:"80%", top:"44%"}}><span></span></div>
            <div id = "30" className="box" style={{position:"absolute", left:"80%", top:"35%"}}><span></span></div>
            <div id = "22" className="box" style={{position:"absolute", left:"68%", top:"23%"}}><span></span></div>
            <div id = "23" className="box" style={{position:"absolute", left:"73%", top:"22%"}}><span></span></div>
            <div id = "24" className="box" style={{position:"absolute", left:"77%", top:"25%"}}><span></span></div>
            <div id = "26" className="box" style={{position:"absolute", left:"80%", top:"13%"}}><span></span></div>
            <div id = "27" className="box" style={{position:"absolute", left:"86%", top:"10%"}}><span></span></div>
            <div id = "25" className="box" style={{position:"absolute", left:"90%", top:"27%"}}><span></span></div>
            <div id = "47" className="box" style={{position:"absolute", left:"80%", top:"71%"}}><span></span></div>
            <div id = "48" className="box" style={{position:"absolute", left:"89%", top:"68%"}}><span></span></div>
            <div id = "50" className="box" style={{position:"absolute", left:"94%", top:"84%"}}><span></span></div>
            <div id = "49" className="box" style={{position:"absolute", left:"90%", top:"84%"}}><span></span></div>
        </div>
    </>
    );
  }
}
