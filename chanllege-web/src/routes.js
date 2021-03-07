import React from "react";
import {BrowserRouter, Redirect, Route, Switch} from "react-router-dom";

import LadingPage from "./views/LadingPage";
import CreateQuiz from "./views/CreateQuiz";
import EditQuiz from "./views/EditQuiz";

export default function Routes() {
    return (
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
                <Route
                    path="/edit-quiz"
                    exact
                    render={props => <EditQuiz {...props} />}
                />
                <Redirect to="/" />
            </Switch>
        </BrowserRouter>
    )
}
