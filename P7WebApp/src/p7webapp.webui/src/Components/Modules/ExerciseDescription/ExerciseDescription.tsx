import React, { useState } from 'react';
import { Container } from 'react-bootstrap';
import { ModuleType } from '../../ExerciseBoard/ExerciseBoard';
import { ShowChangeModuleModalRef } from '../../Modals/ChangeModuleModal/ChangeModuleModal';
import '../Module.css';
import ModuleActionBar from '../ModuleActionBar/ModuleActionBar';
import './ExerciseDescription.css';

interface ExerciseDescriptionProps {
    changeModuleModalRef: React.RefObject<ShowChangeModuleModalRef>;
    position: number;
    isOwner: boolean;
}

export default function ExerciseDescriptionModule(props: ExerciseDescriptionProps) {
    
    const [bodyText, setBodyText] = useState('Description here...');
    const [titleText, setTitle] = useState('Title here...');
    const [editMode, setEditMode] = useState(false);

    let editModePanel = (
        <div className='exercise-description-content'>
            <input className='exercise-description-title' type={'text'} value={titleText} onChange={(e)=>setTitle(e.target.value)} />
            <textarea className='exercise-description-body-ta' value={bodyText} onChange={(e)=>setBodyText(e.target.value)} />
        </div>
    )
    let bodyTextLinesElements: JSX.Element[] = [];
    if(!editMode) {
        let bodyTextLinesArray = bodyText.match(/[^\r\n]+/g);
        bodyTextLinesElements = !bodyTextLinesArray ? [] : bodyTextLinesArray.map((val, i, arr) => {return <p key={i}>{val}</p>})
    }

    return (
        <>
            <div id='exercise-description-container' className='module-container'>
                {<ModuleActionBar 
                    changeModule={()=>props.changeModuleModalRef.current?.handleShow(ModuleType.EXERCISE_DESCRIPTION, props.position)}
                    changeEditMode={()=>setEditMode(!editMode)}
                />}
                {/* <button onClick={()=>setEditMode(!editMode)}>Edit</button> */}
                {editMode ? editModePanel :
                 <div className='exercise-description-content'>
                    <p className='exercise-description-title'>{titleText}</p>
                    <div className='exercise-description-body'>
                        {bodyTextLinesElements}
                    </div>
                </div>}
            </div>
        </>
    )
}

