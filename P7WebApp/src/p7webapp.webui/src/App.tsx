import React, { useRef, useState } from 'react';
import './App.css';
import Navbar from './Components/Navbar/Navbar';
import { Routes, Route, useNavigate } from 'react-router-dom';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import ExerciseBoard from './Components/ExerciseBoard/ExerciseBoard';
import CreateExerciseModal, { LayoutType, ShowModal } from './Components/Modals/CreateExerciseModal/CreateExerciseModal';
import Landingpage from './Components/Landingpage/Landingpage';
import CourseView, { Exercise } from './Components/Course/Course';

export interface User {
    id: number;
    firstname: string;
    lastname: string;
    email: string;
    username: string;
}

function App() {
    const openCreateExerciseModalRef = useRef<ShowModal>(null);
    
    const [loggedIn, setLoggedIn] = useState<boolean>(false);
    const [boardLayout, setBoardLayout] = useState<LayoutType>(LayoutType.SINGLE);
    const [user, setUser] = useState<User | null>(null);
    const [newExerciseCreated, setNewExerciseCreated] = useState<Exercise | null>(null);
    const navigator = useNavigate();

    return (
        <div className='main-container'>
            <Navbar
                user={user}
                logOut={() => {
                    setUser(null);
                    setLoggedIn(false);
                    navigator('/');
                }}
            />
            <Routes>
                <Route path="/" element={
                    <div className="space-from-navbar">
                        <Frontpage
                            loggedIn={() => {
                                setLoggedIn(true);
                                setUser({ id: 0, firstname: "Jonas", lastname: "Noermark", email: "dummy@mail.dk", username: "jonse" });
                            }}
                            alreadyLoggedIn={loggedIn}
                        />
                    </div>
                } />
                <Route path="/signup" element={
                    <div className="space-from-navbar">
                        <SignUp />
                    </div>
                } />
                {user && loggedIn && <><Route path="/exercise/:id" element={
                    <ExerciseBoard
                        editMode={false}
                        boardLayout={boardLayout}
                        newExercise={newExerciseCreated}
                    />
                } />
                    <Route path="/home" element={
                        <Landingpage />
                    } />
                    <Route path="/course/:id" element={
                        <CourseView
                            user={user!}
                            openCreateExerciseModalRef={openCreateExerciseModalRef}
                        />
                    } /></>}
            </Routes>
            {/* <Footer/> */}
            <CreateExerciseModal ref={openCreateExerciseModalRef} created={(layout: LayoutType, exercise: Exercise) => {
                setBoardLayout(layout);
                setNewExerciseCreated(exercise);
                navigator('/exercise/-1');
            }} />
        </div>
    );
}

export default App;
