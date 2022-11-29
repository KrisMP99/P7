import http from 'k6/http';
import { check } from 'k6';


export const options = {
    vus: 1,
    iterations: 1,
}

export const USERNAME = `${randomString(10)}`;
export const FIRSTNAME = "Load";
export const LASTNAME = "Test";
export const EMAIL = `${USERNAME}@LoadTest.com`; 
export const PASSWORD = "LoadTest";
export const BASE_URL = "https://localhost:7001/api";
export const BASE_HEADER = { headers: { 'Content-Type': 'application/json' } }


function randomString(length, charset = '') {
    if (!charset) charset = 'abcdefghijklmnopqrstuvwxyz';
    let res = '';
    while (length--) res += charset[(Math.random() * charset.length) | 0];
    return res;
  }

export default function setup () {
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

// Example function. DO NOT DELETE
// See https://k6.io/docs/examples/advanced-api-flow/ for more information
export function post (authToken) {
    // GET request
    const params = {
        headers: {
            Authorization: `Bearer ${authToken}`,
        }
    }
    const getRes = http.get(`${BASE_URL}/courses/1`,params);
    check(getRes, {'got course': r => r.status === 200})
    
    // POST request
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
    const body = 
    {
        title: "new",
        description: "string",
        isPrivate: false,
    }
    
    const postRes = http.post(`${BASE_URL}/courses`,
     JSON.stringify({
        title: "new",
        description: "string",
        isPrivate: false,
    }),
    {headers: {
        'Authorization': `Bearer ${authToken}`,
        'Content-Type' : 'application/json'
    }})
    check(postRes, {'Created course': r => r.status === 200})

}
