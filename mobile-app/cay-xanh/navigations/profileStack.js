import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';
import Header from '../shared/header';
import Profile from '../screens/profile';
/*************************************************************
**________________ HOME NAVIGATION OF APP __________________**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/

const Stack = createNativeStackNavigator();
function ProfileStackRouting() {
    return (
        <Stack.Navigator>
            <Stack.Screen name="Ho so" component={Profile} options={({ navigation }) => ({
                headerTitle: () => <Header navigation={navigation} title='Thông tin cá nhân' />,
            })}>
            </Stack.Screen>
        </Stack.Navigator>
    )
}

export default ProfileStackRouting;