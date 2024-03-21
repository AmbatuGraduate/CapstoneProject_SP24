import { Text, View, Modal, StyleSheet, TouchableOpacity, FlatList } from "react-native";
import { LinearGradient } from 'expo-linear-gradient';
import React, { useState, useEffect } from 'react';

import AsyncStorage from '@react-native-async-storage/async-storage';


// --------------------------------------------------------------------------------------------
export default function Report({ navigation }) {

    const [reports, setReports] = useState([]);
    const [isLoading, setLoading] = useState(true);


    // get reports from the server
    const getReports = async () => {
        try {
            AsyncStorage.getItem("@accessToken").then(atoken => {
                if (atoken !== null) {
                    fetch('http://192.168.1.7:45455/api/Report/GetReportFormats?accessToken=' + atoken,
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

    // render reports
    const renderReports = () => {
        return (
            <LinearGradient
                colors={['rgba(197, 252, 234, 0.5)', 'rgba(255, 255, 255, 0.6)']}
                start={{ x: 0, y: 0 }}
                end={{ x: 0, y: 1 }}
            >
                <FlatList
                    data={reports}
                    keyExtractor={(item) => item.id.toString()}
                    renderItem={({ item }) => (
                        <TouchableOpacity
                            style={styles.records}
                            onPress={() => {
                                navigation.navigate('ReportDetails', {
                                    reportId: item.id,
                                });
                            }}
                        >
                            <View style={styles.itemContainer}>
                                <Text style={styles.itemLabel}>{item.id}</Text>
                            </View>
                            <View style={styles.itemContainer}>
                                <Text style={styles.itemText}>{item.reportSubject}</Text>
                            </View>
                            <View style={styles.itemContainer}>
                                <Text style={styles.itemText}>{item.reportBody}</Text>
                            </View>
                            <View style={styles.itemContainer}>
                                <Text style={styles.itemText}>{item.reportStatus}</Text>
                            </View>
                        </TouchableOpacity>
                    )}
                />
            </LinearGradient>
        );
    }

    // --------------------------------------------------------------------------------------------
    return (
        <View style={{ flex: 1 }}>
            {isLoading ? (
                <Text>Loading...</Text>
            ) : (
                renderReports()
            )}
        </View>
    );
}

const styles = StyleSheet.create({
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
    },
    itemLabel: {
        fontSize: 20,
        fontFamily: 'nunito-regular',
        fontWeight: 'bold',
        letterSpacing: 1,
    },
    itemText: {
        fontSize: 16,
        flex: 0.75,
    },
})
