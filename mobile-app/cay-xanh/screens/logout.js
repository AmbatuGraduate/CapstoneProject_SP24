import React, { useEffect } from 'react';
import { View, ActivityIndicator } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';

const LogoutScreen = ({ navigation, setUser }) => {
    useEffect(() => {
        const logout = async () => {
            await AsyncStorage.removeItem("@user");
            await AsyncStorage.removeItem("@accessToken");
            await AsyncStorage.removeItem("@refreshToken");
            setUser(null);
        };

        logout();
    }, [navigation, setUser]);

    return (
        <View>
            <ActivityIndicator size="large" />
        </View>
    );
};

export default LogoutScreen;