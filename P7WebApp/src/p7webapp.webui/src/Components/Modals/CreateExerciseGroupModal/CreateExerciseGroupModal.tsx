import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { getApiRoot } from '../../../App';
import '../../../App.css';

interface CreateExerciseGroupModalProps {
    updateExerciseGroups: (dummyTitle: string, dummyVisibility: boolean) => void;
}
export interface ShowCreateExerciseGroupModal {
    handleShow: () => void;
}

export const CreateExerciseGroupModal = forwardRef<ShowCreateExerciseGroupModal, CreateExerciseGroupModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [title, setTitle] = useState<string>('');
    const [visible, setVisible] = useState<boolean>(true);

    const handleClose = () => setShow(false);

    useImperativeHandle(
        ref,
        () => ({
            handleShow() {
                setShow(true);
                setTitle('');
                setVisible(true);
            }
        }),
    )

    const createExerciseGroup = async () => {
        try {
            const requestOptions = {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': ''
                },
                body: JSON.stringify({
                    "username": '',
                })
            }
            await fetch(getApiRoot() + 'ExerciseGroup', requestOptions)
                .then((res) => {
                    if (!res.ok) {
                        throw new Error('Response not okay from backend - server unavailable');
                    }
                    return;
                })
                .then(() => {
                    console.log("Successfully created exercise group!");
                });
        } catch (error) {
            alert(error);
        }
    }

    return (
        <Modal show={show} onHide={handleClose}>
            <Form onSubmit={(e) => { 
                e.preventDefault();
                //WIP - POST TO CREATE EXERCISEGROUP
                createExerciseGroup();
                props.updateExerciseGroups(title, visible);
                handleClose();
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>Create Exercise Group:</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Name:</Form.Label>
                        <Form.Control className='modal-form-field-text' type="text" required value={title} maxLength={30} onChange={(e)=>{
                            setTitle(e.target.value);
                        }}/>
                    </Form.Group>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Visible:</Form.Label>
                        <Form.Check type="switch" checked={visible} onChange={(e)=>{
                            setVisible(!visible);
                        }}/>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Cancel
                    </Button>
                    <Button variant="primary" type='submit'>
                        Create
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>
    )
});

export default CreateExerciseGroupModal;