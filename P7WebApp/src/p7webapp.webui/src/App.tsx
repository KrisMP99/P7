import React from 'react';
import './App.css';
import Router from './Components/Router/Router';
import Footer from './Components/Footer/Footer';
import Navbar from './Components/Navbar/Navbar';

function App() {
    return (
        <div>
            <Navbar />

            <div className="space-from-navbar">
                <Router />
            </div>

            <Footer/>
        </div>
    );
}

export default App;
