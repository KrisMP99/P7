import React, { useState } from 'react';
import { Container } from 'react-bootstrap';
import '../Module.css';
import './ExerciseDescription.css';

interface ExerciseDescriptionProps {
    exerciseID: number;
    title: string;
    body: string;
}

export default function ExerciseDescriptionModule() {
    
    const [bodyText, setBodyText] = useState('');
    const [titleText, setTitle] = useState('');
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
                <button onClick={()=>setEditMode(!editMode)}>Edit</button>
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

