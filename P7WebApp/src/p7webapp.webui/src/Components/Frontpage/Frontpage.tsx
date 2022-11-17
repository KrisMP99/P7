import React, { useEffect, useState } from 'react';
import { Container } from 'react-bootstrap';
import './Frontpage.css';
import { useNavigate } from 'react-router-dom';
import { getApiRoot, User } from '../../App';

interface LoginProps {
    loggedIn: (user: User) => void;
    alreadyLoggedIn: boolean;
}

export default function Frontpage(props: LoginProps) {
    const navigator = useNavigate();
    useEffect(() => {
        if (props.alreadyLoggedIn) {
            navigator('/home');
        }
    });

    const [{ username, password }, setCredentials] = useState({
        username: '',
        password: ''
    })

    const [error, setError] = useState<string>();

    const login = (event: React.FormEvent) => {
        event.preventDefault();

        // props.loggedIn({id: 1, username: 's', email: 'a@a', firstname: 'a', lastname: 'b'});
        // return;
        if (username && password) {
            attemptLogin(username, password, (user) => {
                user ? props.loggedIn(user) : setError('Incorrect Username or Password');
            });
        } else {
            setError('Fill out Username or Password')
        }
        return;
    }

    return (
        <Container>
            <div className="row">

                {/* Left side of page */}
                <div className="col">
                    <div className="row mt-3">
                        <h3>Welcome to UniAcdademy!</h3>
                    </div>
                    <div className="row mt-5">
                        <div className="border rounded">
                            <p>Welcome to UniAcademy, your one stop for solving coding problems!</p>

                            <p> Lorem ipsum dolor sit amet, consectetur adipiscing elit.
                                Etiam sagittis tortor sit amet est fringilla imperdiet.
                                Quisque et velit dolor. Sed tempor massa dolor, eu condimentum lorem pretium sed.
                                Fusce gravida diam enim, vel interdum magna accumsan finibus.
                                Aliquam id erat faucibus, venenatis augue eu, malesuada erat.
                                In mattis quam quis justo sagittis, sed dictum sapien suscipit.
                                Nam congue viverra orci. Morbi rhoncus tortor elit, non condimentum odio elementum at.
                                Proin id faucibus velit, non sodales urna. </p>
                        </div>
                    </div>
                </div>



                {/* Right side of page */}
                <div className="col login-wrapper">
                    <form onSubmit={login} >
                        <div className="row mt-3">
                            <label>Username:</label>
                            <input type="text" required name='username'
                                value={username}
                                onChange={(event) => setCredentials({
                                    username: event.target.value,
                                    password
                                })} />
                        </div>

                        <div className="row mt-3">
                            <label>Password:</label>
                            <input type="password" required name='password'
                                value={password}
                                onChange={(event) => setCredentials({
                                    username,
                                    password: event.target.value
                                })} />
                        </div>
                        <div className="row mt-4">
                            <input className="button rounded" type="submit" value="Login" />
                        </div>
                        <div className='error-box mt-4 rounded'>
                            {error}
                        </div>
                    </form>
                    <div className="row mt-2">
                        <p>
                            Not registered? <a className='link' 
                                onMouseOver={(e)=>{e.currentTarget.style.cursor = 'pointer'}} 
                                onClick={()=>{navigator('/')}}
                            >
                                Sign up here!
                            </a>
                        </p>
                    </div>
                </div>
            </div>
        </Container>
    );
}

async function attemptLogin (username: string, password: string, callback: (user: User | null) => void) {
    try {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "username": username,
                "password": password
            })
        }
        await fetch(getApiRoot() + 'accounts/login', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error(res.statusText);
                }
                return res.json();
            })
            .then(({ user, token }: {user: User | null, token: string}) => {
                //WIP - SAVE TOKEN IN SESSION HERE
                callback(user);
            });
    } catch (error) {
        alert(error);
    }
}