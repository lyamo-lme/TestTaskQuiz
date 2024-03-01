import {useContext, useEffect, useState} from "react";
import {Context} from "../../store/context";
import {useNavigate} from "react-router-dom";
import {observable} from "mobx";
import './test.css';
import {observer} from "mobx-react-lite";

const Tests = observer(() => {
    const {testStore, authStore} = useContext(Context);
    const [loading, setLoading] = useState(true);
    const {userTests} = testStore;
    const {user} = authStore;
    const navigate = useNavigate();

    useEffect(() => {
        console.log(<user className="id"></user>);
        testStore.getUsersTests(user.id).then(() => setLoading(false));
    }, []);
    const Test = (test) =>
        <div className={"test-list"} key={test.id}>
            <div className={"test-ent"}>
                <p>{test.testName}</p>
                <button className={"button-3"} onClick={() => navigate(`/test/${test.id}`)}>Pass test</button>
            </div>
        </div>;
    return (<div>
        {loading ? "Loading" : userTests.map(test => Test(test))}
    </div>);
});


export default Tests;


