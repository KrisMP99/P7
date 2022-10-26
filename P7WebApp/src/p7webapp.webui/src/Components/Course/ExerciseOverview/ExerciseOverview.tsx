import React, { useEffect, useState } from 'react'
import { Accordion, Button } from 'react-bootstrap';
import AccordionBody from 'react-bootstrap/esm/AccordionBody';
import AccordionHeader from 'react-bootstrap/esm/AccordionHeader';
import { Course } from '../Course';
import '../Course.css';
import { DeleteElementType, ShowDeleteConfirmModal } from '../../Modals/DeleteConfirmModal/DeleteConfirmModal';
import '../../../App.css';
import { Eye, EyeSlash, Pencil, Trash } from 'react-bootstrap-icons';

interface ExerciseOverviewProps {
    changeCourse: (course: Course) => void;
    course: Course | null;
    openDeleteExerciseModalRef: React.RefObject<ShowDeleteConfirmModal>;
    isOwner: boolean;
}

export default function ExerciseOverview(props: ExerciseOverviewProps) {
    const [course, setCourse] = useState<Course | null>(props.course);
    const [isOwner, setIsOwner] = useState<boolean>(props.isOwner);

    useEffect(() => {
        setCourse(props.course);
    }, [props.course?.exerciseGroups, props.course?.exercises]);
    useEffect(() => {
        setIsOwner(props.isOwner);
    }, [props.isOwner]);

    //Goes through every group and finds all exercises with a coresponding ID
    //Afterwards we create each group and places the exercises inside.
    let exerciseGroupElements: JSX.Element[] = [];
    if (course && course.exerciseGroups.length > 0 && course.exercises) {
        exerciseGroupElements = course.exerciseGroups.filter((group) => {

            return (!isOwner ? group.isVisible : true);

        }).map((value, index) => {

            //Creates the exercise elements below:
            const exerciseElements: JSX.Element[] = course.exercises.filter((val) => {

                return val.exerciseGroupId === value.id && (!isOwner ? val.isVisible : true);

            }).map((val, id) => {

                let visibilityElement: JSX.Element = val.isVisible ? (<Eye />) : (<EyeSlash />);

                return (
                    <div key={id} className={'exercise-container d-flex ' + (!val.isVisible && 'is-invisible')} onClick={(e) => {
                        console.log("Opening exercise");
                    }}>
                        <div className='exercise-title'>{val.title}</div>
                        <div className={'exercise-owner-container'} style={{ display: props.isOwner ? '' : 'none' }}>
                            <Button onClick={(e) => {
                                e.stopPropagation();
                                let updatedExercises = course.exercises.map((ex) => {
                                    if (ex.id === val.id) return { ...ex, isVisible: !ex.isVisible }
                                    else return ex;
                                })
                                props.changeCourse({ ...course, exercises: updatedExercises })
                            }}>
                                {visibilityElement}
                            </Button>
                            <Button onClick={(e) => {
                                e.stopPropagation();
                                console.log("Opening exercise in edit mode");
                            }}>
                                <Pencil />
                            </Button>
                            <Button variant='danger' onClick={(e) => {
                                e.stopPropagation();
                                props.openDeleteExerciseModalRef.current?.handleShow(val.title, val.id, DeleteElementType.EXERCISE);
                            }}>
                                <Trash />
                            </Button>
                        </div>
                    </div>
                )
            });

            return (
                <Accordion key={index}>
                    <div className='d-flex align-items-center flex-grow-1'>
                        <AccordionHeader className={'flex-grow-1' + (value.isVisible ? '' : ' is-invisible')}>
                            <input
                                className='input-field'
                                value={value.title}
                                onChange={(e) => {
                                    if (course && e.target.value !== value.title) {
                                        let exerciseGroups = [...course.exerciseGroups]
                                        exerciseGroups[exerciseGroups.findIndex((val) => val.id === value.id)].title = e.target.value;
                                        setCourse({ ...course, exerciseGroups: exerciseGroups });
                                    }
                                }}
                                readOnly={!props.isOwner}
                            />
                        </AccordionHeader>
                        {props.isOwner &&
                            (<div>
                                <Button className={'btn-2'} onClick={(e) => {
                                    e.stopPropagation();
                                    let updatedGroups = course.exerciseGroups.map((group) => {
                                        if (group.id === value.id) return { ...group, isVisible: !group.isVisible }
                                        else return group;
                                    })
                                    props.changeCourse({ ...course, exerciseGroups: updatedGroups });
                                }}>
                                    {value.isVisible ? (<Eye />) : (<EyeSlash />)}
                                </Button>
                                <Button className='btn-2' variant='danger' onClick={(e) => {
                                    e.stopPropagation();
                                    props.openDeleteExerciseModalRef.current?.handleShow(value.title, value.id, DeleteElementType.EXERCISEGROUP)
                                }}>
                                    <Trash />
                                </Button>
                            </div>)
                        }
                    </div>
                    <AccordionBody>
                        {exerciseElements}
                    </AccordionBody>
                </Accordion>)
        });
    }

    return (
        <div>{exerciseGroupElements}</div>
    )
}
