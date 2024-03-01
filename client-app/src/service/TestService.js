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
        try {
            return apiHttp.post(apiRoutes.beginAttempt, {
                userId, testId
            }).then(response => {
                if (response === undefined) {
                    return null;
                }
                let data = response.data;
                console.log(data);
                return data;
            });
        } catch (e) {
            console.log(e);
        }
    }

    static async choseAnswer(userId, answerId) {
        try {
            console.log(userId)
            console.log(answerId)
            return apiHttp.post(apiRoute.choseAnswer, {
                userId, answerId
            }).then(response => {
                if (response === undefined) {
                    return null;
                }
                return true;
            });
        } catch (e) {
            console.log(e);
        }
    }
}