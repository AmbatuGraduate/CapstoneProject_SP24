import React, { useState, useEffect } from "react";
import { StyleSheet, View, Text, TouchableOpacity, FlatList } from 'react-native';
import { Calendar } from "react-native-calendars";
import moment from "moment";

import AsyncStorage from '@react-native-async-storage/async-storage';


export default function TasksList({ navigation }) {
    const [events, setEvents] = useState([]);
    const [isLoading, setLoading] = useState(true);
    const [selectedDate, setSelectedDate] = useState(moment().format("YYYY-MM-DD"));
    const [transformedData, setTransformedData] = useState({});
    const [marked, setMarked] = useState({}); // marked date
    const [pressedDate, setPressedDate] = useState(null);


    // local test: 'http://192.168.1.40:45456/api/Calendar/GetCalendarEvents/' + atoken
    // server: 'http://vesinhdanang.xyz/AmbatuGraduate_API/api/Calendar/GetCalendarEvents/' + atoken
    const getEvents = async () => {
        try {
            AsyncStorage.getItem("@accessToken").then(atoken => {
                if (atoken !== null) {
                    fetch('http://vesinhdanang.xyz/AmbatuGraduate_API/api/Calendar/GetCalendarEvents/' + atoken,
                        {
                            method: 'GET',
                            headers: {
                                "Content-Type": "application/json",
                            },
                        })
                        .then((res) => {
                            if (res.ok) {
                                return res.json();
                            } else {
                                throw new Error('Network response was not ok');
                            }
                        })
                        .then((json) => {
                            const jsonEvents = json.value.map(item => {
                                // add extended properties in the event object
                                const event = {
                                    ...item.myEvent,
                                    extendedProperties: item.myEvent.extendedProperties,
                                };
                                return event;
                            });
                            setEvents(jsonEvents);
                        })
                        .catch((error) => {
                            console.log('There has been a problem with fetch operation: ', error.message);
                        });
                } else {
                    console.log('token null');
                }
            });
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    }

    // Events list not change when add new data, only change if  reload
    useEffect(() => {
        getEvents();
    }, []);

    useEffect(() => {
        if (Array.isArray(events)) {
            const data = {};
            const markedDates = {};
            events.forEach(item => {
                const [date, time] = item.start.split("T");
                const currentDate = date;
                if (!data[currentDate]) {
                    data[currentDate] = [];
                }
                data[currentDate].push({ ...item, date, time: time.split('+')[0], day: currentDate });
                markedDates[currentDate] = { marked: true };
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
                    <Text style={styles.emptyText}>Không có hoạt động nào trong hôm nay</Text>
                </View>
            );
        }

        return (
            <FlatList
                // The data prop is the array of tasks for the selected date.
                data={items}
                // The keyExtractor prop is a function that returns a unique identifier for each task.
                keyExtractor={(item) => item.id.toString()}
                // The renderItem prop is a function that returns a component for each task.
                renderItem={({ item }) => (
                    <TouchableOpacity
                        // Apply styles to the TouchableOpacity.
                        style={styles.records}
                        // When the TouchableOpacity is pressed, navigate to the 'TaskDetails' screen.
                        onPress={() => {
                            navigation.navigate('TaskDetails', {
                                key: item.id,
                                description: item.description,
                                address: item.location,
                                start: item.date,
                                status: item.extendedProperties.privateProperties?.JobWorkingStatus,
                                img: 'https://www.canhquan.net/Content/Images/FileUpload/2018/2/p1030345_500_03%20(1)-1.jpg'
                            });
                        }}
                    >
                        {/* <Text style={styles.itemText}>Loai Cay: {item.type}</Text>
                        <Text style={styles.itemText}>Dia chi: {item.street}</Text> */}
                        <View style={styles.itemContainer}>
                            <Text style={styles.itemLabel}>Nhiệm vụ:</Text>
                            <Text style={styles.itemText}>{item.summary}</Text>
                        </View>
                        <View style={styles.itemContainer}>
                            <Text style={styles.itemLabel}>Ngày:</Text>
                            <Text style={styles.itemText}>{item.date}</Text>
                        </View>
                        <View style={styles.itemContainer}>
                            <Text style={styles.itemLabel}>Giờ:</Text>
                            <Text style={styles.itemText}>{item.time}</Text>
                        </View>
                        <View style={styles.itemContainer}>
                            <Text style={styles.itemLabel}>Địa chỉ:</Text>
                            <Text style={styles.itemText}>{item.location}</Text>
                        </View>
                        {item.extendedProperties.privateProperties?.JobWorkingStatus && (
                            <View style={styles.itemContainer}>
                                <Text style={styles.itemLabel}>Trạng thái:</Text>
                                <Text style={styles.itemText}>{item.extendedProperties.privateProperties.JobWorkingStatus}</Text>
                            </View>
                        )}
                    </TouchableOpacity>
                )}
            />
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
                    <Text>Loading...</Text>
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
    },
    itemLabel: {
        fontSize: 16,
        fontWeight: 'bold',
        flex: 0.25,
    },
    itemText: {
        fontSize: 16,
        flex: 0.75,
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
});

// Initially, the selectedDate state is set to the current date when the component mounts.

// When you press a day on the calendar, the handleDayPress function is called. This function updates the selectedDate state to the date that was pressed and updates the marked state to highlight the selected day.

// After the selectedDate state is updated, the component re-renders, and the renderItemsForSelectedDate function is called inside the ScrollView to render the items for the selected date.