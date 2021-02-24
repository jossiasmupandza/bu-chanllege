import api from "../../helpers/api";

export const LOGIN = "LOGIN";
export const LOGIN_SUCCESS = "LOGIN_SUCCESS";
export const LOGIN_FAIL = "LOGIN_FAIL";

export const LOGOUT = "LOGOUT";
export const LOGOUT_SUCCESS= "LOGOUT_SUCCESS";
export const LOGOUT_FAIL = "LOGOUT_FAIL";

export const login = (username, password) => {
    return async (dispatch) => {
        dispatch({type: LOGIN});

        await api.post('login', {username, password},{
            validateStatus:(status)=>{
                return status;
            }
        }).then(response => {
            if(response.status === 200){
                dispatch({
                    type: LOGIN_SUCCESS,
                    payload: response.data
                })
            } else {
                dispatch({
                    type: LOGIN_FAIL,
                    payload: response.data
                })
            }
        }, (error) => {
            dispatch({
                type: LOGIN_FAIL,
                payload: `Ops! Ocorreu um erro ao conectar com o servidor. Erro ${error}`
            })
        });
    }
}


export const logout = (token) => {
    return async (dispatch) => {
        dispatch({type: LOGIN});

        await api.post('login' ,{
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
            validateStatus:(status)=>{
                return status;
            }
        }).then(response => {
            if(response.status === 200){
                dispatch({
                    type: LOGOUT_SUCCESS,
                    payload: response.data
                })
            } else {
                dispatch({
                    type: LOGOUT_FAIL,
                    payload: response.data
                })
            }
        }, (error) => {
            dispatch({
                type: LOGOUT_FAIL,
                payload: `Ops! Ocorreu um erro ao conectar com o servidor. Erro ${error}`
            })
        });
    }
}
