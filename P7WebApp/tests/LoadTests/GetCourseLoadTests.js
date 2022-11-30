import http from 'k6/http';
import { check, group } from 'k6';


// Options https://k6.io/docs/using-k6/k6-options/reference/
export const options = {
    stages: [{target : 30, duration: '10s'},
             {target : 50, duration: '10s'},
             {target : 100, duration: '10s'},
             {target : 200, duration: '10s'},
             {target : 300, duration: '10s'},
             {target : 1000, duration: '10s'},
             {target : 300, duration: '10s'},
             {target : 0, duration: '10s'},]
}

export const USERNAME = `${randomString(10)}`;
export const FIRSTNAME = "Load";
export const LASTNAME = "Test";
export const EMAIL = `${USERNAME}@LoadTest.com`; 
export const PASSWORD = "LoadTest";
export const BASE_URL = "https://localhost:7001/api/";
export const BASE_HEADER = { headers: { 'Content-Type': 'application/json' } }


function randomString(length, charset = '') {
    if (!charset) charset = 'abcdefghijklmnopqrstuvwxyz';
    let res = '';
    while (length--) res += charset[(Math.random() * charset.length) | 0];
    return res;
  }

export function setup () {
    const res = http.post(`${BASE_URL}profiles`,JSON.stringify({
        "username": USERNAME,
        "password": PASSWORD,
        "email": EMAIL,
        "firstName": FIRSTNAME,
        "lastName": LASTNAME,
    }), BASE_HEADER);
    check(res, {'created user': (r) => r.status === 200});

    const loginRes = http.post(`${BASE_URL}profiles/login`, JSON.stringify({
        username: USERNAME,
        password: PASSWORD,
    }), BASE_HEADER);

    check(loginRes, { 'logged in successfully': (r) => r.json('token') !== '' && r.json('token') !== undefined && r.status === 200});
    
    const authToken = loginRes.json('token');

    //Setup Courses
    const requestHeaderConfig = {
        headers :{
            'Authorization': `Bearer ${authToken}`,
            'Content-Type' : 'application/json'
        }
    }
    const responses = http.batch([
        ['POST', `${BASE_URL}courses`, JSON.stringify({ title: "new1", description: "string", isPrivate: false,}), requestHeaderConfig],
        ['POST', `${BASE_URL}courses`, JSON.stringify({ title: "new2", description: "string", isPrivate: true,}), requestHeaderConfig],
        ['POST', `${BASE_URL}courses`, JSON.stringify({ title: "new3", description: "string", isPrivate: false,}), requestHeaderConfig],
        ['POST', `${BASE_URL}courses`, JSON.stringify({ title: "new4", description: "string", isPrivate: true,}), requestHeaderConfig],
    ])
    
    responses.forEach((x) => check(x, { 'Created course' : r => r.status === 200 }))


    return authToken;
}

// Example function. DO NOT DELETE
// See https://k6.io/docs/examples/advanced-api-flow/ for more information
export default (authToken) => {
    const requestHeaderConfigWithTag = (tag) => ({
        headers : {
            'Authorization': `Bearer ${authToken}`,
            'Content-Type' : 'application/json',
        },
        tags: {
            name: `${tag}`
        }
    });

    // Get Course GET request
    const params = {
        headers: {
            Authorization: `Bearer ${authToken}`,
        }
    }
    const responses = http.batch([
        ['GET', `${BASE_URL}courses/1`, null, requestHeaderConfigWithTag('GetCourse')],
        ['GET', `${BASE_URL}courses/2`, null, requestHeaderConfigWithTag('GetCourse')],
        ['GET', `${BASE_URL}courses/3`, null, requestHeaderConfigWithTag('GetCourse')],
        ['GET', `${BASE_URL}courses/4`, null, requestHeaderConfigWithTag('GetCourse')],
    ]) 
    responses.forEach(x => check(x, {'got course': r => r.status === 200}))


    // Create, Update and Delete Course group
    // group('Create, update and delete course', () => {
    //     let courseUrl = `${BASE_URL}courses`;
    //     let body = {
    //             title: "new",
    //             description: "string",
    //             isPrivate: false
    //         }
    //     const createRes = http.post(courseUrl, JSON.stringify(body), requestHeaderConfig)
    //     const courseId = createRes.json('id')
    //     check(createRes, {'Created course': r => r.status === 200})
    // })
}