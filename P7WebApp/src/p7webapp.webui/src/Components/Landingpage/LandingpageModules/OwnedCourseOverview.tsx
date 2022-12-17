import React, { useEffect, useRef, useState } from 'react';
import { Container, Table, Form, InputGroup, Button, Pagination, Spinner} from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import './OwnedCourseOverview.css';
import { ShowModal } from '../../Modals/CreateExerciseModal/CreateExerciseModal';
import { ArrowCounterclockwise, Pencil, Trash } from 'react-bootstrap-icons';
import DeleteConfirmModal, { DeleteElementType, ShowDeleteConfirmModal } from '../../Modals/DeleteConfirmModal/DeleteConfirmModal';
import { getApiRoot } from '../../../App';
import CreateCourseModal from '../../Modals/CreateCourseModal/CreateCourseModal';
import EditCourseModal, { ShowEditCourseModal } from '../../Modals/EditCourseModal/EditCourseModal';

export interface CourseOverview {
    id: number;
    title: string;
    numberOfExercises: number;
    numberOfMembers: number;
    ownerName: string | null;
    isPrivate: boolean;
    createdDate: Date;
    modifiedDate: Date;
}

interface OwnedCourseOverviewProps {

}

export default function OwnedCourseOverview(props: OwnedCourseOverviewProps): JSX.Element {
    const openCreateCourseModalRef = useRef<ShowModal>(null);

    const [isFetching, setIsFetching] = useState<boolean>(false);
    const [errorText, setErrorText] = useState<string | null>(null);
    const [search, setSearch] = useState('');
    const navigate = useNavigate();
    const deleteCourseModalRef = useRef<ShowDeleteConfirmModal>(null);
    const editCourseModalRef = useRef<ShowEditCourseModal>(null);
    const [ownedCourses, setOwnedCourses] = useState<CourseOverview[]>([]);

    const [currentPage, setCurrentPage] = useState<number>(0);
    const [coursesPerPage, setCoursesPerPage] = useState<number>(10);
    const [maxPages, setMaxPages] = useState<number>(1);

    useEffect(() => {
        setIsFetching(true)
        fetchOwnedCourses((courses) =>{
            courses ? setOwnedCourses(courses) : setErrorText('Could not fetch your own courses at the moment');
            setIsFetching(false);
        });
    }, []);

    useEffect(() => {
        setErrorText(null);
    }, [ownedCourses.length]);

    useEffect(() => {
        let endPage = Math.ceil(ownedCourses.filter((item: { title: string; }) => {
            return search.toLowerCase() === '' ? item : item.title.toLowerCase().includes(search)
        }).length / coursesPerPage)
        setMaxPages(endPage === 0 ? 1 : endPage);
    }, [ownedCourses.length, coursesPerPage, search]);

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
                        <Button className="btn-2" onClick={()=>openCreateCourseModalRef.current?.handleShow()}>
                            Create course
                        </Button> 
                        <Button className="btn-2" disabled={isFetching} onClick={()=>{
                            setIsFetching(true);
                            fetchOwnedCourses((courses) => {
                                courses ? setOwnedCourses(courses) : setErrorText('Could not fetch your own courses at the moment');
                                setIsFetching(false);
                            })
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
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        {
                            ownedCourses.filter((item: { title: string; }) => {
                                return search.toLowerCase() === '' ? item : item.title.toLowerCase().includes(search)
                            })
                            .slice(currentPage * coursesPerPage, (currentPage+1)*coursesPerPage)
                            .map((item: CourseOverview) => (
                                <tr key={item.id} onClick={()=>{navigate('/course/' + item.id)}}>
                                    <td>{item.title}</td>
                                    <td>{item.numberOfExercises ?? 0}</td>
                                    <td>{item.numberOfMembers ?? 0}</td>
                                    <td>{item.ownerName ?? 'Missing'}</td>
                                    <td className="d-flex justify-content-center">
                                        <Button size='sm' className="btn-3" onClick={(e)=>{
                                            e.stopPropagation();
                                            editCourseModalRef.current?.handleShow(item.id);
                                        }}>
                                            <Pencil/>
                                        </Button>
                                        <Button variant='danger' className="btn-3" size='sm' onClick={(e)=>{
                                            e.stopPropagation();
                                            deleteCourseModalRef.current?.handleShow(item.title, item.id, DeleteElementType.COURSE);
                                        }}>
                                            <Trash/>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        }
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
                            setMaxPages(Math.ceil(ownedCourses.length / Number(e.target.value)))
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
            <DeleteConfirmModal 
                ref={deleteCourseModalRef}
                confirmDelete={(courseId: number)=>{
                    deleteOwnedCourse(courseId, () => {
                        setIsFetching(true);
                        fetchOwnedCourses((courses) => {
                            courses ? setOwnedCourses(courses) : setErrorText('Could not fetch your own courses at the moment');;
                            setIsFetching(false);
                        });
                    });
                }}                
            />
            <EditCourseModal
                ref={editCourseModalRef}
                updatedCourse={()=>{
                    setIsFetching(true);
                    fetchOwnedCourses((courses) => {
                        courses ? setOwnedCourses(courses) : setErrorText('Could not fetch your own courses at the moment');
                        setIsFetching(false);
                    })
                }}
            />
            <CreateCourseModal 
                ref={openCreateCourseModalRef}
                createdCourse={() => {
                    setIsFetching(true);
                    fetchOwnedCourses((courses) => {
                        courses ? setOwnedCourses(courses) : setErrorText('Could not fetch your own courses at the moment');
                        setIsFetching(false);
                    });
                }}
            />
        </Container>
    );
}

async function fetchOwnedCourses(callback: (courses: CourseOverview[] | null) => void) {
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
        await fetch(getApiRoot() + 'profiles/courses/created', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend');
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

async function deleteOwnedCourse(courseId: number, callback: () => void) {
    let jwt = sessionStorage.getItem('jwt');
    if (jwt === null) return;
    try {
        const requestOptions = {
            method: 'DELETE',
            headers: { 
                'Accept': 'application/json', 
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            }
        }
        await fetch(getApiRoot() + 'courses/' + courseId, requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend');
                }
                return res.json();
            })
            .then(() => {
                callback();
            });
    } catch (error) {
        alert(error);
    }
}