import React, { useEffect, useRef, useState } from 'react';
import { Accordion, Button, Container } from 'react-bootstrap';
import AccordionBody from 'react-bootstrap/esm/AccordionBody';
import AccordionHeader from 'react-bootstrap/esm/AccordionHeader';
import { useParams } from 'react-router-dom';
import { User } from '../../App';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import './Course.css';
import CreateExerciseModal, { ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';

export interface ExerciseGroup {
    id: number;
    title: string;
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
    const params = useParams();
    const [isOwner, setIsOwner] = useState<boolean>(false);
    const [course, setCourse] = useState<Course|null>(null);

    //DUMMY DATA:
    const exGroups: ExerciseGroup[] = [{id: 0, title:"Session 1"}, {id: 1, title:"Session 2"}, {id: 2, title:"Session 3"}, {id: 3, title:"Session 4"}];
    const ex: Exercise[] = [
        {id:0, exerciseGroupId:0, title:'Exercise 1', isVisible:true}, 
        {id:1, exerciseGroupId:1, title:'Exercise 1', isVisible:true}, 
        {id:2, exerciseGroupId:1, title:'Exercise 2', isVisible:true},
        {id:3, exerciseGroupId:2, title:'Exercise 1', isVisible:true},
        {id:4, exerciseGroupId:2, title:'Exercise 2', isVisible:true},
        {id:5, exerciseGroupId:2, title:'Exercise 3', isVisible:true},
        {id:6, exerciseGroupId:3, title:'Exercise 1', isVisible:true},
        {id:7, exerciseGroupId:3, title:'Exercise 2', isVisible:true},
        {id:8, exerciseGroupId:3, title:'Exercise 3', isVisible:true},
        {id:9, exerciseGroupId:3, title:'Exercise 4', isVisible:true}
    ]


    useEffect(()=>{
        //Fetch course here and set it
        if (props.user.id === course?.ownerId && !isOwner) {
            setIsOwner(true);
        }
        setCourse({title: 'Course Title', description: 'Course description very specific to course', exerciseGroups: exGroups, exercises: ex, ownerId: 0});
    },  [course?.ownerId]);

    let exerciseGroupElements: JSX.Element[] = [];
    if (course && course.exerciseGroups.length > 0 && course.exercises) {
        exerciseGroupElements = course.exerciseGroups.map((value, index, array) => {
            const exerciseElements: JSX.Element[] = course.exercises.filter((val) => {
                return val.exerciseGroupId === value.id;
            }).map((val, id, arr) => {
                return (
                    <div key={id} className='exercise-container d-flex' onClick={(e)=>{
                        console.log("Opening exercise");
                    }}>
                        <div className='exercise-title'>{val.title}</div>
                        <div className='exercise-owner-container' style={{display:isOwner?'':'none'}}>
                            <button onClick={(e) => {
                                e.stopPropagation();
                                console.log("Opening exercise in edit mode");
                            }}>
                                Edit
                            </button>
                            <button onClick={(e) => {
                                e.stopPropagation();
                                console.log("Deleting exercise");
                            }}>
                                Delete
                            </button>
                        </div>
                    </div>
                )
            });
            return (
            <Accordion key={index}>
                <AccordionHeader>
                    <input 
                        className='input-field'
                        value={value.title} 
                        onChange={(e)=>{
                            if (course && e.target.value !== value.title) {
                                let exerciseGroups = [...course.exerciseGroups]
                                exerciseGroups[exerciseGroups.findIndex((val) => val.id === value.id)].title = e.target.value;
                                setCourse({...course, exerciseGroups: exerciseGroups});
                            }
                        }} 
                        readOnly={!isOwner}
                    />
                </AccordionHeader>
                <AccordionBody>
                    {exerciseElements}
                </AccordionBody>
            </Accordion>)
        });
    }
    return (
        <Container>
            <div className='course-title-container'>
                <input
                    className='input-field course-title'
                    value={course ? course.title : 'Title'}
                    onChange={(e)=>{
                        if (course && e.target.value !== course?.title) {
                            setCourse({...course, title: e.target.value});
                        }
                    }} 
                    readOnly={!isOwner}
                />
            </div>
            <div className='course-description-container'>
                <textarea
                    className='text-area'
                    value={course ? course.description : 'Description here'}
                    onChange={(e)=>{
                        if (course && e.target.value !== course?.description) {
                            setCourse({...course, description: e.target.value});
                        }
                    }} 
                    readOnly={!isOwner}
                />
            </div>
            <div className='course-exercises-container'>
                <Tabs defaultActiveKey={'exercises'} fill={isOwner}>
                    <Tab eventKey={'exercises'} title={'Exercises'}>
                        <div className='d-flex'>
                            <Button onClick={() => {
                                let groups = [...course?.exerciseGroups!];
                                
                            }}>
                                Create ExerciseGroup
                            </Button>
                            <Button onClick={() => {
                                props.openCreateExerciseModalRef.current?.handleShow();
                            }}>
                                Create Exercise
                            </Button>
                        </div>
                        {exerciseGroupElements}
                    </Tab>
                    <Tab eventKey={'members'} title={'Members'}>
                        <p>Member overview here.</p>
                    </Tab>
                    <Tab eventKey={'statistics'} title={'Statistics'}>
                        <p>Statistics overview here.</p>
                    </Tab>
                </Tabs>
            </div>
        </Container>
    )
}
