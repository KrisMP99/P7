import React from 'react';
import './App.css';
import Footer from './Components/Footer/Footer';
import Navbar from './Components/Navbar/Navbar';
import { Routes, Route } from 'react-router-dom';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import ExerciseBoard from './Components/ExerciseBoard/ExerciseBoard';

function App() {
    return (
        <div className='main-container'>
            <Navbar />
            <Routes>
                <Route path="/" element={
                    <Frontpage />
                } />
                <Route path="/signup" element={
                    <SignUp />
                } />
                <Route path="/task/1" element={
                    <ExerciseBoard />
                } />
            </Routes>
            <Footer/>
        </div>
    );
}

export default App;
