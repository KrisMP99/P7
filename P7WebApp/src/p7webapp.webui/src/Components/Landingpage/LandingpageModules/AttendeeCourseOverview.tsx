import React, { useState, useEffect } from 'react';
import { Container, Table, Form, InputGroup, Pagination } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import { getApiRoot } from '../../../App';
import { CourseOverview } from './OwnedCourseOverview';
import './OwnedCourseOverview.css';

interface AttendedCourseOverviewProps {
    
}

export default function AttendedCourseOverview(props: AttendedCourseOverviewProps): JSX.Element {
    
    const [search, setSearch] = useState('');
    const [attendedCourses, setAttendedCourses] = useState<CourseOverview[]>([]);

    const [currentPage, setCurrentPage] = useState<number>(0);
    const [coursesPerPage, setCoursesPerPage] = useState<number>(5);
    const [maxPages, setMaxPages] = useState<number>(1);

    const navigate = useNavigate();

    useEffect(() => {
        // fetchAttendedCourses((courses) =>{
        //     setAttendedCourses(courses);
        // });
    }, []);
    
    useEffect(() => {  
        let endPage = Math.ceil(attendedCourses.filter((item: { name: string; }) => {
            return search.toLowerCase() === '' ? item : item.name.toLowerCase().includes(search)
        }).length / coursesPerPage);
        setMaxPages(endPage === 0 ? 1 : endPage);
    }, [attendedCourses.length, coursesPerPage, search]);

    return (
        <Container>
            <div className="row justify-content-center">
                <h1 className='text-center'>Find Courses</h1>

                <div className='row col-7'>
                    <Form>
                        <InputGroup className='my-3'>
                            <Form.Control placeholder='Search for course name' onChange={(e) => { 
                                setSearch(e.target.value.toLowerCase());
                            }} />
                        </InputGroup>
                    </Form>
                </div>

                <div className='row col-9 m-2'>
                    <div className='col text-start'>
                        <h3>Attended courses</h3>
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
                            {attendedCourses.filter((item: { name: string; }) => {
                                return search.toLowerCase() === '' ? item : item.name.toLowerCase().includes(search)
                            }).slice(currentPage * coursesPerPage, (currentPage+1)*coursesPerPage).map((item: CourseOverview) => (
                                <tr key={item.id} onClick={() => { navigate('/course/' + item.id) }}>
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
                            setMaxPages(Math.ceil(attendedCourses.length / Number(e.target.value)))
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
    );
}

async function fetchAttendedCourses(callback: (courses: CourseOverview[]) => void) {
    console.log("FetchAttendedCourses");
    //let jwt = sessionStorage.getItem('jwt');
    //if (jwt === null) return;
    //try {
    //    const requestOptions = {
    //        method: 'POST',
    //        headers: { 
    //            'Accept': 'application/json', 
    //            'Content-Type': 'application/json',
    //            'Authorization': 'Bearer ' + jwt
    //        }
    //    }
    //    await fetch(getApiRoot() + 'users/courses', requestOptions)
    //        .then((res) => {
    //            if (!res.ok) {
    //                throw new Error('Response not okay from backend');
    //            }
    //            return res.json();
    //        })
    //        .then((data) => {
    //            console.log("ATTENDED (CHANGE LATER IS ALL COURSES NOW)");
    //            // console.log(data)
    //        });
    //        // .then((ownedCourses: CourseOverview[]) => {
    //        //     callback(ownedCourses);
    //        // });
    //} catch (error) {
    //    alert(error);
    //}
}