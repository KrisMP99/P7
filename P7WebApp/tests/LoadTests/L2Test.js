// Load scenario L2
// 0.1 create a profile
// 1. Login
// 2. Create a course
// 3. Create x number of exercise groups
// 4. Create x number of exercises
// 5. Delete the course

import http from 'k6/http';
import { check } from 'k6';

export const BASE_URL = "http://130.225.39.193/api/";
export const BASE_URL_dev = "https://localhost:7001/api/";
export const BASE_HEADER = { 
    headers: { 'Content-Type': 'application/json' } 
}

function randomIntNumber(max) {
    return Math.floor(Math.random() * max) + 1;
}

function randomString(length, charset = '') {
    if (!charset) charset = 'abcdefghijklmnopqrstuvwxyz';
    let res = '';
    while (length--) res += charset[(Math.random() * charset.length) | 0];
    return res;
}

export const options = {
    stages: [{target : 5, duration: '30s'},
             {target : 10, duration: '2m'},
             {target : 10, duration: '2m'},
             {target : 15, duration: '2m'},
             {target : 15, duration: '2m'},
             {target : 20, duration: '2m'},
             {target : 20, duration: '2m'},
             {target : 25, duration: '2m'},
             {target : 25, duration: '2m'},
             {target : 30, duration: '2m'},
             {target : 30, duration: '2m'},
             {target : 35, duration: '2m'},
             {target : 40, duration: '2m'},
             {target : 40, duration: '2m'},
             {target : 50, duration: '2m'},
             {target : 50, duration: '2m'},
             {target : 60, duration: '2m'},
             {target : 60, duration: '2m'},
             {target : 70, duration: '2m'},
             {target : 70, duration: '2m'},
             {target : 80, duration: '2m'},
             {target : 80, duration: '2m'},
             {target : 90, duration: '2m'},
             {target : 90, duration: '2m'},
             {target : 100, duration: '2m'},
             {target : 100, duration: '2m'},
             {target : 50, duration: '2m'},
             {target : 25, duration: '2m'},
             {target : 5, duration: '2m'}],
}

// steps:
// login
// Create a course
// Create x number of exercise groups
// For each exercise group, create x number of exercises
export default () => {
    // Pick a random user to log in to and creating a course
    // creating the user
    let userName = randomString(100)
    const res = http.post(`${BASE_URL}profiles`, JSON.stringify({
        "username": userName,
        "password": "Test123!",
        "email": "Test@test.dk",
        "firstName": "TestFirst",
        "lastName": "TestLast"
    }), BASE_HEADER);
    check(res, {'Created user': (r) => r.status === 200});

    // Login and get the auth token
    const loginRes = http.post(`${BASE_URL}profiles/login`, JSON.stringify({
        "username": userName,
        "password": "Test123!"
    }), BASE_HEADER)

    check(loginRes, {'logged in successfully': (r) => r.json('token') !== '' && r.json('token') !== undefined && r.status === 200})
    const authToken = loginRes.json('token')
    const profileId = loginRes.json('userId')

    const USER_TOKEN_HEADER = {
        headers: {
            'Authorization': `Bearer ${authToken}`,
            'Content-Type' : 'application/json'
        }
    }

    // Get own courses + attending courses
    const ownCourses = http.get(`${BASE_URL}profiles/${profileId}/courses/created`, USER_TOKEN_HEADER)
    check(ownCourses, r => r.status === 200)

    const attendingCourses = http.get(`${BASE_URL}profiles/${profileId}/courses/attends`, USER_TOKEN_HEADER)
    check(attendingCourses, r => r.status === 200)

    // Creating a course
    const resCourse = http.post(`${BASE_URL}courses`, JSON.stringify({ 
        title: "_course_" + randomString(100), 
        description: "_description", 
        isPrivate: false
    }), USER_TOKEN_HEADER);
    check(resCourse, {"Created course" : r => r.status === 200});

    // After the course is created, the course is once again fetched
    const ownCourses2 = http.get(`${BASE_URL}profiles/${profileId}/courses/created`, USER_TOKEN_HEADER)
    check(ownCourses2, {"Got own courses" : r => r.status === 200})

    // We extract the id from the own courses created
    // and fetch the course
    const newCourseId = ownCourses2.json()[0].id

    const newCourse = http.get(`${BASE_URL}courses/${newCourseId}`, USER_TOKEN_HEADER)
    check(newCourse, {"Got own specific course" : r => r.status === 200})

    // pick a random number of exercise groups to create
    let numOfEGs = randomIntNumber(10)
    for(let i = 0; i < numOfEGs; i++) {
        const resEG = http.post(`${BASE_URL}courses/exercise-groups`, JSON.stringify({ 
            courseId: newCourseId, 
            title: i + "_exercisegroup_" + randomString(100), 
            description: i + "_exercisegroup", 
            exerciseGroupNumber: 0, 
            isVisible: true, 
            visibleFromData: "2022-11-30T11:09:33.510Z"
        }), USER_TOKEN_HEADER);
        check(resEG, {"Created exercise group" : r => r.status === 200});

        // After the exercise group has been created, we get the course again
        const newCourse2 = http.get(`${BASE_URL}courses/${newCourseId}`, USER_TOKEN_HEADER)
        check(newCourse2, {"Got own specific course" : r => r.status === 200})
        
        // Get the latest exercise group id
        const exGroups = newCourse2.json('exerciseGroups')

        let exGroupId = 1
        if(exGroups.length > 0) {
            exGroupId = exGroups[exGroups.length - 1].id
        }

        // Create a random number of exercises between 1 - 10 for each of the exercise groups created above
        let numOfExs = randomIntNumber(10)
        for(let exId = 0; exId < numOfExs; exId++) {
            const resEx = http.post(`${BASE_URL}courses/exercise-groups/exercises`, JSON.stringify({
                exerciseGroupId: exGroupId, 
                title: exId.toString() + "_exercise_" + randomString(100), 
                isVisible: true,
                exerciseNumber: 0, 
                startDate:"2022-11-30T11:11:10.457Z", 
                endDate: "2022-11-30T11:11:10.457Z", 
                visibleFrom:"2022-11-30T11:11:10.457Z", 
                visibleTo:"2022-11-30T11:11:10.457Z",
                layoutId:1,
                modules:[{
                    description: exId.toString() + "_description", 
                    height:0, 
                    width:0, 
                    position: 1, 
                    type:"text", 
                    title: exId.toString() + "_module_title", 
                    content:exId.toString() + "_module_content"
                }]
            }), USER_TOKEN_HEADER);
            check(resEx, {"Created exercise" : r => r.status === 200});
        }
    }

    // delete the course
    const deleteCourseRes = http.del(`${BASE_URL}courses/${newCourseId}`, null, USER_TOKEN_HEADER)
    check(deleteCourseRes, {"Deleted course" : r => r.status === 200})
}