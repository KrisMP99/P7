import React, { useState } from 'react';
import { Container} from 'react-bootstrap';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import OwnedCourseOverview from './LandingpageModules/OwnedCourseOverview';
import AttendeeCourseOverview from './LandingpageModules/AttendeeCourseOverview';

function Landingpage(): JSX.Element {

    return (
        <Container>
            <div className='tabs-container'>
                <Tabs
                    defaultActiveKey="My courses"
                    id="fill-tab-example"
                    className="mb-3 mt-3"
                    fill>
                    <Tab eventKey="My courses" title="My courses">
                        <OwnedCourseOverview></OwnedCourseOverview>
                    </Tab>
                    <Tab eventKey="Attending courses" title="Attending courses">
                        <AttendeeCourseOverview></AttendeeCourseOverview>
                    </Tab>
                </Tabs>
            </div>
        </Container>
    );
}

export default Landingpage;