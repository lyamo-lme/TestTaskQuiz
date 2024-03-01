import {makeAutoObservable} from "mobx"
import {TestService} from "../service/TestService";

export default class TestStore {
    userTests = null;
    currentAttempt = null;

    constructor() {
        makeAutoObservable(this);
    }

    async getUsersTests(userId) {
        return TestService.getUsersTests(userId).then(data => {
            try {
                this.userTests = data;
                return data;
            } catch (e) {
                console.log(e);
            }
        });
    }

    async beginAttempt(userId, testId) {
        return TestService.beginAttempt(userId, testId).then(data => {
            try {
                this.currentAttempt = data;
                console.log(data)
                return data;
            } catch (e) {
                console.log(e);
            }
        });

    }

    async choseAnswer(userId, answerId) {
        return TestService.choseAnswer(userId, answerId).then(data => {
            try {
                this.currentAttempt = data;
                console.log(data)
                return data;
            } catch (e) {
                console.log(e);
            }
        });

    }
}