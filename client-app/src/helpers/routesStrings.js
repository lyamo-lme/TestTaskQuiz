export const apiRoutes = {
    API_URL: "http://localhost:5299",
    login: "/auth/login",
    refreshToken: "/auth/refresh",
    logout: "/auth/logout",
    usersTests: (userId) => `/test/user/${userId}`,
    beginAttempt: "/test/attempt",
    choseAnswer: "test/answer"
}