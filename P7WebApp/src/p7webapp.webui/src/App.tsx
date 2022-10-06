import React, { useRef, useState } from 'react';
import './App.css';
import Footer from './Components/Footer/Footer';
import Navbar from './Components/Navbar/Navbar';
import { Routes, Route } from 'react-router-dom';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import ExerciseBoard, { BoardModuleType } from './Components/ExerciseBoard/ExerciseBoard';
import CreateExerciseModal, { ShowModal } from './Components/Modals/CreateExerciseModal/CreateExerciseModal';

function App() {
    const openCreateExerciseModalRef = useRef<ShowModal>(null);
    const [boardLayout, setBoardLayout] = useState<BoardModuleType[][]>([[BoardModuleType.ExerciseDescription]]);

    return (
        <div className='main-container'>
            <Navbar />
            {/* <button onClick={()=>{ openCreateExerciseModalRef.current?.handleShow() }}>Create Exercise</button> */}
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
                        boardLayout={boardLayout}
                    />
                } />
            </Routes>
            <Footer/>
            <CreateExerciseModal ref={openCreateExerciseModalRef} created={(newBoard: BoardModuleType[][])=>{ setBoardLayout(newBoard) }} />
        </div>
    );
}

export default App;
