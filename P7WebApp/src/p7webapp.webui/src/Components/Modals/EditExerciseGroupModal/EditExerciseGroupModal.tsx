import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { getApiRoot } from '../../../App';
import '../../../App.css';
import { ExerciseGroup } from '../../Course/CourseView';

interface EditExerciseGroupModalProps {
    updatedExerciseGroup: (newExGroup: ExerciseGroup) => void;
}
export interface ShowEditExerciseGroupModal {
    handleShow: (exerciseGroup: ExerciseGroup, index: number) => void;
}

export const EditExerciseGroupModal = forwardRef<ShowEditExerciseGroupModal, EditExerciseGroupModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [exGroup, setExGroup] = useState<ExerciseGroup | null>(null);
    const [courseId, setCourseId] = useState<number | null>(null);

    const handleClose = () => setShow(false);

    useImperativeHandle(
        ref,
        () => ({
            handleShow(exerciseGroup: ExerciseGroup, courseId: number) {
                setShow(true);
                if (exerciseGroup === null || courseId === null) return;
                setExGroup(exerciseGroup);
                setCourseId(courseId);
            }
        }),
    )

    return (!exGroup ? (<></>) :
        (<Modal show={show} onHide={handleClose}>
            <Form onSubmit={(e) => { 
                e.preventDefault();
                updateExerciseGroup(courseId!, exGroup, () => {
                    props.updatedExerciseGroup(exGroup!);
                    handleClose();
                });
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit Exercise Group:</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Name:</Form.Label>
                        <Form.Control className='modal-form-field-text' type="text" required value={exGroup!.title} maxLength={30} onChange={(e)=>{
                            setExGroup({...exGroup!, title: e.target.value});
                        }}/>
                    </Form.Group>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Visible:</Form.Label>
                        <Form.Check type="switch" checked={exGroup!.isVisible} onChange={(e)=>{
                            setExGroup({...exGroup!, isVisible: !exGroup!.isVisible});
                        }}/>
                    </Form.Group>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Cancel
                    </Button>
                    <Button variant="primary" type='submit'>
                        Confirm
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>)
    );
});
    
async function updateExerciseGroup(courseId: number, exerciseGroup: ExerciseGroup, callback: () => void) {
    let jwt = sessionStorage.getItem('jwt');
    if (jwt === null) return;
    try {
        const requestOptions = {
            method: 'PUT',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Authorization': 'Bearer ' + jwt
            },
            body: JSON.stringify({
                "courseId": courseId,
                "id": exerciseGroup.id,
                "title": exerciseGroup.title,
                "description": "undefined",
                "isVisible": exerciseGroup.isVisible,
                "exerciseGroupNumber": exerciseGroup.exerciseGroupNumber,
                "becomesVisibleAt": null
            })
        }
        await fetch(getApiRoot() + 'courses/exercise-groups', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Failed to update exercise group - server unavailable');
                }
                callback();
            });
    } catch (error) {
        alert(error);
    }
}

export default EditExerciseGroupModal;