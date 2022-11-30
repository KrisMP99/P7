import http from 'k6/http';
import { check } from 'k6';

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

export const USERNAME = "Test";
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
        "username": "Test",
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
        ['POST', `${BASE_URL}courses/exercise-groups/exercises`, JSON.stringify({exerciseGroupId: 1, title: "new3", isVisible: false,
                    exerciseNumber: 1, startDate:"2022-11-30T11:11:10.457Z", endDate: "2022-11-30T11:11:10.457Z", visibleFrom:"2022-11-30T11:11:10.457Z", visibleTo:"2022-11-30T11:11:10.457Z",
                    layoutId:1,modules:[{description: "string", height:0, width:0, position: 1, type:"text", title:"string", content:"content"}]}), requestHeaderConfig],
    ])

    return authToken;
}

export default (authToken) => {
    const requestHeaderConfig = {
        'headers' : {
            'Authorization': `Bearer ${authToken}`,
            'Content-Type' : 'application/json',
        }
    }
    
    const urlParams = [{method: 'POST', url: `${BASE_URL}profiles`, body:JSON.stringify({"username": randomString(10),"password": PASSWORD,"email": EMAIL,"firstName": FIRSTNAME,"lastName": LASTNAME,}),params: requestHeaderConfig},
                 {method: 'POST', url: `${BASE_URL}profiles/login`, body: JSON.stringify({"username":"Test", password: PASSWORD}), params: requestHeaderConfig},
                 {method: 'GET', url:`${BASE_URL}courses/1`, body:null, params: requestHeaderConfig},
                 {method: 'POST', url:`${BASE_URL}courses/exercise-group`, body:JSON.stringify({ courseId: 1, title: randomString(5), description: "string", exerciseGroupNumber: 1, isVisible: true, visibleFromData: "2022-11-30T11:09:33.510Z"}), params: requestHeaderConfig},
                 {method: 'POST', url:`${BASE_URL}courses/exercise-groups/exercise`, body:JSON.stringify({exerciseGroupId: 1, title: "new3", isVisible: false,
                 exerciseNumber: 1, startDate:"2022-11-30T11:11:10.457Z", endDate: "2022-11-30T11:11:10.457Z", visibleFrom:"2022-11-30T11:11:10.457Z", visibleTo:"2022-11-30T11:11:10.457Z",
                 layoutId:1,modules:[{description: "string", height:0, width:0, position: 1, type:"text", title:"string", content:"content"}]}), 
                 params: requestHeaderConfig},
                 {method: 'GET', url:`${BASE_URL}courses/1/exercise-groups`, body:null, params: requestHeaderConfig},
                 {method: 'GET', url:`${BASE_URL}courses/1/exercise-groups/1/exercises/1`, body:null, params: requestHeaderConfig},
    ]
    
    const maxRequest = 10
    let numberOfRequests = Math.floor(Math.random() * maxRequest)

    for (let i = 0; i < numberOfRequests; i++) {
        let requestNumber = Math.floor(Math.random() * urlParams.length)
        let res = http.batch([[urlParams[requestNumber].method, 
                               urlParams[requestNumber].url, 
                               urlParams[requestNumber].body, 
                               urlParams[requestNumber].params]])
        check(res[0], {'Simulation check': r => r. status === 200})
    }
    
}