import http from 'k6/http';
import { check, sleep } from 'k6';


export const options = {
    vus: 1,
    duration: '1s',
}

const USERNAME = `${randomString(10)}`;
const FIRSTNAME = "Load";
const LASTNAME = "Test";
const EMAIL = `${USERNAME}@LoadTest.com`; 
const PASSWORD = "LoadTest";
const BASE_URL = "https://localhost:7001/api";
const BASE_HEADER = { headers: { 'Content-Type': 'application/json' } }

function randomString(length, charset = '') {
    if (!charset) charset = 'abcdefghijklmnopqrstuvwxyz';
    let res = '';
    while (length--) res += charset[(Math.random() * charset.length) | 0];
    return res;
  }

export function setup(){
    const res = http.post(`${BASE_URL}/accounts`,JSON.stringify({
        "username": USERNAME,
        "password": PASSWORD,
        "email": EMAIL,
        "firstname": FIRSTNAME,
        "lastname": LASTNAME,
    }), BASE_HEADER);

    check(res, {'created user': (r) => r.status === 200});

    const loginRes = http.post(`${BASE_URL}/accounts/login`, JSON.stringify({
        username: USERNAME,
        password: PASSWORD,
    }), BASE_HEADER);

    const authToken = loginRes.json('token');
    check(authToken, { 'logged in successfully': () => authToken !== '' && authToken !== undefined});

    return authToken;
}

export default (authToken) => {
    const requestConfigWithTag = (tag) => ({
        headers: {
            Authorization: `Bearer ${authToken}`,
        },
        tags: Object.assign(
            {},
            {
                name: 'PrivateAccount',
            },
            tag
        ), 
    });
}
