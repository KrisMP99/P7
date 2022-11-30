import http from 'k6/http';
import { check } from 'k6';

// Options https://k6.io/docs/using-k6/k6-options/reference/
export const options = {
    vus: 1,
    iterations: 1,
    // stages: [{target : 30, duration: '10s'},
    //          {target : 50, duration: '10s'},
    //          {target : 100, duration: '10s'},
    //          {target : 200, duration: '10s'},
    //          {target : 300, duration: '10s'},
    //          {target : 1000, duration: '10s'},
    //          {target : 300, duration: '10s'},
    //          {target : 0, duration: '10s'},]
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
        ['POST', `${BASE_URL}courses/exercise-group`, JSON.stringify({ courseId: 1, title: "new1", description: "string", exerciseGroupNumber: 1, isVisible: true, visibleFromData: "2022-11-30T11:09:33.510Z"}), requestHeaderConfig],
        ['POST', `${BASE_URL}courses/exercise-group/exercises`, JSON.stringify({exerciseGroupId: 1, title: "new3", isVisible: false,
                    exerciseNumber: 1, startDate:"2022-11-30T11:11:10.457Z", endDate: "2022-11-30T11:11:10.457Z", visibleFrom:"2022-11-30T11:11:10.457Z", visibleTo:"2022-11-30T11:11:10.457Z",
                    layoutId:1,modules:[{description: "string", height:0, width:0, position: 1, type:"text"}]}), requestHeaderConfig],
    ])
    console.log(responses[0].status, responses[1].status, responses[2].status)
    responses.forEach((x) => check(x, { 'setup' : r => r.status === 200 }))


    return authToken;
}

export default () => {

}