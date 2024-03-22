import { Text, View, Modal, StyleSheet, TouchableOpacity, FlatList, TextInput } from "react-native";
import { LinearGradient } from 'expo-linear-gradient';
import React, { useState, useEffect } from 'react';

import AsyncStorage from '@react-native-async-storage/async-storage';
import { Icon } from '@rneui/themed';


// --------------------------------------------------------------------------------------------
export default function Report({ navigation }) {

    const [reports, setReports] = useState([]);
    const [isLoading, setLoading] = useState(true);
    const [transformedData, setTransformedData] = useState({});
    const [filteredReports, setFilteredReports] = useState([]);

    // search
    const [searchInput, setSearchInput] = useState(''); // search input

    // get reports from the server
    const getReports = async () => {
        try {
            var useremail = JSON.parse(await AsyncStorage.getItem("@user"))?.email;
            AsyncStorage.getItem("@accessToken").then(atoken => {
                if (atoken !== null) {
                    fetch('http://192.168.1.7:45455/api/Report/GetReportsByUser?accessToken=' + atoken + '&email=' + useremail,
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
                            const jsonReports = json.value.map(item => {
                                const report = {
                                    ...item.reportFormat,
                                };
                                return report;
                            });
                            setReports(jsonReports);
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

    useEffect(() => {
        const unsubscribe = navigation.addListener('focus', () => {
            // The screen is focused
            // Call any action
            getReports();
        });

        // Return the function to unsubscribe from the event so it gets removed on unmount
        return unsubscribe;
    }, [navigation]);

    useEffect(() => {
        if (Array.isArray(reports)) {
            const data = {};
            reports.forEach(item => {
                const [date, time] = item.expectedResolutionDate.split("T");
                const currentDate = date;
                if (!data[currentDate]) {
                    data[currentDate] = [];
                }
                data[currentDate].push({ ...item, date, time: time.split('+')[0], day: currentDate });
            });
            setTransformedData(data);
        }
    }, [reports]);


    // search
    useEffect(() => {
        if (searchInput === '') {
            setFilteredReports(reports);
        } else {
            setFilteredReports(reports.filter(report => report.reportSubject.toLowerCase().includes(searchInput.toLowerCase())));
        }
    }, [searchInput, reports]);

    const impactLevels = {
        0: 'Low',
        1: 'Medium',
        2: 'High'
    };
    const impactColors = {
        0: '#ADF35B',
        1: 'orange',
        2: 'red'
    };

    function formatDate(dateString) {
        const months = [
            'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
            'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
        ];
        const date = new Date(dateString);
        return `${date.getDate()} ${months[date.getMonth()]} năm ${date.getFullYear()}`;
    }


    // render reports
    const renderReports = () => {
        if (reports.length === 0) {
            return (
                <View style={styles.emptyContainer}>
                    <Text style={styles.emptyText}>Chưa có báo cáo nào</Text>
                </View>
            );
        }
        return (

            <FlatList
                data={filteredReports}
                keyExtractor={(item) => item.id.toString()}
                renderItem={({ item }) => (
                    <TouchableOpacity
                        style={styles.records}
                        onPress={() => {
                            navigation.navigate('ReportDetails', {
                                reportId: item.id,
                                reportBody: item.reportBody,
                                reportSubject: item.reportSubject,
                                reportImpact: item.reportImpact,
                                reportStatus: item.reportStatus,
                                reportResponse: item.reportResponse,
                                expectedResolutionDate: formatDate(item.expectedResolutionDate),
                            });
                        }}
                    >
                        <View style={styles.itemContainer}>
                            <Text style={styles.itemLabel} numberOfLines={1} ellipsizeMode='tail'>
                                {item.reportSubject}
                            </Text>
                            <View style={{ flexDirection: 'row' }}>
                                <Icon style={{ marginRight: 5 }} name="warning" type="Ionicons" size={16} color={impactColors[item.reportImpact]} />
                                <Icon name="chevron-right" size={18} color="grey" />
                            </View>

                        </View>
                        <View style={styles.itemContainer}>
                            <Text style={styles.itemTextSmall}>{item.reportStatus}</Text>
                        </View>
                        <View style={styles.itemContainer}>
                            <Text style={styles.itemText}>Cần giải quyết trước: </Text>
                        </View>
                        <View style={styles.itemContainer}>
                            <Text style={styles.itemTextSmall}>{formatDate(item.expectedResolutionDate)}</Text>
                        </View>
                    </TouchableOpacity>
                )}
            />


        );
    }

    // --------------------------------------------------------------------------------------------
    return (
        <View style={{ flex: 1 }}>
            <LinearGradient
                colors={['rgba(197, 252, 234, 0.5)', 'rgba(255, 255, 255, 0.6)']}
                start={{ x: 0, y: 0 }}
                end={{ x: 0, y: 1 }}
            >
                <View style={styles.searchContainer}>
                    {/* filter button */}
                    <TouchableOpacity
                        style={styles.filterButton}
                        onPress={() => {
                            // handle press
                        }}
                    >
                        <Icon name="filter" type="font-awesome" size={24} color="#333" />
                    </TouchableOpacity>

                    {/* search input */}
                    <View style={styles.searchInput}>
                        <Icon name="search" size={20} color="grey" />
                        <TextInput
                            style={styles.input}
                            placeholder="Tìm kiếm..."
                            onChangeText={text => setSearchInput(text)}
                        />
                    </View>
                </View>
                {isLoading ? (
                    <Text>Loading...</Text>
                ) : (

                    renderReports()
                )}

            </LinearGradient>
            <TouchableOpacity
                style={styles.fab}
                onPress={() => {
                    // handle press
                }}
            >
                <Icon name="add" size={30} color="#FFF" />
            </TouchableOpacity>
        </View>

    );
}

const styles = StyleSheet.create({
    searchContainer: {
        width: '94%',
        alignSelf: 'center',
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        marginVertical: 10,
        paddingHorizontal: 10,

    },
    filterButton: {
        marginRight: 5,
        borderRadius: 8,
        width: 32,
        height: 32,
        alignItems: 'center',
        justifyContent: 'center',
    },
    searchInput: {
        flex: 1,
        flexDirection: 'row',
        alignItems: 'center',
        paddingLeft: 10,
        backgroundColor: '#FFF',
        borderRadius: 30,
    },
    input: {
        flex: 1,
        marginLeft: 10,
    },
    fab: {
        position: 'absolute',
        width: 56,
        height: 56,
        alignItems: 'center',
        justifyContent: 'center',
        right: 20,
        bottom: 20,
        backgroundColor: '#03A9F4',
        borderRadius: 30,
        elevation: 8
    },
    modelContent: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'white',
        padding: 40,
        margin: 20
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
        flex: 0.75
    },
    itemTextSmall: {
        fontSize: 14,
    },
    itemText: {
        fontSize: 13,
        flex: 0.75,
        color: 'gray'
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
})
