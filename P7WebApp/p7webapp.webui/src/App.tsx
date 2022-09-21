import React from 'react';
import logo from './logo.svg';
import './App.css';
import { HashRouter as Router, Route, Link, Routes } from 'react-router-dom'
import Frontpage from './Components/Frontpage/Frontpage';
import SignUp from './Components/SignUp/SignUp';

function App() {
  return (
          <div>
              <nav>
                  <Link to="/">Home</Link>
                  <Link to="/foo">Foo</Link>
              </nav>
              <Routes>
                  <Route path="/" element={<Frontpage />} />
                  <Route path="/foo" element={<SignUp />} />
              </Routes>
          </div>
  );
}

export default App;
