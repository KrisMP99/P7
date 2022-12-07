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
import { check, sleep } from 'k6';

export const BASE_URL = "https://localhost:7001/api/";
export const BASE_HEADER = { headers: { 'Content-Type': 'application/json' } }
export const NUMBER_COURSES = 100
export const USERS = 1000

export const options = {
    stages: [{target : 500, duration: '10s'},
             {target : 1000, duration: '10s'},
             {target : 10000, duration: '10s'}],
    setupTimeout: '1000s'
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

    check(res, {'created user': (r) => r.status === 200});

    // Login to get the auth token
    const loginRes = http.post(`${BASE_URL}profiles/login`, JSON.stringify({
        "username": "owner",
        "password": "Owner123!"
    }), BASE_HEADER);
    sleep(5)

    check(loginRes, {'logged in successfully': (r) => r.json('token') !== '' && r.json('token' !== undefined && r.status === 200)})
    let ownerToken = loginRes.json('token')

    // header with token so we can create courses
    const BASE_TOKEN_HEADER = {
        headers :{
            'Authorization': `Bearer ${ownerToken}`,
            'Content-Type' : 'application/json'
        }
    }

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
                    exerciseGroupId: egId + 1, 
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
                courseId: 0
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

        check(loginRes, {'logged in successfully': (r) => r.json('token') !== '' && r.json('token' !== undefined && r.status === 200)})
        userData[i].value.token = loginRes.json('token')

        // Attend the courses
        // Accomplished by getting the course overview -> extracting the course id's -> enroll
        // header with token so we can create courses
        // token header for user
        const USER_TOKEN_HEADER = {
            headers: {
                'Authorization': `Bearer ${userData[i].value.token}`,
                'Content-Type' : 'application/json'
            }
        }
        const publicCoursesRes = http.get(`${BASE_URL}courses/public`, USER_TOKEN_HEADER)
        check(publicCoursesRes, r => r.status === 200)
        const courseIds = publicCoursesRes.json().map(c => c.id)

        // enroll in a random course selected from the course ids
        const enrollInCourseId = courseIds[randomIntNumber(courseIds.length)]
        userData[i].value.courseId = enrollInCourseId
        const enrollRes = http.post(`${BASE_URL}courses/enroll`, JSON.stringify({
            "courseId": enrollInCourseId
        }), USER_TOKEN_HEADER)
        check(enrollRes, r => r.status === 200)
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
    check(loginRes, {'logged in successfully': (r) => r.json('token') !== '' && r.json('token' !== undefined && r.status === 200)})
    user.value.token = loginRes.json('token')

    const USER_TOKEN_HEADER = {
        headers: {
            'Authorization': `Bearer ${user.value.token}`,
            'Content-Type' : 'application/json'
        }
    }

    // Get own courses + attending courses
    const ownCourses = http.get(`${BASE_URL}courses/created`, USER_TOKEN_HEADER)
    check(ownCourses, r => r.status === 200)

    const attendingCourses = http.get(`${BASE_URL}courses/attends`, USER_TOKEN_HEADER)
    check(attendingCourses, r => r.status === 200)

    // Get the course id of the course the user attends
    const courseId = publicCoursesRes.json().map(c => c.id)
    const specificCourse = http.get(`${BASE_URL}courses/${courseId[0]}`, USER_TOKEN_HEADER)
    check(specificCourse, r => r.status === 200)

    // Get the exercise group ids with each exercise id
    const egs = specificCourse.json().exerciseGroups
    const specificEg = egs[randomIntNumber(egs.length) - 1]
    const specificExId = specificEg.exercises[randomIntNumber(specificEg.exercises.length()) - 1].id

    // Get the specific exercise
    const exerciseResponse = http.get(`${BASE_URL}courses/${courseId[0]}/exercise-groups/${specificEg.id}/exercises/${specificExId}`, USER_TOKEN_HEADER)
    check(exerciseResponse, r => r.status === 200)
} 