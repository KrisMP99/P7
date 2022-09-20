import React from 'react';
import logo from './logo.svg';
import './App.css';
import { HashRouter as Router, Route, Link, Switch } from 'react-router-dom'

function App() {
  return (
      <Router>
          <div>
              <nav>
                  <Link to="/">Home</Link>
                  <Link to="/foo">Foo</Link>
                  <Link to="/bar">Bar</Link>
              </nav>
              <Switch>
              </Switch>
          </div>
      </Router>
  );
}

export default App;
