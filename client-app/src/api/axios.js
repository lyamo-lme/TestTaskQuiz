import axios from "axios";
import {accessTokenKey, getCookie} from "../helpers/cookie";
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
