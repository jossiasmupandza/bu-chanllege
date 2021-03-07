import {GET_INPUTTYPES, GET_INPUTTYPES_FAIL, GET_INPUTTYPES_SUCCESS} from "../actions/inputTypes.action";

const initialStatus = {
    inputs: [],
    isLoading: false,
    errorMessage: ""
}

export default function inputTypesReducer(state = initialStatus, action) {
    switch (action.type) {
        case GET_INPUTTYPES:
            return {
                ...state,
                isLoading: true
            }

        case GET_INPUTTYPES_SUCCESS:
            return {
                ...state,
                isLoading: false,
                inputs: action.payload
            }

        case GET_INPUTTYPES_FAIL:
            return {
                ...state,
                isLoading: false,
                errorMessage: action.payload
            }

        default:
            return state;
    }
}
