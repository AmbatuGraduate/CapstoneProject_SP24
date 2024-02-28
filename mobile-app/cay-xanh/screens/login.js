import React, { useEffect, useState } from 'react';
import { View, Text, Button } from 'react-native';
import * as Google from 'expo-auth-session/providers/google';
import AsyncStorage from '@react-native-async-storage/async-storage';
import * as WebBrowser from 'expo-web-browser';

import { makeRedirectUri } from 'expo-auth-session';

WebBrowser.maybeCompleteAuthSession();
const discovery = {
    authorizationEndpoint: 'https://accounts.google.com/o/oauth2/v2/auth',
    tokenEndpoint: 'https://oauth2.googleapis.com/token',
    revocationEndpoint: 'https://oauth2.googleapis.com/revoke',
};

const LoginScreen = ({ setUser }) => {
    const [userInfo, setUserInfo] = useState(null);
    const [req, res, promptAsyn] = Google.useAuthRequest({
        clientId: '1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com',
        webClientId: '1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com',
        iosClientId: '1083724780407-fu3mbn9hnn8u240phh1hlc1v41nu4r4b.apps.googleusercontent.com',
        androidClientId: '1083724780407-unohjah2mlejmahsc71516hsh9or3ash.apps.googleusercontent.com',
        scopes: ['openid', 'profile', 'email'],
        redirectUri: makeRedirectUri({
            // custome uri scheme
            native: 'com.googleusercontent.apps.1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com://redirect',
            useProxy: true,
        }),
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
                await getUserInfo(res.authentication.accessToken);
            }
        }
    }

    const getUserInfo = async (token) => {
        const response = await fetch('https://www.googleapis.com/userinfo/v2/me', {
            headers: { Authorization: `Bearer ${token}` },
        });
        const user = await response.json();
        console.log(user);
        await AsyncStorage.setItem("@user", JSON.stringify(user));
        setUserInfo(user);
        setUser(user);
    }

    return (
        <View style={styles.container}>
            <Text>{JSON.stringify(userInfo)}</Text>

            <Button
                title="Login with Google"
                onPress={() => promptAsyn()}
            />
            <Button title="Logout" onPress={() => {
                AsyncStorage.removeItem("@user");
                setUserInfo(null);
            }}
            />
        </View>
    );
}
const styles = {
    container: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
    },
};

export default LoginScreen;