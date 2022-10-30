import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Modal, Button } from 'react-bootstrap';
import { ModuleType } from '../../ExerciseBoard/ExerciseBoard';
import './CreateExerciseModal.css';
import '../../../App.css';

import defaultSingleImg from '../../../Images/LayoutDefault/default_single.svg';
import defaultTwoVertivalImg from '../../../Images/LayoutDefault/default_vertical.svg';
import defaultTwoHorizontalImg from '../../../Images/LayoutDefault/default_horizontal.svg';
import defaultTwoLeftOneRightImg from '../../../Images/LayoutDefault/default_2left_1right.svg';
import defaultOneLeftTwoRightImg from '../../../Images/LayoutDefault/default_1left_2right.svg';
import defaultTwoLeftTwoRightImg from '../../../Images/LayoutDefault/default_2left_2right.svg';

import selectedSingleImg from '../../../Images/LayoutSelected/selected_single.svg';
import selectedTwoVertivalImg from '../../../Images/LayoutSelected/selected_vertical.svg';
import selectedTwoHorizontalImg from '../../../Images/LayoutSelected/selected_horizontal.svg';
import selectedTwoLeftOneRightImg from '../../../Images/LayoutSelected/selected_2left_1right.svg';
import selectedOneLeftTwoRightImg from '../../../Images/LayoutSelected/selected_1left_2right.svg';
import selectedTwoLeftTwoRightImg from '../../../Images/LayoutSelected/selected_2left_2right.svg';

interface CreateExerciseModalProps {
    created: (layout: Layout) => void;
}
export interface ShowModal {
    handleShow(): void;
}
export interface ShowCreateExerciseModal {
    handleShow: (exerciseGroupId: number) => void;
}

export enum LayoutType {
    SINGLE = 0,
    TWO_VERTICAL = 1,
    TWO_HORIZONTAL = 2,
    TWO_LEFT_ONE_RIGHT = 3,
    ONE_LEFT_TWO_RIGHT = 4,
    TWO_LEFT_TWO_RIGHT = 5
}

export interface Layout {
    layoutType: LayoutType;
    leftRows: number;
    rightRows: number;
}

export const CreateExerciseModal = forwardRef<ShowCreateExerciseModal, CreateExerciseModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [exerciseGroupId, setExerciseGroupId] = useState<number>(-1);
    const [title, setTitle] = useState<string>('');
    const [layout, setLayout] = useState<Layout>({ layoutType: LayoutType.SINGLE, leftRows: 1, rightRows: 0 });

    const handleClose = () => setShow(false);


    useImperativeHandle(
        ref,
        () => ({
            handleShow(exerciseGroupId: number) {
                setShow(true);
                setExerciseGroupId(exerciseGroupId);
            }
        }),
    );

    useEffect((() => {
        setTitle('');
        setLayout({ layoutType: LayoutType.SINGLE, leftRows: 1, rightRows: 0 });
    }), [exerciseGroupId, show]);

    const handleChooseLayout = (type: LayoutType, left: number, right: number) => {
        setLayout({ layoutType: type, leftRows: left, rightRows: right });
    }

    return (
        <Modal show={show} onHide={handleClose} size='lg'>
            <form onSubmit={(e) => {
                e.preventDefault();
                props.created(layout);
                handleClose();
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>Create exercise:</Modal.Title>
                </Modal.Header>
                <Modal.Body >
                    <div className="mb-3 form-group modal-form-field">
                        <label>Name:</label>
                        <input className='modal-form-field-text form-control' type="text" required value={title} maxLength={35} onChange={(e) => {
                            setTitle(e.target.value);
                        }} />
                    </div>
                    <div className="mb-3">
                        <label>Layout:</label>
                        <div>
                            <div className='layout-row'>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.SINGLE} onChange={(e) => { handleChooseLayout(Number(e.target.value), 1, 0) }} />
                                    <img src={layout.layoutType === LayoutType.SINGLE ? selectedSingleImg : defaultSingleImg} alt="Single page" />
                                </label>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.TWO_HORIZONTAL} onChange={(e) => { handleChooseLayout(Number(e.target.value), 2, 0) }} />
                                    <img src={layout.layoutType === LayoutType.TWO_HORIZONTAL ? selectedTwoHorizontalImg : defaultTwoHorizontalImg} alt="Single page" />
                                </label>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.TWO_VERTICAL} onChange={(e) => { handleChooseLayout(Number(e.target.value), 1, 1) }} />
                                    <img src={layout.layoutType === LayoutType.TWO_VERTICAL ? selectedTwoVertivalImg : defaultTwoVertivalImg} alt="Single page" />
                                </label>
                            </div>
                            <div className='layout-row'>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.ONE_LEFT_TWO_RIGHT} onChange={(e) => { handleChooseLayout(Number(e.target.value), 1, 2) }} />
                                    <img src={layout.layoutType === LayoutType.ONE_LEFT_TWO_RIGHT ? selectedOneLeftTwoRightImg : defaultOneLeftTwoRightImg} alt="Single page" />
                                </label>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.TWO_LEFT_ONE_RIGHT} onChange={(e) => { handleChooseLayout(Number(e.target.value), 2, 1) }} />
                                    <img src={layout.layoutType === LayoutType.TWO_LEFT_ONE_RIGHT ? selectedTwoLeftOneRightImg : defaultTwoLeftOneRightImg} alt="Single page" />
                                </label>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.TWO_LEFT_TWO_RIGHT} onChange={(e) => { handleChooseLayout(Number(e.target.value), 2, 2) }} />
                                    <img src={layout.layoutType === LayoutType.TWO_LEFT_TWO_RIGHT ? selectedTwoLeftTwoRightImg : defaultTwoLeftTwoRightImg} alt="Single page" />
                                </label>
                            </div>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" type='submit'>
                        Create
                    </Button>
                </Modal.Footer>
            </form>
        </Modal>
    )
});

export function getModuleOptions(id: number) {
    switch (id) {
        case ModuleType.EXERCISE_DESCRIPTION:
            return 'Exercise Description';
        case ModuleType.EMPTY:
            return 'Empty';
        default:
            return 'Error';
    }
}

export default CreateExerciseModal;