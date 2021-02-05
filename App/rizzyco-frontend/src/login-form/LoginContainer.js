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
      score: "0"
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

  submitLogin(user) {
    var params = { username: user.usr, password: user.pw, email: user.email };
    axios
      .post("https://ouramazingserver.com/api/signup/submit", params)
      .then(res => {
        if (res.data.success === true) {
          localStorage.token = res.data.token;
          localStorage.isAuthenticated = true;
          window.location.reload();
        } else {
          this.setState({
            errors: { message: res.data.message }
          });
        }
      })
      .catch(err => {
        console.log("Login data submit error: ", err);
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
          score={this.state.score}
          btnTxt={this.state.btnTxt}
          type={this.state.type}
          pwMask={this.pwMask}
        />
      </div>
    );
  }
}

