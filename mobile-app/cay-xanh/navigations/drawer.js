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


                // local test: http://192.168.1.7:45455/api/auth/RefreshMobile
                // server: https://vesinhdanang.xyz:7024/api/auth/RefreshMobile
                const response = await axios.get(`http://192.168.1.7:45455/api/auth/RefreshMobile`, {
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
                        drawerItemStyle: { justifyContent: 'flex-end' },
                        drawerIcon: ({ color, size }) => (
                            <Icon name="home" type='font-awesome' color="#01796E" size={size} />
                        ),
                    }} />

                {/* Profile */}
                <Drawer.Screen name="Profile"
                    component={ProfileStackRouting}
                    options={{
                        drawerLabel: 'Hồ sơ',
                        drawerItemStyle: { justifyContent: 'flex-end' },

                        drawerIcon: ({ color, size }) => (
                            <Icon name="user-circle-o" type='font-awesome' color="#01796E" size={size} />
                        ),
                    }} />

                {/* Schedule */}
                <Drawer.Screen component={TaskStackRouting}
                    name="Tasks"
                    options={{
                        drawerLabel: 'Lịch trình',
                        drawerItemStyle: { justifyContent: 'flex-end' },
                        drawerIcon: ({ color, size }) => (
                            <Icon name="calendar" type='font-awesome' color="#01796E" size={size} />
                        ),
                    }} />

                {/* report */}
                <Drawer.Screen
                    name="Report"
                    options={{
                        drawerLabel: 'Báo cáo vấn đề',
                        drawerItemStyle: { justifyContent: 'flex-end' },

                        drawerIcon: ({ color, size }) => (
                            <Icon name="file-text-o" type='font-awesome' color="#01796E" size={size} />
                        ),
                    }}>
                    {props => <ReportStackRouting {...props} />}
                </Drawer.Screen>

                {/* logout */}
                <Drawer.Screen
                    name="Logout"
                    options={{
                        drawerLabel: 'Đăng xuất',
                        drawerItemStyle: { justifyContent: 'flex-end' },
                        drawerIcon: ({ color, size }) => (
                            <Icon name="sign-out" type='font-awesome' color="#01796E" size={size} />
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