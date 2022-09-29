import { Allotment } from 'allotment';
import React, { Component } from 'react';
import ExerciseDescriptionModule from '../Modules/ExerciseDescription/ExerciseDescription';
import './ExerciseBoard.css';

export default function ExerciseBoard() {
    return (
        <div className='board-container'>
            <Allotment separator>
                <Allotment.Pane minSize={100}>
                    <ExerciseDescriptionModule />
                </Allotment.Pane>
                    <Allotment separator vertical={true} minSize={100}>
                        <Allotment.Pane>
                            <ExerciseDescriptionModule />
                        </Allotment.Pane>
                        <Allotment.Pane>
                            <ExerciseDescriptionModule />
                        </Allotment.Pane>
                    </Allotment>
            </Allotment>
        </div>
    )
}
