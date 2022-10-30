import React, { useRef, useState } from 'react';
import './App.css';
import Footer from './Components/Footer/Footer';
import Navbar from './Components/Navbar/Navbar';
import { Routes, Route, useNavigate } from 'react-router-dom';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import ExerciseBoard from './Components/ExerciseBoard/ExerciseBoard';
import CreateExerciseModal, { Layout, LayoutType, ShowModal } from './Components/Modals/CreateExerciseModal/CreateExerciseModal';
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
    const [boardLayout, setBoardLayout] = useState<Layout>({layoutType: LayoutType.SINGLE, leftRows: 1, rightRows: 0});
    // const [user, setUser] = useState<User | null>(null);
    const navigator = useNavigate();

    return (
        <div className='main-container'>
            <Navbar />
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
                <Route path="/exercise/:id" element={
                    <ExerciseBoard 
                        editMode={false}
                        boardLayout={boardLayout}
                        
                    />
                } />
                <Route path="/landingpage" element={
                    <Landingpage />
                } />
                <Route path="/course/:id" element={
                    <Course 
                        user={{id: 0, name:'Kristian Morsing'}}
                        openCreateExerciseModalRef={openCreateExerciseModalRef}
                    />
                } />
            </Routes>
            {/* <Footer/> */}
            <CreateCourseModal ref={openCreateCourseModalRef}
            />
            <CreateExerciseModal ref={openCreateExerciseModalRef} created={(layout: Layout)=>{ 
                setBoardLayout(layout);
                navigator('/exercise/-1');
            }} />
        </div>
    );
}

export default App;
