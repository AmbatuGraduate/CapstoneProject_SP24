import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';
import LoginScreen from '../screens/login';
import Routes from './drawer';

const RootStack = createNativeStackNavigator();

function RootStackScreen() {
    return (
        <RootStack.Navigator headerMode='none'>
            <RootStack.Screen name="LoginScreen" component={LoginScreen} />
            <RootStack.Screen name="Main" component={Routes} />
        </RootStack.Navigator>
    );
}

export default RootStackScreen;