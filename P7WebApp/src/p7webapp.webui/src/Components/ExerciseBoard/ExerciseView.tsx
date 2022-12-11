import React, { useEffect, useState } from 'react'
import { Allotment } from 'allotment';
import { getApiRoot } from '../../App';
import { Exercise } from '../Course/CourseView';
import { LayoutType } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import EmptyModule from '../Modules/EmptyModule/EmptyModule';
import ExerciseDescriptionModule from '../Modules/ExerciseDescription/ExerciseDescription';
import { ExerciseModule, ModuleType } from './ExerciseBoard';
import { ShowChangeModuleModalRef } from '../Modals/ChangeModuleModal/ChangeModuleModal';
import { Button } from 'react-bootstrap';
import { ArrowLeft } from 'react-bootstrap-icons';
import { useNavigate } from 'react-router-dom';

interface ExerciseViewProps {
    exerciseId: number;
    exerciseGroupId: number;
    courseId: number;
}

export default function ExerciseView(props: ExerciseViewProps) {
    const [exercise, setExercise] = useState<Exercise>({title: '', id: 0, layoutId: LayoutType.SINGLE, exerciseGroupId: 0, isVisible: true, modules: [], exerciseNumber: 0, startDate: null, endDate: null, visibleFrom: null, visibleTo: null})
    const navigator = useNavigate();
    
    useEffect(() => {
        console.log("Rendering exercise view");
        fetchExercise(props.exerciseId, props.exerciseGroupId, props.courseId, (ex) => {
            setExercise(ex)
        });
    }, []);

    let columns: JSX.Element[] = []
    let colElements = [];
    if (exercise.modules.length > 0) {
        for (let i = 0; i < 4; i++) {
            let temp = exercise.modules.find((val) => val.position === i+1);
            if (temp !== undefined) {
                colElements.push((<Allotment.Pane key={i}>
                    {getModuleFromType(temp, null, exercise, (newEx) => setExercise(newEx), false)}
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
                <span className='place-center exercise-title-text'>{exercise.title}</span>
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

export async function fetchExercise(exerciseId: number, exerciseGroupId: number, courseId: number, callback: (newExercise: Exercise) => void) {
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
        await fetch(getApiRoot() + 'courses/' + courseId + '/exercise-groups/' + exerciseGroupId + '/exercises/' + exerciseId, requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error(res.statusText);
                }
                return res.json();
            })
            .then((exercise) => {
                callback(exercise);
            });
     } catch (error) {
        alert(error);
     }
}

export function getModuleFromType (module: ExerciseModule,
                                   changeModuleModalRef: React.RefObject<ShowChangeModuleModalRef> | null,
                                   exercise: Exercise,
                                   setExerciseCallback: (exercise: Exercise) => void,
                                   editMode: boolean
                                   ): React.ReactNode {
    console.log(module.type)
    switch (module.type) {
        case ModuleType.EMPTY:
            return <EmptyModule changeModuleModalRef={changeModuleModalRef} position={module.position} />;
        case ModuleType.EXERCISE_DESCRIPTION:
            return (
                <ExerciseDescriptionModule 
                    changeModuleModalRef={changeModuleModalRef} 
                    position={module.position} 
                    editMode={editMode} 
                    title={module?.title ?? ''} 
                    body={module?.content ?? ''} 
                    changedContent={(position: number, title: string, body: string) => {
                        let mods = exercise.modules.map((m) => {
                            if (m.position === position) {
                                m.title = title;
                                m.content = body;
                            }
                            return m;
                        });
                        setExerciseCallback({...exercise, modules: mods});
                    }}
                />
            );
        default:
            return <EmptyModule changeModuleModalRef={changeModuleModalRef} position={module.position} />;
    };
}