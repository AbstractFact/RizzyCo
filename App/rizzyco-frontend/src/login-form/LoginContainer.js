import { ActionViewCarousel, NavigationFullscreen } from "material-ui/svg-icons";
import React, { Component } from "react";
import LoginForm from "./LoginForm.js";
const FormValidators = require("./Validate");
const validateLoginForm = FormValidators.validateLoginForm;

export default class LoginContainer extends Component {
  constructor(props) {
    super(props);

    this.state = {
      errors: {},
      user: {
        username: "",
        password: ""
      },
      btnTxt: "show",
      type: "password",
    };

    this.pwMask = this.pwMask.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.submitLogin = this.submitLogin.bind(this);
    this.validateForm = this.validateForm.bind(this);
    this.pwHandleChange = this.pwHandleChange.bind(this);
  }

  handleChange(event) {
    const field = event.target.name;
    const user = this.state.user;
    user[field] = event.target.value;

    this.setState({
      user
    });
  }

  pwHandleChange(event) {
    const field = event.target.name;
    const user = this.state.user;
    user[field] = event.target.value;

    this.setState({
      user
    });
  }

  async handleGetMaps(){
    var existingEntries = JSON.parse(localStorage.getItem("allMaps"));
    if(existingEntries.length === 0)
    {
        existingEntries = [];
        const res = await fetch("https://localhost:44348/api/Map", { method: "GET"})
        if (res.ok) {
          const d = await res.json()
                d.forEach(element => {
                    var entry = {
                        "id": element.id,
                        "name": element.name
                    };
                    localStorage.setItem("entry", JSON.stringify(entry));
                    existingEntries.push(entry);
                });
                localStorage.setItem("allMaps", JSON.stringify(existingEntries));  
        } else {
          this.setState({
            errors: { message: res.message }
          });
        }  
    }    
  }
  
  async submitLogin(user) {
    localStorage.setItem("allMaps", JSON.stringify([]));
    await this.handleGetMaps();
    await fetch("https://localhost:44348/api/User/Authenticate", { method: "POST",
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify({ "username": user.usr, "password": user.pw })
    }).then(res => {
        if (res.ok) {
          res.json().then(d=> {
            localStorage.token = d.token;
            localStorage.userID=d.id;
            localStorage.username=d.username;
            localStorage.isAuthenticated = true;
            console.log(localStorage.getItem("redirect"));
            if(localStorage.getItem("redirect")!=="")
              window.location.href=localStorage.getItem("redirect"); 
            else
              window.location.href="/home";
          })    
        } else {
          this.setState({
            errors: { message: res.message }
          });
        }
      })
      .catch(err => {
        console.log("Log in data submit error: ", err);
    });  
  }

  validateForm(event) {
    event.preventDefault();
    var payload = validateLoginForm(this.state.user);
    if (payload.success) {
      this.setState({
        errors: {}
      });
      var user = {
        usr: this.state.user.username,
        pw: this.state.user.password
      };
      this.submitLogin(user);
    } else {
      const errors = payload.errors;
      this.setState({
        errors
      });
    }
  }

  pwMask(event) {
    event.preventDefault();
    this.setState(state =>
      Object.assign({}, state, {
        type: this.state.type === "password" ? "input" : "password",
        btnTxt: this.state.btnTxt === "show" ? "hide" : "show"
      })
    );
  }

  render() {
    return (
      <div>
        <LoginForm
          onSubmit={this.validateForm}
          onChange={this.handleChange}
          onPwChange={this.pwHandleChange}
          errors={this.state.errors}
          user={this.state.user}
          btnTxt={this.state.btnTxt}
          type={this.state.type}
          pwMask={this.pwMask}
        />
      </div>
    );
  }
}

