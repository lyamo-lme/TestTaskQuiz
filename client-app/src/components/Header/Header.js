import {NavLink} from "react-router-dom";
import './header.css';
export const Header = () => {
    return (
        <nav className={"nav-menu"}>
            <ul>
                <li>
                    <NavLink to="/login">Login</NavLink>
                </li>
                <li>
                    <NavLink to="/tests">Tests</NavLink>
                </li>
            </ul>
        </nav>)
};