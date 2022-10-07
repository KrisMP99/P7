import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { Modal, Button, Table } from 'react-bootstrap';
import { BoardModuleType } from '../../ExerciseBoard/ExerciseBoard';
import './CreateExerciseModal.css';

interface CreateExerciseModalProps {
    created: (cols: BoardModuleType[][]) => void;
}
export interface ShowModal {
    handleShow(): void;
}

export const CreateExerciseModal = forwardRef<ShowModal, CreateExerciseModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [columnCount, setColumnCount] = useState(1);
    const [rowCount, setRowCount] = useState<number[]>([1]);

    const handleClose = () => setShow(false);


    useImperativeHandle(
        ref,
        () => ({
            handleShow() {
                setShow(true);
            }
        }),
    )

    let rows: JSX.Element[] = [];
    let rowTables: JSX.Element[] = [];
    for (let i = 0; i < columnCount; i++) {
        rows.push((
            <div className='row-input-container' key={i}>
                <label>Rows for column {i + 1}:</label>
                <input type={'number'} min={1} max={5}
                    value={rowCount[i]}
                    onKeyDown={(e) => e.preventDefault()}
                    onChange={(e) => {
                        setRowCount(rowCount.map((val, index) => {
                            if (i === index) return Number(e.currentTarget.value);
                            return val;
                        }));
                    }}
                />
            </div>
        ));
        let tableRow = [];
        for (let j = 0; j < rowCount[i]; j++) {
            tableRow.push((
                <tbody key={j}>
                    <tr>
                        <td>{j+1}</td>
                        <td>
                            <select>
                                <option>{getModuleOptions(BoardModuleType.ExerciseDescription)}</option>
                            </select>
                        </td>
                    </tr>
                </tbody>
            ));
        }
        rowTables.push(
            <div key={i}>
                <Table>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Row Content:</th>
                        </tr>
                    </thead>
                    { tableRow }
                </Table>
            </div>
        )
    }

    return (
        <Modal show={show} onHide={handleClose} size='lg'>
            <Modal.Header closeButton>
                <Modal.Title>Create exercise:</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <div>
                    <label>Colums:</label>
                    <input type={'number'} min={1} max={3} onKeyDown={(e) => e.preventDefault()} value={columnCount} onChange={(e) => {
                        if (Number(e.target.value) > columnCount) {
                            setRowCount([...rowCount, 1]);
                        }
                        else if (Number(e.target.value) < columnCount) {
                            setRowCount(rowCount.slice(0, Number(e.target.value)));
                        }
                        setColumnCount(Number(e.target.value));
                    }} />
                </div>
                <div className='flex-space-evenly'>
                    {rows}
                </div>
                <div className='flex-space-evenly'>
                    {rowTables}
                </div>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>
                    Close
                </Button>
                <Button variant="primary" onClick={
                    () => {
                        let columns: BoardModuleType[][] = [];
                        for (let i = 0; i < columnCount; i++) {
                            columns.push([]);
                            for (let j = 0; j < rowCount[i]; j++) {
                                columns[i].push(BoardModuleType.ExerciseDescription);
                            }
                        }
                        props.created(columns);
                        handleClose();
                    }
                }>
                    Save Changes
                </Button>
            </Modal.Footer>
        </Modal>
    )
});

function getModuleOptions(id: number) {
    switch (id) {
        case BoardModuleType.ExerciseDescription:
            return 'Exercise Description';
        default:
            return 'Error';
    }
} 

export default CreateExerciseModal;