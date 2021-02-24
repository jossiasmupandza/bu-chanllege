import {LOGIN, LOGIN_FAIL, LOGIN_SUCCESS, LOGOUT_SUCCESS} from "../actions/login.action";

const initialState = {
    username:"",
    isLoggedIn: false,
    isSaving: false,
    token: "",
    errorMessage: "",
    successMessage: "",
    isLoading: false
}

export default function loginReducer(state = initialState, action) {
    switch (action.type) {
        case LOGIN:
            return {
                ...state,
                isSaving: true
            };

        case LOGIN_SUCCESS:
            return {
                ...state,
                isSaving: false,
                username: action.payload.username,
                token: action.payload.token,
                isLoggedIn: true,
            };

        case LOGIN_FAIL:
            return {
                ...state,
                isSaving: false,
                errorMessage: action.payload.message,
                isLoggedIn: false,
            };

        default:
            return state;
    }
}
