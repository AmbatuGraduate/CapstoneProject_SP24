import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';
import Header from '../shared/header';
import Profile from '../screens/profile';
import Password from '../screens/password';
import EditProfile from '../screens/editProfile';
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
            <Stack.Screen name="ChangePassword" component={Password}
                options={{
                    title: 'Đổi mật khẩu',
                    headerTintColor: 'palegreen'
                }}>
            </Stack.Screen>
            <Stack.Screen name="EditProfile" component={EditProfile}
                options={{
                    title: 'Chỉnh sửa thông tin',
                    headerTintColor: '#f1356d'
                }}>
            </Stack.Screen>
        </Stack.Navigator>
    )
}

export default ProfileStackRouting;