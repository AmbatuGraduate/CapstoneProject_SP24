import { createDrawerNavigator } from "@react-navigation/drawer";
import { NavigationContainer } from '@react-navigation/native';
import HomeStackRouting from "./homeStack";
import ProfileStackRouting from "./profileStack";
import TaskStackRouting from "./taskStack";
import React, { useState } from 'react';

import LoginScreen from "../screens/login";
import LogoutScreen from "../screens/logout";

/***************************************************************
**____CONFIGURE ALL ROUTES THAT USE DRAWER NAVIGATION _____**
**                  CREATED BY: LE ANH QUAN                 **
***************************************************************/

const Drawer = createDrawerNavigator();
function Routes() {

    const [user, setUser] = useState(null);

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