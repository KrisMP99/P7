import React from 'react';
import { Container } from 'react-bootstrap';
import './Frontpage.css';
import { NavLink } from 'react-router-dom';


function Frontpage() {

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
                        <form >
                            <div className="row mt-3">
                                <label>Email or username:</label>
                                <input type="text" />
                            </div>

                            <div className="row mt-3">
                                <label>Password:</label>
                                <input type="password" />
                            </div>

                            <div className="row mt-4">
                                <input className="button rounded" type="submit" value="Login" />
                            </div>
                        </form>
                        <div className="row mt-2">
                            <p>Not registered? <NavLink to="/signup">Sign up here!</NavLink> </p>
                        </div>
                    </div>
                </div>
        </Container>
    );
}


export default Frontpage;