import http from 'k6/http';
import { check } from 'k6';


// Options https://k6.io/docs/using-k6/k6-options/reference/
export const options = {
    vus: 5000,
    duration: '1m'
}

export const FIRSTNAME = "Load";
export const LASTNAME = "Test";
export const EMAIL = `test@LoadTest.com`; 
export const PASSWORD = "LoadTest1!";
export const BASE_URL = "http://localhost/api/";
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
    check(res, {'created user': (r) => r.status === 200});
    return USERNAME
}

export default (userName) => {
    const loginRes = http.post(`${BASE_URL}profiles/login`, JSON.stringify({
        username: userName,
        password: PASSWORD,
    }), BASE_HEADER);
    if (loginRes.status !== 200){
        console.log(loginRes.status, loginRes.body)
    }
    check(loginRes, { 'logged in successfully': (r) => r.json('token') !== '' && r.json('token') !== undefined && r.status === 200});
}