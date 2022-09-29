import React from 'react';
import { Route, Routes } from 'react-router-dom'
import Frontpage from '../Frontpage/Frontpage';
import SignUp from '../SignUp/SignUp';

/* 
 *  This component is responsible for the routing of pages.
 */ 
function Router() {
    return (
        <Routes>
            <Route path="/" element={<Frontpage />} />
            <Route path="/signup" element={<SignUp />} />
        </Routes>
    );
}

export default Router;