import api from "../../helpers/api";

export const GET_INPUTTYPES = "GET_IPUTTYPES";
export const GET_INPUTTYPES_SUCCESS = "GET_IPUTTYPES_SUCCESS";
export const GET_INPUTTYPES_FAIL = "GET_IPUTTYPES_FAIL";

export const getInputTypes = ()=> {
    return async (dispatch) => {
        dispatch({type: GET_INPUTTYPES});

        await api.get('inputTypes' ,{
            validateStatus:(status)=>{
                return status;
            }
        }).then(response => {
            if(response.status === 200){
                dispatch({
                    type: GET_INPUTTYPES_SUCCESS,
                    payload: response.data
                })
            } else {
                dispatch({
                    type: GET_INPUTTYPES_FAIL,
                    payload: response.data
                })
            }
        }, (error) => {
            dispatch({
                type: GET_INPUTTYPES_FAIL,
                payload: `Ops! Ocorreu um erro ao conectar com o servidor. Erro ${error}`
            })
        });
    }
}
