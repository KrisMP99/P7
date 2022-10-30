import React, { useState } from 'react';
import { Plus, PlusCircle } from 'react-bootstrap-icons';
import { ModuleType } from '../../ExerciseBoard/ExerciseBoard';
import { ShowChangeModuleModalRef } from '../../Modals/ChangeModuleModal/ChangeModuleModal';
import '../Module.css';

interface EmptyModuleProps {
    index: number[];
    changeModuleModalRef: React.RefObject<ShowChangeModuleModalRef>;
}

export default function EmptyModule(props: EmptyModuleProps) {
    const plusRef = useState<boolean>(false);

    return (
        <>
            <div id='exercise-description-container' className='module-container'>
                <div 
                    className={'d-flex align-items-center justify-content-center flex-grow-1'} 
                >
                    <Plus size={70} className='scale-transition' onClick={()=>{props.changeModuleModalRef.current?.handleShow(ModuleType.EMPTY, props.index)}}/>
                </div>
            </div>
        </>
    )
}

