import React from "react";
import ReactDOM from "react-dom";
import {combineReducers, createStore, applyMiddleware} from "redux";
import {Provider} from "react-redux";
import thunk from "redux-thunk";

import "assets/vendor/nucleo/css/nucleo.css";
import "assets/vendor/font-awesome/css/font-awesome.min.css";
import "assets/scss/argon-design-system-react.scss?v1.1.0";

import Routes from "./routes";
import loginReducer from "./store/reducers/login.reducer";
import settingsReducer from "./store/reducers/settings.reducer";
import inputTypesReducer from "./store/reducers/inputTypes.reducer";


const rootReducer = combineReducers({
    login: loginReducer,
    settings: settingsReducer,
    inputType: inputTypesReducer
})

export const store = createStore(rootReducer, applyMiddleware(thunk));

ReactDOM.render(
    <Provider store={store}>
        <Routes />
    </Provider>,
    document.getElementById("root")
);
