import React, { useEffect, useRef, useState } from 'react';
import { Button, Container, Form, OverlayTrigger, Tooltip } from 'react-bootstrap';
import { getApiRoot, User } from '../../App';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import './CourseView.css';
import { LayoutType, ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import ExerciseGroupsOverview from './ExerciseGroupsOverview/ExerciseGroupsOverview';
import DeleteConfirmModal, { DeleteElementType, ShowDeleteConfirmModal } from '../Modals/DeleteConfirmModal/DeleteConfirmModal';
import { Gear, Pencil, Plus } from 'react-bootstrap-icons';
import CreateExerciseGroupModal, { ShowCreateExerciseGroupModal } from '../Modals/CreateExerciseGroupModal/CreateExerciseGroupModal';
import { useNavigate, useParams } from 'react-router-dom';
import internal from 'stream';
import EditCourseModal, { ShowEditCourseModal } from '../Modals/EditCourseModal/EditCourseModal';
import AttendeeOverview from '../AttendeeOverview/AttendeeOverview';
import { ExerciseModule } from '../ExerciseBoard/ExerciseBoard';

export interface ExerciseOverview {
    id: number;
    title: string;
    isVisible: boolean;
    exerciseGroupId: number;
    exerciseNumber: number;
    startDate: Date | null;
    endDate: Date | null;
    visibleFrom: Date | null;
    visibleTo: Date | null;
    createdDate: Date | null;
    lastModifiedDate: Date | null;
    layoutId: LayoutType;
}

export interface ExerciseGroup {
    id: number;
    title: string;
    description: string;
    exerciseGroupNumber: number;
    isVisible: boolean;
    exercises: ExerciseOverview[];
    createdDate?: Date;
    lastModifiedDate?: Date;
    becomesVisibleAt?: Date;
}

export interface Exercise {
    id: number;
    exerciseGroupId: number;
    title: string;
    isVisible: boolean;
    exerciseNumber: number;
    layoutId: LayoutType;
    startDate: Date | null;
    endDate: Date | null;
    visibleFrom: Date | null;
    visibleTo: Date | null;
    modules: ExerciseModule[];
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
    attendees: Attendee[];
}

export interface Attendee {
    userId: string;
    roleId: number;
    roleName: string;
    firstname: string;
    lastname: string;
    username: string;
}

interface CourseProps {
    user: User;
}

export default function CourseView(props: CourseProps) {
    const openDeleteExerciseModalRef = useRef<ShowDeleteConfirmModal>(null);
    const createExerciseGroupModalRef = useRef<ShowCreateExerciseGroupModal>(null);
    const editCourseModalRef = useRef<ShowEditCourseModal>(null);

    const [isLoading, setIsLoading] = useState<boolean>(true);

    const [course, setCourse] = useState<Course | null>(null);
    const [editedCourse, setEditedCourse] = useState<Course | null>(null);
    const [isOwner, setIsOwner] = useState<boolean>(false);
    const [isAttendee, setIsAttendee] = useState<boolean>(false);
    const [isEditMode, setIsEditMode] = useState<boolean>(false);
    const [inviteCode, setInviteCode] = useState<string>("");

    const params = useParams();
    const courseId = params.courseId ? Number(params.courseId) : undefined;
    const navigate = useNavigate();
    
    //Checks if the Course ID isn't present in the URL (shouldn't happen)
    useEffect(() => {
        if (!courseId) {
            navigate('/home')
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

    useEffect(()=> {
        if (course){
            if (props.user.id === course?.createdById) {
                setIsOwner(true);
            }
            else {
                setIsOwner(false);
            }
            if (course!.attendees.filter((a) => a.userId === props.user.id).length > 0 && !isOwner){
                setIsAttendee(true);
            }
            else {
                setIsAttendee(false);
            }
        }
    }, [course, props.user.id, course?.attendees.length]);

    useEffect(() => {
        setEditedCourse(course);
    }, [course?.createdById, props.user.id, isOwner, course?.description, course?.isPrivate, course?.title]);

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
                {!isOwner && <div style={{float:'right'}} >
                    {
                        isAttendee?
                        <Button size='sm' className='btn-3' onClick={() => {
                            if (courseId) {
                                leaveCourse(courseId, ()=>{
                                    fetchCourse(courseId, (newCourse) => setCourse(newCourse));
                                });
                            }
                        }}>
                            Leave
                        </Button>: 
                        <Button size='sm' className='btn-3' onClick={() => {
                            if (courseId) {
                                enrollToCourse(courseId, ()=>{
                                    fetchCourse(courseId, (newCourse) => setCourse(newCourse));
                                });
                            }
                        }}>
                            Enroll
                        </Button>
                    }     
                </div> }
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
                            <>
                                {inviteCode !== "" && <Form.Control readOnly value={inviteCode}/>}
                                <Button size='sm' className='btn-3' onClick={() => {
                                    getInviteCode(course?.id!, (inviteCode) => 
                                    {
                                        navigator.clipboard.writeText(inviteCode);
                                        setInviteCode(inviteCode);
                                    })
                                }}>
                                    Get invite code
                                </Button>
                                <OverlayTrigger
                                    placement='top'
                                    overlay={
                                        <Tooltip>
                                            Edit title and/or description
                                        </Tooltip>
                                    }
                                >
                                    <Button size='sm' className='btn-3' onClick={() => {
                                            if (course){
                                                editCourseModalRef.current?.handleShow(course.id);
                                            }
                                        }
                                    }>
                                        <Pencil />
                                    </Button>
                                </OverlayTrigger>
                            </>
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
                                if (course) {
                                    createExerciseGroupModalRef.current?.handleShow(course.id, course.exerciseGroups.length);
                                }
                            }}>
                                <Plus />ExerciseGroup
                            </Button>
                        </div>
                        <ExerciseGroupsOverview
                            exerciseGroups={course ? course.exerciseGroups : []}
                            openDeleteExerciseModalRef={openDeleteExerciseModalRef}
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
                        <AttendeeOverview 
                            attendees={course ? course.attendees : []}
                            reFetchCourse={() => fetchCourse(courseId!, (newCourse) => setCourse(newCourse)) }
                        />
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
            {isOwner && 
                <EditCourseModal
                    ref={editCourseModalRef}
                    updatedCourse={()=>{
                        if (course){
                            fetchCourse(course?.id, (course) => {
                                setCourse(course);
                            })
                        }
                    }}
                />
            }
            <CreateExerciseGroupModal
                ref={createExerciseGroupModalRef}
                updatedExerciseGroups={() => {
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
            method: 'DELETE',
            headers: { 
                'Accept': 'application/json', 
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            }
        }
        await fetch(getApiRoot() + 'courses/' + courseId + '/exercise-group/' + exerciseGroupId, requestOptions)
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

async function leaveCourse(courseId: number, callback: () => void) {
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
        await fetch(getApiRoot() + 'courses/' + courseId + '/leave', requestOptions)
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

async function enrollToCourse(courseId: number, callback: () => void) {
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
                'courseId': courseId 
            })
        }
        await fetch(getApiRoot() + 'courses/enroll', requestOptions)
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

export async function fetchCourse(courseId: number, callback: (course: Course) => void) {
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
                callback(course);
            });
    } catch (error) {
        alert(error);
    }
}
async function getInviteCode(courseId: number, callback: (inviteCode: string) => void) {
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
        await fetch(getApiRoot() + 'courses/' + courseId + "/invite-code", requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend - server unavailable');
                }
                return res.json();
            })
            .then((inviteCode: string) => {
                callback(inviteCode);
            });
    } catch (error) {
        alert(error);
    }
}
