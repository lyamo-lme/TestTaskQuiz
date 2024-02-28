import {AuthService} from "../service/AuthService";
import {makeAutoObservable} from "mobx"

export default class AuthStore {
    isAuth = false;
    user = null;

    constructor() {
        makeAutoObservable(this);
    }

    async login(email, password) {
        try {
            let data = await AuthService.login(email, password);
            console.log(data)
            this.isAuth = true;
            this.user = data.user;
            console.log(this)
        } catch (e) {
            console.log(e.response);
        }
    }

    async logout() {
        this.isAuth = false;
        this.user = null;
        return AuthService.logout();
    }
}