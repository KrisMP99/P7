import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Modal, Button, Table, Form } from 'react-bootstrap';
import '../../../App.css';
import { ModuleType } from '../../ExerciseBoard/ExerciseBoard';

interface ChangeModuleModalProps {
    changedModule: (module: ModuleType, index: number[]) => void;
}
export interface ShowChangeModuleModalRef {
    handleShow: (updateModule: ModuleType, index: number[]) => void;
}

export const ChangeModuleModal = forwardRef<ShowChangeModuleModalRef, ChangeModuleModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [updateModule, setUpdateModule] = useState<ModuleType>();
    const [module, setModule] = useState<ModuleType>(ModuleType.EMPTY);
    const [index, setIndex] = useState<number[]>([])

    const handleClose = () => setShow(false);

    useImperativeHandle(
        ref,
        () => ({
            handleShow(module: ModuleType, index: number[]) {
                setShow(true);
                setModule(module);
                setIndex(index);
            }
        }),
    )

    return (
        <Modal show={show} onHide={handleClose}>
            <Form onSubmit={(e) => {
                e.preventDefault();
                props.changedModule(module, index);
                handleClose();
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>Create Exercise Group:</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Module:</Form.Label>
                        <Form.Select>
                            <option value={ModuleType.EMPTY} selected>Open this select menu</option>
                            <option value={ModuleType.EXERCISE_DESCRIPTION}>One</option>
                        </Form.Select>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Cancel
                    </Button>
                    <Button variant="primary" type='submit'>
                        Accept
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>
    )
});

export default ChangeModuleModal;