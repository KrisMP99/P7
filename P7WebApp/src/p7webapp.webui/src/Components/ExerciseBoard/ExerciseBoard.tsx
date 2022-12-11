import { Allotment } from 'allotment';
import React, { useEffect, useRef, useState } from 'react';
import { Button, Form } from 'react-bootstrap';
import { ArrowLeft } from 'react-bootstrap-icons';
import { useNavigate } from 'react-router-dom';
import { getApiRoot } from '../../App';
import { Exercise } from '../Course/CourseView';
import { ChangeLayoutModal } from '../Modals/ChangeLayoutModal/ChangeLayoutModal';
import ChangeModuleModal, { ShowChangeModuleModalRef } from '../Modals/ChangeModuleModal/ChangeModuleModal';
import { LayoutType, ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import EmptyModule from '../Modules/EmptyModule/EmptyModule';
import ExerciseDescriptionModule from '../Modules/ExerciseDescription/ExerciseDescription';
import './ExerciseBoard.css';
import { fetchExercise, getModuleFromType } from './ExerciseView';

export enum ModuleType {
    EMPTY = 'empty',
    EXERCISE_DESCRIPTION = 'text',
    CODE = 'code',
    QUIZ = 'quiz'
}
export interface ExerciseModule {
    id: number;
    position: number;
    type: ModuleType;
    title?: string;
    content?: string;
    code?: string;
}

interface ExerciseBoardProps {
    courseId: number;
    exerciseId: number;
    exerciseGroupId: number;
    isNewExercise: boolean;
}

export default function ExerciseBoard(props: ExerciseBoardProps) {
    const changeModuleModalRef = useRef<ShowChangeModuleModalRef>(null);
    const changeLayoutModalRef = useRef<ShowModal>(null);
    const navigator = useNavigate();
    const [exercise, setExercise] = useState<Exercise>({
        title: '', 
        id: 0, 
        layoutId: LayoutType.SINGLE, 
        exerciseGroupId: 0, 
        isVisible: false, 
        modules: [], 
        exerciseNumber: 0, 
        startDate: null, 
        endDate: null, 
        visibleFrom: null, 
        visibleTo: null    
    });

    useEffect(() => {
        if (props.exerciseId >= 1) {
            fetchExercise(props.exerciseId, props.exerciseGroupId, props.courseId, (newExercise) => {
                setExercise(newExercise);
            });
        }
    }, [props.exerciseId]);

    useEffect(() => {
        if (props.isNewExercise || exercise.id >= 1) {
            handleSetModules(exercise.layoutId);
        }
    }, [exercise.modules.length, exercise.layoutId])

    const handleSetModules = (layout: LayoutType) => {
        let tempModules: ExerciseModule[] = [...exercise.modules]; 
        let tempEmpty: ExerciseModule = {id: 0, position: 0, type: ModuleType.EMPTY};
        //Adds or removes left rows
        switch (layout) {
            case LayoutType.SINGLE:
                if (!tempModules.find((val) => val.position === 1)) {tempModules.push({...tempEmpty, position: 1})};
                tempModules = tempModules.filter((val) => val.position <= 1);
                break;
            case LayoutType.TWO_VERTICAL:
                if (!tempModules.find((val) => val.position === 3)) tempModules.push({...tempEmpty, position: 3});
                tempModules = tempModules.filter((val) => val.position <= 3 && val.position !== 2);
                break;
            case LayoutType.TWO_HORIZONTAL:
                if (!tempModules.find((val) => val.position === 2)) tempModules.push({...tempEmpty, position: 2});
                tempModules = tempModules.filter((val) => val.position <= 2);
                break;
            case LayoutType.TWO_LEFT_ONE_RIGHT:
                if (!tempModules.find((val) => val.position === 2)) tempModules.push({...tempEmpty, position: 2});
                if (!tempModules.find((val) => val.position === 3)) tempModules.push({...tempEmpty, position: 3});
                tempModules = tempModules.filter((val) => val.position <= 3);
                break;
            case LayoutType.ONE_LEFT_TWO_RIGHT:
                if (!tempModules.find((val) => val.position === 3)) tempModules.push({...tempEmpty, position: 3});
                if (!tempModules.find((val) => val.position === 4)) tempModules.push({...tempEmpty, position: 4});
                tempModules = tempModules.filter((val) => val.position <= 4 && val.position !== 2);
                break;
            case LayoutType.TWO_LEFT_TWO_RIGHT:
                if (!tempModules.find((val) => val.position === 1)) tempModules.push({...tempEmpty, position: 1});
                if (!tempModules.find((val) => val.position === 2)) tempModules.push({...tempEmpty, position: 2});
                if (!tempModules.find((val) => val.position === 3)) tempModules.push({...tempEmpty, position: 3});
                if (!tempModules.find((val) => val.position === 4)) tempModules.push({...tempEmpty, position: 4});
                tempModules = tempModules.filter((val) => val.position <= 4);
                break;
            default:
                break;
        }
        
        setExercise({...exercise, layoutId: layout, modules: tempModules.sort((a, b) => a.position - b.position)});
    }

    let columns: JSX.Element[] = []
    let colElements = [];
    if (exercise.modules.length > 0) {
        for (let i = 0; i < 4; i++) {
            let temp = exercise.modules.find((val) => val.position === i+1);
            if (temp !== undefined) {
                colElements.push((<Allotment.Pane key={i}>
                    {getModuleFromType(temp, changeModuleModalRef, exercise, (newExercise) => setExercise(newExercise), true)}
                </Allotment.Pane>));
            }
            if ((i === 1  && exercise.modules.find((val) => val.position>2)) || i === 3) {
                columns[i] = (<Allotment separator vertical key={i}>{colElements}</Allotment>);
                colElements = [];
            }
        }
    }

    return (
        <div className='exercise-wrapper'>
            <div className='board-actions-container'>
                <Button onClick={() => navigator(-1)}><ArrowLeft /></Button>
                <Button onClick={() => changeLayoutModalRef.current?.handleShow()}>Change layout</Button>
                <Form.Check 
                    type="switch"
                    label="Should be visible to other users"
                    checked={exercise.isVisible} 
                    onChange={(e) => {
                        if (exercise.modules.some(m => m.type === ModuleType.EMPTY) && !exercise.isVisible) {
                            alert('Please fill out the layout with modules before you set the exercise to visible');
                        }
                        else {
                            setExercise({...exercise, isVisible: e.target.checked});
                        }
                    }}
                />
                <Form.Control className='exercise-title-text place-center' 
                    value={exercise.title}
                    onChange={(e) => setExercise({...exercise, title: e.target.value})}
                />
                <Button variant='success' onClick={() => {
                        if (exercise.modules.some((m) => m.type === ModuleType.EMPTY)) {
                            setExercise({...exercise, isVisible: false});
                        }
                        createOrUpdateExercise(exercise, props.exerciseGroupId, props.isNewExercise)
                    }}>
                    Save changes
                </Button>
                <Button variant='danger' onClick={() => navigator(-1)}>
                    Cancel
                </Button>
            </div>
            <div className='board-container'>
                <Allotment className='board-outer' separator>
                    {columns.map((col) => {
                        return col;
                    })}
                </Allotment>
            </div>
            <ChangeModuleModal ref={changeModuleModalRef} changedModule={(newModule: ModuleType, position: number) => {
                if (position > 0 && position <= 4) {
                    let temp = [...exercise.modules];
                    temp = temp.map((val) => {
                        if (val.position === position) {
                            return {...val, type: newModule}
                        }
                        return val;
                    });
                    setExercise({...exercise, modules: temp});
                }
            }} />
            <ChangeLayoutModal
                ref={changeLayoutModalRef}
                currentLayout={exercise.layoutId}
                changedLayout={(newLayout: LayoutType) => setExercise({...exercise, layoutId: newLayout})}
            />
        </div>
    )
}

async function createOrUpdateExercise(exercise: Exercise, exerciseGroupId: number, isNewExercise: boolean) {
    const jwt = sessionStorage.getItem('jwt');
    if (jwt === null) return;
    try {
        const requestOptions = {
            method: isNewExercise ? 'POST' : 'PUT',
            headers: { 
                'Accept': 'application/json', 
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            },
            body: JSON.stringify({
                id: exercise.id,
                exerciseGroupId: exerciseGroupId,
                title: exercise.title,
                exerciseNumber: exercise.exerciseNumber,
                isVisible: exercise.isVisible,
                startDate: exercise.startDate,
                endDate: exercise.endDate,
                visibleFrom: exercise.visibleFrom,
                visibleTo: exercise.visibleTo,
                layoutId: exercise.layoutId,
                modules: convertModulesToRequest(exercise.modules, isNewExercise)
            })
        }
        await fetch(getApiRoot() + 'courses/exercise-groups/exercises', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error(res.statusText);
                }
                return;
            });
    } catch (error) {
        alert(error);
    }
}

function convertModulesToRequest(modules: ExerciseModule[], isNewExercise: boolean) {
    let convertedModules = [];
    for (let i = 0; i < modules.length; i++) {
        switch (modules[i].type) {
            case ModuleType.EXERCISE_DESCRIPTION:
                convertedModules.push({
                    id: isNewExercise ? 0 : modules[0].id,
                    description: 'undefined',
                    height: 1,
                    width: 1,
                    position: modules[i].position,
                    type: modules[i].type,
                    title: modules[i].title,
                    content: modules[i].content
                });
                break;
            case ModuleType.CODE:
                break;
            case ModuleType.QUIZ:
                break;
            case ModuleType.EMPTY:
                break;
            default:
                break;
        }
    }
    return convertedModules;
}