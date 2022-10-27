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

// interface ListPaginationProps {

// }

// function ListPagination(courses: CourseOverview, currentPage: number, elementsPerPage: number) {
//     const

//     return (
//         <div style={{ display: 'flex', justifyContent: 'center' }}>
//             <Pagination>
//                 <Pagination.Prev disabled={currentPage <= 0} onClick={() => this.setState({ currentPage: this.state.currentPage - 1 })} />

//                 <Pagination.Item active={this.state.currentPage === 0}
//                     onClick={() => {
//                         if (this.state.currentPage !== 0)
//                             this.setState({ currentPage: 0 });
//                     }}>
//                     {1}
//                 </Pagination.Item>

//                 {this.state.currentPage >= 3 && <Pagination.Ellipsis disabled />}

//                 {this.state.currentPage === this.state.maxPages - 1 && this.state.maxPages > 3 &&
//                     <Pagination.Item
//                         onClick={() => {
//                             this.setState({ currentPage: this.state.currentPage - 2 });
//                         }}>
//                         {this.state.currentPage - 1}
//                     </Pagination.Item>}

//                 {this.state.currentPage >= 2 &&
//                     <Pagination.Item
//                         onClick={() => {
//                             this.setState({ currentPage: this.state.currentPage - 1 });
//                         }}>
//                         {this.state.currentPage}
//                     </Pagination.Item>}

//                 {this.state.currentPage !== 0 && this.state.currentPage !== this.state.maxPages - 1 && this.state.maxPages !== 1 &&
//                     <Pagination.Item
//                         active
//                         onClick={() => { }}>
//                         {this.state.currentPage + 1}
//                     </Pagination.Item>}

//                 {this.state.currentPage < this.state.maxPages - 2 &&
//                     <Pagination.Item
//                         onClick={() => {
//                             this.setState({ currentPage: this.state.currentPage + 1 });
//                         }}>
//                         {this.state.currentPage + 2}
//                     </Pagination.Item>}

//                 {this.state.currentPage === 0 && this.state.maxPages > 3 &&
//                     <Pagination.Item
//                         onClick={() => {
//                             this.setState({ currentPage: this.state.currentPage + 2 });
//                         }}>
//                         {this.state.currentPage + 3}
//                     </Pagination.Item>}

//                 {this.state.currentPage < this.state.maxPages - 3 && <Pagination.Ellipsis disabled />}

//                 {this.state.maxPages !== 1 &&
//                     <Pagination.Item active={this.state.currentPage === this.state.maxPages - 1}
//                         onClick={() => {
//                             this.setState({ currentPage: this.state.maxPages - 1 });
//                         }}>
//                         {this.state.maxPages}
//                     </Pagination.Item>}

//                 <Pagination.Next disabled={this.state.currentPage >= this.state.maxPages - 1} onClick={() => this.setState({ currentPage: this.state.currentPage + 1 })} />
//             </Pagination>
//             <div style={{ marginLeft: '5px', height: '33.5px' }}>
//                 <Form.Select size="sm" style={{ height: '100%' }} value={this.state.licensePerPage}
//                     onChange={(e) => {
//                         this.setState({ licensePerPage: Number(e.target.value), maxPages: Math.ceil(this.state.visibleLicenses!.length / Number(e.target.value)), currentPage: 0 });
//                     }
//                     }>
//                     <option value={5}>5 per page</option>
//                     <option value={10}>10 per page</option>
//                     <option value={25}>25 per page</option>
//                     <option value={50}>50 per page</option>
//                     <option value={100}>100 per page</option>
//                 </Form.Select>
//             </div>
//         </div>
//     );
// }

export default Landingpage;