import React, { useEffect, useState } from 'react'
import { Container, InputGroup, Button, Table, Pagination, Form } from 'react-bootstrap';
import { ArrowCounterclockwise } from 'react-bootstrap-icons';
import { Attendee } from '../Course/CourseView';

interface AttendeeOverviewProps {
    attendees: Attendee[];
    reFetchCourse: () => void;
}

export default function AttendeeOverview(props: AttendeeOverviewProps) {
    
    const [search, setSearch] = useState('');

    const [currentPage, setCurrentPage] = useState<number>(0);
    const [coursesPerPage, setCoursesPerPage] = useState<number>(10);
    const [maxPages, setMaxPages] = useState<number>(1);

    useEffect(() => {  
        // let endPage = Math.ceil(props.attendees.filter((attendee: Attendee) => { //WIP - Change userId to firstname and lastname below here
        //     return search.toLowerCase() === '' ? attendee : (attendee.courseId.toLowerCase().includes(search) || attendee.userId.toLowerCase().includes(search))
        // }).length / coursesPerPage);
        let endPage = Math.ceil(props.attendees.length / coursesPerPage);
        setMaxPages(endPage === 0 ? 1 : endPage);
    }, [props.attendees.length, coursesPerPage, search]);

    return (
        <Container>
            <div className="row justify-content-center">
                <div className='row col-7'>
                    <Form>
                        <InputGroup className='my-3'>
                            <Form.Control placeholder='Search for attendee name' onChange={(e) => {
                                setSearch(e.target.value.toLowerCase());
                            }} />
                        </InputGroup>
                    </Form>
                </div>

                <div className='row col-9 m-2'>
                    <div className='col text-start'>
                        <h3>Attendees:</h3>
                    </div>
                    <div className="col text-end d-flex justify-content-end">
                        <Button className="btn-2" onClick={() => {
                            props.reFetchCourse();
                        }}>
                            <ArrowCounterclockwise />
                        </Button>
                    </div>
                </div>

                <div className='col-9'>
                    <Table striped size='sm' bordered hover>
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Name</th>
                                <th>Total: {props.attendees.length}</th>
                            </tr>
                        </thead>
                        <tbody>
                            {/* {props.attendees.filter((attendee) => {
                                return search.toLowerCase() === '' ? attendee : attendee.userId.toLowerCase().includes(search)
                            }).slice(currentPage * coursesPerPage, (currentPage + 1) * coursesPerPage).map((item: Attendee, index) => (
                                <tr key={item.userId}>
                                    <td>{index + 1}</td>
                                    <td>{item.userId}</td>
                                    <td></td>
                                </tr>
                            ))} */}
                            {
                                props.attendees.slice(currentPage * coursesPerPage, (currentPage + 1) * coursesPerPage).map((item: Attendee, index) => (
                                    <tr key={item.userId}>
                                        <td>{index + 1}</td>
                                        <td>{item.userId}</td>
                                        <td></td>
                                    </tr>
                                ))
                            }
                        </tbody>
                    </Table>
                </div>
            </div>
            <div style={{ display: 'flex', justifyContent: 'center' }}>
                <Pagination>
                    <Pagination.Prev disabled={currentPage <= 0} onClick={() => setCurrentPage(currentPage - 1)} />

                    <Pagination.Item active={currentPage === 0}
                        onClick={() => {
                            if (currentPage !== 0)
                                setCurrentPage(0);
                        }}>
                        {1}
                    </Pagination.Item>

                    {currentPage >= 3 && <Pagination.Ellipsis disabled />}

                    {currentPage === maxPages - 1 && maxPages > 3 &&
                        <Pagination.Item
                            onClick={() => {
                                setCurrentPage(currentPage - 2)
                            }}>
                            {currentPage - 1}
                        </Pagination.Item>}

                    {currentPage >= 2 &&
                        <Pagination.Item
                            onClick={() => {
                                setCurrentPage(currentPage - 1)
                            }}>
                            {currentPage}
                        </Pagination.Item>}

                    {currentPage !== 0 && currentPage !== maxPages - 1 && maxPages !== 1 &&
                        <Pagination.Item
                            active
                            onClick={() => { }}>
                            {currentPage + 1}
                        </Pagination.Item>}

                    {currentPage < maxPages - 2 &&
                        <Pagination.Item
                            onClick={() => {
                                setCurrentPage(currentPage + 1);
                            }}>
                            {currentPage + 2}
                        </Pagination.Item>}

                    {currentPage === 0 && maxPages > 3 &&
                        <Pagination.Item
                            onClick={() => {
                                setCurrentPage(currentPage + 2);
                            }}>
                            {currentPage + 3}
                        </Pagination.Item>}

                    {currentPage < maxPages - 3 && <Pagination.Ellipsis disabled />}

                    {maxPages !== 1 &&
                        <Pagination.Item active={currentPage === maxPages - 1}
                            onClick={() => {
                                setCurrentPage(maxPages - 1);
                            }}>
                            {maxPages}
                        </Pagination.Item>}

                    <Pagination.Next disabled={currentPage >= maxPages - 1} onClick={() => setCurrentPage(currentPage + 1)} />
                </Pagination>
                <div style={{ marginLeft: '5px', height: '33.5px' }}>
                    <Form.Select size="sm" style={{ height: '100%' }} value={coursesPerPage}
                        onChange={(e) => {
                            setCoursesPerPage(Number(e.target.value));
                            setMaxPages(Math.ceil(props.attendees.length / Number(e.target.value)))
                            setCurrentPage(0);
                        }
                        }>
                        <option value={5}>5 per page</option>
                        <option value={10}>10 per page</option>
                        <option value={25}>25 per page</option>
                        <option value={50}>50 per page</option>
                        <option value={100}>100 per page</option>
                    </Form.Select>
                </div>
            </div>
        </Container>
    )
}
