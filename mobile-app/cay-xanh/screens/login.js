import React, { useEffect, useState } from 'react';
import { View, Text, Button, Image, StyleSheet, TouchableOpacity } from 'react-native';
import { LinearGradient } from 'expo-linear-gradient';


import * as Google from 'expo-auth-session/providers/google';
import AsyncStorage from '@react-native-async-storage/async-storage';
import * as WebBrowser from 'expo-web-browser';
import axios from 'axios';

WebBrowser.maybeCompleteAuthSession();
const discovery = {
    authorizationEndpoint: 'https://accounts.google.com/o/oauth2/v2/auth',
    tokenEndpoint: 'https://oauth2.googleapis.com/token',
    revocationEndpoint: 'https://oauth2.googleapis.com/revoke',
};

const LoginScreen = ({ setUser }) => {
    useEffect(() => {
        checkTokenExpiration();
    }, []);

    async function checkTokenExpiration() {
        const user = await AsyncStorage.getItem("@user");
        if (user) {
            const userData = JSON.parse(user);
            const { token_received_at, expire_in, token } = userData;

            // Calculate the expiration timestamp
            const expireTimestamp = token_received_at * 1000 + expire_in * 1000;

            // Check if the token is expired
            if (Date.now() >= expireTimestamp) {
                // Token is expired, refresh it
                const refreshToken = await AsyncStorage.getItem("@refreshToken");
                const response = await axios.get(`http://192.168.1.40:45456/api/auth/RefreshMobile?refreshToken=${refreshToken}`);
                const newTokenData = response.data.value;

                // Update the user data with the new token
                const updatedUser = {
                    ...userData,
                    token: newTokenData.token,
                    token_received_at: Date.now() / 1000, // Update the time when the new token was received
                    expire_in: newTokenData.expire_in // Update the number of seconds the new token is valid for
                };

                // Save the updated user data
                await AsyncStorage.setItem("@user", JSON.stringify(updatedUser));
                await AsyncStorage.setItem("@accessToken", newTokenData.token);

                setUserInfo(updatedUser);
            } else {
                // Token is not expired, set the user info
                setUserInfo(userData);
            }
        }
    }

    const [userInfo, setUserInfo] = useState(null);
    const [req, res, promptAsyn] = Google.useAuthRequest({
        clientId: '1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com',
        webClientId: '1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com',
        iosClientId: '1083724780407-fu3mbn9hnn8u240phh1hlc1v41nu4r4b.apps.googleusercontent.com',
        androidClientId: '1083724780407-unohjah2mlejmahsc71516hsh9or3ash.apps.googleusercontent.com',
        scopes: ['openid', 'profile', 'email', 'https://www.googleapis.com/auth/calendar', ' https://www.googleapis.com/auth/userinfo.email', 'https://www.googleapis.com/auth/admin.directory.user', 'https://www.googleapis.com/auth/userinfo.profile'],
        // redirectUri: makeRedirectUri({
        //     // custome uri scheme
        //     native: 'https://auth.expo.io/@aobnao9/cay-xanh',
        //     useProxy: true,
        // }),
    }, discovery);

    useEffect(() => {
        handleGoogleLogin();
    }, [res]);


    async function handleGoogleLogin() {
        const user = await AsyncStorage.getItem("@user");

        if (user) {
            setUserInfo(JSON.parse(user));
        } else {
            if (res?.type === 'success') {
                const userInfo = await getUserInfo(res.authentication.accessToken);
                const currentTime = Date.now() / 1000; // Current time in seconds
                const userData = {
                    ...userInfo,
                    token: res.authentication.accessToken,
                    token_received_at: currentTime, // Save the time when the token was received
                    expire_in: res.authentication.expiresIn // Save the number of seconds the token is valid for
                };
                await AsyncStorage.setItem("@accessToken", res.authentication.accessToken);
                await AsyncStorage.setItem("@refreshToken", res.authentication.refreshToken);
            }
        }
    }

    const getUserInfo = async (token) => {
        const response = await fetch('https://www.googleapis.com/userinfo/v2/me', {
            headers: { Authorization: `Bearer ${token}` },
        });
        const user = await response.json();
        await AsyncStorage.setItem("@user", JSON.stringify(user));

        setUserInfo(user);
        setUser(user);
    }

    return (
        <LinearGradient
            colors={['#98FB98', '#E0FFD1']}
            style={styles.container}
        >
            <Image
                source={require('../assets/icons/cayxanh.jpg')}
                style={styles.logo}
            />
            <Text style={styles.title}>Cay xanh</Text>
            <TouchableOpacity style={styles.button} onPress={() => promptAsyn()}>
                <Image
                    source={require('../assets/icons/google.jpg')}
                    style={styles.buttonIcon}
                />
                <Text style={styles.buttonText}>Login with Google</Text>
            </TouchableOpacity>
        </LinearGradient>
    );
}
const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#F5FCFF',
    },
    logo: {
        width: '100%',
        height: '50%',
        position: 'absolute',
        top: 0,
        opacity: 0.5,
    },
    title: {
        fontSize: 30,
        marginBottom: 20,
        color: '#000',
        letterSpacing: 1.5,
        fontWeight: 'bold',
        textShadowColor: 'rgba(0, 0, 0, 0.75)',
        textShadowOffset: { width: -1, height: 1 },
        textShadowRadius: 10
    },
    button: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#4285F4',
        padding: 10,
        borderRadius: 5,
    },
    buttonIcon: {
        width: 24,
        height: 24,
        marginRight: 10,
    },
    buttonText: {
        color: '#fff',
        fontSize: 18,
    },
});
export default LoginScreen;