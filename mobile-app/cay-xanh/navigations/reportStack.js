import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';
import Header from '../shared/header';
import ReportDetails from '../screens/reportDetails';
import Report from '../screens/report';

const Stack = createNativeStackNavigator();

function ReportStackRouting() {
    return (
        <Stack.Navigator>
            <Stack.Screen name="Bao cao" component={Report} options={({ navigation }) => ({
                headerTitle: () => <Header navigation={navigation} title='Báo cáo vấn đề' />,
            })}>
            </Stack.Screen>
            <Stack.Screen name="ReportDetails" component={ReportDetails}
                options={{
                    title: 'Chi tiết báo cáo',
                    headerTintColor: 'green'
                }}>
            </Stack.Screen>
        </Stack.Navigator>
    )
}

export default ReportStackRouting;