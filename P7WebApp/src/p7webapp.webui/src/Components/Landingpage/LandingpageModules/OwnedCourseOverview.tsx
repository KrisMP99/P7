import React, { useEffect, useRef, useState } from 'react';
import { Container, Table, Form, InputGroup, Button, Pagination} from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import './OwnedCourseOverview.css';
import { ShowModal } from '../../Modals/CreateExerciseModal/CreateExerciseModal';
import { Trash } from 'react-bootstrap-icons';
import DeleteConfirmModal, { DeleteElementType, ShowDeleteConfirmModal } from '../../Modals/DeleteConfirmModal/DeleteConfirmModal';
import { getApiRoot } from '../../../App';
import CreateCourseModal from '../../Modals/CreateCourseModal/CreateCourseModal';

export interface CourseOverview {
    id: number;
    name: string;
    exerciseAmount: number;
    membersAmount: number;
    owner: string;
    isPrivate: boolean;
}

interface OwnedCourseOverviewProps {

}

export default function OwnedCourseOverview(props: OwnedCourseOverviewProps): JSX.Element {
    const openCreateCourseModalRef = useRef<ShowModal>(null);

    const [search, setSearch] = useState('');
    const navigate = useNavigate();
    const deleteCourseModalRef = useRef<ShowDeleteConfirmModal>(null);
    const [ownedCourses, setOwnedCourses] = useState<CourseOverview[]>([]);

    const [currentPage, setCurrentPage] = useState<number>(0);
    const [coursesPerPage, setCoursesPerPage] = useState<number>(5);
    const [maxPages, setMaxPages] = useState<number>(1);

    useEffect(() => {
        // fetchOwnedCourses((courses) =>{
        //     setOwnedCourses(courses);
        // });
    }, []);

    useEffect(() => {
        let endPage = Math.ceil(ownedCourses.filter((item: { name: string; }) => {
            return search.toLowerCase() === '' ? item : item.name.toLowerCase().includes(search)
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
                        <Button className="rounded p-2 create-course" onClick={()=>openCreateCourseModalRef.current?.handleShow()}>
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
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                        {ownedCourses.filter((item: { name: string; }) => {
                            return search.toLowerCase() === '' ? item : item.name.toLowerCase().includes(search)
                        }).slice(currentPage * coursesPerPage, (currentPage+1)*coursesPerPage).map((item: CourseOverview) => (
                            <tr key={item.id} onClick={()=>{navigate('/course/' + item.id)}}>
                                <td>{item.name}</td>
                                <td>{item.exerciseAmount}</td>
                                <td>{item.membersAmount}</td>
                                <td>{item.owner}</td>
                                <td>
                                    <Button variant='danger' onClick={(e)=>{
                                        e.stopPropagation();
                                        deleteCourseModalRef.current?.handleShow(item.name, item.id, DeleteElementType.COURSE);
                                    }}>
                                        <Trash/>
                                    </Button>
                                </td>
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
                        // fetchOwnedCourses((courses) => setOwnedCourses(courses));
                    });
                }}                
            />
            <CreateCourseModal 
                ref={openCreateCourseModalRef}
                createdCourse={() => {
                    console.log("HHH")
                    // fetchOwnedCourses((courses) => {
                    //     setOwnedCourses(courses);
                    // });
                }}
            />
        </Container>
    );
}

async function fetchOwnedCourses(callback: (courses: CourseOverview[]) => void) {
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
        await fetch(getApiRoot() + 'users/courses', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend');
                }
                return res.json();
            })
            .then((data) => {
                console.log(data);
            });
            // .then((ownedCourses: CourseOverview[]) => {
            //     callback(ownedCourses);
            // });
    } catch (error) {
        alert(error);
    }
}

async function deleteOwnedCourse(courseId: number, callback: () => void) {
    try {
        const requestOptions = {
            method: 'POST',
            headers: { 
                'Accept': 'application/json', 
                'Content-Type': 'application/json',
                'Authorization': '' //WIP - SET AUTH
            },
            body: JSON.stringify({
                'id': courseId
            })
        }
        await fetch(getApiRoot() + 'Course/get-assigned-courses', requestOptions)
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