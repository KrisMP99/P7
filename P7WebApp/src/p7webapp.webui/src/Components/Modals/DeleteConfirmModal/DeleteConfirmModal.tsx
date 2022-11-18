import React, { useState, forwardRef, useImperativeHandle } from 'react';
import { Modal, Button } from 'react-bootstrap';

interface DeleteConfirmModalProps {
    confirmDelete: (id: number, type: DeleteElementType) => void;
}
export interface ShowDeleteConfirmModal {
    handleShow: (displayName: string, id: number, type: DeleteElementType) => void;
}

export enum DeleteElementType {
    EXERCISE,
    EXERCISEGROUP,
    COURSE
}

export const DeleteConfirmModal = forwardRef<ShowDeleteConfirmModal, DeleteConfirmModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [displayName, setDisplayName] = useState<string>('');
    const [deleteId, setDeleteId] = useState<number>(0);
    const [type, setType] = useState<DeleteElementType | null>(null);

    const handleClose = () => setShow(false);

    useImperativeHandle(
        ref,
        () => ({
            handleShow(name: string, id: number, delType: DeleteElementType) {
                setDisplayName(name);
                setDeleteId(id);
                setType(delType);
                setShow(true);
            }
        }),
    )


    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Warning!</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <span>
                    Are you sure that you want to delete {displayName}?<br/><br/>
                    This action cannot be undone
                </span>
            </Modal.Body>
            <Modal.Footer>
                <Button variant="secondary" onClick={handleClose}>
                    Cancel
                </Button>
                <Button variant="danger" onClick={() => {
                        props.confirmDelete(deleteId, type!);
                        handleClose();
                    }
                }>
                    Delete
                </Button>
            </Modal.Footer>
        </Modal>
    )
});

export default DeleteConfirmModal;