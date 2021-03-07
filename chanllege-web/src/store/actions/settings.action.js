export const SHOW_QUIZ_CODES = "SHOW_QUIZ_CODES";

export const toggleModalShowQuizCodes = (show) => {
    return async (dispatch) => {
        dispatch({
            type: SHOW_QUIZ_CODES,
            payload: show
        })
    }
}
