import { createNativeStackNavigator } from '@react-navigation/native-stack';
import React from 'react';
import Header from '../shared/header';
import TasksList from '../screens/tasks';
import TaskDetails from '../screens/taskDetails';
import { Image, View } from 'react-native';
import SharedMap from '../shared/map';

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
                        headerTintColor: 'skyblue',
                        headerStyle: {
                            height: 60,
                        },
                        headerBackground: () => (
                            <React.Fragment>
                                <Image
                                    style={{ width: '100%', height: '100%', position: 'absolute' }}
                                    source={require('../assets/game_bg.png')}
                                />
                                <View
                                    style={{
                                        position: 'absolute',
                                        width: '100%',
                                        height: '100%',
                                    }}
                                />
                            </React.Fragment>
                        )

                    }
                }></Stack.Screen>
            <Stack.Screen name="MapsView" component={SharedMap}
                options={{
                    title: 'Vị trí trên bản đồ',
                    headerTintColor: 'skyblue',
                }}>
            </Stack.Screen>
        </Stack.Navigator>
    )
}

export default TaskStackRouting;