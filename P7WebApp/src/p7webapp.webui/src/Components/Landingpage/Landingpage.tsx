import React, { useRef, useState } from 'react';
import { Container} from 'react-bootstrap';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import OwnedCourseOverview from './LandingpageModules/OwnedCourseOverview';
import AttendeeCourseOverview from './LandingpageModules/AttendeeCourseOverview';
import CreateCourseModal from '../Modals/CreateCourseModal/CreateCourseModal';
import { ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';

function Landingpage(): JSX.Element {
    const openCreateCourseModal = useRef<ShowModal>(null);

    return (
        <Container>
            <div className='tabs-container'>
                <Tabs
                    defaultActiveKey="My courses"
                    id="fill-tab-example"
                    className="mb-3 mt-3"
                    fill>
                    <Tab eventKey="My courses" title="My courses">
                        <OwnedCourseOverview 
                            openCreateCourseModal={openCreateCourseModal}
                        />
                    </Tab>
                    <Tab eventKey="Attending courses" title="Attending courses">
                        <AttendeeCourseOverview />
                    </Tab>
                </Tabs>
            </div>
            <CreateCourseModal ref={openCreateCourseModal}/>
        </Container>
    );
}

export default Landingpage;