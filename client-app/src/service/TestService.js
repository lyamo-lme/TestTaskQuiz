import axios from "axios";
import {apiHttp} from "../api/axios";
import {apiRoutes as apiRoute, apiRoutes} from "../helpers/routesStrings";
import {accessTokenKey, clearCookie, refreshTokenKey, setCookie} from "../helpers/cookie";

export class TestService {
    static async getUsersTests(userId) {
        return apiHttp.get(apiRoutes.usersTests(userId)).then(response => {
            let data = response.data;
            console.log(data);
            return data;
        });
    }

    static async beginAttempt(userId, testId) {
        return apiHttp.post(apiRoutes.beginAttempt, {
            userId, testId
        }).then(response => {
            let data = response.data;
            console.log(data);
            return data;
        });
    }

    static async choseAnswer(userId, answerId) {
        return apiHttp.post(apiRoute.choseAnswer, {
            userId, answerId
        }).then(response => {
            let data = response.data;
            console.log(data);
            return data;
        });
    }
}