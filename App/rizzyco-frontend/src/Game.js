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
      allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
      playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
      gameParticipants: JSON.parse(localStorage.getItem("gameParticipants")), 
    }
    this.connection = null;
    this.sendJoinGameMessage = this.sendJoinGameMessage.bind(this);
    this.onAddArmie = this.onAddArmie.bind(this);
    this.onAddReinforcement = this.onAddReinforcement.bind(this); 
    this.onAttack = this.onAttack.bind(this);
    this.onDeffend = this.onDeffend.bind(this);  
    this.handleLogOut = this.handleLogOut.bind(this);
    this.handleGameStopped = this.handleGameStopped.bind(this); 
    this.transferArmies = this.transferArmies.bind(this);
    this.endTurn = this.endTurn.bind(this);
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
      this.connection.on('PlayerAddArmie', async message => {
        var territories = JSON.parse(localStorage.getItem("allTerritories"));
        territories.forEach(el => {if (el.territoryID===message.territoryID) {el.numArmies=message.numArmies; return;}});
        localStorage.setItem("allTerritories", JSON.stringify(territories));

        var participants = JSON.parse(localStorage.getItem("gameParticipants"));
        participants.forEach(el => {if (el.username===message.nextPlayer) {el.onTurn=true; } else el.onTurn=false;});
        localStorage.setItem("gameParticipants", JSON.stringify(participants));

        var playerInfo = JSON.parse(localStorage.getItem("playerInfo"));
        if(localStorage.username === message.nextPlayer)
        {
          playerInfo.onTurn = true;
          localStorage.setItem("playerInfo", JSON.stringify(playerInfo));
          if(playerInfo.availableArmies===0 && parseInt(localStorage.gameStage)===0)
          {
            await this.sendFirstPhaseDone();
          }
          else if(parseInt(localStorage.gameStage)===0)
          {
            document.getElementById("attackBtn").disabled=true;
            document.getElementById("defendBtn").disabled=true;
            document.getElementById("endTurnBtn").disabled=true;
            document.getElementById("addArmieBtn").disabled=false;
          }         
        } 
        else
        {
          playerInfo.onTurn = false;
          localStorage.setItem("playerInfo", JSON.stringify(playerInfo));
          if(parseInt(localStorage.gameStage)===0)
          {
            document.getElementById("attackBtn").disabled=true;
            document.getElementById("defendBtn").disabled=true;
            document.getElementById("endTurnBtn").disabled=true;
            document.getElementById("addArmieBtn").disabled=true;
          } 
        }
         
        this.setState(
            {
              playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
              allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
              playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
              gameParticipants: JSON.parse(localStorage.getItem("gameParticipants"))
            });
      
        JSON.parse(localStorage.getItem("allTerritories")).forEach(element => {
          let el = document.getElementById(element.territoryID);
          el.style.backgroundColor=element.playerColor;
          el.querySelector('span').innerHTML=element.numArmies;
        });
      });

      this.connection.on('PlayerAddReinforcement', async message => {
        var territories = JSON.parse(localStorage.getItem("allTerritories"));
        territories.forEach(el => {if (el.territoryID===message.territoryID) {el.numArmies=message.numArmies; return;}});
        localStorage.setItem("allTerritories", JSON.stringify(territories));
        
        this.setState(
            {
              playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
              allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
              playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
              gameParticipants: JSON.parse(localStorage.getItem("gameParticipants"))
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

      this.connection.on('PlayerAttacked', async message => {
        if(localStorage.username===message.targetPlayer)
        {
          document.getElementById("defendBtn").disabled=false;
          localStorage.attackedFromTerritoryID = message.attackFromTerritory;
          localStorage.attackedTerritoryID = message.targetTerritory;
          localStorage.attackNumDice=message.numDice;
          localStorage.playerAttacked = message.playerAttackedID;
          document.getElementById("attackedTerritoryName").innerHTML=message.targetTerritoryName;
          alert(message.playerAttackedName + " attacked your territory " + message.targetTerritoryName + " from " + message.attackFromTerritoryName+" with " + message.numDice + " dice");
        }
        else if(localStorage.username===message.playerAttackedName)
        {
          document.getElementById("attackBtn").disabled=true;
          localStorage.attackedFromTerritoryID = message.attackFromTerritory;
          localStorage.attackedTerritoryID = message.targetTerritory;
        }
      });

      this.connection.on('PlayerDefended', async message => {

        var territories = JSON.parse(localStorage.getItem("allTerritories"));
        territories.forEach(el => {
          if (el.territoryID===message.territory1ID)
          {
            el.numArmies=message.numArmies1;
          }
          else if(el.territoryID===message.territory2ID)
          {
            el.numArmies=message.numArmies2;
            if(message.winner)
              el.playerColor = message.player1Color;
          }
        });
        localStorage.setItem("allTerritories", JSON.stringify(territories));

        JSON.parse(localStorage.getItem("allTerritories")).forEach(element => {
          let el = document.getElementById(element.territoryID);
          el.style.backgroundColor=element.playerColor;
          el.querySelector('span').innerHTML=element.numArmies;
        });
        
        if(message.player1ID===(JSON.parse(localStorage.getItem("playerInfo"))).playerID)
        {
          document.getElementById("attackBtn").disabled=false;
          document.getElementById("endTurnBtn").disabled=false;
          if(message.winner)
          {
            document.getElementById("transferArmiesDiv").style.display="block";
            localStorage.attackFromTerritory = 0;
          }
            
        }

        if((JSON.parse(localStorage.getItem("playerInfo"))).playerID === message.player1ID || (JSON.parse(localStorage.getItem("playerInfo"))).playerID == message.player2ID)
        {
            await this.getPlayerTerritories();
        }

        this.setState(
              {
                playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
                allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
                playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
                gameParticipants: JSON.parse(localStorage.getItem("gameParticipants"))
              });
      });

      this.connection.on('PlayerTransferedArmies', async message => {
        var territories = JSON.parse(localStorage.getItem("allTerritories"));
        territories.forEach(el => {
          if (el.territoryID===message.terrFromID) 
          {
            el.numArmies-=message.numArmies; 
          }
          else if(el.territoryID===message.terrToID) 
          {
            el.numArmies+=message.numArmies; 
          }
        });
        localStorage.setItem("allTerritories", JSON.stringify(territories));
        
        if((JSON.parse(localStorage.getItem("playerInfo"))).playerID === message.playerID)
        {
            await this.getPlayerTerritories();
        }

        this.setState(
            {
              playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
              allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
              playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
              gameParticipants: JSON.parse(localStorage.getItem("gameParticipants"))
            });
      
        JSON.parse(localStorage.getItem("allTerritories")).forEach(element => {
          let el = document.getElementById(element.territoryID);
          el.style.backgroundColor=element.playerColor;
          el.querySelector('span').innerHTML=element.numArmies;
        });
      });

      this.connection.on('ReceiveFirstStageDone', async message => {
          localStorage.gameStage = 1;
          document.getElementById("addArmieBtn").style.display="none";
          document.getElementById("addReinforcementBtn").style.display="block";
          document.getElementById("addReinforcementInput").style.display="block";
          var playerInfo = JSON.parse(localStorage.getItem("playerInfo"));
          if(playerInfo.onTurn===true)
          {
            document.getElementById("addReinforcementBtn").disabled=false;
           
            playerInfo.availableArmies=message;
            localStorage.setItem("playerInfo", JSON.stringify(playerInfo));
            this.setState(
              {
                playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
                allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
                playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
                gameParticipants: JSON.parse(localStorage.getItem("gameParticipants"))
              });
          }
            
          else
            document.getElementById("addReinforcementBtn").disabled=true;
     
      });
    })
    .catch(err => console.error('SignalR Connection Error: ', err));

    JSON.parse(localStorage.getItem("allTerritories")).forEach(element => {
      let el = document.getElementById(element.territoryID);
      el.style.backgroundColor=element.playerColor;
      el.querySelector('span').innerHTML=element.numArmies;
    });

    var playerInfo = JSON.parse(localStorage.getItem("playerInfo"));
    if(parseInt(localStorage.gameStage)===0 && playerInfo.onTurn===true)
    {
          document.getElementById("addReinforcementBtn").style.display="none";
          document.getElementById("addReinforcementInput").style.display="none";
          document.getElementById("addArmieBtn").style.display="block";
          document.getElementById("attackBtn").disabled=true;
          document.getElementById("defendBtn").disabled=true;
          document.getElementById("endTurnBtn").disabled=true;
          document.getElementById("addArmieBtn").disabled=false;
    }
    else if(parseInt(localStorage.gameStage)===0 && playerInfo.onTurn===false)
    {
          document.getElementById("addReinforcementBtn").style.display="none";
          document.getElementById("addReinforcementInput").style.display="none";
          document.getElementById("addArmieBtn").style.display="block";
          document.getElementById("attackBtn").disabled=true;
          document.getElementById("defendBtn").disabled=true;
          document.getElementById("endTurnBtn").disabled=true;
          document.getElementById("addArmieBtn").disabled=true;
    }
    else if(parseInt(localStorage.gameStage)===1 && playerInfo.onTurn===true && playerInfo.availableArmies===0)
    {
          document.getElementById("addReinforcementBtn").style.display="block";
          document.getElementById("addReinforcementInput").style.display="block";
          document.getElementById("addReinforcementBtn").disabled=true;
          document.getElementById("addArmieBtn").style.display="none";
          document.getElementById("attackBtn").disabled=false;
          document.getElementById("defendBtn").disabled=true;
          document.getElementById("endTurnBtn").disabled=false;
    }
    else if(parseInt(localStorage.gameStage)===1 && playerInfo.onTurn===true)
    {
          document.getElementById("addReinforcementBtn").style.display="block";
          document.getElementById("addReinforcementInput").style.display="block";
          document.getElementById("addReinforcementBtn").disabled=false;
          document.getElementById("addArmieBtn").style.display="none";
          document.getElementById("attackBtn").disabled=true;
          document.getElementById("defendBtn").disabled=true;
          document.getElementById("endTurnBtn").disabled=true;
    }
    else if(parseInt(localStorage.gameStage)===1 && playerInfo.onTurn===false)
    {
          document.getElementById("addReinforcementBtn").style.display="block";
          document.getElementById("addReinforcementInput").style.display="block";
          document.getElementById("addReinforcementBtn").disabled=true;
          document.getElementById("addArmieBtn").style.display="none";
          document.getElementById("attackBtn").disabled=true;
          document.getElementById("defendBtn").disabled=true;
          document.getElementById("endTurnBtn").disabled=true;
    }
   
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
          allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
          playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
          gameParticipants: JSON.parse(localStorage.getItem("gameParticipants"))
        }
      );
      
    }
    else {
        this.setState({
            errors: { message: res.message }
        });
    }
  }

  async onAddReinforcement(){
    if(parseInt(localStorage.selectedAddArmieTerritory) === 0)
    {
      alert("Please select territory.");
      return;
    }

    if(document.getElementById("addReinforcementInput").value==="")
    {
      alert("Select valid number of reinforcements!");
      return;
    }

    var armies = parseInt(document.getElementById("addReinforcementInput").value);

    if(armies<1 || armies > this.state.playerInfo.availableArmies)
    {
      alert("Select valid number of reinforcements!");
      return;
    }
      

    var msg = {
      "gameID" : parseInt(localStorage.gameID),
      "playerID" : (JSON.parse(localStorage.getItem("playerInfo"))).playerID,
      "territoryID" : parseInt(localStorage.selectedAddArmieTerritory),
      "numArmies" : armies
    }
    const res =  await fetch("https://localhost:44348/api/PlayerTerritory/AddReinforcement", { method: "POST",
    headers: {
    "Content-Type": "application/json"
    },
    body: JSON.stringify(msg)
    });
    if (res.ok) {
      alert("Reinforcement added!");

      var tmp = JSON.parse(localStorage.getItem("playerInfo"));
      tmp.availableArmies=tmp.availableArmies-armies;
      localStorage.setItem("playerInfo", JSON.stringify(tmp));
      this.setState( 
        {
          playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
          allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
          playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
          gameParticipants: JSON.parse(localStorage.getItem("gameParticipants"))
        }
      );

      if(tmp.availableArmies===0)
      {
        await this.getPlayerTerritories();
        this.setState(
          {
            playerInfo: JSON.parse(localStorage.getItem("playerInfo")),
            allTerritories:JSON.parse(localStorage.getItem("allTerritories")),
            playerTerritories:JSON.parse(localStorage.getItem("playerTerritories")),
            gameParticipants: JSON.parse(localStorage.getItem("gameParticipants"))
          });
        document.getElementById("addReinforcementBtn").disabled=true;
        document.getElementById("attackBtn").disabled=false;
        document.getElementById("endTurnBtn").disabled=false;
      }
      
    }
    else {
        this.setState({
            errors: { message: res.message }
        });
    }
  }

  async onAttack(){
    var attackNumDice = parseInt(document.getElementById("attackNumDice").value);
    var playerTerritories = JSON.parse(localStorage.getItem("playerTerritories"))
    var numDice = playerTerritories.filter(el=>el.territoryID === parseInt(localStorage.attackFromTerritory))[0].numArmies;
    if(parseInt(localStorage.attackFromTerritory)===0 || parseInt(localStorage.attackTargetTerritory)===0)
    {
      alert("Select attack from and terget territories!");
      return;
    }
    if(attackNumDice>numDice-1)
    {
      alert("Invalid dice num!");
      return;
    }
    var msg = {
      "playerID" :(JSON.parse(localStorage.getItem("playerInfo"))).playerID,
      "playerUsername" :localStorage.username,
      "attackFromID" : parseInt(localStorage.attackFromTerritory),
      "attackFromName" : localStorage.attackFromTerritoryName,
      "targetID" : parseInt(localStorage.attackTargetTerritory),
      "numDice" : attackNumDice,
      "gameID" :  parseInt(localStorage.gameID)
    }
    const res =  await fetch("https://localhost:44348/api/PlayerTerritory/Attack", { method: "POST",
    headers: {
    "Content-Type": "application/json"
    },
    body: JSON.stringify(msg)
    }); 
    if (res.ok) {
      document.getElementById("endTurnBtn").disabled=true;
      alert("Territory attacked!");
    }
    else {
        this.setState({
            errors: { message: res.message }
        });
    }
  }

  async onDeffend(){
    var defendNumDice = document.getElementById("defendNumDice").value;
    var playerTerritories = JSON.parse(localStorage.getItem("playerTerritories"));
    var validNumDice = playerTerritories.filter(el=>el.territoryID === parseInt(localStorage.attackedTerritoryID))[0].numArmies;
    if(parseInt(defendNumDice)>parseInt(validNumDice))
    {
      alert("Invalid dice num!");
      return;
    }
    var msg = {
      "gameID" :  parseInt(localStorage.gameID),
      "numDice1" : parseInt(localStorage.attackNumDice),
      "numDice2" : parseInt(defendNumDice),
      "territory1ID" : parseInt(localStorage.attackedFromTerritoryID),
      "territory2ID" : parseInt(localStorage.attackedTerritoryID),
      "player1ID" : parseInt(localStorage.playerAttacked),
      "player2ID" : (JSON.parse(localStorage.getItem("playerInfo"))).playerID  
    }
    const res =  await fetch("https://localhost:44348/api/PlayerTerritory/ThrowDice", { method: "POST",
    headers: {
    "Content-Type": "application/json"
    },
    body: JSON.stringify(msg)
    }); 
    if (res.ok) {
      document.getElementById("defendBtn").disabled=true;
      document.getElementById("attackedTerritoryName").innerHTML="";
      alert("Defending territory!");
    }
    else {
        this.setState({
            errors: { message: res.message }
        });
    }
   }

   async transferArmies(){
    var terrFromID = parseInt(localStorage.attackedFromTerritoryID);
    var terrToID = parseInt(localStorage.attackedTerritoryID);
    var playerID = (JSON.parse(localStorage.getItem("playerInfo"))).playerID;
    var numArmies = parseInt(document.getElementById("transferArmiesInput").value);
    var playerTerritories = JSON.parse(localStorage.getItem("playerTerritories")).filter(el=>el.territoryID === terrFromID);
    var validNumArmies = playerTerritories.filter(el=>el.territoryID === terrFromID)[0].numArmies;
  
    if(numArmies <= 0)
    {
      alert("Invalid number of armies!");
      return;
    }
    if(validNumArmies - numArmies <= 0)
    {
      alert("Invalid number of armies!");
      return;
    }
    var msg = {
      "gameID" : parseInt(localStorage.gameID),
      "terrFromID" :  terrFromID,
      "terrToID" : terrToID,
      "playerID" : playerID,
      "numArmies" : numArmies  
    }

    const res =  await fetch("https://localhost:44348/api/PlayerTerritory/Transfer", { method: "POST",
    headers: {
    "Content-Type": "application/json"
    },
    body: JSON.stringify(msg)
    }); 
    if (res.ok) {
      document.getElementById("transferArmiesDiv").style.display="none";
    }
    else {
        this.setState({
            errors: { message: res.message }
        });
    }
   }

  endTurn(){

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

  async sendFirstPhaseDone() {
    const res =  await fetch("https://localhost:44348/api/Game/NextStage/"+localStorage.gameID+"/"+(JSON.parse(localStorage.getItem("playerInfo"))).playerID+"/"+localStorage.mapID, { method: "PUT"}); 
    if (res.ok) {
      alert("Ide gas!");
    }
    else {
        this.setState({
            errors: { message: res.message }
        });
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

async getPlayerTerritories(){
  const res = await fetch("https://localhost:44348/api/PlayerTerritory/GetPlayerTerritories/"+(JSON.parse(localStorage.getItem("playerInfo"))).playerID, { method: "GET"})
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
      localStorage.setItem("playerTerritories", JSON.stringify(array)) 
  } else {
      console.log(res.message);
  }  
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
            <button id="addArmieBtn" onClick={this.onAddArmie}>Add armie</button>
            <input id="addReinforcementInput" type="number" min="1" max={this.state.playerInfo.availableArmies}></input>
            <button id="addReinforcementBtn" onClick={this.onAddReinforcement}>Add armie(s)</button>
          </div>
          <div>
            <h4>ATTACK</h4>
            <AttackTerritorySelect dataParentToChild = {this.state.playerTerritories}/>
            <button id="attackBtn" onClick={this.onAttack}>Attack</button>
            <div id="transferArmiesDiv" style={{display:"none"}}>
              <label>Transfer armies</label>
              <input id="transferArmiesInput" type="number" min="1"></input>
              <button onClick={this.transferArmies}>Transfer</button>
            </div>
          </div>
          <div>
            <h4>DEFENSE</h4>
            <label id="attackedTerritoryName"></label>
            <select id="defendNumDice">
                  <option key = "1" value={1} default>1</option>
                  <option key = "2" value={2}>2</option>
                  <option key = "3" value={3}>3</option>
            </select>
            <br/>
            <button id="defendBtn" onClick={this.onDeffend}>Deffend</button>
          </div>
          <div>
            <button id="endTurnBtn" onClick={this.endTurn}>END TURN</button> 
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
