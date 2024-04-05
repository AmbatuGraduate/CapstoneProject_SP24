import React, { useEffect, useState } from 'react';
import { Text, Image, StyleSheet, TouchableOpacity, Alert } from 'react-native';
import { LinearGradient } from 'expo-linear-gradient';
import * as Google from 'expo-auth-session/providers/google';
import AsyncStorage from '@react-native-async-storage/async-storage';
import * as WebBrowser from 'expo-web-browser';



WebBrowser.maybeCompleteAuthSession();

const discovery = {
    authorizationEndpoint: 'https://accounts.google.com/o/oauth2/v2/auth',
    tokenEndpoint: 'https://oauth2.googleapis.com/token',
    revocationEndpoint: 'https://oauth2.googleapis.com/revoke',
};

const LoginScreen = ({ setUser }) => {
    const [userInfo, setUserInfo] = useState(null);
    const [req, res, promptAsync] = Google.useAuthRequest({
        clientId: '1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com',
        webClientId: '1083724780407-f10bbbl6aui68gfglabjalr9ae0627jj.apps.googleusercontent.com',
        iosClientId: '1083724780407-fu3mbn9hnn8u240phh1hlc1v41nu4r4b.apps.googleusercontent.com',
        androidClientId: '1083724780407-unohjah2mlejmahsc71516hsh9or3ash.apps.googleusercontent.com',
        scopes: [
            'openid',
            'profile',
            'email',
            'https://www.googleapis.com/auth/calendar',
            'https://www.googleapis.com/auth/userinfo.email',
            'https://www.googleapis.com/auth/admin.directory.user',
            'https://www.googleapis.com/auth/userinfo.profile',
            'https://www.googleapis.com/auth/admin.directory.group',
            'https://www.googleapis.com/auth/admin.directory.group.member.readonly',
            'https://mail.google.com/',
            'https://www.googleapis.com/auth/gmail.send',
            'https://www.googleapis.com/auth/gmail.readonly',
            'https://www.googleapis.com/auth/gmail.labels',
            'https://www.googleapis.com/auth/gmail.compose',

        ],
        hd: 'vesinhdanang.xyz',
    }, discovery);


    useEffect(() => {
        handleGoogleLogin();
    }, [res]);

    const isEmployee = async (email) => {
        try {
            const response = await fetch(`https://vesinhdanang.xyz:7024/api/User/IsEmployee?email=${encodeURIComponent(email)}`);
            const data = await response.json();

            if (!response.ok) {
                throw new Error(data.title || 'Something went wrong');
            }

            return data;
        } catch (error) {
            console.error('Error:', error);
            return false;
        }
    }


    async function handleGoogleLogin() {
        const user = await AsyncStorage.getItem("@user");

        if (user) {
            setUserInfo(JSON.parse(user));
        } else {
            if (res?.type === 'success') {
                const userInfo = await getUserInfo(res.authentication.accessToken);
                const isEmployeeResponse = await isEmployee(userInfo.email);
                if (!isEmployeeResponse) {
                    Alert.alert('Đăng nhập không thành công', 'Tài khoản của bạn không có quyền truy cập');
                    return;
                }
                const currentTime = Date.now() / 1000; // Current time in seconds
                const userData = {
                    ...userInfo,
                    token: res.authentication.accessToken,
                    token_received_at: currentTime, // Save the time when the token was received
                    expire_in: res.authentication.expiresIn // Save the number of seconds the token is valid for
                };
                await AsyncStorage.setItem("@user", JSON.stringify(userData));
                await AsyncStorage.setItem("@accessToken", res.authentication.accessToken);
                await AsyncStorage.setItem("@refreshToken", res.authentication.refreshToken);
                setUserInfo(userData);
                setUser(userData);
            }
        }
    }

    const getUserInfo = async (token) => {
        const response = await fetch('https://www.googleapis.com/userinfo/v2/me', {
            headers: { Authorization: `Bearer ${token}` },
        });
        const user = await response.json();

        return user;
    }

    return (
        <LinearGradient
            colors={['#ffffff', '#f5f5f5']}
            style={styles.container}
        >
            <Image
                source={require('../assets/ambatu.png')}
                style={styles.logo}
            />
            <TouchableOpacity style={styles.button} onPress={() => promptAsync()}>
                <Image
                    source={require('../assets/icons/google.jpg')}
                    style={styles.buttonIcon}
                />
                <Text style={styles.buttonText}>Đăng nhập với Google</Text>
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
        width: 150, // Adjust as needed
        height: 150, // Adjust as needed
        marginBottom: 50, // Creates some space between the logo and the button
        resizeMode: 'contain',
    },
    button: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#fff',
        padding: 10,
        borderRadius: 5,
        borderWidth: 1,
        borderColor: 'lightgrey',
        shadowColor: 'black',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.8,
        shadowRadius: 2,
        elevation: 5,
    },
    buttonIcon: {
        width: 24,
        height: 24,
        marginRight: 10,
    },
    buttonText: {
        color: 'black',
        fontSize: 18,
    },
});

export default LoginScreen;