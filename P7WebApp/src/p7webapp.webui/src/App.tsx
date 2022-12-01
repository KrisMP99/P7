import React, { useState } from 'react';
import './App.css';
import Navbar from './Components/Navbar/Navbar';
import { Routes, Route, useNavigate } from 'react-router-dom';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import Landingpage from './Components/Landingpage/Landingpage';
import CourseView from './Components/Course/CourseView';
import PublicCourses from './Components/PublicCourses/PublicCourses';
import ExerciseViewHandler from './Components/ExerciseViewHandler/ExerciseViewHandler';

const ServerUrl = "http://130.225.39.193/api/";
const LocalHost = "https://localhost:7001/api/"

export function getApiRoot() {
    return ServerUrl;
}

export interface User {
    id: number;
    firstname: string;
    lastname: string;
    email: string;
    username: string;
}

export default function App() {
    
    const [loggedIn, setLoggedIn] = useState<boolean>(false);
    const [user, setUser] = useState<User | null>(null);
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
                            loggedIn={(user: User) => {
                                setLoggedIn(true);
                                setUser(user);
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
                {user && loggedIn && 
                <>
                    <Route path="/home" element={
                        <Landingpage 
                            user={user}
                        />
                    } />
                    <Route path="/course/:courseId" element={
                        <CourseView
                            user={user}
                        />
                    } />
                    <Route path="/exercise/:exerciseGroupId/:exerciseId/:isEdit" element={
                        <ExerciseViewHandler user={user} />
                    } />
                    <Route path="/public-courses" element={
                        <PublicCourses />
                    } />
                </>}
            </Routes>
        </div>
    );
}