import http from 'k6/http';
import { check } from 'k6';


// Options https://k6.io/docs/using-k6/k6-options/reference/
export const options = {
    stages: [{target : 30, duration: '10s'},
             {target : 50, duration: '10s'},
             {target : 100, duration: '10s'},
             {target : 200, duration: '10s'},
             {target : 300, duration: '10s'},
             {target : 1000, duration: '10s'},]
}

export const FIRSTNAME = "Load";
export const LASTNAME = "Test";
export const EMAIL = `test@LoadTest.com`; 
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
    const USERNAME = `${randomString(10)}`
    const res = http.post(`${BASE_URL}profiles`,JSON.stringify({
        "username": USERNAME,
        "password": PASSWORD,
        "email": EMAIL,
        "firstName": FIRSTNAME,
        "lastName": LASTNAME,
    }), BASE_HEADER);
    console.log(USERNAME, PASSWORD)
    check(res, {'created user': (r) => r.status === 200});
    return USERNAME
}

export default (userName) => {
    const loginRes = http.post(`${BASE_URL}profiles/login`, JSON.stringify({
        username: userName,
        password: PASSWORD,
    }), BASE_HEADER);
    check(loginRes, { 'logged in successfully': (r) => r.json('token') !== '' && r.json('token') !== undefined && r.status === 200});
}