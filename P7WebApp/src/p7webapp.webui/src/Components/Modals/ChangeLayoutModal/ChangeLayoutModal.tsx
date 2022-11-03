import React, { useState, forwardRef, useImperativeHandle, useEffect } from 'react';
import { Modal, Button } from 'react-bootstrap';
import './ChangeLayoutModal.css';
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
import { Layout, LayoutType, ShowModal } from '../CreateExerciseModal/CreateExerciseModal';

interface ChangeLayoutProps {
    changedLayout: (layout: LayoutType) => void;
    currentLayout: LayoutType;
}


export const ChangeLayoutModal = forwardRef<ShowModal, ChangeLayoutProps>((props, ref) => {
    const [show, setShow] = useState(false);
    const [layout, setLayout] = useState<LayoutType>(LayoutType.SINGLE);

    const handleClose = () => setShow(false);


    useImperativeHandle(
        ref,
        () => ({
            handleShow() {
                setShow(true);
                setLayout(props.currentLayout);
            }
        }),
    );

    const handleChooseLayout = (type: LayoutType, left: number, right: number) => {
        setLayout(type);
    }

    return (
        <Modal show={show} onHide={handleClose} size='lg'>
            <form onSubmit={(e) => {
                e.preventDefault();
                props.changedLayout(layout);
                handleClose();
            }}>
                <Modal.Header closeButton>
                    <Modal.Title>Create exercise:</Modal.Title>
                </Modal.Header>
                <Modal.Body >
                    <div className="mb-3">
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
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
                    </Button>
                    <Button variant="primary" type='submit'>
                        Confirm
                    </Button>
                </Modal.Footer>
            </form>
        </Modal>
    )
});

export default ChangeLayoutModal;