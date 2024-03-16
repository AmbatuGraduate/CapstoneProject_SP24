import { useGoogleLogin } from '@react-oauth/google';
import React from 'react';
import { useState } from 'react';

export const Login = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [accessToken, setAccessToken] = useState(''); // [1

    const handleSuccess = (response) => {
        const authCode = response.code;
        console.log('Authentication successful, auth code: ' + authCode);
        fetch('https://localhost:7024/api/auth/google', {
            credentials: 'include',
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Credentials': true,
                "Access-Control-Allow-Origin": "*"
            },
            body: JSON.stringify({ AuthCode: authCode })
        })
            .then((res) => {
                if (res.ok) {
                    return res.text(); // This returns a Promise
                } else {
                    throw new Error(res.statusText);
                }
            })
            .then((accessToken) => { // This block will be executed after the Promise resolves
                console.log('Authentication successful, access token: ' + accessToken);
                setAccessToken(accessToken); // Save the access token
                setIsLoggedIn(true); // Update logged-in state
            })
            .catch((error) => {
                console.log(error);
                // Handle errors here
            });
    };

    const gglogin = useGoogleLogin({
        onSuccess: handleSuccess,
        flow: 'auth-code',
        scope: 'https://www.googleapis.com/auth/calendar https://www.googleapis.com/auth/admin.directory.group https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/admin.directory.user https://www.googleapis.com/auth/userinfo.profile openid profile email',

    });

    const getEvents = () => {
        fetch('https://localhost:7024/api/ScheduleTreeTrim/GetCalendarEvents/' + accessToken, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        })
            .then((res) => {
                if (res.ok) {
                    return res.json(); // This returns a Promise
                } else {
                    console.error('Failed to get events:', res.statusText);
                    // Handle authentication failure
                }
            })
            .then((events) => { // This block will be executed after the Promise resolves
                console.log('Events:', events);
                // Do something with the events
            })
            .catch((error) => {
                console.log(error);
                // Handle errors here
            });
    }

    const refreshHandler = () => {
        fetch('https://localhost:7024/api/auth/refresh', {
            credentials: 'include',
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Credentials': true,
                "Access-Control-Allow-Origin": "*"
            },
        })
            .then((res) => {
                if (res.ok) {
                    return res.json(); // This returns a Promise
                } else {
                    console.error('Failed to get events:', res.statusText);
                    // Handle authentication failure
                }
            })
            .then((events) => { // This block will be executed after the Promise resolves
                console.log('Events:', events);
                // Do something with the events
            })
            .catch((error) => {
                console.log(error);
                // Handle errors here
            });
    }

    return (
        <div>
            {!isLoggedIn ? (
                <button onClick={() => gglogin()}>
                    Login with Google
                </button>
            ) : (
                <div>
                    <p>User is logged in.</p>
                    <button onClick={() => getEvents()}>
                        get events
                    </button>
                    <button onClick={() => refreshHandler()}>
                        Refresh
                    </button>
                </div>
            )}
        </div>
    );
};