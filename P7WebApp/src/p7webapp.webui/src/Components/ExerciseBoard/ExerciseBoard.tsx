import { Allotment } from 'allotment';
import React, { useEffect, useRef, useState } from 'react';
import { useParams } from 'react-router';
import { Routes } from 'react-router-dom';
import { Route } from 'react-router-dom';
import Course from '../Course/Course';
import ChangeModuleModal, { ShowChangeModuleModalRef } from '../Modals/ChangeModuleModal/ChangeModuleModal';
import { Layout, LayoutType } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import EmptyModule from '../Modules/EmptyModule/EmptyModule';
import ExerciseDescriptionModule from '../Modules/ExerciseDescription/ExerciseDescription';
import './ExerciseBoard.css';

export enum ModuleType {
    EMPTY,
    EXERCISE_DESCRIPTION,
}

interface ExerciseBoardProps {
    boardLayout: Layout;
    editMode: boolean;
}

export default function ExerciseBoard(props: ExerciseBoardProps) {
    const params = useParams();
    const changeModuleModalRef = useRef<ShowChangeModuleModalRef>(null);
    const [modules, setModules] = useState<ModuleType[][]>([[ModuleType.EMPTY]]);
    const [exerciseId, setExerciseId] = useState<number>(Number(params.id));

    useEffect(() => {
        //Fetch the exercise and set the module to the result
        // setModules()
        setExerciseId(Number(params.id));
        handleSetModules(props.boardLayout);
        
    }, [params.id, props.boardLayout.layoutType, props.boardLayout.leftRows, props.boardLayout.rightRows])

    const handleSetModules = (layout: Layout) => {

        let tempModules: ModuleType[][] = [...modules];
        //Adds or removes left rows
        for (let i = 0; i < layout.leftRows - 1; i++) {
            if (tempModules[0].length < layout.leftRows) {
                tempModules[0].push(ModuleType.EMPTY);
            }
            else if (tempModules[0].length > layout.leftRows) {
                tempModules = tempModules.slice(1);
            }  
        }
        //Adds or removes right rows + right col
        if (layout.rightRows === 0 && tempModules.length !== 1) {
            tempModules = tempModules.slice(1);
        }
        else if (layout.rightRows > 0 && tempModules.length === 1) {
            tempModules.push([ModuleType.EMPTY]);
        }
        
        if (layout.rightRows > 0) {
            for (let i = 0; i < layout.rightRows - 1; i++) {
                if (tempModules[1].length < layout.leftRows) {
                    tempModules[1].push(ModuleType.EMPTY);
                }
                else if (tempModules[1].length > layout.leftRows) {
                    tempModules = tempModules.slice(1);
                }  
            }
        }
        setModules(tempModules);
    } 

    const getModuleFromType = (type: ModuleType, id: number[]): React.ReactNode => {
        switch (type) {
            case ModuleType.EMPTY:
                return (<EmptyModule changeModuleModalRef={changeModuleModalRef} id={id} />)
            case ModuleType.EXERCISE_DESCRIPTION:
                return <ExerciseDescriptionModule/>    
            default:
                return <ExerciseDescriptionModule/>
        };
    }

    let columns: JSX.Element[] = []
    let colElements = [];
    if (modules.length !== 0) {
        let keyCounter = 0;
        for (let i = 0; i < modules.length; i++) {
            for (let j = 0; j < modules[i].length; j++) {
                colElements.push((<Allotment.Pane key={keyCounter}>
                    {getModuleFromType(modules[i][j], [i,j])}
                </Allotment.Pane>));
                keyCounter++;
            }
            columns[i] = (<Allotment separator vertical key={keyCounter}>{colElements}</Allotment>);
            colElements = [];
            keyCounter++;
        }
    }
    
    return (
        <div className='board-container'>
            <Allotment className='board-outer' separator>
                {columns.map((col) => {
                    return col;
                })}
            </Allotment>
            <ChangeModuleModal ref={changeModuleModalRef} changedModule={(newModule: ModuleType, index: number[])=>{
                
            }} />
        </div>
    )
}

