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
        AsyncStorage.getItem("@user").then(user => {
            if (user !== null) {
                setUser(JSON.parse(user));
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