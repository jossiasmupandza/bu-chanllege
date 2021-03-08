import api from "../../helpers/api";

export const ADD_ELEMENT_IN_ARRAY = "ADD_ELEMENT_IN_ARRAY";
export const REMOVE_ELEMENT_FROM_ARRAY = "REMOVE_ELEMENT_FROM_ARRAY";

export const CREATE_QUESTION = "CREATE_QUESTION";
export const CREATE_QUESTION_SUCCESS = "CREATE_QUESTION_SUCCESS";
export const CREATE_QUESTION_FAIL = "CREATE_QUESTION_FAIL";

export const UPDATE_QUESTION = "UPDATE_QUESTION";
export const UPDATE_QUESTION_SUCCESS = "UPDATE_QUESTION_SUCCESS";
export const UPDATE_QUESTION_FAIL = "UPDATE_QUESTION_FAIL";

export const DELETE_QUESTION = "DELETE_QUESTION";
export const DELETE_QUESTION_SUCCESS = "DELETE_QUESTION_SUCCESS";
export const DELETE_QUESTION_FAIL = "DELETE_QUESTION_FAIL";

export const addElementInArray = () => {
    return async (dispatch)=>{
        dispatch({type:ADD_ELEMENT_IN_ARRAY})
    }
}

export const removeElementFromArray = () => {
    return async (dispatch)=>{
        dispatch({type:REMOVE_ELEMENT_FROM_ARRAY})
    }
}

export const createQuestion = (quizId, title, required, multipleOptions, inputTypeId, options, file) => {
    return async (dispatch) => {
        dispatch({type: CREATE_QUESTION});

        await api.post('questions' ,
            {quizId, title, required, multipleOptions, inputTypeId, options, file}
            ,{
            headers: {
                "Content-Type": "application/json",
            },
            validateStatus:(status)=>{
                return status;
            }
        }).then(response => {
            if(response.status === 200){
                dispatch({
                    type: CREATE_QUESTION_SUCCESS,
                    payload: response.data
                })
            } else {
                dispatch({
                    type: CREATE_QUESTION_FAIL,
                    payload: response.data
                })
            }
        }, (error) => {
            dispatch({
                type: CREATE_QUESTION_FAIL,
                payload: `Ops! Ocorreu um erro ao conectar com o servidor. Erro ${error}`
            })
        });
    }
}
