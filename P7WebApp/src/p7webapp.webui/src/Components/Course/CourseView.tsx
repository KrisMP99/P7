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
    exerciseNumber: number;
    isVisible: boolean;
    visibleTo: Date | null;
    visibleFrom: Date | null;
    startDate: Date | null;
    endDate: Date | null;
    createdDate: Date | null;
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
    ownerId: number;
    ownerName: string | null;
    exerciseGroups: ExerciseGroup[];
    createdDate: Date | null;
    modifiedDate: Date | null;
    attendees: Attendee[];
}

export interface Attendee {
    courseId: number;
    userId: number;
    roleId: number;
}

interface CourseProps {
    user: User;
}

export default function CourseView(props: CourseProps) {
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
            let l = course.attendees.filter((a) => a.userId === props.user.id);
            // console.log(course);
            // l.length > 0 && console.log(typeof(course.attendees.filter((a) => a.userId === props.user.id)[0].userId));
            if (props.user.id === course?.ownerId) {
                setIsOwner(true);
            }
            else {
                setIsOwner(false);
            }
            if (course.attendees.filter((a) => a.userId === props.user.id).length > 0 && !isOwner){
                setIsAttendee(true);
            }
            else {
                setIsAttendee(false);
            }
        }
    }, [course, props.user.id, course?.attendees.length]);

    useEffect(() => {
        setEditedCourse(course);
    }, [course?.ownerId, props.user.id, isOwner, course?.description, course?.isPrivate, course?.title]);

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
                {!isOwner && 
                <div style={{float:'right'}} >
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
                    (<div className='d-flex align-items-center'>
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
                                <div className='d-flex'>
                                {inviteCode !== "" && <Form.Control style={{maxWidth: '50px', maxHeight: '33px'}} readOnly value={inviteCode}/>}
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
                                </div>
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
                            courseId={course?.id ?? 0}
                            exerciseGroups={course ? course.exerciseGroups : []}
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
                            attendees={course?.attendees ?? []} 
                            reFetchCourse={() => {
                                if (courseId) {
                                    fetchCourse(courseId, (newCourse) => setCourse(newCourse));
                                }
                            }} 
                        />
                    </Tab>
                    <Tab eventKey={'statistics'} title={'Statistics'} tabClassName={!isOwner ? 'd-none' : ''}>
                        <p>Statistics overview here.</p>
                    </Tab>
                </Tabs>
            </div>
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
