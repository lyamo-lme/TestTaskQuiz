import axios from "axios";
import {apiHttp} from "../api/axios";
import {apiRoutes} from "../helpers/routesStrings";
import {accessTokenKey, clearCookie, getCookie, refreshTokenKey, setCookie} from "../helpers/cookie";

export class AuthService {
    static async login(email, password) {

        return apiHttp.post(apiRoutes.login, {
            email, password
        }).then(response => {
            let data = response.data;
            setCookie(accessTokenKey, data.accessToken);
            setCookie(refreshTokenKey, data.refreshToken);
            return data;
        });
    }

    static async refresh() {
        return apiHttp.post(apiRoutes.refreshToken, {
            refreshToken: getCookie(refreshTokenKey)
        }).then(response => {
            let data = response.data;
            setCookie(accessTokenKey, data.accessToken);
            setCookie(refreshTokenKey, data.refreshToken);
            return data;
        });
    }

    static async logout() {
        clearCookie(accessTokenKey);
        clearCookie(refreshTokenKey);
        return apiHttp.post(apiRoutes.logout);

    }
}