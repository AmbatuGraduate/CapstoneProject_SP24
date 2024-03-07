import { createDrawerNavigator } from "@react-navigation/drawer";
import { NavigationContainer } from '@react-navigation/native';
import HomeStackRouting from "./homeStack";
import ProfileStackRouting from "./profileStack";
import TaskStackRouting from "./taskStack";
import React, { useState, useEffect } from 'react';

import LoginScreen from "../screens/login";
import LogoutScreen from "../screens/logout";
import LoadingScreen from "../screens/loadingScreen";
import AsyncStorage from '@react-native-async-storage/async-storage';
import axios from 'axios';


/***************************************************************
**____CONFIGURE ALL ROUTES THAT USE DRAWER NAVIGATION _____**
**                  CREATED BY: LE ANH QUAN                 **
***************************************************************/

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

            // Calculate the expiration timestamp
            const expireTimestamp = token_received_at * 1000 + expire_in * 1000;

            // Check if the token is expired
            if (Date.now() >= expireTimestamp) {
                // Token is expired, refresh it
                const refreshToken = await AsyncStorage.getItem("@refreshToken");
                const response = await axios.get(`http://vesinhdanang.xyz/AmbatuGraduate_API/api/auth/RefreshMobile?refreshToken=${refreshToken}`);
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

    function DrawerNavigator() {
        return (
            <Drawer.Navigator screenOptions={{ headerShown: false }}>
                <Drawer.Screen name="Trang chủ" component={HomeStackRouting} />
                <Drawer.Screen name="Hồ sơ" component={ProfileStackRouting} />
                <Drawer.Screen name="Lịch trình" component={TaskStackRouting} />
                <Drawer.Screen name="Logout">
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