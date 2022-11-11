import React from 'react';
import { Gear, Pencil } from 'react-bootstrap-icons';
import './ModuleActionBar.css';
import '../Module.css';
import '../../../App.css';

interface ModuleActionBarProps {
    changeEditMode: () => void;
    changeModule: () => void;
}

export default function ModuleActionBar(props: ModuleActionBarProps) {
    return (
        <div className='action-bar-container d-flex'>
            <div style={{marginLeft:'auto'}}>
                <Pencil className='scale-transition btn-1' onClick={props.changeEditMode}/>
                <Gear className='scale-transition btn-1' onClick={props.changeModule}/>
            </div>
        </div>
    )
}
