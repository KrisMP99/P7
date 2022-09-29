import React from 'react';
import logo from './logo.svg';
import './App.css';
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';
import { TabContainer } from 'react-bootstrap';
import Router from './Components/Router/Router';

function App() {
    return (
        <div className='main-container'>
            <Router />
        </div>
    );
}

export default App;
