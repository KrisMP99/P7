import React from 'react';
import './App.css';
import Router from './Components/Router/Router';
import Footer from './Components/Footer/Footer';
import Navbar from './Components/Navbar/Navbar';

function App() {
    return (
        <div className='main-container'>
            <Navbar />
            <Router />
            <Footer/>
        </div>
    );
}

export default App;
