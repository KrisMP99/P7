import React, { useEffect, useRef, useState } from 'react';
import { Button, Container } from 'react-bootstrap';
import { User } from '../../App';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import './Course.css';
import { ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import ExerciseOverview from './ExerciseOverview/ExerciseOverview';
import DeleteConfirmModal, { DeleteElementType, ShowDeleteConfirmModal } from '../Modals/DeleteConfirmModal/DeleteConfirmModal';
import { Gear, Plus } from 'react-bootstrap-icons';
import CreateExerciseGroupModal, { ShowCreateExerciseGroupModal } from '../Modals/CreateExerciseGroupModal/CreateExerciseGroupModal';

export interface ExerciseGroup {
    id: number;
    title: string;
    isVisible: boolean;
}

export interface Exercise {
    id: number;
    exerciseGroupId: number;
    title: string;
    isVisible: boolean;
}

export interface Course {
    title: string;
    ownerId: number;
    description: string;
    exerciseGroups: ExerciseGroup[];
    exercises: Exercise[];
}

interface CourseProps {
    user: User;
    openCreateExerciseModalRef: React.RefObject<ShowModal>;
}

export default function Course(props: CourseProps) {
    //DUMMY DATA:
    const exGroups: ExerciseGroup[] = [
        { id: 0, title: "Session 1", isVisible: true },
        { id: 1, title: "Session 2", isVisible: true },
        { id: 2, title: "Session 3", isVisible: true },
        { id: 3, title: "Session 4", isVisible: true }
    ];
    const ex: Exercise[] = [
        { id: 0, exerciseGroupId: 0, title: 'Exercise 1', isVisible: true },
        { id: 1, exerciseGroupId: 1, title: 'Exercise 1', isVisible: true },
        { id: 2, exerciseGroupId: 1, title: 'Exercise 2', isVisible: true },
        { id: 3, exerciseGroupId: 2, title: 'Exercise 1', isVisible: true },
        { id: 4, exerciseGroupId: 2, title: 'Exercise 2', isVisible: true },
        { id: 5, exerciseGroupId: 2, title: 'Exercise 3', isVisible: true },
        { id: 6, exerciseGroupId: 3, title: 'Exercise 1', isVisible: true },
        { id: 7, exerciseGroupId: 3, title: 'Exercise 2', isVisible: true },
        { id: 8, exerciseGroupId: 3, title: 'Exercise 3', isVisible: true },
        { id: 9, exerciseGroupId: 3, title: 'Exercise 4', isVisible: true }
    ]

    const openDeleteExerciseModalRef = useRef<ShowDeleteConfirmModal>(null);
    const createExerciseGroupModalRef = useRef<ShowCreateExerciseGroupModal>(null);

    const [course, setCourse] = useState<Course>({ title: 'Course Title', description: 'Course description very specific to course', exerciseGroups: exGroups, exercises: ex, ownerId: 0 });
    const [editedCourse, setEditedCourse] = useState<Course>({...course});
    const [isOwner, setIsOwner] = useState<boolean>(false);
    const [isEditMode, setIsEditMode] = useState<boolean>(false);




    useEffect(() => {
        //WIP - Fetch course here and set it
        if (props.user.id === course?.ownerId && !isOwner) {
            setIsOwner(true);
        }
        setEditedCourse(course);
    }, [course?.ownerId]);
    return (
        <Container>
            <div className='course-title-container'>
                <input
                    className='input-field course-title'
                    value={editedCourse ? editedCourse.title : 'Title'}
                    onChange={(e) => {
                        if (editedCourse && e.target.value !== editedCourse?.title) {
                            setEditedCourse({ ...editedCourse, title: e.target.value });
                        }
                    }}
                    readOnly={!isEditMode}
                />
                {isOwner && (
                    <div style={{ float: 'right' }}>
                        {isEditMode &&
                            <>
                                <Button className='btn-1' variant='success' onClick={() => {
                                    setCourse(editedCourse);
                                    setIsEditMode(false);
                                }}>
                                    Save Changes
                                </Button>
                                <Button onClick={() => {
                                    setEditedCourse(course);
                                    setIsEditMode(false);
                                }}>
                                    Cancel
                                </Button>
                            </>
                        }
                        {!isEditMode &&
                            <Button style={{ height: '50px', width: '50px' }} onClick={() => setIsEditMode(true)}>
                                <Gear />
                            </Button>
                        }
                    </div>
                )}
            </div>
            <div className='course-description-container'>
                <textarea
                    className='text-area'
                    value={editedCourse ? editedCourse.description : 'Description here'}
                    onChange={(e) => {
                        if (editedCourse && e.target.value !== editedCourse?.description) {
                            setEditedCourse({ ...editedCourse, description: e.target.value });
                        }
                    }}
                    readOnly={!isEditMode}
                />
            </div>
            <div className='course-exercises-container'>
                <Tabs defaultActiveKey={'exercises'} fill={isOwner}>
                    <Tab eventKey={'exercises'} title={'Exercises'}>
                        <div className={'d-flex' + (isOwner ? '' : ' d-none')}>
                            <Button className={'create-btns'} onClick={() => {
                                createExerciseGroupModalRef.current?.handleShow();
                            }}>
                                <Plus />ExerciseGroup
                            </Button>
                        </div>
                        <ExerciseOverview
                            course={course}
                            changeCourse={(newCourse: Course) => { setCourse(newCourse) }}
                            openDeleteExerciseModalRef={openDeleteExerciseModalRef}
                            isOwner={isOwner}
                            openCreateExerciseModalRef={props.openCreateExerciseModalRef}
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
                    if (course) {
                        if (type === DeleteElementType.EXERCISE) {
                            let newExercises = course.exercises.filter((ex) => ex.id !== id);
                            //WIP - MAKE POST TO DELETE EXERCISE
                            //WIP - REFETCH COURSE
                            setCourse({ ...course, exercises: newExercises });
                        }
                        else if (type === DeleteElementType.EXERCISEGROUP) {
                            let newExerciseGroups = course.exerciseGroups.filter((group) => group.id !== id)
                            //WIP - MAKE POST TO DELETE EXERCISEGROUP
                            //WIP - REFETCH COURSE
                            setCourse({ ...course, exerciseGroups: newExerciseGroups });
                        }
                    }
                }}
            />
            <CreateExerciseGroupModal
                ref={createExerciseGroupModalRef}
                updateExerciseGroups={(dummyTitle: string, dummyVisibility: boolean) => {
                    //WIP - Fetch exercisegroups again
                    let dummyGroup = {id: course.exerciseGroups.length, title: dummyTitle, isVisible: dummyVisibility}
                    setCourse({...course, exerciseGroups: [...course.exerciseGroups, dummyGroup]})
                }}
            />
        </Container>
    )
}
