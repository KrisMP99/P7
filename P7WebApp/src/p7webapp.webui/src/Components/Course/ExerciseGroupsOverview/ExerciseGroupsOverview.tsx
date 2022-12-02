import React, { useEffect, useRef, useState } from 'react'
import { Accordion, Button } from 'react-bootstrap';
import AccordionBody from 'react-bootstrap/esm/AccordionBody';
import AccordionHeader from 'react-bootstrap/esm/AccordionHeader';
import { ExerciseGroup, ExerciseOverview } from '../CourseView';
import '../CourseView.css';
import DeleteConfirmModal, { DeleteElementType, ShowDeleteConfirmModal } from '../../Modals/DeleteConfirmModal/DeleteConfirmModal';
import '../../../App.css';
import { Eye, EyeSlash, Pencil, Plus, Trash } from 'react-bootstrap-icons';
import EditExerciseGroupModal, { ShowEditExerciseGroupModal } from '../../Modals/EditExerciseGroupModal/EditExerciseGroupModal';
import { NavigateFunction, useNavigate } from 'react-router-dom';
import { getApiRoot } from '../../../App';

interface ExerciseOverviewProps {
    courseId: number;
    exerciseGroups: ExerciseGroup[];
    changedCourse: () => void;
    isOwner: boolean;
    isAttendee: boolean;
}

export default function ExerciseGroupsOverview(props: ExerciseOverviewProps) {
    const openDeleteExerciseModalRef = useRef<ShowDeleteConfirmModal>(null);
    const [groupElements, setGroupElements] = useState<JSX.Element[]>([]);

    const navigate = useNavigate();
    
    const openEditExerciseGroupModalRef = useRef<ShowEditExerciseGroupModal>(null);

    useEffect(() => {
        setGroupElements(makeExerciseGroupElements(navigate, props.exerciseGroups, props.isOwner, props.isAttendee, openEditExerciseGroupModalRef, openDeleteExerciseModalRef, props.courseId));
    }, [props.exerciseGroups.length, props.exerciseGroups]);

    return (
        <div>
            {groupElements}
            <EditExerciseGroupModal 
                ref={openEditExerciseGroupModalRef} 
                updatedExerciseGroup={() => {
                    props.changedCourse();
                }}
            />
            <DeleteConfirmModal
                ref={openDeleteExerciseModalRef}
                confirmDelete={(id: number, type: DeleteElementType) => {
                    if (props.courseId) {
                        if (type === DeleteElementType.EXERCISE) {
                            let exerciseGroup = props.exerciseGroups.find(eg => eg.exercises.some(e => e.id === id));
                            if (exerciseGroup) {
                                deleteExercise(exerciseGroup?.id, id, ()=>{
                                    props.changedCourse();
                                });
                            } 
                        }
                        else if (type === DeleteElementType.EXERCISEGROUP) {
                            deleteExerciseGroup(props.courseId, id, ()=>{
                                props.changedCourse();
                            });
                        }
                    }
                }}
            />
        </div>
    )
}

function makeExerciseGroupElements (navigate: NavigateFunction, 
                                    exercisegroups: ExerciseGroup[], 
                                    isOwner: boolean,
                                    isAttendee: boolean,
                                    editExerciseGroupModalRef: React.RefObject<ShowEditExerciseGroupModal>,
                                    deleteExerciseModalRef: React.RefObject<ShowDeleteConfirmModal>,
                                    courseId: number
                                    ): JSX.Element[] {
    let exerciseGroupElements: JSX.Element[] = [];
    if (exercisegroups.length <= 0) return [];
    exerciseGroupElements = exercisegroups.filter((exGroup: ExerciseGroup) => isOwner ? true : exGroup.isVisible).map((exGroup: ExerciseGroup, index: number, _arr: ExerciseGroup[]) => {
        let exerciseElements: JSX.Element[] = [];
        exerciseElements = exGroup.exercises.filter((exercise) => {

            return !isOwner ? exGroup.isVisible : true; //WIP - måske ændre til exercise.isVisible i stedet idk

        }).map((exercise: ExerciseOverview, id: number) => {
            return (
                <div key={id} className={'exercise-container d-flex ' + (!exercise.isVisible && 'is-invisible')} onClick={(e) => {
                    e.stopPropagation();
                    if(isAttendee || isOwner) {
                        navigate('/course/' + courseId + '/exercise-group/' + exGroup.id + '/exercise/' + exercise.id + '/0');
                    }
                    else {
                        alert("Please enroll, before you start with any exercises")
                    }
                }}>
                    <div className='exercise-title'>{exercise.title}</div>
                    {isOwner &&
                        <div className={'exercise-owner-container'}>
                            <Button size='sm' className='btn-3' onClick={(e) => {
                                e.stopPropagation();
                                navigate('/course/' + courseId + '/exercise-group/' + exGroup.id + '/exercise/' + exercise.id + '/1')
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
                    {isOwner &&
                    <Button className={'create-btns'} onClick={() => {
                        navigate('/course/' + courseId + '/exercise-group/' + exGroup.id + '/exercise/-1/0');
                    }}>
                        <Plus /> Exercise
                    </Button>}
                    {exerciseElements}
                </AccordionBody>
            </Accordion>)
        });
        
    return exerciseGroupElements;
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

async function deleteExercise(exerciseGroupId: number, exerciseId: number, callback: ()=>void) {
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
        await fetch(getApiRoot() + 'courses/exercise-groups/' + exerciseGroupId + '/exercises/' + exerciseId, requestOptions)
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