import React, { useEffect, useRef, useState } from 'react'
import { Accordion, Button } from 'react-bootstrap';
import AccordionBody from 'react-bootstrap/esm/AccordionBody';
import AccordionHeader from 'react-bootstrap/esm/AccordionHeader';
import { Course } from '../Course';
import '../Course.css';
import { DeleteElementType, ShowDeleteConfirmModal } from '../../Modals/DeleteConfirmModal/DeleteConfirmModal';
import '../../../App.css';
import { Eye, EyeSlash, Pencil, Plus, Trash } from 'react-bootstrap-icons';
import { ShowCreateExerciseModal } from '../../Modals/CreateExerciseModal/CreateExerciseModal';
import EditExerciseGroupModal, { ShowEditExerciseGroupModal } from '../../Modals/EditExerciseGroupModal/EditExerciseGroupModal';

interface ExerciseOverviewProps {
    changedCourse: (course: Course) => void;
    course: Course | null;
    isOwner: boolean;
    openDeleteExerciseModalRef: React.RefObject<ShowDeleteConfirmModal>;
    openCreateExerciseModalRef: React.RefObject<ShowCreateExerciseModal>;
}

export default function ExerciseOverview(props: ExerciseOverviewProps) {
    const [course, setCourse] = useState<Course | null>(props.course);
    const [isOwner, setIsOwner] = useState<boolean>(props.isOwner);
    
    const openEditExerciseGroupModalRef = useRef<ShowEditExerciseGroupModal>(null);

    useEffect(() => {
        setCourse(props.course);
        setIsOwner(props.isOwner);
    }, [props.isOwner, props.course?.exerciseGroups, props.course?.exercises]);

    //Goes through every group and finds all exercises with a coresponding ID
    //Afterwards we create each group and places the exercises inside.
    let exerciseGroupElements: JSX.Element[] = [];
    if (course && course.exerciseGroups.length > 0 && course.exercises) {
        exerciseGroupElements = course.exerciseGroups.filter((group) => {

            return (!isOwner ? group.isVisible : true);

        }).map((value, index) => {

            //Creates the exercise elements under each accordion:
            const exerciseElements: JSX.Element[] = course.exercises.filter((val) => {

                return val.exerciseGroupId === value.id && (!isOwner ? val.isVisible : true);

            }).map((val, id) => {

                let visibilityElement: JSX.Element = val.isVisible ? (<Eye />) : (<EyeSlash />);

                return (
                    <div key={id} className={'exercise-container d-flex ' + (!val.isVisible && 'is-invisible')} onClick={(e) => {
                        console.log("Opening exercise");
                        //WIP - navigate to the exercise and fetch it there
                    }}>
                        <div className='exercise-title'>{val.title}</div>
                        {isOwner &&
                            <div className={'exercise-owner-container'}>
                                <Button size='sm' className='btn-3' onClick={(e) => {
                                    e.stopPropagation();
                                    let updatedExercises = course.exercises.map((ex) => {
                                        if (ex.id === val.id) return { ...ex, isVisible: !ex.isVisible }
                                        else return ex;
                                    })
                                    props.changedCourse({ ...course, exercises: updatedExercises });
                                }}>
                                    {visibilityElement}
                                </Button>
                                <Button size='sm' className='btn-3' onClick={(e) => {
                                    e.stopPropagation();
                                    openEditExerciseGroupModalRef.current?.handleShow(value, index);
                                }}>
                                    <Pencil />
                                </Button>
                                <Button size='sm' className='btn-3' variant='danger' onClick={(e) => {
                                    e.stopPropagation();
                                    props.openDeleteExerciseModalRef.current?.handleShow(val.title, val.id, DeleteElementType.EXERCISE);
                                }}>
                                    <Trash />
                                </Button>
                            </div>
                        }
                    </div>
                )
            });

            return (
                <Accordion key={index} defaultActiveKey={index+''}>
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
                                readOnly={true}
                            />
                        </AccordionHeader>
                        {isOwner &&
                            (<div className='group-owner-buttons'>
                                {/* <Button className={'btn-2'} onClick={(e) => {
                                    e.stopPropagation();
                                    let updatedGroups = course.exerciseGroups.map((group) => {
                                        if (group.id === value.id) return { ...group, isVisible: !group.isVisible }
                                        else return group;
                                    })
                                    props.changedCourse({ ...course, exerciseGroups: updatedGroups });
                                }}>
                                    {value.isVisible ? (<Eye />) : (<EyeSlash />)}
                                </Button> */}
                                <Button size='sm' className={'btn-3'} onClick={(e) => {
                                    e.stopPropagation();
                                    openEditExerciseGroupModalRef.current?.handleShow(value, index);
                                }}>
                                    <Pencil />
                                </Button>
                                <Button size='sm' className='btn-3' variant='danger' onClick={(e) => {
                                    e.stopPropagation();
                                    props.openDeleteExerciseModalRef.current?.handleShow(value.title, value.id, DeleteElementType.EXERCISEGROUP)
                                }}>
                                    <Trash />
                                </Button>
                            </div>)
                        }
                    </div>
                    <AccordionBody className='exercise-container-box'>
                        <Button className={'create-btns'} onClick={() => {
                                props.openCreateExerciseModalRef.current?.handleShow(value.id);
                        }}>
                            <Plus /> Exercise
                        </Button>
                        {exerciseElements}
                    </AccordionBody>
                </Accordion>)
        });
    }

    return (
        <div>
            {exerciseGroupElements}
            <EditExerciseGroupModal 
                ref={openEditExerciseGroupModalRef} 
                updateExerciseGroup={(newGroup, index) => {
                    if (course) {
                        let tempGroups = [...course.exerciseGroups];
                        tempGroups[index] = newGroup;
                        props.changedCourse({...course, exerciseGroups: tempGroups});
                    }
                }}
            />
        </div>
    )
}
