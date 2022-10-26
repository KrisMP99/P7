import React, { useState } from 'react';
import { Container, Table, Form, InputGroup, Button} from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import './OwnedCourseOverview.css';
import { ShowModal } from '../../Modals/CreateExerciseModal/CreateExerciseModal';

export interface CourseOverview {
    id: number;
    name: string;
    exerciseAmount: number;
    membersAmount: number;
    owner: string;
}

interface OwnedCourseOverviewProps {
    openCreateCourseModal: React.RefObject<ShowModal>;
    courses: CourseOverview[];
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
                        {props.courses.filter((item: { name: string; }) => {
                            return search.toLowerCase() === '' ? item : item.name.toLowerCase().includes(search)
                        }).map((item: CourseOverview) => (
                            <tr key={item.id} onClick={()=>{navigate('/course/' + item.id)}}>
                                <td>{item.name}</td>
                                <td>{item.exerciseAmount}</td>
                                <td>{item.membersAmount}</td>
                                <td>{item.owner}</td>
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