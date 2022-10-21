import React, { useEffect, useState } from 'react';
import { Accordion, Container } from 'react-bootstrap';
import AccordionBody from 'react-bootstrap/esm/AccordionBody';
import AccordionHeader from 'react-bootstrap/esm/AccordionHeader';
import { useParams } from 'react-router-dom';
import { dummyData } from '../Landingpage/dummyData';
import './Course.css';

export interface IExerciseGroup {
    id: number;
    title: string;
}

export interface IExercise {
    id: number;
    exerciseGroupId: number;
    title: string;
    isVisible: boolean;
}

export interface ICourse {
    title: string;
    description: string;
    exerciseGroups: IExerciseGroup[];
    exercises: IExercise[];
}

export default function Course() {
    const params = useParams();
    const [editorMode, setEditorMode] = useState<boolean>(false);
    const [course, setCourse] = useState<ICourse|null>(null);

    //DUMMY DATA:
    const exGroups: IExerciseGroup[] = [{id: 0, title:"A"}, {id: 1, title:"B"}, {id: 2, title:"C"}, {id: 3, title:"D"}];
    const ex: IExercise[] = [
        {id:0, exerciseGroupId:0, title:'E', isVisible:true}, 
        {id:1, exerciseGroupId:1, title:'S', isVisible:true}, 
        {id:2, exerciseGroupId:1, title:'SS', isVisible:true},
        {id:3, exerciseGroupId:2, title:'W', isVisible:true},
        {id:4, exerciseGroupId:2, title:'WW', isVisible:true},
        {id:5, exerciseGroupId:2, title:'WWW', isVisible:true},
        {id:6, exerciseGroupId:3, title:'L', isVisible:true},
        {id:7, exerciseGroupId:3, title:'LL', isVisible:true},
        {id:8, exerciseGroupId:3, title:'LLL', isVisible:true},
        {id:9, exerciseGroupId:3, title:'LLLL', isVisible:true}
    ]


    useEffect(()=>{
        if (!course) {
            //Fetch course here and set it
            setCourse({title: 'Course Title', description: 'Course description very specific to course', exerciseGroups: exGroups, exercises: ex});
        }
    }, [params.id])

    let exerciseGroupElements: JSX.Element[] = [];
    if (course && course.exerciseGroups.length > 0 && course.exercises) {
        exerciseGroupElements = course.exerciseGroups.map((value, index, array) => {
            const exerciseElements: JSX.Element[] = course.exercises.filter((val) => {
                return val.exerciseGroupId === value.id;
            }).map((val, id, arr) => {
                return (
                    <div key={id} className='exercise-container d-flex'>
                        <div className='exercise-id'>#{val.id}</div>
                        <div className='exercise-title'>{val.title}</div>
                    </div>
                )
            });
            return (
            <Accordion key={index}>
                <AccordionHeader>{value.title}</AccordionHeader>
                <AccordionBody>
                    {exerciseElements}
                </AccordionBody>
            </Accordion>)
        });
    }
    
    return (
        <Container>
            <div className='course-title-container'>
                <span>{course ? course.title : 'Title'}</span>
            </div>
            <div className='course-description-container'>
                <p>{course ? course.description : 'Description here'}</p>
            </div>
            <div className='course-exercises-container'>
                <span className='exercises-title'>Exercises:</span>
                {exerciseGroupElements}
            </div>
        </Container>
    )
}
