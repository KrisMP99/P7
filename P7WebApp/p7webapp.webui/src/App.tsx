import React from 'react';
import logo from './logo.svg';
import './App.css';
import { HashRouter as Router, Route, Link } from 'react-router-dom'

function App() {
  return (
      <Router>
          <div>
              <nav>
                  <Link to="/">Home</Link>
                  <Link to="/foo">Foo</Link>
                  <Link to="/bar">Bar</Link>
              </nav>
          </div>
      </Router>
  );
}

export default App;
