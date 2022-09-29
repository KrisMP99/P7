import React from 'react';
import logo from './logo.svg';
import './App.css';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import { TabContainer } from 'react-bootstrap';
import Router from './Components/Router/Router';

function App() {
    return (
        <div>
            <Router />
            <TabContainer>
                <nav>
                    HEJJEJE
                </nav>
            </TabContainer>
        </div>
    );
}

export default App;
