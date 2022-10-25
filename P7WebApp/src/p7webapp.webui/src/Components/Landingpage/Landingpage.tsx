import React, { useState } from 'react';
import { Container, Table, Form, InputGroup} from 'react-bootstrap';
import { NavigateOptions, Route, Routes, useNavigate } from 'react-router-dom';
import Course from '../Course/Course';
import {dummyData} from './dummyData.js';
import Modal from '../Modals/CreateExerciseModal/CreateCourseModal';
import useModal from '../Modals/CreateExerciseModal/useModal';
import './Landingpage.css';

function Landingpage(): JSX.Element {
    const [search, setSearch] = useState('')
    const { isOpen, toggle } = useModal();
    console.log(search)
    const navigate = useNavigate();
    return (
        <Container>
            <Modal isOpen={isOpen} toggle={toggle}>
                <div className='d-flex justify-content-center'>
                    <div className="col-6 create-course-wrapper">
                        <div>
                            <h1>Create course!</h1>
                        </div>
                        <form>
                            <div className="row mt-3">
                                <label>Course name:</label>
                                <input className='rounded' type="text" />
                            </div>

                            <div className="row mt-3">
                                <label>Course description:</label>
                                <input className='rounded' type="text" />
                            </div>

                            <div className="row mt-3">
                                <label>Add Teaching Assistants:</label>
                                <input className='rounded' type="text" />
                            </div>

                            <div className="row mt-5">
                                <div className='col-6'>
                                    <input className="button rounded create" type="submit" value="Create course" />
                                </div>
                                <div className='col-6'>
                                    <input className="button rounded cancel" type="submit" value="Cancel" onClick={toggle} />
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </Modal>

            <div className="row justify-content-center">
                <h1 className='text-center mt-4'>Find Courses</h1>

                <div className='row col-7'>
                    <Form>
                        <InputGroup className='my-3'>
                            <Form.Control onChange={(e) => setSearch(e.target.value.toLowerCase())} placeholder='Search for course name'/>
                        </InputGroup>
                    </Form>
                </div>
                
                <div className='row col-9 m-2'>
                    <div className='col text-start'>
                        <h3>My courses</h3>
                    </div>
                    <div className="col text-end"> 
                        <button className="rounded p-2 create-course" onClick={toggle}>Create course</button>
                    </div>
                </div>

                <div className='col-9'>
                <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Course name</th>
                        <th>Exercises</th>
                        <th>Members</th>
                        <th>Owner</th>
                    </tr>
                </thead>
                <tbody>
                {dummyData.filter((item: { Course_name: string; }) => {
                    return search.toLowerCase() === '' ? item : item.Course_name.toLowerCase().includes(search)
                }).map((item: { id: React.Key | null | undefined; Course_name: string | number | boolean | React.ReactElement<any, string | React.JSXElementConstructor<any>> | React.ReactFragment | React.ReactPortal | null | undefined; Exercises: string | number | boolean | React.ReactElement<any, string | React.JSXElementConstructor<any>> | React.ReactFragment | React.ReactPortal | null | undefined; Members: string | number | boolean | React.ReactElement<any, string | React.JSXElementConstructor<any>> | React.ReactFragment | React.ReactPortal | null | undefined; Owner: string | number | boolean | React.ReactElement<any, string | React.JSXElementConstructor<any>> | React.ReactFragment | React.ReactPortal | null | undefined; }) => (
                    <tr key={item.id} onClick={()=>{navigate('/course/' + item.id)}}>
                        <td>{item.Course_name}</td>
                        <td>{item.Exercises}</td>
                        <td>{item.Members}</td>
                        <td>{item.Owner}</td>
                    </tr>
                ))}

                </tbody>
            </Table>
                </div>
            </div>
            
        </Container>
    );
}

export default Landingpage;