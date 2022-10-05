import React, { useState, forwardRef, useImperativeHandle, useCallback } from 'react';
import { Modal, Button } from 'react-bootstrap';
import { useNavigate } from 'react-router-dom';
import ExerciseDescriptionModule from '../../Modules/ExerciseDescription/ExerciseDescription';
import './CreateExerciseModal.css';

interface CreateExerciseModalProps {

    created: (cols: JSX.Element[][]) => void;

}
export interface ShowModal {
    handleShow(): void;
}

export const CreateExerciseModal = forwardRef<ShowModal, CreateExerciseModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [cols, setCols] = useState<JSX.Element[][]>([])
    const [columnCount, setColumnCount] = useState(1);
    const [rowCount, setRowCount] = useState<number[]>([1]);

    const handleClose = () => setShow(false);
    const handleAddCol = () => setCols(cols => [...cols, []]);
    const handleAddRow = (colIndex: number,) =>
        setCols(cols => [...cols.map((val, index) => {
            if (colIndex === index) {
                return [...val, <ExerciseDescriptionModule />]
            }
            return val
        })]);


    useImperativeHandle(
        ref,
        () => ({
            handleShow() {
                setShow(true);
            }
        }),
    )

    let rows: JSX.Element[] = [];
    for (let i = 0; i < columnCount; i++) {
        if(rowCount[columnCount - 1] === undefined) setRowCount([...rowCount.map((val) => {if(val != undefined) {return val;} else return 1 }), 1] );
        else if (rowCount[columnCount] !== undefined) setRowCount(rowCount.slice(0, -1));
        rows.push((
            <div>
                <label>Rows for column {i}:</label>
                <input type={'number'} min={1} max={50} 
                    defaultValue={1}
                    onKeyDown={(e)=>e.preventDefault()} 
                    onChange={(e) => { 
                        setRowCount(rowCount.map((val, index)=>{
                            if(i === index) return Number(e.target.value);
                            return val;
                        }));
                    }}
                />
            </div>
        ));
    }

    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Create exercise:</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                //ADD FUNCTIONALITY HERE
                <div>
                    <label>Colums:</label>
                    <input type={'number'} min={1} max={30} onKeyDown={(e)=>e.preventDefault()} value={columnCount} onChange={(e) => { setColumnCount(Number(e.target.value)) }} />
                </div>
                <div>
                    { rows }
                </div>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>
                    Close
                </Button>
                <Button variant="primary" onClick={
                    () => {
                        let columns: JSX.Element[][] = [];
                        for (let i = 0; i < columnCount; i++) {
                            columns.push([]);
                        }
                        for (let i = 0; i < columnCount; i++) {
                            columns[i].push(<ExerciseDescriptionModule/>);
                        }
                        props.created(columns)
                        handleClose();
                    }
                }>
                    Save Changes
                </Button>
            </Modal.Footer>
        </Modal>
    )
});
export default CreateExerciseModal;