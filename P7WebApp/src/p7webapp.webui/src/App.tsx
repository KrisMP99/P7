import React from 'react';
import './App.css';
import Footer from './Components/Footer/Footer';
import Navbar from './Components/Navbar/Navbar';
import { Routes, Route } from 'react-router-dom';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import ExerciseBoard, { BoardModuleType } from './Components/ExerciseBoard/ExerciseBoard';
import Landingpage from './Components/Landingpage/Landingpage';

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
                    <ExerciseBoard 
                        creatorMode={false}
                        boardLayout={[[BoardModuleType.ExerciseDescription], [BoardModuleType.ExerciseDescription]]}
                    />
                } />
                <Route path="/ladningpage" element={
                    <Landingpage />
                } />
            </Routes>
            <Footer/>
        </div>
    );
}

export default App;
