import React, { useRef, useState } from 'react';
import { Container, Form, Pagination } from 'react-bootstrap';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import OwnedCourseOverview, { CourseOverview } from './LandingpageModules/OwnedCourseOverview';
import AttendeeCourseOverview from './LandingpageModules/AttendeeCourseOverview';
import CreateCourseModal from '../Modals/CreateCourseModal/CreateCourseModal';
import { ShowModal } from '../Modals/CreateExerciseModal/CreateExerciseModal';
import { dummyDataOwned } from './dummyDataOwned';
import { dummyDataAttendee } from './dummyDataAttendee';

export function Landingpage(): JSX.Element {
    const openCreateCourseModal = useRef<ShowModal>(null);

    const [ownedCourses, setOwnedCourses] = useState<CourseOverview[]>(dummyDataOwned);
    const [attendedCourses, setAttendedCourses] = useState<CourseOverview[]>(dummyDataAttendee);

    const [ownedCoursesCurrentPage, setOwnedCoursesCurrentPage] = useState<number>(0);
    const [attendedCoursesCurrentPage, setAttendedCoursesCurrentPage] = useState<number>(0);

    const [ownedCoursesPerPage, setOwnedCoursesPerPage] = useState<number>(10);
    const [attendedCoursesPerPage, setAttendedCoursesPerPage] = useState<number>(10);

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
                            courses={ownedCourses}
                            deletedCourse={(courses: CourseOverview[]) => setOwnedCourses(courses)}
                        />
                    </Tab>
                    <Tab eventKey="Attending courses" title="Attending courses">
                        <AttendeeCourseOverview
                            courses={attendedCourses}
                        />

                    </Tab>
                </Tabs>
            </div>
            <CreateCourseModal ref={openCreateCourseModal} />
        </Container>
    );
}

export default Landingpage;