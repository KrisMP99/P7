import React, { useState } from 'react';
import { Container, Table, Form, InputGroup, Button} from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import {dummyData} from './dummyDataOwned';
import './OwnedCourseOverview.css';
import { ShowModal } from '../../Modals/CreateExerciseModal/CreateExerciseModal';

interface OwnedCourseOverviewProps {
    openCreateCourseModal: React.RefObject<ShowModal>;
}

function OwnedCourseOverview(props: OwnedCourseOverviewProps): JSX.Element {
    const [search, setSearch] = useState('');
    const navigate = useNavigate();

    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <Container>
            <div className="row justify-content-center">
                <h1 className='text-center'>Find Courses</h1>

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
                        <Button className="rounded p-2 create-course" onClick={()=>props.openCreateCourseModal.current?.handleShow()}>
                            Create course
                        </Button> 
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

export default OwnedCourseOverview;