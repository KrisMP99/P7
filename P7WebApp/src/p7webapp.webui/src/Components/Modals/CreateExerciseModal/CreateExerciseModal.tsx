import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Modal, Button, Form } from 'react-bootstrap';
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
import { ExerciseOverview } from '../../Course/CourseView';
import { getApiRoot } from '../../../App';

interface CreateExerciseModalProps {
    created: (layout: LayoutType, exercise: ExerciseOverview) => void;
}
export interface ShowModal {
    handleShow(): void;
}
export interface ShowCreateExerciseModal {
    handleShow: (exerciseGroupId: number) => void;
}

export enum LayoutType {
    SINGLE = 1,
    TWO_VERTICAL = 2,
    TWO_HORIZONTAL = 3,
    TWO_LEFT_ONE_RIGHT = 4,
    ONE_LEFT_TWO_RIGHT = 5,
    TWO_LEFT_TWO_RIGHT = 6
}

export const CreateExerciseModal = forwardRef<ShowCreateExerciseModal, CreateExerciseModalProps>((props, ref) => {
    const [show, setShow] = useState(false);
    // const [exerciseGroupId, setExerciseGroupId] = useState<number>(-1);
    // const [exercise, setExercise] = useState<Exercise>({id: 0, exerciseGroupId: 0, title:'', isVisible: false});
    const [exercise, setExercise] = useState<ExerciseOverview>({
        id: 0,
        title: '',
        isVisible: true,
        exerciseNumber: 1,
        startDate: null,
        endDate: null,
        visibleFrom: null,
        visibleTo: null,
        createdDate: null
    });
    const [layout, setLayout] = useState<LayoutType>(LayoutType.SINGLE);

    const handleClose = () => setShow(false);


    useImperativeHandle(
        ref,
        () => ({
            handleShow(exerciseGroupId: number) {
                setShow(true);
                setExercise({...exercise});
            }
        }),
    );

    useEffect((() => {
        setExercise(e => ({ ...e, title: ''}));
        setLayout(LayoutType.SINGLE);
    }), [show]);

    const handleChooseLayout = (type: LayoutType, left: number, right: number) => {
        setLayout(type);
    }

    // const createExercise = async () => {
    //     let jwt = sessionStorage.getItem('jwt');
    //     if (jwt === null) return;
    //     try {
    //         const requestOptions = {
    //             method: 'POST',
    //             headers: {
    //                 'Accept': 'application/json',
    //                 'Content-Type': 'application/json',
    //                 'Authorization': 'Bearer ' + jwt
    //             },
    //             body: JSON.stringify({
    //                 "exerciseGroupId": '',
    //                 "title": '',
    //                 "isVisible": '',
    //                 "exerciseNumber": '',
    //                 "startDate": '',
    //                 "endDate": '',
    //                 "visibleFrom": '',
    //                 "visibleTo": '',
    //                 "createdDate": '',
    //                 "lastModifiedDate": '',
    //                 "layoutId": '',
    //             })
    //         }
    //         await fetch(getApiRoot() + 'Exercise', requestOptions)
    //             .then((res) => {
    //                 if (!res.ok) {
    //                     throw new Error('Response not okay from backend - server unavailable');
    //                 }
    //                 return res.json();
    //             })
    //             .then((user: any) => {
    //                 console.log("Successfully created exercise!");
    //             });
    //     } catch (error) {
    //         alert(error);
    //     }
    // }

    return (
        <Modal show={show} onHide={handleClose} size='lg'>
            <form onSubmit={(e) => {
                e.preventDefault();
                //WIP - Create exercise?? Or open in edit mode??
                props.created(layout, exercise);
                handleClose();
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>Create exercise:</Modal.Title>
                </Modal.Header>
                <Modal.Body >
                    <div className="mb-3 form-group modal-form-field">
                        <label>Name:</label>
                        <input className='modal-form-field-text form-control' type="text" required value={exercise.title} maxLength={35} onChange={(e) => {
                            setExercise({...exercise, title: e.target.value});
                        }} />
                    </div>
                    <Form.Group className="mb-3 modal-form-field">
                        <Form.Label>Visible:</Form.Label>
                        <Form.Check type="switch" checked={exercise.isVisible} onChange={(e)=>{
                            setExercise({...exercise, isVisible: !exercise.isVisible});
                        }}/>
                    </Form.Group>
                    <div className="mb-3">
                        <label>Layout:</label>
                        <div>
                            <div className='layout-row'>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.SINGLE} onChange={(e) => { handleChooseLayout(Number(e.target.value), 1, 0) }} />
                                    <img src={layout === LayoutType.SINGLE ? selectedSingleImg : defaultSingleImg} alt="Single page" />
                                </label>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.TWO_HORIZONTAL} onChange={(e) => { handleChooseLayout(Number(e.target.value), 2, 0) }} />
                                    <img src={layout === LayoutType.TWO_HORIZONTAL ? selectedTwoHorizontalImg : defaultTwoHorizontalImg} alt="Single page" />
                                </label>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.TWO_VERTICAL} onChange={(e) => { handleChooseLayout(Number(e.target.value), 1, 1) }} />
                                    <img src={layout === LayoutType.TWO_VERTICAL ? selectedTwoVertivalImg : defaultTwoVertivalImg} alt="Single page" />
                                </label>
                            </div>
                            <div className='layout-row'>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.ONE_LEFT_TWO_RIGHT} onChange={(e) => { handleChooseLayout(Number(e.target.value), 1, 2) }} />
                                    <img src={layout === LayoutType.ONE_LEFT_TWO_RIGHT ? selectedOneLeftTwoRightImg : defaultOneLeftTwoRightImg} alt="Single page" />
                                </label>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.TWO_LEFT_ONE_RIGHT} onChange={(e) => { handleChooseLayout(Number(e.target.value), 2, 1) }} />
                                    <img src={layout === LayoutType.TWO_LEFT_ONE_RIGHT ? selectedTwoLeftOneRightImg : defaultTwoLeftOneRightImg} alt="Single page" />
                                </label>
                                <label>
                                    <input type="radio" name="layout" value={LayoutType.TWO_LEFT_TWO_RIGHT} onChange={(e) => { handleChooseLayout(Number(e.target.value), 2, 2) }} />
                                    <img src={layout === LayoutType.TWO_LEFT_TWO_RIGHT ? selectedTwoLeftTwoRightImg : defaultTwoLeftTwoRightImg} alt="Single page" />
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

export function getModuleOptions(id: string) {
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