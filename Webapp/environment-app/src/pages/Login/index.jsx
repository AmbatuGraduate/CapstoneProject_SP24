import { useGoogleLogin } from '@react-oauth/google';
import React from 'react';
import { useState } from 'react';

export const Login = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [accessToken, setAccessToken] = useState(''); // [1

    const handleSuccess = (response) => {
        const authCode = response.code;
        fetch('https://localhost:7024/api/auth/google', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
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
    });

    const getEvents = () => {
        fetch('https://26.25.18.126:45456/api/ScheduleTreeTrim/GetCalendarEvents/' + accessToken, {
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
                </div>
            )}
        </div>
    );
};