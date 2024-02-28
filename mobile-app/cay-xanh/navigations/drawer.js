
import { createDrawerNavigator } from "@react-navigation/drawer";
import { NavigationContainer } from '@react-navigation/native';
import HomeStackRouting from "./homeStack";
import ProfileStackRouting from "./profileStack";
import TaskStackRouting from "./taskStack";
import React, { useState } from 'react';

import LoginScreen from "../screens/login";

/*************************************************************
**____CONFIGUURE ALL ROUTES THAT USE DRAWER NAVIGATION _____**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/

const Drawer = createDrawerNavigator();
function Routes() {

    const [user, setUser] = useState(null);

    if (!user) {
        return <LoginScreen setUser={setUser} />;
    }

    return (
        <NavigationContainer>
            <Drawer.Navigator screenOptions={{
                headerShown: false,
            }}>
                <Drawer.Screen name="Trang chủ" component={HomeStackRouting} />
                <Drawer.Screen name="Hồ sơ" component={ProfileStackRouting} />
                <Drawer.Screen name="Lịch trình" component={TaskStackRouting} />
            </Drawer.Navigator>
        </NavigationContainer>
    );
}


export default Routes;