import React, { Component } from "react";
import SignUpForm from "./SignUpForm.js";
const FormValidators = require("./Validate");
const validateSignUpForm = FormValidators.validateSignUpForm;
const zxcvbn = require("zxcvbn");

export default class SignUpContainer extends Component {
  constructor(props) {
    super(props);

    this.state = {
      errors: {},
      user: {
        username: "",
        email: "",
        password: "",
        pwconfirm: ""
      },
      btnTxt: "show",
      type: "password",
      score: "0"
    };

    this.pwMask = this.pwMask.bind(this);
    this.handleChange = this.handleChange.bind(this);
    this.submitSignup = this.submitSignup.bind(this);
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

    if (event.target.value === "") {
      this.setState(state =>
        Object.assign({}, state, {
          score: "null"
        })
      );
    } else {
      var pw = zxcvbn(event.target.value);
      this.setState(state =>
        Object.assign({}, state, {
          score: pw.score + 1
        })
      );
    }
  }

  async handleGetMaps(){
    var existingEntries = JSON.parse(localStorage.getItem("allMaps"));
    if(existingEntries.length === 0)
    {
        existingEntries = [];
        await fetch("https://localhost:44348/api/Map", { method: "GET"}).then(res => {
        if (res.ok) {
          res.json().then(d=>{
                d.forEach(element => {
                    var entry = {
                        "id": element.id,
                        "name": element.name
                    };
                    localStorage.setItem("entry", JSON.stringify(entry));
                    existingEntries.push(entry);
                });
                localStorage.setItem("allMaps", JSON.stringify(existingEntries));
          })    
        } else {
          this.setState({
            errors: { message: res.message }
          });
        }
        })
        .catch(err => {
          console.log("Get maps error: ", err);
        });  
    }    
  }
  
  submitSignup(user) {
    fetch("https://localhost:44348/api/User/Signup", { method: "POST",
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify({ "username": user.usr, "password": user.pw, "email": user.email, "role": "User" })
    }).then(res => {
      if (res.ok) {
        res.json().then(d=>{
          localStorage.token = d.token;
          localStorage.userID=d.id;
          localStorage.isAuthenticated = true;
          this.handleGetMaps();
          window.location.href="/home";
        })    
      } else {
        this.setState({
          errors: { message: res.message }
        });
      }
      })
      .catch(err => {
        console.log("Sign up data submit error: ", err);
    });  
  }

  validateForm(event) {
    event.preventDefault();
    var payload = validateSignUpForm(this.state.user);
    if (payload.success) {
      this.setState({
        errors: {}
      });
      var user = {
        usr: this.state.user.username,
        pw: this.state.user.password,
        email: this.state.user.email
      };
      this.submitSignup(user);
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
        <SignUpForm
          onSubmit={this.validateForm}
          onChange={this.handleChange}
          onPwChange={this.pwHandleChange}
          errors={this.state.errors}
          user={this.state.user}
          score={this.state.score}
          btnTxt={this.state.btnTxt}
          type={this.state.type}
          pwMask={this.pwMask}
        />
      </div>
    );
  }
}
