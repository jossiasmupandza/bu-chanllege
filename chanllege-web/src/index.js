import React from "react";
import ReactDOM from "react-dom";
import { BrowserRouter, Route, Switch, Redirect } from "react-router-dom";

import "assets/vendor/nucleo/css/nucleo.css";
import "assets/vendor/font-awesome/css/font-awesome.min.css";
import "assets/scss/argon-design-system-react.scss?v1.1.0";

// //import Index from "src/template/Index.js";
// import Landing from "src/template/examples/Landing.js";
// import Login from "src/template/examples/Login.js";
//import Profile from "./template/examples/Profile";
// import Register from "src/template/examples/Register.js";
import LadingPage from "./views/LadingPage.js";
import CreateQuiz from "./views/CreateQuiz";

ReactDOM.render(
  <BrowserRouter>
    <Switch>
      <Route
          path="/"
          exact
          render={props => <LadingPage {...props} />}
      />
        <Route
            path="/create-quiz"
            exact
            render={props => <CreateQuiz {...props} />}
        />
      {/*<Route*/}
      {/*  path="/landing-page"*/}
      {/*  exact*/}
      {/*  render={props => <Landing {...props} />}*/}
      {/*/>*/}
      {/*<Route path="/login-page" exact render={props => <Login {...props} />} />*/}
      {/*<Route*/}
      {/*  path="/profile-page"*/}
      {/*  exact*/}
      {/*  render={props => <Profile {...props} />}*/}
      {/*/>*/}
      {/*<Route*/}
      {/*  path="/register-page"*/}
      {/*  exact*/}
      {/*  render={props => <Register {...props} />}*/}
      {/*/>*/}
      <Redirect to="/" />
    </Switch>
  </BrowserRouter>,
  document.getElementById("root")
);
