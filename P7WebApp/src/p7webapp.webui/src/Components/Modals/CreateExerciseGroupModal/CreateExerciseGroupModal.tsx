import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { getApiRoot } from '../../../App';
import '../../../App.css';

interface CreateExerciseGroupModalProps {
    updateExerciseGroups: () => void;
}
export interface ShowCreateExerciseGroupModal {
    handleShow: (courseId: number) => void;
}

export const CreateExerciseGroupModal = forwardRef<ShowCreateExerciseGroupModal, CreateExerciseGroupModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [title, setTitle] = useState<string>('');
    const [visible, setVisible] = useState<boolean>(true);
    const [courseId, setCourseId] = useState<number>(-1);
    const handleClose = () => setShow(false);

    useImperativeHandle(
        ref,
        () => ({
            handleShow(currCourseId: number) {
                setCourseId(currCourseId);
                setShow(true);
                setTitle('');
                setVisible(true);
            }
        }),
    )

    const createExerciseGroup = async () => {
        let jwt = sessionStorage.getItem('jwt');
        if (jwt === null) return;
        try {
            const requestOptions = {
                method: 'POST',
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json',
                    'Authorization': 'Bearer ' + jwt
                },
                body: JSON.stringify({
                    "courseId": courseId,
                    "title": title,
                    "description": "s",
                    "exerciseGroupNumber": 0,
                    "isVisible": visible,
                    "visibleFromDate": null
                })
            }
            
            await fetch(getApiRoot() + 'courses/exercise-group', requestOptions)
                .then((res) => {
                    if (!res.ok) {
                        throw new Error('Failed to create exercise group - server unavailable');
                    }
                    return;
                })
                .then(() => {
                    console.log("Successfully created exercise group!");
                    props.updateExerciseGroups();
                    handleClose();
                });
        } catch (error) {
            alert(error);
        }
    }

    return (
        <Modal show={show} onHide={handleClose}>
            <Form onSubmit={(e) => { 
                e.preventDefault();
                createExerciseGroup();
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