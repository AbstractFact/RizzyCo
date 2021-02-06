import SignUpContainer from "./signup-form/SignUpContainer"
import MuiThemeProvider from "material-ui/styles/MuiThemeProvider";
import './style/index.css'

function Signup (){
    
   return (
    <>
      <MuiThemeProvider>
          <SignUpContainer />
      </MuiThemeProvider>
    </>
   )
}

export default Signup;
