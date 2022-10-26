import React, { useEffect, useRef, useState } from 'react';
import { Button, Container } from 'react-bootstrap';
import { User } from '../../App';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import './Course.css';
import { ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import ExerciseOverview from './ExerciseOverview/ExerciseOverview';
import DeleteConfirmModal, { DeleteElementType, ShowDeleteConfirmModal } from '../Modals/DeleteConfirmModal/DeleteConfirmModal';
import { Plus } from 'react-bootstrap-icons';

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
    const [isOwner, setIsOwner] = useState<boolean>(false);
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
    }, [course?.ownerId]);

    return (
        <Container>
            <div className='course-title-container'>
                <input
                    className='input-field course-title'
                    value={course ? course.title : 'Title'}
                    onChange={(e) => {
                        if (course && e.target.value !== course?.title) {
                            setCourse({ ...course, title: e.target.value });
                        }
                    }}
                    readOnly={!isOwner}
                />
            </div>
            <div className='course-description-container'>
                <textarea
                    className='text-area'
                    value={course ? course.description : 'Description here'}
                    onChange={(e) => {
                        if (course && e.target.value !== course?.description) {
                            setCourse({ ...course, description: e.target.value });
                        }
                    }}
                    readOnly={!isOwner}
                />
            </div>
            <div className='course-exercises-container'>
                <Tabs defaultActiveKey={'exercises'} fill={isOwner}>
                    <Tab eventKey={'exercises'} title={'Exercises'}>
                        <div className={'d-flex' + (isOwner?'':' d-none')}>
                            <Button className={'create-btns'} onClick={() => {
                                let groups = [...course?.exerciseGroups!,
                                {
                                    id: Math.max(...course!.exerciseGroups?.map(o => o.id), 0) + 1,
                                    title: 'New group added',
                                    isVisible: true
                                }];
                                setCourse({ ...course!, exerciseGroups: groups });
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
                            course={course}
                            changeCourse={(newCourse: Course) => { setCourse(newCourse) }}
                            openDeleteExerciseModalRef={openDeleteExerciseModalRef}
                            isOwner={isOwner}
                        />
                    </Tab>
                    <Tab eventKey={'members'} title={'Members'}>
                        <p>Member overview here.</p>
                    </Tab>
                    <Tab eventKey={'statistics'} title={'Statistics'}>
                        <p>Statistics overview here.</p>
                    </Tab>
                </Tabs>
            </div>
            <DeleteConfirmModal 
                ref={openDeleteExerciseModalRef} 
                confirmDelete={(id: number, type: DeleteElementType) => {
                    if(course) {
                        if(type === DeleteElementType.EXERCISE) {
                            let newExercises = course.exercises.filter((ex) => ex.id !== id);
                            setCourse({...course, exercises: newExercises});
                        }
                        else if (type === DeleteElementType.EXERCISEGROUP) {
                            let newExerciseGroups = course.exerciseGroups.filter((group) => group.id !== id)
                            setCourse({...course, exerciseGroups: newExerciseGroups});
                        }
                    }
                }} 
            />
        </Container>
    )
}
