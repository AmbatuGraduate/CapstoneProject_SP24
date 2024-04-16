import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';
import Header from '../shared/header';
import ReportDetails from '../screens/reportDetails';
import Report from '../screens/report';
import SharedMap from '../shared/map';

const Stack = createNativeStackNavigator();

function ReportStackRouting() {
    return (
        <Stack.Navigator>
            <Stack.Screen name="Bao cao" component={Report} options={({ navigation }) => ({
                headerTitle: () => <Header navigation={navigation} title='Danh sách báo cáo đã gửi' />,
            })}>
            </Stack.Screen>
            <Stack.Screen name="ReportDetails" component={ReportDetails}
                options={{
                    title: 'Chi tiết báo cáo',
                    headerTintColor: 'skyblue',
                }}>
            </Stack.Screen>
            <Stack.Screen name="MapsView" component={SharedMap}
                options={{
                    title: 'Vị trí trên bản đồ',
                    headerTintColor: 'skyblue',
                }}>
            </Stack.Screen>
        </Stack.Navigator>
    )
}

export default ReportStackRouting;