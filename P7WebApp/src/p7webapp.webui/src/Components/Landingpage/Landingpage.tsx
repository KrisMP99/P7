import React, { useState } from 'react';
import { Container, Table, Form, InputGroup} from 'react-bootstrap';
import {dummyData} from './dummyData.js';

function Landingpage(): JSX.Element {
    const [search, setSearch] = useState('')
    console.log(search)
    return (
        <Container>
            <h1 className='text-center mt-4'>Find Courses</h1>
            <Form>
                <InputGroup className='my-3'>
                    <Form.Control onChange={(e) => setSearch(e.target.value)} placeholder='Search for course name'/>
                </InputGroup>
            </Form>

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
                {dummyData.filter((item) => {
                    return search.toLowerCase() === '' ? item : item.Course_name.toLowerCase().includes(search)
                }).map((item) => (
                    <tr key={item.id}>
                        <td>{item.Course_name}</td>
                        <td>{item.Exercises}</td>
                        <td>{item.Members}</td>
                        <td>{item.Owner}</td>
                    </tr>
                ))}

                </tbody>
            </Table>

        </Container>
    );
}

export default Landingpage;