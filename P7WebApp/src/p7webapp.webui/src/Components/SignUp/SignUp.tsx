import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { getApiRoot } from '../../App';

interface NewUserForm {
    username: string;
    password: string;
    confirmPassword: string;
    email: string;
    firstname: string;
    lastname: string;
}

export default function SignUp(): JSX.Element {

    const emptyUser: NewUserForm = {username:'', password:'', confirmPassword:'', email:'', firstname:'', lastname:''};
    const [newUser, setNewUser] = useState<NewUserForm>(emptyUser);
    const [errorTxt, setErrorTxt] = useState<string | null>(null);
    const navigator = useNavigate();

    const resetPage = () => {
        setErrorTxt(null);
        setNewUser(emptyUser);
    }
    
    return (
        <div className="col login-wrapper">
            <div>
                <h1>Create account!</h1>
            </div>
            <form onSubmit={(e) => {
                e.preventDefault();
                if (newUser.password !== newUser.confirmPassword) {
                    setErrorTxt('Passwords do not match');
                } 
                else {
                    attemptSignUp(newUser, () => {
                        navigator('/');
                    });
                }
            }}>
                <div className="row mt-3">
                    <label>Username:</label>
                    <input type="text" required value={newUser.username} onChange={(e) => setNewUser({...newUser, username: e.target.value})} />
                </div>

                <div className="row mt-3">
                    <label>Firstname:</label>
                    <input type="text" required value={newUser.firstname} onChange={(e) => setNewUser({...newUser, firstname: e.target.value})} />
                </div>

                <div className="row mt-3">
                    <label>Lastname:</label>
                    <input type="text" required value={newUser.lastname} onChange={(e) => setNewUser({...newUser, lastname: e.target.value})} />
                </div>

                <div className="row mt-3">
                    <label>Email:</label>
                    <input type="email" required value={newUser.email} onChange={(e) => setNewUser({...newUser, email: e.target.value})} />
                </div>

                <div className="row mt-3">
                    <label>Password:</label>
                    <input type="password" required value={newUser.password} onChange={(e) => setNewUser({...newUser, password: e.target.value})} />
                </div>

                <div className="row mt-3">
                    <label>Confirm password:</label>
                    <input type="password" required value={newUser.confirmPassword} onChange={(e) => setNewUser({...newUser, confirmPassword: e.target.value})} />
                </div>

                {errorTxt &&
                 <div className="row mt-3" style={{color:'red'}}>
                    <span>{errorTxt}</span>
                </div>}

                <div className="row mt-5">
                    <input className="button rounded" type="submit" value="Sign up" />
                </div>
            </form>
            <div className="row mt-2">
                <p>Already registered? <a className='link' onMouseOver={(e)=>{e.currentTarget.style.cursor = 'pointer'}} onClick={()=>{navigator('/')}}>Login!</a> </p>
            </div>
        </div>
    );
}

async function attemptSignUp(newUser: NewUserForm, callback: () => void) {
    try {
        const requestOptions = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                "username": newUser.username,
                "password": newUser.password,
                "email": newUser.email,
                "firstname": newUser.firstname,
                "lastname": newUser.lastname
            })
        }
        await fetch(getApiRoot() + 'SignUp', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend - server unavailable');
                }
                return res.json();
            })
            .then(() => {
                console.log("Successfully signed up!");
                callback();
            });
    } catch (error) {
        alert(error);
    }
}