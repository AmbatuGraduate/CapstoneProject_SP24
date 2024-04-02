import React, { useState, useEffect } from "react";
import { StyleSheet, View, Text, FlatList, TouchableOpacity, ActivityIndicator } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { Icon } from '@rneui/themed';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { api } from "../shared/api";
import * as signalR from '@microsoft/signalr';


export default function Home() {
    // notification
    // -----------------------------------

    const [connection, setConnection] = useState(null);
    const [notifications, setNotifications] = useState([]);
    const [loadingNotif, setLoadingNotif] = useState(false);

    const fetchNotifications = async () => {
        setLoadingNotif(true);
        try {
            var username = JSON.parse(await AsyncStorage.getItem("@user"))?.email;
            const atoken = await AsyncStorage.getItem("@accessToken");
            if (atoken !== null) {
                api.get(`https://vesinhdanang.xyz:7024/api/Notification/GetByUsername/${username}`, {
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${atoken}`,
                        "Client-Type": "Mobile"
                    },
                })
                    .then((res) => {
                        setNotifications(res.data);
                        setLoadingNotif(false);
                    })
                    .catch((error) => {
                        console.log('There has been a problem with fetch operation: ', error.message);
                        setLoadingNotif(false);
                    });
            } else {
                console.log('token null');
                setLoadingNotif(false);
            }
        } catch (error) {
            console.error(error);
            setLoadingNotif(false);
        }
    }


    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl('https://vesinhdanang.xyz:7024/chatHub')
            .configureLogging(signalR.LogLevel.Information)
            .build();

        setConnection(newConnection);
    }, []);

    useEffect(() => {
        if (connection) {
            connection.start()
                .then(() => {
                    console.log('Connection started!');
                    fetchNotifications();
                })
                .catch(err => console.log('Error while establishing connection :('));

            connection.on('OnConnected', (data) => {
                OnConnected();
            });

            connection.on("ReceivedNotification", function (msg) {
                console.log(msg);
            });

            connection.on("ReceivedPersonalNotification", function (msg, user) {
                console.log("ReceivePersonalNotification event triggered");
                console.log("Message: " + msg);
                console.log("User: " + user);

                setNotifications(prevNotifications => {
                    console.log("Previous notifications: " + prevNotifications);
                    const newNotifications = [...prevNotifications, user + ' - ' + msg];
                    console.log("New notifications: " + newNotifications);
                    return newNotifications;
                });
            });

        }

        return () => {
            if (connection) {
                connection.stop();
            }
        };
    }, [connection]);

    const OnConnected = async () => {
        try {
            var username = JSON.parse(await AsyncStorage.getItem("@user"))?.email;
            await connection.invoke('SaveUserConnection', username)
        } catch (err) {
            console.error(err.toString());
        }
    }

    // connected -----------------------------------------------------

    // -----------------------------------------------------
    const currentDate = new Date();
    const dayOfWeek = currentDate.toLocaleDateString('vi-VN', { weekday: 'long' });

    const navigation = useNavigation();
    const [eventsCount, setEventsCount] = useState(0);
    const [isLoading, setIsLoading] = useState(false);

    const handlePress = () => {
        navigation.navigate('Tasks');
    };

    const getEventsCount = async () => {
        setIsLoading(true);
        try {
            var useremail = JSON.parse(await AsyncStorage.getItem("@user"))?.email;
            const atoken = await AsyncStorage.getItem("@accessToken");;
            if (atoken !== null) {
                api.get(`https://vesinhdanang.xyz:7024/api/Calendar/NumberOfEventsUser?calendarTypeEnum=1&attendeeEmail=${useremail}`, {
                    headers: {
                        "Content-Type": "application/json",
                        "Authorization": `Bearer ${atoken}`,
                        "Client-Type": "Mobile"
                    },
                })
                    .then((res) => {
                        setEventsCount(res.data);
                        setIsLoading(false);
                    })
                    .catch((error) => {
                        console.log('There has been a problem with fetch operation: ', error.message);
                        setEventsCount(0);
                        setIsLoading(false);
                    });
            } else {
                console.log('token null');
                setEventsCount(0);
                setIsLoading(false);
            }
        } catch (error) {
            console.error(error);
            setEventsCount(0);
            setIsLoading(false);
        }
    }

    useEffect(() => {
        const unsubscribe = navigation.addListener('focus', () => {
            // The screen is focused
            // Call any action
            getEventsCount();
        });

        // Return the function to unsubscribe from the event so it gets removed on unmount
        return unsubscribe;
    }, [navigation]);

    return (
        <View style={styles.container}>
            <Text style={styles.titleText}>
                {dayOfWeek}
            </Text>
            <View style={{ flexDirection: 'row' }}>
                <Icon name="tasks" type="font-awesome" size={24} color="skyblue" />
                <Text style={styles.subtitleText}>
                    Công việc
                </Text>

            </View>


            {/* so cong viec */}
            {isLoading ? <ActivityIndicator /> : null}
            {
                <View style={styles.taskContainer}>
                    {eventsCount > 0 ? (
                        <TouchableOpacity style={styles.taskButton} onPress={handlePress}>
                            <Icon style={styles.notifIcon} name="calendar" type="font-awesome" size={20} color="green" />
                            <Text style={styles.taskText}>
                                Bạn có {eventsCount} công việc {eventsCount > 1 ? '' : ''}hôm nay. Nhấn để xem.
                            </Text>
                        </TouchableOpacity>
                    ) : (
                        !isLoading && <Text style={styles.noTasksText}>Chưa có công việc nào</Text>
                    )}
                </View>
            }
            <View style={styles.hrline}></View>

            {/* thong bao */}
            <View style={{ flexDirection: 'row' }}>
                <Icon name="bell" type="font-awesome" size={24} color="skyblue" />
                <Text style={styles.subtitleText}>
                    Thông báo
                </Text>

            </View>
            <View style={styles.notif}>
                {loadingNotif ? <ActivityIndicator size="large" color="lightgreen" /> : (

                    <FlatList
                        showsVerticalScrollIndicator={false}
                        showsHorizontalScrollIndicator={false}
                        data={notifications}
                        keyExtractor={(item, index) => index.toString()}
                        contentContainerStyle={{ paddingBottom: 20 }}
                        initialNumToRender={5}
                        maxToRenderPerBatch={10}
                        windowSize={10}
                        renderItem={({ item }) => <View style={styles.notifButton}>
                            <View>
                                <Text style={styles.taskText}>
                                    {item.message}

                                </Text>
                                <Text style={styles.dateText}>{item.notificationDateTime}</Text>

                            </View>

                        </View>}
                    />
                )}
            </View>


        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 24,
        backgroundColor: '#f5f5f5',
    },
    titleText: {
        fontSize: 20,
        color: '#2282F3',
        marginBottom: 20,
        fontFamily: 'quolibet',
    },
    subtitleText: {
        fontSize: 22,
        fontWeight: 'bold',
        color: '#2282F3',
        marginBottom: 10,
        fontFamily: 'quolibet',
        marginLeft: 10,
    },
    taskButton: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#fff',
        padding: 20,
        borderRadius: 10,
        marginVertical: 10,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.1,
        shadowRadius: 2,
        elevation: 3,
    },
    notifButton: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#fff',
        padding: 20,
        borderRadius: 10,
        marginVertical: 10,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.1,
        shadowRadius: 2,
        elevation: 3,
    },
    taskText: {
        fontSize: 20,
        color: 'gray',
        fontFamily: 'quolibet',
    },
    noTasksText: {
        fontSize: 18,
        color: '#333',
        fontStyle: 'italic',
        textAlign: 'center',
    },
    hrline: {
        borderBottomColor: '#BABABA',
        borderBottomWidth: 1,
        marginVertical: 20,
        width: '90%',
        alignSelf: 'center',
    },
    notifIcon: {
        marginRight: 15,
    },
    notif: {
        backgroundColor: '#f5f5f5'
    },
    dateText: {
        color: '#2282F3',
        fontWeight: 'bold',
        fontSize: 13,
        paddingTop: 8
    }
});