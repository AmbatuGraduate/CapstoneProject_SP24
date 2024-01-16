import React, { useState, useEffect } from "react";
import { StyleSheet, View, Text, Dimensions, TouchableOpacity } from 'react-native';
import { Agenda } from "react-native-calendars";
import moment from "moment";

export default function TasksList({ navigation }) {
    const [trees, setTrees] = useState([]);
    const [isLoading, setLoading] = useState(true);
    const [transformedData, setTransformedData] = useState({});

    //set selected month and year
    const [date, setDate] = useState(moment().format("MMMM YYYY"));

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
        trees.forEach(item => {
            const currentDate = item.plantTime.split("T")[0];
            if (!data[currentDate]) {
                data[currentDate] = [];
            }
            data[currentDate].push({ ...item, day: currentDate });
        });
        setTransformedData(data);
    }, [trees]);

    return (
        <View>
            {isLoading ? (
                <Text>Loading...</Text>
            ) : (
                <View style={
                    { height: 600 }
                }>
                    <View style={styles.dateViewStyle}>
                        <TouchableOpacity
                            style={styles.dateButtonStyle}
                        //onPress = {() => openCalendar ? setOpenCalendar(false) : setOpenCalendar(true) }
                        >
                            <Text style={styles.dateStyle}>{date}</Text>
                        </TouchableOpacity>
                    </View>
                    <Agenda
                        renderEmptyDate={() => {
                            return (
                                <View>
                                    <Text>TRống</Text>
                                </View>
                            )
                        }}
                        scrollEnabled={false}
                        pastScrollRange={12}
                        futureScrollRange={12}
                        items={transformedData}
                        markingType={"multi-dot"}
                        renderItem={(item) => (
                            <TouchableOpacity style={styles.records} onPress={() => {
                                navigation.navigate('TaskDetails', {
                                    key: item.id,
                                    name: item.type,
                                    img: 'https://www.canhquan.net/Content/Images/FileUpload/2018/2/p1030345_500_03%20(1)-1.jpg'
                                })
                            }}>
                                <Text style={styles.itemText}>Loai Cay: {item.type}</Text>
                                <Text style={styles.itemText}>Dia chi: {item.street}</Text>

                            </TouchableOpacity>
                        )}

                    />

                </View>
            )}
        </View>
    );
}

const styles = StyleSheet.create({
    dateViewStyle: {
        flexDirection: "row",
        backgroundColor: 'white',
        justifyContent: "center",
        height: "auto"
    },
    records: {
        backgroundColor: 'white',
        flex: 1,
        borderRadius: 5,
        padding: 10,
        marginRight: 10,
        marginTop: 17,
    },
    dateStyle: {
        color: "#2079B3",
        fontSize: 18,
        marginTop: 5
    },
    itemText: {
        fontSize: 16,
        marginBottom: 5,
    },
});