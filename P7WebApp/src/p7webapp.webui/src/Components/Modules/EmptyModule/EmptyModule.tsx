import React, { useState } from 'react';
import { ModuleType } from '../../ExerciseBoard/ExerciseBoard';
import { ShowChangeModuleModalRef } from '../../Modals/ChangeModuleModal/ChangeModuleModal';
import '../Module.css';

interface EmptyModuleProps {
    id: number[];
    changeModuleModalRef: React.RefObject<ShowChangeModuleModalRef>;
}

export default function EmptyModule(props: EmptyModuleProps) {

    return (
        <>
            <div id='exercise-description-container' className='module-container'>
                <div 
                    className={'d-flex align-items-center justify-content-center flex-grow-1'} 
                    onClick={()=>{props.changeModuleModalRef.current?.handleShow(ModuleType.EMPTY, props.id)}}
                >
                    <button>CHANGE MODULE</button>
                </div>
            </div>
        </>
    )
}

