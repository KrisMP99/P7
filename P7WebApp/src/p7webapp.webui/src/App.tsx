import React, { useRef, useState } from 'react';
import './App.css';
import Footer from './Components/Footer/Footer';
import Navbar from './Components/Navbar/Navbar';
import { Routes, Route, useNavigate  } from 'react-router-dom';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import ExerciseBoard, { BoardModuleType } from './Components/ExerciseBoard/ExerciseBoard';
import CreateExerciseModal, { ShowModal } from './Components/Modals/CreateExerciseModal/CreateExerciseModal';
import CreateCourseModal from './Components/Modals/CreateCourseModal/CreateCourseModal';
import Landingpage from './Components/Landingpage/Landingpage';
import Course from './Components/Course/Course';

export interface User {
    id: number;
    name: string;
}

function App() {
    const openCreateExerciseModalRef = useRef<ShowModal>(null);
    const openCreateCourseModalRef = useRef<ShowModal>(null);
    const [boardLayout, setBoardLayout] = useState<BoardModuleType[][]>([[BoardModuleType.ExerciseDescription]]);
    const [user, setUser] = useState<User | null>(null);

    return (
        <div className='main-container'>
            <Navbar />
            {/* <button onClick={()=>{ openCreateExerciseModalRef.current?.handleShow() }}>Create Exercise</button> */}
            <Routes>
                <Route path="/" element={
                    <div className="space-from-navbar">
                        <Frontpage />
                    </div>
                } />
                <Route path="/signup" element={
                    <div className="space-from-navbar">
                        <SignUp />
                    </div>
                } />
                <Route path="/task/1" element={
                    <ExerciseBoard 
                        creatorMode={false}
                        boardLayout={boardLayout}
                    />
                } />
                <Route path="/landingpage" element={
                    <Landingpage />
                } />
                <Route path="/course/:id" element={
                    <Course 
                        user={{id: 0, name:'Kristian Morsing'}}
                    />
                } />
            </Routes>
            {/* <Footer/> */}
            <CreateExerciseModal ref={openCreateExerciseModalRef} created={(newBoard: BoardModuleType[][])=>{ setBoardLayout(newBoard) }} />
            <CreateCourseModal ref={openCreateCourseModalRef}
            />
        </div>
    );
}

export default App;
