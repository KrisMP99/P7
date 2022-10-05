import React from 'react';
import { NavLink } from 'react-router-dom';

function Navbar(): JSX.Element {
    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container">
                <NavLink className="navbar-brand" to={'/'}>UniAcademy</NavLink>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav">
                        <li className="nav-item">
                            <NavLink className="nav-link" to={'/'}>Home</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to={'/signup'}>Signup</NavLink>
                        </li>
                        <li className="nav-item">
                            <NavLink className="nav-link" to={'/task/1'}>Exercise</NavLink>
                        </li>      
                    </ul>
                </div>
            </div>
        </nav>
    );
}

export default Navbar;