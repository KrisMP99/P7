import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
import { getApiRoot } from '../../../App';
import CourseView, { Course, fetchCourse } from '../../Course/CourseView';

interface EditCourseModalProps {
    updatedCourse: () => void;
}

export interface ShowEditCourseModal {
    handleShow: (courseId: number) => void;
}

export const EditCourseModal = forwardRef<ShowEditCourseModal, EditCourseModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const emptyCourse: Course = {
        id: 0,
        title: '',
        description: '',
        ownerId: 'undefined',
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
            handleShow(courseId: number) {
                fetchCourse(courseId, (course)=>{
                    setCourse(course);
                });
                setShow(true);
            }
        }),
    );

    return (
        <Modal show={show} onHide={handleClose}>
            <Form onSubmit={(e) => {
                e.preventDefault();
                editCourse(course.title, course.description, course.isPrivate, course.id, () => {
                    props.updatedCourse();
                    handleClose();
                });
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>Edit course:</Modal.Title>
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
                        Save
                    </Button>
                </Modal.Footer>
            </Form>
        </Modal>

    )
});


async function editCourse(title: string, description: string, isPrivate: boolean, id : number, callback: ()=>void) {
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
                'id': id,
                'title': title,
                'description': description,
                'isPrivate': isPrivate
            })
        }
        await fetch(getApiRoot() + "courses/update", requestOptions)
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



export default EditCourseModal;