import {makeAutoObservable} from "mobx"
import {TestService} from "../service/TestService";

export default class TestStore {
    userTests = null;
    currentAttempt = null;
    access = true;

    constructor() {
        makeAutoObservable(this);
    }

    async getUsersTests(userId) {
        this.access =true;
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
        try {
            return TestService.beginAttempt(userId, testId).then((data) => {
                if (data === null) {
                    this.access = false;
                    return;
                }
                this.currentAttempt = data;
                return data;
            });
        } catch (e) {
            console.log(e);
        }
    }

    async choseAnswer(userId, answerId) {
        this.access =true;
        console.log(userId)
        console.log(answerId)
        return TestService.choseAnswer(userId, answerId).then(data => {
            try {
                if(data==null){
                    this.access = false;
                }
                return data;
            } catch (e) {
                console.log(e);
            }
        });

    }
}