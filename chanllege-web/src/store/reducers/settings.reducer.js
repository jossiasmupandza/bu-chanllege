import {SHOW_QUIZ_CODES} from "../actions/settings.action";

const initialState = {
    showQuizCodes: false
}

export default function settingsReducer(state = initialState, action) {
    switch (action.type) {
        case SHOW_QUIZ_CODES:
            return {
                ...state,
                showQuizCodes: action.payload
            };

        default:
            return state;
    }
}
