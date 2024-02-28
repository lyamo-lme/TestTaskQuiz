import AuthStore from "./AuthState";
import {createContext} from "react";

const authStore =new AuthStore();
export const  Context = createContext({
   authStore 
});