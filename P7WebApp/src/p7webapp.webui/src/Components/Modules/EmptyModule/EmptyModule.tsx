import React from 'react';
import { Plus } from 'react-bootstrap-icons';
import { ModuleType } from '../../ExerciseBoard/ExerciseBoard';
import { ShowChangeModuleModalRef } from '../../Modals/ChangeModuleModal/ChangeModuleModal';
import '../Module.css';

interface EmptyModuleProps {
    position: number;
    changeModuleModalRef: React.RefObject<ShowChangeModuleModalRef> | null;
}

export default function EmptyModule(props: EmptyModuleProps) {
    return (
        <>
            <div id='exercise-description-container' className='module-container'>
                <div 
                    className={'d-flex align-items-center justify-content-center flex-grow-1'} 
                >
                    <Plus size={70} className='scale-transition' onClick={()=>{
                        props.changeModuleModalRef?.current?.handleShow(ModuleType.EMPTY, props.position)
                    }}/>
                </div>
            </div>
        </>
    )
}

