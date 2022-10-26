import React, { useState, forwardRef } from 'react';
import { Modal, Button } from 'react-bootstrap';
import { ShowModal } from '../CreateExerciseModal/CreateExerciseModal';

export const CreateCourseModal = forwardRef<ShowModal>((props, ref) => {
    const [show, setShow] = useState(false);
    const handleClose = () => setShow(false);

    return (
       <>
            <Modal show={show} onHide={handleClose} 
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered>
                <Modal.Header closeButton>
                <Modal.Title>Create course!</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <div className='d-flex justify-content-center'>
                        <div className="col-6">
                            <form>
                                <div className="row mt-3">
                                    <label>Course name:</label>
                                    <input className='rounded' type="text"/>
                                </div>

                                <div className="row mt-3">
                                    <label>Course description:</label>
                                    <input className='rounded' type="text"/>
                                </div>

                                <div className="row mt-3 mb-3">
                                    <label>Availability:</label>
                                    <input className='rounded' type="text"/>
                                </div>
                            </form>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                <div className="row footer text-center">
                    <div className='col-6'>
                        <Button className="cancel" onClick={handleClose}>
                            Cancel
                        </Button>
                    </div>
                    <div className='col-6'>
                        <Button className='create' onClick={handleClose}>
                            Save Changes
                        </Button>
                    </div>
                </div>
                </Modal.Footer>
            </Modal>
        </>

    )
});


export default CreateCourseModal;