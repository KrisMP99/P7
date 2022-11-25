import React, { useEffect, useState } from 'react'
import { Allotment } from 'allotment';
import { getApiRoot } from '../../App';
import { Exercise } from '../Course/CourseView';

interface ExerciseViewProps {
    exerciseId: number;
}

export default function ExerciseView(props: ExerciseViewProps) {
    const [exercise, setExercise] = useState<Exercise>({title: '', id: 0, exerciseGroupId: 0, isVisible: true, modules: []})
        
    useEffect(() => {
        console.log("Rendering exercise view");
        fetchExercise(props.exerciseId, (ex) => {
            setExercise(ex)
        });
    }, []);

    let columns: JSX.Element[] = []
    let colElements = [];
    // if (modules.length > 0) {
    //     for (let i = 0; i < 4; i++) {
    //         let temp = modules.find((val) => val.position === i+1);
    //         if (temp !== undefined) {
    //             colElements.push((<Allotment.Pane key={i}>
    //                 {getModuleFromType(temp.type, temp.position)}
    //             </Allotment.Pane>));
    //         }
    //         if ((i === 1  && modules.find((val) => val.position>2)) || i === 3) {
    //             columns[i] = (<Allotment separator vertical key={i}>{colElements}</Allotment>);
    //             colElements = [];
    //         }
    //     }
    // }

    return (
        <div className='exercise-wrapper'>
            <div className='board-actions-container'>
                <span className='place-right exercise-title-text'>{exercise.title}</span>
            </div>
            <div className='board-container'>
                <Allotment className='board-outer' separator>
                    {columns.map((col) => {
                        return col;
                    })}
                </Allotment>
            </div>
        </div>
    )
}


export async function fetchExercise(exerciseId: number, callback: (newExercise: Exercise) => void) {
    const jwt = sessionStorage.getItem('jwt');
    if (jwt === null) return;
    try {
        const requestOptions = {
            method: 'GET',
            headers: { 
                'Accept': 'application/json', 
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            }
        }
        await fetch(getApiRoot() + 'exercise', requestOptions) //WIP - set path
            .then((res) => {
                if (!res.ok) {
                    throw new Error(res.statusText);
                }
                return res.json();
            })
            .then((data) => {
                console.log(data);
                // callback(exercise);
            });
     } catch (error) {
        alert(error);
     }
}