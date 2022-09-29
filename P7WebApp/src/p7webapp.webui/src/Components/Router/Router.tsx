import React from 'react';
import { Route, Routes } from 'react-router-dom'
import ExerciseBoardModule from '../ExerciseBoard/ExerciseBoard';
import Frontpage from '../Frontpage/Frontpage';
import ExerciseDescriptionModule from '../Modules/ExerciseDescription/ExerciseDescription';
import SignUp from '../SignUp/SignUp';

/* 
 *  This component is responsible for the routing of pages.
 */ 
function Router() {
    return (
        <Routes>
            <Route path="/" element={<Frontpage />} />
            <Route path="/signup" element={<SignUp />} />
            <Route path="/task/1" element={<ExerciseBoardModule />} />
        </Routes>
    );
}

export default Router;