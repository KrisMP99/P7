import React, { useEffect, useRef, useState } from 'react';
import { Button, Container, OverlayTrigger, Tooltip } from 'react-bootstrap';
import { getApiRoot, User } from '../../App';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import './CourseView.css';
import { ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import ExerciseGroupsOverview from './ExerciseGroupsOverview/ExerciseGroupsOverview';
import DeleteConfirmModal, { DeleteElementType, ShowDeleteConfirmModal } from '../Modals/DeleteConfirmModal/DeleteConfirmModal';
import { Gear, Plus } from 'react-bootstrap-icons';
import CreateExerciseGroupModal, { ShowCreateExerciseGroupModal } from '../Modals/CreateExerciseGroupModal/CreateExerciseGroupModal';
import { useNavigate, useParams } from 'react-router-dom';

export interface ExerciseOverview {
    id: number;
    title: string;
    isVisible: boolean;
}

export interface ExerciseGroup {
    id: number;
    title: string;
    isVisible: boolean;
    exercises: ExerciseOverview[];
}

export interface Exercise {
    id: number;
    exerciseGroupId: number;
    title: string;
    isVisible: boolean;
}

export interface Course {
    id: number;
    title: string;
    description: string;
    isPrivate: boolean;
    createdById: string;
    ownerName: string | null;
    exerciseGroups: ExerciseGroup[];
    createdDate: Date | null;
    modifiedDate: Date | null;
}

interface CourseProps {
    user: User;
    openCreateExerciseModalRef: React.RefObject<ShowModal>;
}

export default function CourseView(props: CourseProps) {

    const openDeleteExerciseModalRef = useRef<ShowDeleteConfirmModal>(null);
    const createExerciseGroupModalRef = useRef<ShowCreateExerciseGroupModal>(null);

    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [course, setCourse] = useState<Course | null>(null);
    const [editedCourse, setEditedCourse] = useState<Course | null>(null);
    const [isOwner, setIsOwner] = useState<boolean>(false);
    const [isEditMode, setIsEditMode] = useState<boolean>(false);

    const params = useParams();
    const courseId = params.courseId ? Number(params.courseId) : undefined;
    const navigator = useNavigate();
    
    //Checks if the Course ID isn't present in the URL (shouldn't happen)
    useEffect(() => {
        if (!courseId) {
            navigator('/home')
        }
        else {
            setIsLoading(true);
            fetchCourse(courseId, (course) => {
                setCourse(course);
                setEditedCourse(course);
                setIsLoading(false)
            });
        }
    }, []);

    useEffect(() => {
        if (props.user.id === course?.createdById && !isOwner) {
            setIsOwner(true);
        }
        // setIsOwner(true)
        setEditedCourse(course);
    }, [course?.createdById, props.user.id, isOwner]);

    return isLoading ? 
        (<></>) :
        (<Container>
            <div className='course-title-container'>
                <input
                    className={'course-title ' + (!isEditMode && 'input-field')}
                    value={editedCourse?.title ? editedCourse.title : 'Title'}
                    onChange={(e) => {
                        if (editedCourse && e.target.value !== editedCourse?.title) {
                            setEditedCourse({ ...editedCourse, title: e.target.value });
                        }
                    }}
                    readOnly={!isEditMode}
                />
                {isOwner && 
                    (<div style={{ float: 'right' }}>
                        {isEditMode &&
                            <>
                                <Button size='sm' className='btn-3' variant='success' onClick={() => {
                                    setCourse(editedCourse);
                                    setIsEditMode(false);
                                }}>
                                    Save Changes
                                </Button>
                                <Button size='sm' className='btn-3' onClick={() => {
                                    setEditedCourse(course);
                                    setIsEditMode(false);
                                }}>
                                    Cancel
                                </Button>
                            </>
                        }
                        {!isEditMode &&
                            <OverlayTrigger
                                placement='top'
                                overlay={
                                    <Tooltip>
                                        Edit title and/or description
                                    </Tooltip>
                                }
                            >
                                <Button size='sm' className='btn-3' onClick={() => setIsEditMode(true)}>
                                    <Gear />
                                </Button>
                            </OverlayTrigger>
                        }
                    </div>
                )}
            </div>
            <div className='course-description-container'>
                {isEditMode ? (
                    <textarea
                    className={'course-description'}
                    value={editedCourse ? editedCourse.description : 'Description here'}
                    onChange={(e) => {
                        if (editedCourse && e.target.value !== editedCourse?.description) {
                            setEditedCourse({ ...editedCourse, description: e.target.value });
                        }
                    }}
                    readOnly={!isEditMode}
                    />
                ) :(
                    <span className='course-description'>{editedCourse ? editedCourse.description : 'Description here'}</span>
                )
                }
            </div>
            <div className='course-exercises-container'>
                <Tabs defaultActiveKey={'exercises'} fill={isOwner}>
                    <Tab eventKey={'exercises'} title={'Exercises'}>
                        <div className={'d-flex' + (isOwner ? '' : ' d-none')}>
                            <Button className={'create-btns'} onClick={() => {
                                createExerciseGroupModalRef.current?.handleShow(course?.id!);
                            }}>
                                <Plus />ExerciseGroup
                            </Button>
                        </div>
                        <ExerciseGroupsOverview
                            exerciseGroups={course ? course.exerciseGroups : []}
                            openDeleteExerciseModalRef={openDeleteExerciseModalRef}
                            openCreateExerciseModalRef={props.openCreateExerciseModalRef}
                            isOwner={isOwner}
                            changedCourse={() => {
                                if (courseId) {
                                    fetchCourse(courseId, (data) => {
                                        setCourse(data);
                                    })
                                }
                            }}
                        />
                    </Tab>
                    <Tab eventKey={'members'} title={'Members'} tabClassName={!isOwner ? 'd-none' : ''}>
                        <p>Member overview here.</p>
                    </Tab>
                    <Tab eventKey={'statistics'} title={'Statistics'} tabClassName={!isOwner ? 'd-none' : ''}>
                        <p>Statistics overview here.</p>
                    </Tab>
                </Tabs>
            </div>
            <DeleteConfirmModal
                ref={openDeleteExerciseModalRef}
                confirmDelete={(id: number, type: DeleteElementType) => {
                    if (course && courseId) {
                        if (type === DeleteElementType.EXERCISE) {
                            deleteExercise(courseId, id, ()=>{
                                fetchCourse(courseId, (data) => {
                                    setCourse(data);
                                });
                            });
                        }
                        else if (type === DeleteElementType.EXERCISEGROUP) {
                            deleteExerciseGroup(courseId, id, ()=>{
                                fetchCourse(courseId, (data) => {
                                    setCourse(data);
                                });
                            });
                        }
                    }
                }}
            />
            <CreateExerciseGroupModal
                ref={createExerciseGroupModalRef}
                updateExerciseGroups={() => {
                    if (courseId)
                        fetchCourse(courseId, (data) => {
                            setCourse(data);
                        });
                    }
                }
            />
        </Container>)
}

async function deleteExercise(courseId: number, exerciseId: number, callback: ()=>void) {
    let jwt = sessionStorage.getItem('jwt');
    if (jwt === null) return;
    try {
        const requestOptions = {
            method: 'POST',
            headers: { 
                'Accept': 'application/json', 
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            },
            body: JSON.stringify({
                "courseId": courseId,
                "exerciseId": exerciseId
            })
        }
        await fetch(getApiRoot() + 'Course/delete-exercise', requestOptions)
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

async function deleteExerciseGroup(courseId: number, exerciseGroupId: number, callback: ()=>void) {
    let jwt = sessionStorage.getItem('jwt');
    if (jwt === null) return;
    try {
        const requestOptions = {
            method: 'POST',
            headers: { 
                'Accept': 'application/json', 
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            },
            body: JSON.stringify({
                "courseId": courseId,
                "exerciseGroupId": exerciseGroupId
            })
        }
        await fetch(getApiRoot() + 'Course/delete-exercisegroup', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend - server unavailable');
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

async function fetchCourse(courseId: number, callback: (course: Course) => void) {
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
        await fetch(getApiRoot() + 'courses/' + courseId, requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend - server unavailable');
                }
                return res.json();
            })
            .then((course: Course) => {
                console.log(course)
                callback(course);
            });
    } catch (error) {
        alert(error);
    }
}
