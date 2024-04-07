import { Text, View, Modal, StyleSheet, TouchableOpacity, FlatList, TextInput, ActivityIndicator } from "react-native";
import { LinearGradient } from 'expo-linear-gradient';
import React, { useState, useEffect } from 'react';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Icon } from '@rneui/themed';
import ReportForm from "./reportForm";
import Toast from 'react-native-toast-message';
import { api } from "../shared/api";




// --------------------------------------------------------------------------------------------
export default function Report({ navigation }) {

    // states
    const [reports, setReports] = useState([]);
    const [isLoading, setLoading] = useState(true);
    const [transformedData, setTransformedData] = useState({});
    const [filteredReports, setFilteredReports] = useState([]);

    const [modalOpen, setModalOpen] = useState(false); // modal open/close
    // search
    const [searchInput, setSearchInput] = useState(''); // search input

    // data refresh
    const [data, setData] = useState([]);

    const [emptyReport, setEmptyReport] = useState('');

    const refreshData = async () => {
        await getReports();
        setModalOpen(false);
        Toast.show({
            type: 'success',
            text1: 'Thành công',
            text2: 'Gửi báo cáo thành công!',
            visibilityTime: 3000,
            autoHide: true,
            topOffset: 30,
            bottomOffset: 40,
        });
    };

    // get reports from the server

    const getReports = async () => {
        try {
            var useremail = JSON.parse(await AsyncStorage.getItem("@user"))?.email;
            const atoken = await AsyncStorage.getItem("@accessToken");
            if (atoken !== null) {
                const url = `https://vesinhdanang.xyz:7024/api/Report/GetReportsByUser?email=${useremail}`;

                api.get(url, {
                    headers: {
                        "Content-Type": "application/json",
                        'Authorization': `Bearer ${atoken}`,
                        "Client-Type": "Mobile"
                    },
                })
                    .then((res) => {
                        const jsonReports = res.data.value.map(item => {
                            const report = {
                                ...item.reportFormat,
                            };
                            return report;
                        });
                        setReports(jsonReports);

                        // Transform data here
                        const data = {};
                        jsonReports.forEach(item => {
                            const [date, time] = item.expectedResolutionDate.split("T");
                            const currentDate = date;
                            if (!data[currentDate]) {
                                data[currentDate] = [];
                            }
                            data[currentDate].push({ ...item, date, time: time.split('+')[0], day: currentDate });
                        });
                        setTransformedData(data);
                        setLoading(false);
                        setEmptyReport('Không có báo cáo nào');
                    })
                    .catch((error) => {
                        console.log('There has been a problem with fetch operation: ', error.message);
                        setLoading(false);
                        setEmptyReport('Lỗi kết nối!');
                    });
            } else {
                console.log('token null');
                setLoading(false);
                setEmptyReport('Lỗi kết nối!');
            }
        } catch (error) {
            console.error(error);
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

    const impactColors = {
        0: 'green',
        1: 'orange',
        2: 'red'
    };

    function formatDate(dateString) {
        const months = [
            'Tháng 1', 'Tháng 2', 'Tháng 3', 'Tháng 4', 'Tháng 5', 'Tháng 6',
            'Tháng 7', 'Tháng 8', 'Tháng 9', 'Tháng 10', 'Tháng 11', 'Tháng 12'
        ];
        const date = new Date(dateString);
        return `${date.getDate()} / ${date.getMonth()} / ${date.getFullYear()}`;
    }


    // render reports
    const renderReports = () => {
        if (reports.length === 0) {
            return (
                <View style={styles.emptyContainer}>
                    <Text style={styles.emptyText}>{emptyReport}</Text>
                </View>
            );
        }
        return (
            <View style={{ paddingBottom: 100 }}>
                <FlatList
                    data={filteredReports}
                    keyExtractor={(item) => item.id.toString()}
                    renderItem={({ item }) => (
                        <TouchableOpacity
                            style={[styles.records, item.reportStatus === 'UnResolved' ? styles.unresolved : styles.resolved]}
                            onPress={() => {
                                navigation.navigate('ReportDetails', {
                                    reportId: item.id,
                                    reportBody: item.reportBody,
                                    reportImages: item.reportImages,
                                    reportSubject: item.reportSubject.replace('[Report]', '').trim(),
                                    reportImpact: item.reportImpact,
                                    reportStatus: item.reportStatus,
                                    reportResponse: item.reportResponse,
                                    expectedResolutionDate: formatDate(item.expectedResolutionDate),
                                    actualResolutionDate: item.actualResolutionDate ? formatDate(item.actualResolutionDate) : '...',
                                });
                            }}
                        >
                            <View style={styles.itemContainer}>
                                <Text style={styles.itemLabel} numberOfLines={1} ellipsizeMode='tail'>
                                    {item.reportSubject.replace('[Report]', '').trim()}
                                </Text>
                                <View style={{ flexDirection: 'row' }}>
                                    <Icon style={{ marginRight: 5 }} name="warning" type="Ionicons" size={16} color={impactColors[item.reportImpact]} />
                                    <Icon name="chevron-right" size={18} color="grey" />
                                </View>

                            </View>
                            {item.reportStatus === 'UnResolved' ? (
                                <>
                                    <View style={styles.itemContainer}>
                                        <Text style={styles.itemText}>Ngày cần giải quyết </Text>
                                    </View>
                                    <View style={styles.itemContainer}>
                                        <Text style={styles.itemTextSmall}>{formatDate(item.expectedResolutionDate)}</Text>
                                    </View>
                                </>
                            ) : (
                                <View style={styles.statusContainer}>
                                    <View style={styles.resolvedContainer}>
                                        <Text style={styles.resolvedText}>Đã xử lý</Text>
                                        <Icon name="check" size={20} color="green" />

                                        {/* <Text style={styles.dateText}>{formatDate(item.actualResolutionDate)}</Text> */}

                                    </View>
                                </View>
                            )}
                        </TouchableOpacity>
                    )}
                />
            </View>
        );

    }

    // --------------------------------------------------------------------------------------------
    return (
        <View style={{ flex: 1, }}>
            <LinearGradient
                colors={['rgba(197, 252, 234, 0.5)', 'rgba(255, 255, 255, 0.6)']}
                start={{ x: 0, y: 0 }}
                end={{ x: 0, y: 1 }}
                style={StyleSheet.absoluteFill}
            >
                <View style={styles.searchContainer}>

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
                    <View style={{
                        position: 'absolute',
                        top: '50%',
                        left: 0,
                        right: 0,
                        justifyContent: 'center',
                        alignItems: 'center'
                    }}>
                        <ActivityIndicator size="large" color="lightgreen" />
                    </View>
                ) : (
                    renderReports()
                )}

            </LinearGradient>
            <TouchableOpacity
                style={styles.fab}
                onPress={() => {
                    setModalOpen(true);
                }}
            >
                <Icon name="add" size={30} color="#FFF" />
            </TouchableOpacity>

            {/* modal */}
            <Modal visible={modalOpen} animationType='slide'>
                <View style={{
                    flexDirection: 'row',
                    justifyContent: 'space-between',
                    paddingHorizontal: 15,
                    backgroundColor: '#F6F6F6',
                    paddingVertical: 15,
                }}>
                    <TouchableOpacity
                        onPress={() => {
                            setModalOpen(false);
                        }}
                    >
                        <Icon name="remove" type="font-awesome" size={24} color="#000" />
                    </TouchableOpacity>
                    <Text style={styles.modalHeaderText}>Báo cáo vấn đề</Text>

                    <View>
                    </View>


                </View>
                <ReportForm onFormSuccess={refreshData} />

            </Modal>
            <Toast />
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
        fontSize: 18,
        fontFamily: 'nunito-regular',
        fontWeight: 'bold',
        flex: 0.75
    },
    itemTextSmall: {
        fontSize: 14,
        fontWeight: '500',
        color: 'grey'
    },
    itemText: {
        fontSize: 13,
        flex: 0.75,
        color: 'gray',
        letterSpacing: 1,
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
    modalHeaderText: {
        fontSize: 20,
        fontWeight: 'bold',
        color: '#333',
    },
    unresolved: {
        backgroundColor: 'pink',
    },
    resolved: {
        backgroundColor: '#E2FFE3',
    },
    resolvedText: {
        fontSize: 16,
        color: 'green',
        fontWeight: 'bold',
        paddingRight: 5,
    },
    statusContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        borderRadius: 5,
        paddingVertical: 5,
        marginBottom: 10,
    },
    resolvedContainer: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
    },
    dateText: {
        fontSize: 16,
        color: '#333',
        paddingLeft: 5,
    },
})
