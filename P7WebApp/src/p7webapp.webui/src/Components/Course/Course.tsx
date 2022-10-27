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
    const openDeleteExerciseModalRef = useRef<ShowDeleteConfirmModal>(null);
    const [editedCourse, setEditedCourse] = useState<Course | null>(null);
    const [isOwner, setIsOwner] = useState<boolean>(false);
    const [isEditMode, setIsEditMode] = useState<boolean>(false);
    const [course, setCourse] = useState<Course | null>(null);

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


    useEffect(() => {
        //Fetch course here and set it
        if (props.user.id === course?.ownerId && !isOwner) {
            setIsOwner(true);
        }
        setCourse({ title: 'Course Title', description: 'Course description very specific to course', exerciseGroups: exGroups, exercises: ex, ownerId: 0 });
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
            </div>
            {isOwner && (
                <div>
                    {isEditMode && 
                        <>
                            <Button className='btn-1' variant='success' onClick={()=> {
                                setCourse(editedCourse);
                                setIsEditMode(false);
                            }}>
                                Save Changes
                            </Button>
                            <Button onClick={()=> {
                                setEditedCourse(course);
                                setIsEditMode(false);
                            }}>
                                Cancel
                            </Button>
                        </>
                    }                
                    {!isEditMode &&
                        <Button style={{height:'50px', width:'50px'}} onClick={()=>setIsEditMode(true)}>
                            <Gear/>
                        </Button>
                    }
                </div>
            )}
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
                        <div className={'d-flex' + (isEditMode?'':' d-none')}>
                            <Button className={'create-btns'} onClick={() => {
                                let groups = [...editedCourse?.exerciseGroups!,
                                {
                                    id: Math.max(...editedCourse!.exerciseGroups?.map(o => o.id), 0) + 1,
                                    title: 'New group added',
                                    isVisible: true
                                }];
                                setEditedCourse({ ...editedCourse!, exerciseGroups: groups });
                            }}>
                                <Plus/>ExerciseGroup
                            </Button>
                            <Button className={'create-btns'} onClick={() => {
                                props.openCreateExerciseModalRef.current?.handleShow();
                            }}>
                                <Plus/>Exercise
                            </Button>
                        </div>
                        <ExerciseOverview
                            course={editedCourse}
                            changeCourse={(newCourse: Course) => { setEditedCourse(newCourse) }}
                            openDeleteExerciseModalRef={openDeleteExerciseModalRef}
                            isOwner={isOwner}
                            isEditMode={isEditMode}
                        />
                    </Tab>
                    <Tab eventKey={'members'} title={'Members'} tabClassName={!isOwner?'d-none':''}>
                        <p>Member overview here.</p>
                    </Tab>
                    <Tab eventKey={'statistics'} title={'Statistics'} tabClassName={!isOwner?'d-none':''}>
                        <p>Statistics overview here.</p>
                    </Tab>
                </Tabs>
            </div>
            <DeleteConfirmModal 
                ref={openDeleteExerciseModalRef} 
                confirmDelete={(id: number, type: DeleteElementType) => {
                    if(editedCourse) {
                        if(type === DeleteElementType.EXERCISE) {
                            let newExercises = editedCourse.exercises.filter((ex) => ex.id !== id);
                            setEditedCourse({...editedCourse, exercises: newExercises});
                        }
                        else if (type === DeleteElementType.EXERCISEGROUP) {
                            let newExerciseGroups = editedCourse.exerciseGroups.filter((group) => group.id !== id)
                            setEditedCourse({...editedCourse, exerciseGroups: newExerciseGroups});
                        }
                    }
                }} 
            />
        </Container>
    )
}
