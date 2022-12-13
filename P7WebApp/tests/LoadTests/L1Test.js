// Load scenarie (L1) for en user der tilgår en exercise på et kursus
// 0.1 Lav en profil
// 1. Login
// 2. Tryk på kursus
// 3. Tryk på exercise
// = login: 1, hent egne + attending kurser: 2, hent specifik kursus: 1, hent specifik exercise: 1
// = Total 5 requests
// Brugeren kan tage mellem 15, 30 og 1 minut at gøre dette
// Requests svinger mellem 6-8 og kan være enten at hente flere exercises eller et helt nyt kursus


import http from 'k6/http';
import { check, fail, sleep } from 'k6';

import { htmlReport } from "https://raw.githubusercontent.com/benc-uk/k6-reporter/main/dist/bundle.js";

export const BASE_URL = "http://130.225.39.193/api/";
export const BASE_HEADER = { headers: { 'Content-Type': 'application/json' } }
export const NUMBER_COURSES = 100
export const USERS = 100

export const options = {
    stages: [{target : 5, duration: '10s'},
             {target : 10, duration: '1m'},
             {target : 10, duration: '2m'},
             {target : 20, duration: '30s'},
             {target : 20, duration: '2m'},
             {target : 30, duration: '30s'},
             {target : 30, duration: '2m'},
             {target : 40, duration: '30s'},
             {target : 40, duration: '2m'},
             {target : 50, duration: '30s'},
             {target : 50, duration: '2m'},
             {target : 60, duration: '30s'},
             {target : 60, duration: '2m'},
             {target : 70, duration: '30s'},
             {target : 70, duration: '2m'},
             {target : 80, duration: '30s'},
             {target : 80, duration: '2m'},
             {target : 90, duration: '30s'},
             {target : 90, duration: '2m'},
             {target : 100, duration: '30s'},
             {target : 100, duration: '2m'},
             {target : 110, duration: '30s'},
             {target : 110, duration: '2m'},
             {target : 120, duration: '30s'},
             {target : 120, duration: '2m'},
             {target : 130, duration: '30s'},
             {target : 130, duration: '2m'},
             {target : 140, duration: '30s'},
             {target : 140, duration: '2m'},
             {target : 150, duration: '30s'},
             {target : 150, duration: '2m'},
             {target : 160, duration: '30s'},
             {target : 160, duration: '2m'},
             {target : 170, duration: '30s'},
             {target : 170, duration: '2m'},
             {target : 180, duration: '30s'},
             {target : 180, duration: '2m'},
             {target : 190, duration: '30s'},
             {target : 190, duration: '2m'},
             {target : 200, duration: '30s'},
             {target : 200, duration: '2m'},
             {target : 210, duration: '30s'},
             {target : 210, duration: '2m'},
             {target : 220, duration: '30s'},
             {target : 220, duration: '2m'},
             {target : 230, duration: '30s'},
             {target : 230, duration: '2m'},
             {target : 240, duration: '30s'},
             {target : 240, duration: '2m'},
             {target : 250, duration: '30s'},
             {target : 250, duration: '2m'},
             {target : 260, duration: '30s'},
             {target : 260, duration: '2m'},
             {target : 270, duration: '30s'},
             {target : 270, duration: '2m'},
             {target : 280, duration: '30s'},
             {target : 280, duration: '2m'},
             {target : 290, duration: '30s'},
             {target : 290, duration: '2m'},
             {target : 300, duration: '30s'},
             {target : 300, duration: '2m'},
             {target : 350, duration: '30s'},
             {target : 350, duration: '2m'},
             {target : 400, duration: '30s'},
             {target : 400, duration: '2m'},
             {target : 450, duration: '30s'},
             {target : 450, duration: '2m'},
             {target : 500, duration: '30s'},
             {target : 500, duration: '2m'},
             {target : 400, duration: '2m'},
             {target : 300, duration: '2m'},
             {target : 200, duration: '2m'},
             {target : 100, duration: '2m'},
             {target : 50, duration: '2m'}],

    setupTimeout: '10m',

    thresholds: {
        // 100% of requests must finish within 1000ms.
        http_req_duration: ['p(90) < 100', 'p(95) < 200', 'p(99.9) <= 1000'],
    }
}


function randomIntNumber(max) {
    return Math.floor(Math.random() * max) + 1;
}

// Create courses with exercise groups and exercises
// Set the users as attendees on the courses so they can see them upon login
// Create a profile for each virtual user, login and store the auth token
export function setup () {
    let userData = []
    //-----------------------------------COURSE SETUP BEGIN-----------------------------------//
    // Create a profile, login, and create the courses with exercise groups and exercises
    // such that the users can attend the courses
    const res = http.post(`${BASE_URL}profiles`, JSON.stringify({
        "username": "owner",
        "password": "Owner123!",
        "email": "owner@test.dk",
        "firstName": "OwnerFirst",
        "lastName": "OwnerLast"
    }), BASE_HEADER);

    check(res, {'Created owner user': (r) => r.status === 200});

    // Login to get the auth token
    const loginRes = http.post(`${BASE_URL}profiles/login`, JSON.stringify({
        "username": "owner",
        "password": "Owner123!"
    }), BASE_HEADER);

    check(loginRes, {'Owned user logged in successfully': (r) => r.json('token') !== '' && r.json('token') !== undefined && r.status === 200})
    let ownerToken = loginRes.json('token')

    // header with token so we can create courses
    const BASE_TOKEN_HEADER = {
        headers :{
            'Authorization': `Bearer ${ownerToken}`,
            'Content-Type' : 'application/json'
        }
    }

    let ExGroupsCounter = 0;
    // Setup courses
    for(let courseId = 0; courseId < NUMBER_COURSES; courseId++) {
        // Create the course
        const resCourse = http.post(`${BASE_URL}courses`, JSON.stringify({ 
            title: courseId.toString() + "_course", 
            description: courseId.toString() + "_description", 
            isPrivate: false
        }), BASE_TOKEN_HEADER);

        check(resCourse, r => r.status === 200);

        // Create a random number of exercise groups between 1 - 10 for the course created above
        let numOfEGs = randomIntNumber(10)
        for(let egId = 0; egId < numOfEGs; egId++) {
            ExGroupsCounter++
            const resEG = http.post(`${BASE_URL}courses/exercise-groups`, JSON.stringify({ 
                courseId: courseId + 1, 
                title: egId.toString() + "_exercisegroup", 
                description: egId.toString() + "_exercisegroup", 
                exerciseGroupNumber: egId + 1, 
                isVisible: true, 
                visibleFromData: "2022-11-30T11:09:33.510Z"
            }), BASE_TOKEN_HEADER);

            check(resEG, r => r.status === 200);

            // Create a random number of exercises between 1 - 10 for the exercise groups created above
            let numOfExs = randomIntNumber(10)
            for(let exId = 0; exId < numOfExs; exId++) {
                const resEx = http.post(`${BASE_URL}courses/exercise-groups/exercises`, JSON.stringify({
                    exerciseGroupId: ExGroupsCounter, 
                    title: exId.toString() + "_exercise", 
                    isVisible: true,
                    exerciseNumber: exId + 1, 
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
                }), BASE_TOKEN_HEADER);
                check(resEx, r => r.status === 200);
            }
        }
    }
    //-----------------------------------COURSE SETUP END-----------------------------------//



    //-----------------------------------USER SETUP BEGIN-----------------------------------//
    // Creating the profiles, logging in and attending the courses
    // We use a dictionary to store the user data
    for (let i = 0; i < USERS; i++) {
        userData.push({
            key: i,
            value: {
                username: i.toString() + "_user",
                password: "Test123!",
                email: i.toString() + "test@test.dk",
                token: '',
                courseId: 0,
                userId: -1
            }
        })

        // Create the user
        const res = http.post(`${BASE_URL}profiles`, JSON.stringify({
            "username": userData[i].value.username,
            "password": userData[i].value.password,
            "email": userData[i].value.email,
            "firstName": "FirstTest",
            "lastName": "LastTest"
        }), BASE_HEADER);
        check(res, {'created user': (r) => r.status === 200});
        
        // Login
        const loginRes = http.post(`${BASE_URL}profiles/login`, JSON.stringify({
            "username": userData[i].value.username,
            "password": userData[i].value.password
        }), BASE_HEADER);

        check(loginRes, {'logged in successfully': (r) => r.json('token') !== '' && r.json('token') !== undefined && r.status === 200})
        userData[i].value.token = loginRes.json('token')

        // Attend the courses
        // Accomplished by getting the course overview -> extracting the course id's -> enroll
        const USER_TOKEN_HEADER = {
            headers: {
                'Authorization': `Bearer ${userData[i].value.token}`,
                'Content-Type' : 'application/json'
            }
        }
        const publicCoursesRes = http.get(`${BASE_URL}courses/public`, USER_TOKEN_HEADER)
        check(publicCoursesRes, {"Got public courses" : r => r.status === 200})
        const courseIds = publicCoursesRes.json().map(c => c.id)
        
        // enroll in a random course selected from the course ids
        const enrollInCourseId = courseIds[randomIntNumber(courseIds.length) - 1]
        userData[i].value.courseId = enrollInCourseId
        const enrollRes = http.post(`${BASE_URL}courses/enroll`, JSON.stringify({
            "courseId": enrollInCourseId
        }), USER_TOKEN_HEADER)
        check(enrollRes, {"Enrolled in course" : r => r.status === 200})
    }

    return userData
    //-----------------------------------USER SETUP END-----------------------------------//
}

export default (userData) => {
    // The user logs in, clicks on a course -> exercise group -> specific exercise
    // The users can take different amount of time doing this

    // Pick a random user from the user data object
    let user = userData[randomIntNumber(userData.length) - 1]

    // Login and get the auth token
    const loginRes = http.post(`${BASE_URL}profiles/login`, JSON.stringify({
        "username": user.value.username,
        "password": user.value.password
    }), BASE_HEADER);
    check(loginRes, {'logged in successfully': (r) => r.json('token') !== '' && r.json('token') !== undefined && r.status === 200})
    user.value.token = loginRes.json('token')
    user.value.userId = loginRes.json('userId')

    const USER_TOKEN_HEADER = {
        headers: {
            'Authorization': `Bearer ${user.value.token}`,
            'Content-Type' : 'application/json'
        }
    }

    // Get own courses + attending courses
    const ownCourses = http.get(`${BASE_URL}profiles/${user.value.userId}courses/created`, USER_TOKEN_HEADER)
    check(ownCourses, r => r.status === 200)

    const attendingCourses = http.get(`${BASE_URL}profiles/${user.value.userId}/courses/attends`, USER_TOKEN_HEADER)
    check(attendingCourses, r => r.status === 200)

    // Get the course id of the course the user attends
    const courseId = attendingCourses.json().map(c => c.id)

    const specificCourse = http.get(`${BASE_URL}courses/${courseId[0]}`, USER_TOKEN_HEADER)
    check(specificCourse, r => r.status === 200)

    // Get the exercise group ids with each exercise id
    const egs = specificCourse.json('exerciseGroups')
    const specificEg = egs[randomIntNumber(egs.length) - 1]
    const index = randomIntNumber(specificEg.exercises.length) - 1
    const specificExId = specificEg.exercises[index].id

    // Get the specific exercise
    const exerciseResponse = http.get(`${BASE_URL}courses/${courseId[0]}/exercise-groups/${specificEg.id}/exercises/${specificExId}`, USER_TOKEN_HEADER)
    check(exerciseResponse, r => r.status === 200)

    if(exerciseResponse.status !== 200) {
        sleep(0.5)
        console.log("Something went wrong!")
        console.log("Reason: " + exerciseResponse.body)
        console.log("Username: " + user.value.username + " password: " + user.value.password + " token: " + user.value.token)
        console.log("Course id: " + courseId[0] + " exGroup id: " + specificEg.id + " exercise id: " + specificExId)
        sleep(0.5)
    }
}

export function handleSummary(data) {
    return {
      "result.html": htmlReport(data),
      stdout: textSummary(data, { indent: " ", enableColors: true }),
    };
}