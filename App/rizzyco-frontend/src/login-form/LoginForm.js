import React from "react";
import RaisedButton from "material-ui/RaisedButton";
import TextField from "material-ui/TextField";
import "./style.css";

const LoginForm = ({
  history,
  onSubmit,
  onChange,
  errors,
  user,
  score,
  btnTxt,
  type,
  pwMask,
  onPwChange
}) => {
  return (
    <div className="loginBox">
      <h1>Login</h1>
      {errors.message && <p style={{ color: "red" }}>{errors.message}</p>}

      <form onSubmit={onSubmit}>
        <TextField
          name="username"
          floatingLabelText="user name"
          value={user.username}
          onChange={onChange}
          errorText={errors.username}
        />
        <TextField
          type={type}
          name="password"
          floatingLabelText="password"
          value={user.password}
          onChange={onPwChange}
          errorText={errors.password}
        />
        <br />
        <br />
        <RaisedButton
          className="loginSubmit"
          primary={true}
          type="submit"
          label="submit"
        />
      </form>
      <p>
        Don't have an account? <br />
        <a href="/signup" >Sign up here</a>
      </p>
    </div>
  );
};

export default LoginForm;
