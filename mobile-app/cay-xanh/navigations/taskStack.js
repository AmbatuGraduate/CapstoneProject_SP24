import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';
import Header from '../shared/header';
import TasksList from '../screens/tasks';
import TaskDetails from '../screens/taskDetails';

/*************************************************************
**________________ TASK NAVIGATION OF APP __________________**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/

const Stack = createNativeStackNavigator();
function TaskStackRouting() {
    return (
        <Stack.Navigator>
            <Stack.Screen name="Lich trinh / Cong viec" component={TasksList} options={({ navigation }) => ({
                headerTitle: () => <Header navigation={navigation} title='Lịch trình' />,
            })}>
            </Stack.Screen>
            <Stack.Screen name="TaskDetails" component={TaskDetails}
                options={
                    {
                        title: 'Chi tiết công việc',
                        headerTintColor: '#333',
                        headerStyle: {
                            backgroundColor: '#cffdbc',
                            height: 60,
                        }

                    }
                }></Stack.Screen>
        </Stack.Navigator>
    )
}

export default TaskStackRouting;