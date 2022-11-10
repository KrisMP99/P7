import React, { useEffect, useState } from 'react';
import { Container } from 'react-bootstrap';
import './Frontpage.css';
import { useNavigate } from 'react-router-dom';
import { getApiRoot } from '../../App';

interface LoginProps {
    loggedIn: () => void;
    alreadyLoggedIn: boolean;
}

export default function Frontpage(props: LoginProps) {
    const navigator = useNavigate();
    useEffect(() => {
        if (props.alreadyLoggedIn) {
            navigator('/home');
        }
    })

    const [{ username, password }, setCredentials] = useState({
        username: '',
        password: ''
    })

    const users = [{ username: 'Kristian', password: '1234' },
    { username: 'Jonas', password: '1234' }];

    const [error, setError] = useState<string>();



    const login = async (event: React.FormEvent) => {
        event.preventDefault();
        // const account = users.find((user) => user.username === username);

        // if (account && account.password === password) {
        //     props.loggedIn();
        //     navigate("/home");
        // } else {
        //     setError('Invalid Username or Password')
        // }
        // return;
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
            await fetch(getApiRoot() + 'Login', requestOptions)
                .then((res) => {
                    if (!res.ok) {
                        throw new Error('Response not okay from backend - server unavailable');
                    }
                    return res.json();
                })
                .then((user: any) => {
                    console.log("Successfully logged in!");
                    console.log(user);
                });
        } catch (error) {
            alert(error);
        }
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