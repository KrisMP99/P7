import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import '../../../App.css';
import { ModuleType } from '../../ExerciseBoard/ExerciseBoard';
import { getModuleOptions } from '../CreateExerciseModal/CreateExerciseModal';

interface ChangeModuleModalProps {
    changedModule: (module: ModuleType, position: number) => void;
}
export interface ShowChangeModuleModalRef {
    handleShow: (updateModule: ModuleType, position: number) => void;
}

export const ChangeModuleModal = forwardRef<ShowChangeModuleModalRef, ChangeModuleModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [module, setModule] = useState<ModuleType>(ModuleType.EMPTY);
    const [index, setIndex] = useState<number>(0)

    const handleClose = () => setShow(false);

    useImperativeHandle(
        ref,
        () => ({
            handleShow(module: ModuleType, position: number) {
                setShow(true);
                setModule(module);
                setIndex(position);
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
                    <Modal.Title>Change Exercise Module:</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Module:</Form.Label>
                        <Form.Select onChange={(e) => setModule(Number(e.target.value))} value={module}>
                            <option value={ModuleType.EMPTY}>{getModuleOptions(ModuleType.EMPTY)}</option>
                            <option value={ModuleType.EXERCISE_DESCRIPTION}>{getModuleOptions(ModuleType.EXERCISE_DESCRIPTION)}</option>
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