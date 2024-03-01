import {useContext, useState} from "react";
import {Context} from "../../store/context";
import {observer} from "mobx-react-lite";
import './login.css';
import {useNavigate} from "react-router-dom";


const Login = () => {
    const navigate = useNavigate();
    const [LoginClaims, setLoginClaims] = useState({password: '', email: ''});
    const Login = (e) => {
        e.preventDefault();
        console.log(email);
        authStore.login(email, password).then((data => navigate("/")));
    };
    const {authStore} = useContext(Context);

    const {email, password} = LoginClaims;
    return <form onSubmit={Login}>
        <div>
            <label>Email</label>
            <input
                onChange={(e) =>
                    setLoginClaims({...LoginClaims, email: e.target.value})}
                value={email}
                type={"text"}></input>
        </div>
        <div>
            <label>Password</label>
            <input
                onChange={(e) =>
                    setLoginClaims({...LoginClaims, password: e.target.value})}
                value={password}
                type={"password"}></input>
        </div>
        <button className={"button-3"} type={"submit"}>Login</button>
    </form>;
}

export default observer(Login);