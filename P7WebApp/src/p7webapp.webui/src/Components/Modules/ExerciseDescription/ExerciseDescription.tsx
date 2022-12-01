import React, { useEffect, useState } from 'react';
import { ModuleType, TextModule } from '../../ExerciseBoard/ExerciseBoard';
import { ShowChangeModuleModalRef } from '../../Modals/ChangeModuleModal/ChangeModuleModal';
import '../Module.css';
import ModuleActionBar from '../ModuleActionBar/ModuleActionBar';
import './ExerciseDescription.css';

interface ExerciseDescriptionProps {
    changeModuleModalRef: React.RefObject<ShowChangeModuleModalRef> | null;
    position: number;
    editMode: boolean;
    title: string;
    body: string;

    changedContent: (position: number, content: TextModule) => void;
}

export default function ExerciseDescriptionModule(props: ExerciseDescriptionProps) {

    let editModePanel = (
        <div className='exercise-description-content'>
            <input className='exercise-description-title' type={'text'} value={props.title} onChange={(e)=>props.changedContent(props.position, {title: e.target.value, body: props.body})} />
            <textarea className='exercise-description-body-ta' value={props.body} onChange={(e)=>props.changedContent(props.position, {title: props.title, body: e.target.value})} />
        </div>
    )
    let bodyTextLinesElements: JSX.Element[] = [];
    if(!props.editMode) {
        let bodyTextLinesArray = props.body.match(/[^\r\n]+/g);
        bodyTextLinesElements = !bodyTextLinesArray ? [] : bodyTextLinesArray.map((val, i, arr) => {return <p key={i}>{val}</p>})
    }

    return (
        <>
            <div id='exercise-description-container' className='module-container'>
                {<ModuleActionBar 
                    changeModule={()=>props.changeModuleModalRef?.current?.handleShow(ModuleType.EXERCISE_DESCRIPTION, props.position)}
                    changeEditMode={()=>{}}
                />}
                {/* <button onClick={()=>setEditMode(!editMode)}>Edit</button> */}
                {props.editMode ? editModePanel :
                 <div className='exercise-description-content'>
                    <p className='exercise-description-title'>{props.title}</p>
                    <div className='exercise-description-body'>
                        {bodyTextLinesElements}
                    </div>
                </div>}
            </div>
        </>
    )
}

