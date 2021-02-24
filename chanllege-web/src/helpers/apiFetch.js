export const API_URL = "http://127.0.0.1:5000/api";

export const apiCallFetch = (url, method, body, token, fileFlag) => {

    if (method !== "GET") {
        if (fileFlag) {
            return fetch(API_URL+url, {
                method: method,
                headers: {
                    "Content-Type": "multipart/form-data",
                    Authorization: `Bearer ${token}`,
                },
                body: body,
            });
        }

        return fetch(API_URL + url, {
            method: method,
            headers: {
                "Content-Type": "application/json",
                Authorization: `Bearer ${token}`,
            },
            body: JSON.stringify(body),
        });
    }
};

