import AuthStore from "./AuthState";
import {createContext} from "react";
import TestStore from "./TestState";

const authStore = new AuthStore();
const testStore = new TestStore();
export const Context = createContext({
    authStore, testStore
});