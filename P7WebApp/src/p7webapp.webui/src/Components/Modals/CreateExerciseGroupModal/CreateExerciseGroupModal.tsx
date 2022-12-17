import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { getApiRoot } from '../../../App';
import '../../../App.css';
import { ExerciseGroup } from '../../Course/CourseView';

interface CreateExerciseGroupModalProps {
    updatedExerciseGroups: () => void;
}
export interface ShowCreateExerciseGroupModal {
    handleShow: (courseId: number, existingGroupsAmount: number) => void;
}

export const CreateExerciseGroupModal = forwardRef<ShowCreateExerciseGroupModal, CreateExerciseGroupModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [title, setTitle] = useState<string>('');
    const [visible, setVisible] = useState<boolean>(true);
    const [courseId, setCourseId] = useState<number>(-1);
    const [groupsAmount, setGroupsAmount] = useState<number>(0);
    const handleClose = () => setShow(false);

    useImperativeHandle(
        ref,
        () => ({
            handleShow(currCourseId: number, existingGroupsAmount: number) {
                setGroupsAmount(existingGroupsAmount);
                setCourseId(currCourseId);
                setShow(true);
                setTitle('');
                setVisible(true);
            }
        }),
    )

    return (
        <Modal show={show} onHide={handleClose}>
            <Form onSubmit={(e) => { 
                e.preventDefault();
                createExerciseGroup(courseId, title, groupsAmount, visible, () => {
                    props.updatedExerciseGroups();
                    handleClose();
                });
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

async function createExerciseGroup(courseId: number, title: string, groupNumber: number, visible: boolean, callback: () => void) {
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
                "description": "undefined",
                "exerciseGroupNumber": groupNumber + 1,
                "isVisible": visible,
                "visibleFromDate": null
            })
        }
        
        await fetch(getApiRoot() + 'courses/exercise-groups', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Failed to create exercise group - server unavailable');
                }
                callback();
            });
    } catch (error) {
        alert(error);
    }
}

export default CreateExerciseGroupModal;