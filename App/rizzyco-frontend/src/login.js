import LoginContainer from "./login-form/LoginContainer"
import MuiThemeProvider from "material-ui/styles/MuiThemeProvider";
import './style/index.css'

function Login (){
   return (
    <>
      <MuiThemeProvider>
      <LoginContainer />
      </MuiThemeProvider>
    </>
   )
}

export default Login;
