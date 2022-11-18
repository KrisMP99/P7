import React, { useEffect, useRef, useState } from 'react'
import { Accordion, Button } from 'react-bootstrap';
import AccordionBody from 'react-bootstrap/esm/AccordionBody';
import AccordionHeader from 'react-bootstrap/esm/AccordionHeader';
import { Course, ExerciseGroup, ExerciseOverview } from '../CourseView';
import '../CourseView.css';
import { DeleteElementType, ShowDeleteConfirmModal } from '../../Modals/DeleteConfirmModal/DeleteConfirmModal';
import '../../../App.css';
import { Eye, EyeSlash, Pencil, Plus, Trash } from 'react-bootstrap-icons';
import { ShowCreateExerciseModal } from '../../Modals/CreateExerciseModal/CreateExerciseModal';
import EditExerciseGroupModal, { ShowEditExerciseGroupModal } from '../../Modals/EditExerciseGroupModal/EditExerciseGroupModal';

interface ExerciseOverviewProps {
    exerciseGroups: ExerciseGroup[];
    changedCourse: () => void;
    isOwner: boolean;
    openDeleteExerciseModalRef: React.RefObject<ShowDeleteConfirmModal>;
    openCreateExerciseModalRef: React.RefObject<ShowCreateExerciseModal>;
}

export default function ExerciseGroupsOverview(props: ExerciseOverviewProps) {
    const [groupElements, setGroupElements] = useState<JSX.Element[]>([]);
    
    let changesHasBeenMade = false;
    
    const openEditExerciseGroupModalRef = useRef<ShowEditExerciseGroupModal>(null);

    useEffect(() => {
        if (changesHasBeenMade) {
            props.changedCourse();
            changesHasBeenMade = false;
        }
        setGroupElements(makeExerciseGroupElements(props.exerciseGroups, 
                                                   props.isOwner, 
                                                   openEditExerciseGroupModalRef,
                                                   openEditExerciseGroupModalRef, //WIP - CHANGE THIS
                                                   props.openDeleteExerciseModalRef,
                                                   props.openCreateExerciseModalRef));
    }, [props.exerciseGroups.length, changesHasBeenMade])

    // useEffect(() => {
    //     setCourse(props.course);
    //     setIsOwner(props.isOwner);
    // }, [props.isOwner, props.course?.exerciseGroups, props.course?.exercises]);

    //Goes through every group and finds all exercises with a coresponding ID
    return (
        <div>
            {groupElements}
            <EditExerciseGroupModal 
                ref={openEditExerciseGroupModalRef} 
                updateExerciseGroup={(newGroup) => {
                    // if (course) {
                    //     let tempGroups = [...course.exerciseGroups];
                    //     tempGroups[index] = newGroup;
                    //     props.changedCourse({...course, exerciseGroups: tempGroups});
                    // }
                }}
            />
        </div>
    )
}

function makeExerciseGroupElements (exercisegroups: ExerciseGroup[], 
                                    isOwner: boolean,
                                    editExerciseGroupModalRef: React.RefObject<ShowEditExerciseGroupModal>,
                                    editExerciseModalRef: React.RefObject<ShowEditExerciseGroupModal>, //WIP - Make edit exercise modal
                                    deleteExerciseModalRef: React.RefObject<ShowDeleteConfirmModal>,
                                    createExerciseModalRef: React.RefObject<ShowCreateExerciseModal>
                                    ): JSX.Element[] {

        let exerciseGroupElements: JSX.Element[] = [];
        if(exercisegroups.length < 1) return exerciseGroupElements;

        exerciseGroupElements = exercisegroups.filter((exGroup: ExerciseGroup) => {

            if (!isOwner) {
                return exGroup.isVisible;
            }

        })
        .map((exGroup: ExerciseGroup, index: number) => {

            let exerciseElements: JSX.Element[] = [];
            exerciseElements = exGroup.exercises.filter((exercise) => {

                if (!isOwner) {
                    return exGroup.isVisible;
                }

            })
            .map((exercise: ExerciseOverview, id: number) => {

                let visibilityElement: JSX.Element = exercise.isVisible ? (<Eye />) : (<EyeSlash />);
                return (
                    <div key={id} className={'exercise-container d-flex ' + (!exercise.isVisible && 'is-invisible')} onClick={(e) => {
                        console.log("Opening exercise");
                        //WIP - navigate to the exercise and fetch it there
                    }}>
                        <div className='exercise-title'>{exercise.title}</div>
                        {isOwner &&
                            <div className={'exercise-owner-container'}>
                                <Button size='sm' className='btn-3' onClick={(e) => {
                                    e.stopPropagation();
                                    // let updatedExercises = course.exercises.map((ex) => {
                                    //     if (ex.id === exercise.id) return { ...ex, isVisible: !ex.isVisible }
                                    //     else return ex;
                                    // })
                                    // props.changedCourse({ ...course, exercises: updatedExercises });
                                }}>
                                    {visibilityElement}
                                </Button>
                                <Button size='sm' className='btn-3' onClick={(e) => {
                                    e.stopPropagation();
                                    editExerciseModalRef.current?.handleShow(exGroup, index);
                                }}>
                                    <Pencil />
                                </Button>
                                <Button size='sm' className='btn-3' variant='danger' onClick={(e) => {
                                    e.stopPropagation();
                                    deleteExerciseModalRef.current?.handleShow(exercise.title, exercise.id, DeleteElementType.EXERCISE);
                                }}>
                                    <Trash />
                                </Button>
                            </div>
                        }
                    </div>
                )

            })

            return (
                <Accordion key={index} defaultActiveKey={index+''}>
                    <div className='d-flex align-items-center flex-grow-1'>
                        <AccordionHeader className={'flex-grow-1' + (exGroup.isVisible ? '' : ' is-invisible')}>
                            <input
                                className='input-field'
                                value={exGroup.title}
                                onChange={(e) => {
                                    if (e.target.value !== exGroup.title) {
                                        // let exerciseGroups = [...course.exerciseGroups]
                                        // exerciseGroups[exerciseGroups.findIndex((val) => val.id === value.id)].title = e.target.value;
                                        // setCourse({ ...course, exerciseGroups: exerciseGroups });
                                    }
                                }}
                                readOnly={true}
                            />
                        </AccordionHeader>
                        {isOwner &&
                            (<div className='group-owner-buttons'>
                                <Button size='sm' className={'btn-3'} onClick={(e) => {
                                    e.stopPropagation();
                                    editExerciseGroupModalRef.current?.handleShow(exGroup, index);
                                }}>
                                    <Pencil />
                                </Button>
                                <Button size='sm' className='btn-3' variant='danger' onClick={(e) => {
                                    e.stopPropagation();
                                    deleteExerciseModalRef.current?.handleShow(exGroup.title, exGroup.id, DeleteElementType.EXERCISEGROUP)
                                }}>
                                    <Trash />
                                </Button>
                            </div>)
                        }
                    </div>
                    <AccordionBody className='exercise-container-box'>
                        <Button className={'create-btns'} onClick={() => {
                                createExerciseModalRef.current?.handleShow(exGroup.id);
                        }}>
                            <Plus /> Exercise
                        </Button>
                        {exerciseElements}
                    </AccordionBody>
                </Accordion>)
        });

        // if (course && course.exerciseGroups.length > 0 && course.exercises) {
        //     exerciseGroupElements = course.exerciseGroups.filter((group) => {
    
        //         return (!isOwner ? group.isVisible : true);
    
        //     }).map((value, index) => {
    
        //         //Creates the exercise elements under each accordion:
        //         const exerciseElements: JSX.Element[] = course.exercises.filter((val) => {
    
        //             return val.exerciseGroupId === value.id && (!isOwner ? val.isVisible : true);
    
        //         }).map((val, id) => {
    
                    
    
                    
        //         });
                // return (
                //     <Accordion key={index} defaultActiveKey={index+''}>
                //         <div className='d-flex align-items-center flex-grow-1'>
                //             <AccordionHeader className={'flex-grow-1' + (value.isVisible ? '' : ' is-invisible')}>
                //                 <input
                //                     className='input-field'
                //                     value={value.title}
                //                     onChange={(e) => {
                //                         if (course && e.target.value !== value.title) {
                //                             let exerciseGroups = [...course.exerciseGroups]
                //                             exerciseGroups[exerciseGroups.findIndex((val) => val.id === value.id)].title = e.target.value;
                //                             setCourse({ ...course, exerciseGroups: exerciseGroups });
                //                         }
                //                     }}
                //                     readOnly={true}
                //                 />
                //             </AccordionHeader>
                //             {isOwner &&
                //                 (<div className='group-owner-buttons'>
                //                     <Button size='sm' className={'btn-3'} onClick={(e) => {
                //                         e.stopPropagation();
                //                         openEditExerciseGroupModalRef.current?.handleShow(value, index);
                //                     }}>
                //                         <Pencil />
                //                     </Button>
                //                     <Button size='sm' className='btn-3' variant='danger' onClick={(e) => {
                //                         e.stopPropagation();
                //                         props.openDeleteExerciseModalRef.current?.handleShow(value.title, value.id, DeleteElementType.EXERCISEGROUP)
                //                     }}>
                //                         <Trash />
                //                     </Button>
                //                 </div>)
                //             }
                //         </div>
                //         <AccordionBody className='exercise-container-box'>
                //             <Button className={'create-btns'} onClick={() => {
                //                     props.openCreateExerciseModalRef.current?.handleShow(value.id);
                //             }}>
                //                 <Plus /> Exercise
                //             </Button>
                //             {exerciseElements}
                //         </AccordionBody>
                //     </Accordion>)
            // });
        // }

        return exerciseGroupElements;
}
