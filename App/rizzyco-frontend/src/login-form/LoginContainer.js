import React, { Component } from "react";
import LoginForm from "./LoginForm.js";
const axios = require("axios");
const FormValidators = require("./Validate");
const validateLoginForm = FormValidators.validateLoginForm;
const zxcvbn = require("zxcvbn");

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

  submitLogin(user) {
    fetch("https://localhost:44348/api/User/Authenticate", { method: "POST",
        headers: {
        "Content-Type": "application/json"
        },
        body: JSON.stringify({ "username": user.usr, "password": user.pw })
    }).then(res => {
        if (res.ok) {
          localStorage.token = res.json().token;
          localStorage.isAuthenticated = true;
          window.location.href="/";
        } else {
          this.setState({
            errors: { message: res.message }
          });
        }
      })
      .catch(err => {
        console.log("Log in data submit error: ", err);
    });  
    // axios
    //   .post("https://ouramazingserver.com/api/signup/submit", params)
    //   .then(res => {
    //     if (res.data.success === true) {
    //       localStorage.token = res.data.token;
    //       localStorage.isAuthenticated = true;
    //       window.location.reload();
    //     } else {
    //       this.setState({
    //         errors: { message: res.data.message }
    //       });
    //     }
    //   })
    //   .catch(err => {
    //     console.log("Login data submit error: ", err);
    //   });
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

