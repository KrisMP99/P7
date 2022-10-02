import { Allotment } from 'allotment';
import React, { Component } from 'react';
import ExerciseDescriptionModule from '../Modules/ExerciseDescription/ExerciseDescription';
import './ExerciseBoard.css';

export enum BoardLayoutType {
    SinglePage,
    OneLeftOneRight,
    TwoLeftOneRight,
    OneLeftTwoRight,
    TwoLeftTwoRight
}
export enum BoardModuleType {
    ExerciseDescription,
}

interface ExerciseBoardProps {
    boardLayout: BoardModuleType[][]; //Arrays, with arrays - each array contain the content of a column on the board
    creatorMode: boolean;
}

export default function ExerciseBoard(props: ExerciseBoardProps) {

    let columns = []
    let colElements = [];
    if (props.boardLayout.length !== 0) {
        let keyCounter = 0;
        for (let i = 0; i < props.boardLayout.length; i++) {
            for (let j = 0; j < props.boardLayout[i].length; j++) {
                colElements.push((<Allotment.Pane key={keyCounter}>
                    <ExerciseDescriptionModule/>
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
                {columns.map((col, id, arr) => {
                    return col;
                })}
            </Allotment>
        </div>
    )
}
