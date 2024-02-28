import axios from "axios";
import {apiHttp} from "../api/axios";
import {apiRoutes} from "../helpers/routesStrings";
import {accessTokenKey, clearCookie, refreshTokenKey, setCookie} from "../helpers/cookie";

export class AuthService {
    static async login(email, password){
        
        return apiHttp.post(apiRoutes.login, {
            email, password
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