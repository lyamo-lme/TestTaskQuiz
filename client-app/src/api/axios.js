import axios from "axios";
import {accessTokenKey, getCookie, refreshTokenKey, setCookie} from "../helpers/cookie";
import {apiRoutes} from "../helpers/routesStrings";

export const apiHttp = axios.create({
    withCredentials: true,
    baseURL: apiRoutes.API_URL
});

apiHttp.interceptors.request.use((config) => {

    if (getCookie(accessTokenKey) !== undefined) {
        config.headers.Authorization = `Bearer ${getCookie(accessTokenKey)}`;
    }
    return config;
});


apiHttp.interceptors.response.use((config) => {
    return config;
}, async (error) => {
    const request = error.config;
    if (error.response.status == 401) {
        const response = await axios.post(apiRoutes.refreshToken, {
            token: getCookie(refreshTokenKey)
        });
        setCookie(accessTokenKey, response.data.accessToken);
        setCookie(refreshTokenKey, response.data.refreshToken);

        return apiHttp.request(request);
    }
});


let defaultProps = {
    counters: [
        {id: 1, initial: 6, min: -5, max: 10},
        {id: 2, initial: 5, min: -10, max: 20},
        {id: 3, initial: 0, min: -100, max: 100},
    ],
};