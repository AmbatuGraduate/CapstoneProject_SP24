import React, { useState, useEffect } from "react";
import { StyleSheet, View, Text, TouchableOpacity, FlatList } from 'react-native';
import { Calendar } from "react-native-calendars";
import moment from "moment";

export default function TasksList({ navigation }) {
    const [trees, setTrees] = useState([]);
    const [isLoading, setLoading] = useState(true);
    const [selectedDate, setSelectedDate] = useState(moment().format("YYYY-MM-DD"));
    const [transformedData, setTransformedData] = useState({});
    const [marked, setMarked] = useState({}); // marked date
    const [pressedDate, setPressedDate] = useState(null);


    const getTrees = async () => {
        try {
            const response = await fetch('http://192.168.1.20:45455/api/Tree');
            const json = await response.json();
            setTrees(json);
        } catch (error) {
            console.error(error);
        } finally {
            setLoading(false);
        }
    }

    // Trees list not change when add new data, only change if  reload
    useEffect(() => {
        getTrees();
    }, []);

    useEffect(() => {
        const data = {};
        const markedDates = {};

        trees.forEach(item => {
            const currentDate = item.plantTime.split("T")[0];
            if (!data[currentDate]) {
                data[currentDate] = [];
            }
            data[currentDate].push({ ...item, day: currentDate });
            markedDates[currentDate] = { marked: true };
        });
        setTransformedData(data);
        setMarked(markedDates);
    }, [trees]);

    // render marked date for dates that have tasks
    const renderItemsForSelectedDate = () => {
        const items = transformedData[selectedDate] || [];

        return (
            <FlatList
                data={items}
                keyExtractor={(item) => item.id.toString()}
                renderItem={({ item }) => (
                    <TouchableOpacity
                        style={styles.records}
                        onPress={() => {
                            navigation.navigate('TaskDetails', {
                                key: item.id,
                                name: item.type,
                                img: 'https://www.canhquan.net/Content/Images/FileUpload/2018/2/p1030345_500_03%20(1)-1.jpg'
                            });
                        }}
                    >
                        <Text style={styles.itemText}>Loai Cay: {item.type}</Text>
                        <Text style={styles.itemText}>Dia chi: {item.street}</Text>
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
    itemText: {
        fontSize: 16,
        marginBottom: 5,
    },
});