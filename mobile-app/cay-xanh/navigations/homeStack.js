import { createNativeStackNavigator } from '@react-navigation/native-stack';
import Home from '../screens/home';
import React from 'react';
import Header from '../shared/header';

/*************************************************************
**________________ HOME NAVIGATION OF APP __________________**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/

const Stack = createNativeStackNavigator();
function HomeStackRouting() {
    return (
        <Stack.Navigator>
            <Stack.Screen name="Trang chu" component={Home} options={({ navigation }) => ({
                headerTitle: () => <Header navigation={navigation} title='Trang chủ' />,
            })}>
            </Stack.Screen>
        </Stack.Navigator>
    )
}

export default HomeStackRouting;