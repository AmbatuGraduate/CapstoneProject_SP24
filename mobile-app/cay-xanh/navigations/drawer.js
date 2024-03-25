import React, { useState, useEffect } from 'react';
import { createDrawerNavigator } from '@react-navigation/drawer';
import { NavigationContainer } from '@react-navigation/native';
import HomeStackRouting from './homeStack';
import ProfileStackRouting from './profileStack';
import TaskStackRouting from './taskStack';
import LoginScreen from '../screens/login';
import LogoutScreen from '../screens/logout';
import LoadingScreen from '../screens/loadingScreen';
import AsyncStorage from '@react-native-async-storage/async-storage';
import axios from 'axios';
import { Icon } from '@rneui/themed';
import ReportStackRouting from './reportStack';


/***************************************************************
**____CONFIGURE ALL ROUTES THAT USE DRAWER NAVIGATION _____**
**                  CREATED BY: LE ANH QUAN                 **
***************************************************************/

// create axios 
const api = axios.create();

// Add a response interceptor
api.interceptors.response.use(
    response => response,
    async error => {
        const originalRequest = error.config;
        // if  401  (unauthorized) and the request was not a token refresh, refresh the token
        if (error.response.status === 401 && !originalRequest._retry) {
            originalRequest._retry = true;
            const refreshToken = await AsyncStorage.getItem("@refreshToken");
            const response = await axios.get(`https://vesinhdanang.xyz:7024/api/auth/RefreshMobile`, {
                headers: {
                    'Authorization': `Bearer ${refreshToken}`
                }
            });
            const newTokenData = response.data.value;
            const loggedUser = await AsyncStorage.getItem("@user");
            const userData = JSON.parse(loggedUser);
            const updatedUser = {
                ...userData,
                token: newTokenData.token,
                token_received_at: Date.now() / 1000,
                expire_in: newTokenData.expire_in
            };
            await AsyncStorage.setItem("@user", JSON.stringify(updatedUser));
            await AsyncStorage.setItem("@accessToken", newTokenData.token);
            setUser(updatedUser);
            // replace the token in the original request and retry
            originalRequest.headers['Authorization'] = 'Bearer ' + newTokenData.token;
            return api(originalRequest);
        }
        return Promise.reject(error);
    }
);

const Drawer = createDrawerNavigator();
function Routes() {

    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    // check if user is logged in
    useEffect(() => {
        // Check if user is logged in
        AsyncStorage.getItem("@user").then(user => {
            if (user !== null) {
                setUser(JSON.parse(user));
                // Check token expiration
                checkTokenExpiration();
            }
            setLoading(false);
        });
    }, []);


    if (loading) {
        return <LoadingScreen />;
    }

    if (!user) {
        return <LoginScreen setUser={setUser} />;
    }

    async function checkTokenExpiration() {
        const loggedUser = await AsyncStorage.getItem("@user");
        if (loggedUser) {
            const userData = JSON.parse(loggedUser);
            const { token_received_at, expire_in, token } = userData;

            //  expiration timestamp
            const expireTimestamp = token_received_at + expire_in;
            console.log('expireTimestamp', expireTimestamp);
            // check expired token

            if (Date.now() / 1000 >= expireTimestamp) {
                // Token is expired, refresh it
                const refreshToken = await AsyncStorage.getItem("@refreshToken");

                // local test: http://vesinhdanang.xyz/api/auth/RefreshMobile?refreshToken=${refreshToken}
                // server: 'https://192.168.1.7/api/auth/RefreshMobile?refreshToken=${refreshToken}'

                const response = await axios.get(`https://vesinhdanang.xyz:7024/api/auth/RefreshMobile`, {
                    headers: {
                        'Authorization': `Bearer ${refreshToken}`
                    }
                });
                // const response = await api.get(`http://vesinhdanang.xyz/api/auth/RefreshMobile?refreshToken=${refreshToken}`);
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

                setUser(updatedUser);
            }
        }
    }

    // -------------------------------------------------------------------
    // Drawer navigation
    function DrawerNavigator() {
        return (
            <Drawer.Navigator screenOptions={{ headerShown: false }}>

                {/* Home */}
                <Drawer.Screen name="Home"
                    component={HomeStackRouting}
                    options={{
                        drawerLabel: 'Trang chủ',
                        drawerIcon: ({ color, size }) => (
                            <Icon name="home" type='font-awesome' color="#ff6b9b" size={size} />
                        ),
                    }} />

                {/* Profile */}
                <Drawer.Screen name="Profile"
                    component={ProfileStackRouting}
                    options={{
                        drawerLabel: 'Hồ sơ',
                        drawerIcon: ({ color, size }) => (
                            <Icon name="user-circle-o" type='font-awesome' color="#ff6b9b" size={size} />
                        ),
                    }} />

                {/* Schedule */}
                <Drawer.Screen component={TaskStackRouting}
                    name="Tasks"
                    options={{
                        drawerLabel: 'Lịch trình',
                        drawerIcon: ({ color, size }) => (
                            <Icon name="calendar" type='font-awesome' color="#ff6b9b" size={size} />
                        ),
                    }} />

                {/* report */}
                <Drawer.Screen
                    name="Report"
                    options={{
                        drawerLabel: 'Báo cáo vấn đề',
                        drawerIcon: ({ color, size }) => (
                            <Icon name="file-text" type='font-awesome' color="#ff6b9b" size={size} />
                        ),
                    }}>
                    {props => <ReportStackRouting {...props} />}
                </Drawer.Screen>

                {/* logout */}
                <Drawer.Screen
                    name="Logout"
                    options={{
                        drawerLabel: 'Đăng xuất',
                        drawerIcon: ({ color, size }) => (
                            <Icon name="logout" color="#ff6b9b" size={size} />
                        ),
                    }}>
                    {props => <LogoutScreen {...props} setUser={setUser} />}
                </Drawer.Screen>
            </Drawer.Navigator>
        );
    }

    return (
        <NavigationContainer>
            <DrawerNavigator />
        </NavigationContainer>
    );
}

export default Routes;