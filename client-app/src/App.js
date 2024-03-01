import './App.css';
import {Navigate, redirect, Route, Routes, useNavigate} from "react-router-dom";
import {Header} from "./components/Header/Header";
import Login from "./components/Login/Login";
import Tests from "./components/Tests/Tests";
import {observer} from "mobx-react-lite";
import React, {useContext, useEffect} from "react"
import PassTest from "./components/Tests/PassTest";
import {Context} from "./store/context";
import {ProtectedRoute} from "./components/ProtectedRoute/ProtectedRoute";


function App() {
    const {authStore} = useContext(Context);

    return (
        <div className="App">
            <Header/>
            <Routes>
                {/*<Route path="/" element={<Tests/>}/>*/}
                {/*<Route path="/test/:testId" element={<PassTest/>}/>*/}
                <Route path="/login" element={<Login/>}/>
                <Route path="/test/:testId" element={
                    <ProtectedRoute user={authStore.user}>
                        <PassTest/>
                    </ProtectedRoute>
                }/>
                <Route path="/" element={
                    <ProtectedRoute user={authStore.user}>
                        <Tests/>
                    </ProtectedRoute>
                }
                />
            </Routes>
        </div>
    );
}

export default observer(App);
