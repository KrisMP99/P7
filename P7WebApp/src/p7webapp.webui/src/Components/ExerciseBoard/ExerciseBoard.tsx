import { Allotment } from 'allotment';
import React, { useEffect, useRef, useState } from 'react';
import { Button, Form } from 'react-bootstrap';
import { ArrowLeft } from 'react-bootstrap-icons';
import { useNavigate } from 'react-router-dom';
import { Exercise } from '../Course/CourseView';
import { ChangeLayoutModal } from '../Modals/ChangeLayoutModal/ChangeLayoutModal';
import ChangeModuleModal, { ShowChangeModuleModalRef } from '../Modals/ChangeModuleModal/ChangeModuleModal';
import { LayoutType, ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import EmptyModule from '../Modules/EmptyModule/EmptyModule';
import ExerciseDescriptionModule from '../Modules/ExerciseDescription/ExerciseDescription';
import './ExerciseBoard.css';
import { fetchExercise } from './ExerciseView';

interface TextModule {
    title: string;
    body: string;
}

interface CodeModule {
    code: string;
}

export enum ModuleType {
    EMPTY,
    EXERCISE_DESCRIPTION,
    CODE
}
export interface ExerciseModule {
    id: number;
    position: number;
    type: ModuleType;
    content: null | TextModule | CodeModule;
}

interface ExerciseBoardProps {
    exerciseId: number;
    exerciseGroupId: number;
}

export default function ExerciseBoard(props: ExerciseBoardProps) {
    const changeModuleModalRef = useRef<ShowChangeModuleModalRef>(null);
    const changeLayoutModalRef = useRef<ShowModal>(null);
    const [modules, setModules] = useState<ExerciseModule[]>([{id: 0, position: 1, type: ModuleType.EMPTY, content: null}]);
    const [layout, setLayout] = useState<LayoutType>(LayoutType.SINGLE);
    const navigator = useNavigate();
    const [exercise, setExercise] = useState<Exercise>({
        id: props.exerciseId, 
        exerciseGroupId: props.exerciseGroupId, 
        title: '', 
        isVisible: true,
        modules: []
    });

    useEffect(() => {
        console.log("Rendering EDIT");
        if (props.exerciseId >= 1) {
            fetchExercise(props.exerciseId, (newExercise) => setExercise(newExercise));
        }
    }, [props.exerciseId]);
    useEffect(() => {
        handleSetModules(layout);
    }, [layout])

    const handleSetModules = (layout: LayoutType) => {
        let tempModules: ExerciseModule[] = [...modules]; 
        let tempEmpty: ExerciseModule = {id: 0, position: 0, type: ModuleType.EMPTY, content: null};
        //Adds or removes left rows
        switch (layout) {
            case LayoutType.SINGLE:
                if (!tempModules.find((val) => val.position === 1)) {tempModules.push({...tempEmpty, position: 1})};
                tempModules = tempModules.filter((val) => val.position <= 1);
                break;
            case LayoutType.TWO_HORIZONTAL:
                if (!tempModules.find((val) => val.position === 2)) tempModules.push({...tempEmpty, position: 2});
                tempModules = tempModules.filter((val) => val.position <= 2);
                break;
            case LayoutType.TWO_VERTICAL:
                if (!tempModules.find((val) => val.position === 3)) tempModules.push({...tempEmpty, position: 3});
                tempModules = tempModules.filter((val) => val.position <= 3 && val.position !== 2);
                break;
            case LayoutType.ONE_LEFT_TWO_RIGHT:
                if (!tempModules.find((val) => val.position === 3)) tempModules.push({...tempEmpty, position: 3});
                if (!tempModules.find((val) => val.position === 4)) tempModules.push({...tempEmpty, position: 4});
                tempModules = tempModules.filter((val) => val.position <= 4 && val.position !== 2);
                break;
            case LayoutType.TWO_LEFT_TWO_RIGHT:
                if (!tempModules.find((val) => val.position === 2)) tempModules.push({...tempEmpty, position: 2});
                if (!tempModules.find((val) => val.position === 3)) tempModules.push({...tempEmpty, position: 3});
                if (!tempModules.find((val) => val.position === 4)) tempModules.push({...tempEmpty, position: 4});
                tempModules = tempModules.filter((val) => val.position <= 4);
                break;
            case LayoutType.TWO_LEFT_ONE_RIGHT:
                if (!tempModules.find((val) => val.position === 2)) tempModules.push({...tempEmpty, position: 2});
                if (!tempModules.find((val) => val.position === 3)) tempModules.push({...tempEmpty, position: 3});
                tempModules = tempModules.filter((val) => val.position <= 3);
                break;
            default:
                break;
        }
        
        setModules([...tempModules.sort((a, b) => a.position - b.position)]);
    }

    const getModuleFromType = (type: ModuleType, position: number, content: null | TextModule | CodeModule): React.ReactNode => {
        switch (type) {
            case ModuleType.EMPTY:
                return <EmptyModule changeModuleModalRef={changeModuleModalRef} position={position} />;
            case ModuleType.EXERCISE_DESCRIPTION:
                const moduleContent: TextModule = content as TextModule;
                return <ExerciseDescriptionModule changeModuleModalRef={changeModuleModalRef} position={position} isOwner={true} title={moduleContent.title} body={moduleContent.body} />;
            default:
                return <EmptyModule changeModuleModalRef={changeModuleModalRef} position={position} />;
        };
    }

    let columns: JSX.Element[] = []
    let colElements = [];
    if (modules.length > 0) {
        for (let i = 0; i < 4; i++) {
            let temp = modules.find((val) => val.position === i+1);
            if (temp !== undefined) {
                colElements.push((<Allotment.Pane key={i}>
                    {getModuleFromType(temp.type, temp.position, temp.content)}
                </Allotment.Pane>));
            }
            if ((i === 1  && modules.find((val) => val.position>2)) || i === 3) {
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
                <Form.Control className='exercise-title-text place-center' 
                    value={exercise.title}
                    onChange={(e) => setExercise({...exercise, title: e.target.value})}
                />
                <Button variant='success'>
                    Save exercise
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
                    let temp = [...modules];
                    temp = temp.map((val) => {
                        if (val.position === position) {
                            return {...val, type: newModule}
                        }
                        return val;
                    });
                    setModules(temp);
                }
            }} />
            <ChangeLayoutModal
                ref={changeLayoutModalRef}
                currentLayout={layout}
                changedLayout={(newLayout: LayoutType) => setLayout(newLayout)}
            />
        </div>
    )
}

