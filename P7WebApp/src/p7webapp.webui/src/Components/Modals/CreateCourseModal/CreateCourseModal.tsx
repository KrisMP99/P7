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
        title: '',
        description: '',
        ownerId: 0,
        exerciseGroups: [],
        exercises: [],
        private: true,
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
                createCourse(course.title, course.description, course.private);
                //WIP - POST TO CREATE COURSE
                props.createdCourse();
                handleClose();
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
                        <Form.Check type="switch" checked={course.private} onChange={(e) => {
                            setCourse({...course, private: e.target.checked});
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


async function createCourse(title: string, description: string, isPrivate: boolean) {
    
    try {
        const requestOptions = {
            method: 'POST',
            headers: { 
                'Accept': 'application/json', 
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                "title": title,
                "description": description,
                "isPrivate": isPrivate
            })
        }
        await fetch(getApiRoot() + 'Course', requestOptions)
            .then((res) => {
                if (!res.ok) {
                    throw new Error('Response not okay from backend - server unavailable');
                }
                return null;
            })
            .then(() => {
                console.log("Successfully created course!");
            });
    } catch (error) {
        alert(error);
    }
}



export default CreateCourseModal;