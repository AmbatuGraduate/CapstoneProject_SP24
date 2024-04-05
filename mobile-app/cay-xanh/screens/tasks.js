import React, { useState, useEffect } from "react";
import { StyleSheet, View, Text, TouchableOpacity, FlatList, ActivityIndicator } from 'react-native';
import { Calendar } from "react-native-calendars";
import moment from "moment";
import { LocaleConfig } from 'react-native-calendars';
import { LinearGradient } from 'expo-linear-gradient';
import { Icon } from '@rneui/themed';
import { api } from "../shared/api";

import AsyncStorage from '@react-native-async-storage/async-storage';

// cau hinh tieng viet
LocaleConfig.locales['vi'] = {
    monthNames: [
        'Tháng 1',
        'Tháng 2',
        'Tháng 3',
        'Tháng 4',
        'Tháng 5',
        'Tháng 6',
        'Tháng 7',
        'Tháng 8',
        'Tháng 9',
        'Tháng 10',
        'Tháng 11',
        'Tháng 12'
    ],
    monthNamesShort: ['Tháng 1.', 'Tháng 2.', 'Tháng 3.', 'Tháng 4.', 'Tháng 5.', 'Tháng 6.', 'Tháng 7.', 'Tháng 8.', 'Tháng 9.', 'Tháng 10.', 'Tháng 11.', 'Tháng 12.'],
    dayNames: ['Chủ nhật', 'Thứ 2', 'Thứ 3', 'Thứ 4', 'Thứ 5', 'Thứ 6', 'Thứ 7'],
    dayNamesShort: ['CN.', 'T2.', 'T3.', 'T4.', 'T5.', 'T6.', 'T7.'],
    today: "Hôm nay"
};

LocaleConfig.defaultLocale = 'vi';

export default function TasksList({ navigation }) {
    const [events, setEvents] = useState([]);
    const [isLoading, setLoading] = useState(true);
    const [selectedDate, setSelectedDate] = useState(moment().format("YYYY-MM-DD"));
    const [transformedData, setTransformedData] = useState({});
    const [marked, setMarked] = useState({}); // marked date
    const [pressedDate, setPressedDate] = useState(null);

    const [emptyEvents, setEmptyEvents] = useState('');

    const taskStatuses = {
        'Done': 'Đã hoàn thành',
        'Late': 'Quá hạn',
        'In Progress': 'Đang thực hiện',
        'Not Start': 'Chưa bắt đầu',
    };

    const statusColors = {
        'Done': 'green',
        'Late': '#F76400',
        'In Progress': 'darkblue',
        'Not Start': '#840808',
    };

    // local test: 'http://192.168.1.7:45455/api/Calendar/GetCalendarEvents/' + atoken
    // server: 'http://vesinhdanang.xyz/AmbatuGraduate_API/api/Calendar/GetCalendarEvents/' + atoken
    const getEvents = async () => {

        try {
            var useremail = JSON.parse(await AsyncStorage.getItem("@user"))?.email;
            var department = JSON.parse(await AsyncStorage.getItem("@user"))?.department;

            var calendarId;
            if (department.toString().toLowerCase().includes('cay xanh')) {
                calendarId = 1;
            } else if (department.toString().toLowerCase().includes('ve sinh')) {
                calendarId = 2;
            } else if (department.toString().toLowerCase().includes('quet don')) {
                calendarId = 3;
            }
            const atoken = await AsyncStorage.getItem("@accessToken");
            if (atoken !== null) {
                api.get(`https://vesinhdanang.xyz:7024/api/Calendar/GetCalendarEventsByAttendeeEmail?calendarTypeEnum=${calendarId}&attendeeEmail=${useremail}`, {
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${atoken}`,
                        "Client-Type": "Mobile"
                    },
                })
                    .then((res) => {
                        if (Array.isArray(res.data)) {
                            const jsonEvents = res.data.map(item => {
                                const event = {
                                    ...item.myEvent,
                                    extendedProperties: item.myEvent.extendedProperties,
                                };
                                return event;
                            });
                            setEvents(jsonEvents);
                        }
                        else {
                            console.log('Unexpected response from API:', res);
                        }
                        setLoading(false);
                        setEmptyEvents('Không có công việc nào trong ngày này');
                    })
                    .catch((error) => {
                        console.log('There has been a problem with fetch operation: ', error.message);
                        setLoading(false);
                        setEmptyEvents('Không tải được dữ liệu, vui lòng thử lại sau');
                    });
            } else {
                console.log('token null');
                setLoading(false);
                setEmptyEvents('Không tải được dữ liệu, vui lòng thử lại sau');
            }
        } catch (error) {
            console.error(error);
            setLoading(false);
            setEmptyEvents('');
        }
    }

    // Events list not change when add new data, only change if  reload
    useEffect(() => {
        const unsubscribe = navigation.addListener('focus', () => {
            // The screen is focused
            // Call any action
            getEvents();
        });

        // Return the function to unsubscribe from the event so it gets removed on unmount
        return unsubscribe;
    }, [navigation]);

    useEffect(() => {
        if (Array.isArray(events)) {
            const data = {};
            const markedDates = {};
            events.forEach(item => {
                if (item.start) {
                    const [date, time] = item.start.split("T");
                    const currentDate = date;
                    if (!data[currentDate]) {
                        data[currentDate] = [];
                    }
                    data[currentDate].push({ ...item, date, time: time.split('+')[0], day: currentDate });
                    markedDates[currentDate] = { marked: true };
                }
            });
            setTransformedData(data);
            setMarked(markedDates);
        }
    }, [events]);

    // This function is responsible for rendering the tasks for a selected date.
    // It returns a FlatList component that displays a list of tasks for the selected date.
    // Each task is wrapped in a TouchableOpacity component, which allows the user to press on a task.
    // When a task is pressed, the app navigates to a 'TaskDetails' screen where the user can view more details about the task.
    // The function uses the 'data' state variable, which should contain an array of tasks for each date.
    const renderItemsForSelectedDate = () => {
        const items = transformedData[selectedDate] || [];
        if (items.length === 0) {
            return (
                <View style={styles.emptyContainer}>
                    <Text style={styles.emptyText}>{emptyEvents}</Text>
                </View>
            );
        }

        return (
            <LinearGradient
                colors={['rgba(197, 252, 234, 0.5)', 'rgba(255, 255, 255, 0.6)']}
                start={{ x: 0, y: 0 }}
                end={{ x: 0, y: 1 }}
            >
                <FlatList
                    // The data prop is the array of tasks for the selected date.
                    data={items}
                    // The keyExtractor prop is a function that returns a unique identifier for each task.
                    keyExtractor={(item) => item.id.toString()}
                    // The renderItem prop is a function that returns a component for each task.
                    renderItem={({ item }) => (
                        <TouchableOpacity
                            // Apply styles to the TouchableOpacity.
                            style={[
                                styles.records,
                                item.extendedProperties.privateProperties?.JobWorkingStatus === 'Done' ? styles.doneBackground :
                                    item.extendedProperties.privateProperties?.JobWorkingStatus === 'Late' ? styles.lateBackground :
                                        item.extendedProperties.privateProperties?.JobWorkingStatus === 'In Progress' ? styles.inProgressBackground :
                                            (item.extendedProperties.privateProperties?.JobWorkingStatus === 'Not Start' || !item.extendedProperties.privateProperties?.JobWorkingStatus) ? styles.notStartBackground : null
                            ]}
                            // When the TouchableOpacity is pressed, navigate to the 'TaskDetails' screen.
                            onPress={() => {
                                navigation.navigate('TaskDetails', {
                                    key: item.id,
                                    summary: item.summary,
                                    description: item.description,
                                    address: item.location,
                                    start: item.date,
                                    status: item.extendedProperties.privateProperties?.JobWorkingStatus || 'Not Start',
                                    trees: item.extendedProperties.privateProperties?.Tree,
                                });
                            }}
                        >
                            {/* <Text style={styles.itemText}>Loai Cay: {item.type}</Text>
                        <Text style={styles.itemText}>Dia chi: {item.street}</Text> */}
                            <View style={styles.itemContainer}>
                                <Text style={styles.itemLabel} numberOfLines={1} ellipsizeMode='tail'>{item.summary}</Text>
                                <Icon name="chevron-right" size={24} color="black" />
                            </View>
                            <View style={styles.itemContainer}>
                                <Text style={styles.itemText}>{item.location}</Text>
                            </View>
                            <View style={styles.itemContainer}>
                                {/* <Text style={styles.itemLabel}>Trạng thái:</Text> */}
                                <Text style={[
                                    styles.statusText,
                                    item.extendedProperties.privateProperties?.JobWorkingStatus === 'In Progress' ? styles.strongBlue :
                                        item.extendedProperties.privateProperties?.JobWorkingStatus === 'Done' ? styles.strongGreen :
                                            item.extendedProperties.privateProperties?.JobWorkingStatus === 'Not Start' ? styles.strongRed :
                                                item.extendedProperties.privateProperties?.JobWorkingStatus === 'Late' ? styles.strongOrange :
                                                    (!item.extendedProperties.privateProperties?.JobWorkingStatus || item.extendedProperties.privateProperties?.JobWorkingStatus === 'Not Started') ? styles.strongRed : null
                                ]}>
                                    {taskStatuses[item.extendedProperties.privateProperties?.JobWorkingStatus] || 'Not Started'}
                                </Text>
                                <Icon
                                    style={{ marginRight: 5 }}
                                    name={item.extendedProperties.privateProperties?.JobWorkingStatus === 'Done' ? 'check' : 'hourglass-2'}
                                    type="font-awesome"
                                    color={statusColors[item.extendedProperties.privateProperties?.JobWorkingStatus]}
                                    size={16}
                                />

                            </View>
                        </TouchableOpacity>
                    )}
                />
            </LinearGradient>
        );
    };
    // handle day press
    const handleDayPress = (day) => {
        setSelectedDate(day.dateString);

        // Reset the background color of the previously pressed date
        if (pressedDate) {
            setMarked(prevMarked => ({
                ...prevMarked,
                [pressedDate]: { ...prevMarked[pressedDate], selected: false },
            }));
        }

        // Mark the background color of the currently pressed date
        setMarked(prevMarked => ({
            ...prevMarked,
            [day.dateString]: { ...prevMarked[day.dateString], selected: true },
        }));

        // Set the currently pressed date
        setPressedDate(day.dateString);
    };

    return (
        <View style={styles.container}>
            <Calendar
                onDayPress={handleDayPress}
                markedDates={marked}
            />

            <View style={{ flex: 1 }}>
                {isLoading ? (
                    <View style={{
                        position: 'absolute',
                        top: 0,
                        left: 0,
                        right: 0,
                        bottom: 0,
                        justifyContent: 'center',
                        alignItems: 'center'
                    }}>
                        <ActivityIndicator size="large" color="lightgreen" />
                    </View>
                ) : (
                    renderItemsForSelectedDate()
                )}
            </View>
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    records: {
        backgroundColor: 'white',
        flex: 1,
        borderRadius: 5,
        borderWidth: 2,
        borderColor: 'lightgray',
        padding: 10,
        marginHorizontal: 20,
        marginVertical: 10,
        shadowColor: "#333",
        shadowOffset: {
            width: 0,
            height: 6,
        },
        shadowOpacity: 0.37,
        shadowRadius: 7.49,
        elevation: 6,
    },
    itemContainer: {
        flexDirection: 'row',
        marginBottom: 5,
        justifyContent: 'space-between',
    },
    itemLabel: {
        fontSize: 16,
        fontFamily: 'nunito-regular',
        fontWeight: 'bold',
        letterSpacing: 1,
        flex: 0.75,
    },
    itemText: {
        fontSize: 16,
        flex: 0.75,
        fontFamily: 'quolibet',
        color: 'grey',
        fontWeight: '500',

    },
    statusText: {
        fontSize: 15,
        flex: 0.75,
        fontWeight: 'bold',
    },

    emptyContainer: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#f2f2f2',
        padding: 20,
    },
    emptyText: {
        fontWeight: 'bold',
        fontSize: 20,
        color: '#999',
        textAlign: 'center',
    },

    // background color for different status
    doneBackground: {
        backgroundColor: '#E2FFE3',
    },
    lateBackground: {
        backgroundColor: '#F7AD86',
    },
    inProgressBackground: {
        backgroundColor: '#FFFE7E',
    },
    notStartBackground: {
        backgroundColor: '#F29B9B', // light red
    },
    strongBlue: {
        color: 'darkblue',
        fontWeight: 'bold',
        letterSpacing: 1,
    },
    strongGreen: {
        color: 'green',
        fontWeight: 'bold',
        letterSpacing: 1,
    },
    strongRed: {
        color: '#840808',
        fontWeight: 'bold',
        letterSpacing: 1,
    },
    strongOrange: {
        color: '#F76400',
        fontWeight: 'bold',
        letterSpacing: 1,
    },
});

// Initially, the selectedDate state is set to the current date when the component mounts.

// When you press a day on the calendar, the handleDayPress function is called. This function updates the selectedDate state to the date that was pressed and updates the marked state to highlight the selected day.

// After the selectedDate state is updated, the component re-renders, and the renderItemsForSelectedDate function is called inside the ScrollView to render the items for the selected date.