import {Cookies} from "react-cookie";

export const refreshTokenKey = "refreshToken";
export const accessTokenKey = "accessToken";
const cookies = new Cookies();
export const setCookie = (name, value, lifetime) => {
    cookies.set(name, value, {maxAge: lifetime})
}

export const clearCookie = (cookieKey) => {
    cookies.remove(cookieKey);
}

export const getCookie = (cookieKey) => {
    return cookies.get(cookieKey);
}