import React, { useState, useEffect } from 'react';
import { Container, Table, Form, InputGroup, Pagination, Button, Spinner } from 'react-bootstrap';
import { ArrowCounterclockwise } from 'react-bootstrap-icons';
import { useNavigate } from 'react-router-dom';
import { getApiRoot } from '../../../App';
import { CourseOverview } from './OwnedCourseOverview';
import './OwnedCourseOverview.css';

interface AttendedCourseOverviewProps {

}

export default function AttendedCourseOverview(props: AttendedCourseOverviewProps): JSX.Element {

    const [isFetching, setIsFetching] = useState<boolean>(false);
    const [isFetchingInviteCode, setIsFetchingInviteCode] = useState<boolean>(false);
    const [errorText, setErrorText] = useState<string | null>(null);
    const [search, setSearch] = useState('');
    const [attendedCourses, setAttendedCourses] = useState<CourseOverview[]>([]);
    const [inviteCode, setInviteCode] = useState<number | undefined>(undefined);
    const [invalidInviteCode, setInvalidInviteCode] = useState<boolean>(false);

    const [currentPage, setCurrentPage] = useState<number>(0);
    const [coursesPerPage, setCoursesPerPage] = useState<number>(10);
    const [maxPages, setMaxPages] = useState<number>(1);

    const navigate = useNavigate();

    useEffect(() => {
        setIsFetching(true);
        fetchAttendedCourses((courses) => {
            courses ? setAttendedCourses(courses) : setErrorText('Could not fetch attending courses at the moment');
            setIsFetching(false);
        });
    }, []);

    useEffect(() => {
        setErrorText(null);
    }, [attendedCourses.length]);

    useEffect(() => {
        let endPage = Math.ceil(attendedCourses.filter((item: { title: string; }) => {
            return search.toLowerCase() === '' ? item : item.title.toLowerCase().includes(search)
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
                    <div className="col text-end d-flex justify-content-end">
                        <Form className='d-flex' onSubmit={(e) => {
                            e.preventDefault();
                            if (inviteCode && !isFetchingInviteCode) {
                                setIsFetchingInviteCode(true);
                                fetchCourseIdFromInviteCode(inviteCode, (courseId) => {
                                    courseId ?
                                        courseId === -1 ? setInvalidInviteCode(true) : navigate('/course/' + courseId) :
                                        setErrorText('Could not fetch course from invite code');
                                    setIsFetchingInviteCode(false);
                                })
                            }
                        }}>
                            <Form.Control
                                type='number'
                                defaultValue={inviteCode}
                                isInvalid={invalidInviteCode}
                                onChange={(e) => setInviteCode(Number(e.target.value))}
                            />
                            <Button className='btn-2' type='submit' disabled={(!inviteCode || isFetchingInviteCode)}>
                                {isFetchingInviteCode ?
                                    <Spinner
                                        as="span"
                                        animation="border"
                                        size="sm"
                                        role="status"
                                        aria-hidden="true"
                                    /> :
                                    'Find'
                                }
                            </Button>
                        </Form>
                        <Button className="btn-2" onClick={() => {
                            setIsFetching(true);
                            fetchAttendedCourses((courses) => {
                                courses ? setAttendedCourses(courses) : setErrorText('Could not fetch attending courses at the moment');
                                setIsFetching(false);
                            });
                        }}>
                            {isFetching ?
                            <Spinner
                                as="span"
                                animation="border"
                                size="sm"
                                role="status"
                                aria-hidden="true"
                            />
                            : <ArrowCounterclockwise />
                            }
                        </Button> 
                    </div>
                </div>

                <div className='col-9'>
                    <Table striped size='sm' bordered hover>
                        <thead>
                            <tr>
                                <th>Course name</th>
                                <th>Exercises</th>
                                <th>Members</th>
                                <th>Owner</th>
                            </tr>
                        </thead>
                        <tbody>
                            {attendedCourses.filter((item: { title: string; }) => {
                                return search.toLowerCase() === '' ? item : item.title.toLowerCase().includes(search)
                            }).slice(currentPage * coursesPerPage, (currentPage + 1) * coursesPerPage).map((item: CourseOverview) => (
                                <tr key={item.id} onClick={() => { navigate('/course/' + item.id) }}>
                                    <td>{item.title}</td>
                                    <td>{item.numberOfExercises ?? 0}</td>
                                    <td>{item.numberOfMembers ?? 0}</td>
                                    <td>{item.ownerName ?? 'Missing'}</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                </div>
            </div>
            {errorText && <div className='row justify-content-center' style={{color:'red', marginBottom:'15px', fontSize:'1.3rem'}}>{errorText}</div>}
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

async function fetchAttendedCourses(callback: (courses: CourseOverview[] | null) => void) {
    let jwt = sessionStorage.getItem('jwt');
    if (jwt === null) return;
    try {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            }
        }
        await fetch(getApiRoot() + 'profiles/courses/attends', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error(res.statusText);
                }
                return res.json();
            })
            .then((courses: CourseOverview[]) => {
                callback(courses);
            });
    } catch (error) {
        callback(null);
    }
}

async function fetchCourseIdFromInviteCode(inviteCode: number, callback: (courseId: number | null) => void) {
    let jwt = sessionStorage.getItem('jwt');
    if (jwt === null) return;
    try {
        const requestOptions = {
            method: 'GET',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            }
        }
        await fetch(getApiRoot() + 'courses/invite-code/' + inviteCode, requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend');
                }
                return res.json();
            })
            .then((coursesId: number) => {
                callback(coursesId);
            });
    } catch (error) {
        callback(null);
    }
}