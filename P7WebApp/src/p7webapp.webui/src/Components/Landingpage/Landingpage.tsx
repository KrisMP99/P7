import React, { useEffect, useRef, useState } from 'react';
import { Container } from 'react-bootstrap';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import OwnedCourseOverview, { CourseOverview } from './LandingpageModules/OwnedCourseOverview';
import AttendeeCourseOverview from './LandingpageModules/AttendeeCourseOverview';
import CreateCourseModal from '../Modals/CreateCourseModal/CreateCourseModal';
import { ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import { dummyDataOwned } from './dummyDataOwned';
import { dummyDataAttendee } from './dummyDataAttendee';
import { getApiRoot, User } from '../../App';

interface LandingpageProps {
    user: User;
}

export default function Landingpage(props: LandingpageProps): JSX.Element {
    
    return (
        <Container>
            <div className='tabs-container'>
                <Tabs
                    defaultActiveKey="My courses"
                    id="fill-tab-example"
                    className="mb-3 mt-3"
                    fill>
                    <Tab eventKey="My courses" title="My courses">
                        <OwnedCourseOverview />
                    </Tab>
                    <Tab eventKey="Attending courses" title="Attending courses">
                        <AttendeeCourseOverview />
                    </Tab>
                </Tabs>
            </div>
        </Container>
    );
}