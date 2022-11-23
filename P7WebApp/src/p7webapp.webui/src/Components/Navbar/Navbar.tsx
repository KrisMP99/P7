import React from 'react';
import { NavLink } from 'react-router-dom';
import { User } from '../../App';
import './Navbar.css';

interface NavbarProps {
    user: User | null;
    logOut: () => void;
}

function Navbar(props: NavbarProps): JSX.Element {
    return (
        <nav className="navbar navbar-expand-lg navbar-light">
            <div className="container">  
                <NavLink className="navbar-brand" to={ props.user ? 'home' : '/' }>UniAcademy</NavLink>
                <button className="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav flex-grow-1"> 
                        <li className="nav-item">
                            <NavLink className="nav-link" to={ props.user ? 'home' : '/' }>Home</NavLink>
                        </li>
                        {props.user && <li className="nav-item">
                            <NavLink className="nav-link" to={ props.user ? 'public-courses' : '/' }>All Courses</NavLink>
                        </li>}
                        {!props.user ?
                            <li className="nav-item">
                                <NavLink className="nav-link" to={'/signup'}>Signup</NavLink>
                            </li> :
                            <li style={{marginLeft: 'auto'}} className="nav-item">
                                <NavLink className="nav-link" to={'/'} onClick={() => props.logOut()}>Log out</NavLink>
                            </li>
                        }   
                    </ul>
                </div>
            </div>
        </nav>
    );
}

export default Navbar;