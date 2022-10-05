import React from 'react';

function SignUp(): JSX.Element {
    return (
        
        <div className="col login-wrapper">
            <div>
                <h1>Create account!</h1>
            </div>
            <form>
                <div className="row mt-3">
                    <label>Username:</label>
                    <input type="text" />
                </div>

                <div className="row mt-3">
                    <label>Name:</label>
                    <input type="text" />
                </div>

                <div className="row mt-3">
                    <label>Email:</label>
                    <input type="text" />
                </div>

                <div className="row mt-3">
                    <label>Password:</label>
                    <input type="password" />
                </div>

                <div className="row mt-3">
                    <label>Confirm password:</label>
                    <input type="password" />
                </div>

                <div className="row mt-5">
                    <input className="button rounded" type="submit" value="Sign up" />
                </div>
            </form>
            <div className="row mt-2">
                <p>Already registered? <a href="/">Login!</a> </p>
            </div>
        </div>
    );
}

export default SignUp;