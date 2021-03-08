import {
    ADD_ELEMENT_IN_ARRAY,
    CREATE_QUESTION,
    CREATE_QUESTION_FAIL,
    CREATE_QUESTION_SUCCESS,
    REMOVE_ELEMENT_FROM_ARRAY
} from "../actions/questions.action";

const initialStatus = {
    isLoading: false,
    isSaving: false,
    successMessage: "",
    errorMessage: "",
    questions: [],

}

export default function questionsReducer(state = initialStatus, action) {
    switch (action.type) {
        case CREATE_QUESTION:
            return {
                ...state,
                isSaving: true
            }

        case CREATE_QUESTION_SUCCESS:
            return {
                ...state,
                isSaving: false,
                questions: [...state.questions, action.payload],
                successMessage: "Question Saved"
            }

        case CREATE_QUESTION_FAIL:
            return {
                ...state,
                isSaving: false,
                errorMessage: "Error Saving Question"
            }

        case ADD_ELEMENT_IN_ARRAY:
            return {
                ...state,
                questions: [...state.questions, state.questions.length]
            }

        case REMOVE_ELEMENT_FROM_ARRAY:
            let q = [...state.questions];
            q.pop();

            return {
                ...state,
                questions: q
            }

        default:
            return state;
    }
}
