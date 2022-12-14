import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { getApiRoot } from '../../../App';
import { Course } from '../../Course/CourseView';
import './OwnedCourseModal.css';

interface CreateCourseModalProps {
    createdCourse: () => void;
}

export interface ShowCreateCourseModal {
    handleShow: (userId: number) => void;
}

export const CreateCourseModal = forwardRef<ShowCreateCourseModal, CreateCourseModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const emptyCourse: Course = {
        id: 0,
        title: '',
        description: '',
        ownerId: 0,
        ownerName: 'undefined',
        exerciseGroups: [],
        isPrivate: true,
        createdDate: null,
        modifiedDate: null,
        attendees: []
    };
    const [course, setCourse] = useState<Course>(emptyCourse);
    
    const handleClose = () => setShow(false);

    useImperativeHandle(
        ref,
        () => ({
            handleShow(userId: number) {
                setCourse({...course, ownerId: userId});
                setShow(true);
            }
        }),
    );

    useEffect(() => {
        if (show) {
            setCourse({...emptyCourse});
        }
    }, [show])

    return (
        <Modal show={show} onHide={handleClose}>
            <Form onSubmit={(e) => {
                e.preventDefault();
                createCourse(course.title, course.description, course.isPrivate, () => {
                    props.createdCourse();
                    handleClose();
                });
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>Create course:</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Name:</Form.Label>
                        <Form.Control className='modal-form-field-text' type="text" required value={course.title} maxLength={30} onChange={(e) => {
                            setCourse({...course, title: e.target.value});
                        }} />
                    </Form.Group>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Private:</Form.Label>
                        <Form.Check type="switch" checked={course.isPrivate} onChange={(e) => {
                            setCourse({...course, isPrivate: e.target.checked});
                        }} />
                    </Form.Group>
                    <Form.Group className="mb-3">
                        <Form.Label>Description:</Form.Label>
                        <Form.Control as="textarea" style={{resize: 'none'}} className='mb-3 modal-form-field' rows={5} required value={course.description} onChange={(e) => {
                            setCourse({...course, description: e.target.value});
                        }} />
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


async function createCourse(title: string, description: string, isPrivate: boolean, callback: ()=>void) {
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
                'title': title,
                'description': description,
                'isPrivate': isPrivate
            })
        }
        await fetch(getApiRoot() + "courses", requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend - server unavailable');
                }
                return null;
            })
            .then(() => {
                callback();
            });
    } catch (error) {
        alert(error);
    }
}



export default CreateCourseModal;