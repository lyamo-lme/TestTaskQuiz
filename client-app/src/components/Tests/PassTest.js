import {useContext, useEffect, useState} from "react";
import {Context} from "../../store/context";
import {useNavigate, useParams} from "react-router-dom";
import {observable} from "mobx";
import {observer} from "mobx-react-lite";
import tests from "./Tests";

const PassTest = observer(() => {
    const {testStore, authStore} = useContext(Context);
    const {testId} = useParams();
    const navigation = useNavigate();
    const {user} = authStore;
    const {currentAttempt, userTests, access} = testStore;
    const [taskNumber, setTaskNumber] = useState(1);
    useEffect(() => {
        try {
            testStore.beginAttempt(user.id, testId);
        } catch (e) {
            console.log(e);
        }
    }, []);
    useEffect(() => {

    }, [taskNumber])

    const nextQuestion = () => {
        let nextQuestionIndex = taskNumber + 1;
        let countQuestions = JSON.parse(JSON.stringify(userTests)).find(x => x.id == testId).countQuestions;

        if (nextQuestionIndex > countQuestions) {
            navigation("/");
            console.log(nextQuestionIndex);
            return;
        }
        setTaskNumber(nextQuestionIndex);
    }
    const Attempt = () => {
        return currentAttempt != null ? <div>
            <h2 className={"test-name"}>{currentAttempt.test.testName}</h2>
            <p className={"test-question"}>Question: {currentAttempt.test.testQuestions[taskNumber - 1].questionText}</p>
            <div>
                <p className={"test-question"}>Answers</p>
                <div>
                    {currentAttempt
                        .test
                        .testQuestions[taskNumber - 1]
                        .questionAnswer.map((answer =>
                            <div key={answer.id} className={"test-question answer-option"}
                                 onClick={() => {
                                     testStore.choseAnswer(user.id, answer.id);
                                     nextQuestion();
                                 }}>
                                {answer.answerText}
                            </div>))}
                </div>
            </div>

            <button className={"button-3"}
                    onClick={() =>
                        setTaskNumber(taskNumber - 1 === 0 ? taskNumber : taskNumber - 1)}>Back
            </button>
            <button className={"button-3"}
                    onClick={() => {
                        nextQuestion();
                    }}>Next
            </button>

        </div> : "Loading"
    }
    return (
        <>
            <div>
                {Attempt()}
            </div>
        </>);
});

export default PassTest;