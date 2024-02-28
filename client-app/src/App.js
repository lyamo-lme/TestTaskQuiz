import './App.css';
import {Navigate, redirect, Route, Routes, useNavigate} from "react-router-dom";
import {Header} from "./components/Header/Header";
import Login from "./components/Login/Login";
import {Tests} from "./components/Tests/Tests";
import {observer} from "mobx-react-lite";
import React, {useContext, useEffect} from "react"



function App() {
    return (
        <div className="App">
            <Header/>
            <Routes>
                <Route path="/" element={<Tests/>}/>
                <Route path="/login" element={<Login/>}/>

            </Routes>
        </div>
    );
}

export default observer(App);
