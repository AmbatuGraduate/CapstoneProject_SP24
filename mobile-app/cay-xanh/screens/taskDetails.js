import React, { useState } from "react";
import { StyleSheet, View, Text, ScrollView, TouchableOpacity, Modal, ActivityIndicator } from 'react-native';
import AsyncStorage from "@react-native-async-storage/async-storage";
import Toast from 'react-native-toast-message';
import { Icon } from '@rneui/themed';
import { api } from "../shared/api";



/*************************************************************
**_________________ TASK DETAILS SCREEN ____________________**
**                  CREATED BY: LE ANH QUAN                 **

*************************************************************/

export default function TaskDetails({ route }) {

    const [updatedStatus, setUpdatedStatus] = useState(route.params.status); // Create a state variable for the status

    const { key, summary, description, address, start, status, trees } = route.params;

    const [loading, setLoading] = useState(false);

    // ----------------- Update task status -----------------

    const updateStatus = async () => {
        console.log('Updating status...');
        setLoading(true);
        try {
            var department = JSON.parse(await AsyncStorage.getItem("@user"))?.department;

            var calendarId;
            if (department.toString().toLowerCase().includes('cay xanh')) {
                calendarId = 1;
            } else if (department.toString().toLowerCase().includes('ve sinh')) {
                calendarId = 2;
            } else if (department.toString().toLowerCase().includes('quet don')) {
                calendarId = 3;
            }
            AsyncStorage.getItem("@accessToken").then(token => {
                const url = `https://vesinhdanang.xyz:7024/api/Calendar/UpdateJobWorkingStatus?calendarTypeEnum=${calendarId}`;

                api.post(url, {
                    accessToken: "",
                    calendarId: "",
                    jobWorkingStatus: 2,
                    eventId: key
                }, {
                    headers: {
                        Accept: 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`,
                        "Client-Type": "Mobile"
                    }
                })
                    .then(response => {
                        console.log(response.data);
                        if (!response.data.isError) {
                            setLoading(false);
                            Toast.show({
                                type: 'success',
                                text1: 'Thành công',
                                text2: 'Đã cập nhật trạng thái công việc',
                                visibilityTime: 3000,
                                autoHide: true,
                                topOffset: 30,
                                bottomOffset: 40,
                            });
                            setUpdatedStatus('Done');

                        }
                    })
                    .catch(error => {
                        console.error(error);
                        setLoading(false);
                        Toast.show({
                            type: 'error',
                            text1: 'Lỗi xảy ra',
                            text2: 'Vui lòng thử lại sau',
                            visibilityTime: 3000,
                            autoHide: true,
                            topOffset: 30,
                            bottomOffset: 40,
                        });
                    });
            });
        } catch (error) {
            console.error(error);
            setLoading(false);
        }
    }


    // ----------------- GET DATA FROM PREVIOUS SCREEN -----------------

    let treeArray = trees ? trees.split(",") : [];
    treeArray = treeArray.filter(item => item);

    const dateObject = new Date(start);
    const monthNames = ["01", "02", "03", "04", "05", "06",
        "07", "08", "09", "10", "11", "12"
    ];
    const formattedDate = `${dateObject.getDate()} - ${monthNames[dateObject.getMonth()]} - ${dateObject.getFullYear()}`;

    const getStatusColor = (status) => {
        switch (status) {
            case 'Not Start':
                return '#F29B9B';
            case 'In Progress':
                return '#FDFA72';
            case 'Done':
                return '#E2FFE3';
            case 'Late':
                return '#F7AD86';
            default:
                return '#E8E8E8';
        }
    }

    const taskStatuses = {
        'Done': 'Đã hoàn thành',
        'Late': 'Quá hạn',
        'In Progress': 'Đang thực hiện',
        'Not Start': 'Chưa bắt đầu',
    };

    const getStatusTextColor = (status) => {
        switch (status) {
            case 'Not Start':
                return '#840808';
            case 'In Progress':
                return 'darkblue';
            case 'Done':
                return 'green';
            case 'Late':
                return '#F76400';
            default:
                return 'black';
        }
    }

    return (
        <View style={{ flex: 1 }}>
            <ScrollView contentContainerStyle={styles.content}>

                <View style={styles.content}>
                    {/* ANH */}
                    <Text style={styles.subject}>{summary}</Text>
                    <Text style={styles.dateText}>{formattedDate}</Text>

                    <View style={styles.detailsContainer}>


                        <View style={{ flexDirection: 'row', justifyContent: 'flex-start' }}>
                            <Icon name="leaf" type="font-awesome" size={20} color="green" />
                            <Text style={styles.nameText}>
                                {treeArray && treeArray.length > 0 ? 'Cắt tỉa cây có mã số' : 'Chi tiết công việc'}
                            </Text>
                        </View>
                        {treeArray.map((item, index) => (
                            <Text key={index} style={styles.infoText}>{item.trim()}</Text>
                        ))}


                    </View>

                    {/* DIA CHI */}
                    <View style={styles.detailsContainer}>
                        <View style={{ flexDirection: 'row', justifyContent: 'flex-start' }}>
                            <Icon name="map-marker" type="font-awesome" size={20} color="green" />
                            <Text style={styles.nameText}>Địa chỉ</Text>
                        </View>


                        <Text style={styles.infoText}>{address}</Text>
                    </View>

                    {/* Ghi chu */}
                    <View style={styles.detailsContainer}>

                        <View style={{ flexDirection: 'row', justifyContent: 'flex-start' }}>
                            <Icon name="sticky-note" type="font-awesome" size={20} color="green" />
                            <Text style={styles.nameText}>Ghi chú</Text>
                        </View>


                        <Text style={styles.infoText}>{description}</Text>
                    </View>

                    {/* TRANG THAI */}
                    <View style={[styles.detailsContainer, { backgroundColor: getStatusColor(updatedStatus) }]}>
                        <Text style={styles.nameText}>Trạng thái</Text>
                        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                            <Text style={[styles.statusText, { color: getStatusTextColor(updatedStatus) }]}>{taskStatuses[updatedStatus]}</Text>
                            {updatedStatus === 'Done' && <Icon name='check' type='font-awesome' color='green' />}
                        </View>
                    </View>
                </View>
                <Toast />

            </ScrollView>
            {
                updatedStatus !== 'Done' &&

                <TouchableOpacity style={styles.submitButton} onPress={() => updateStatus()}>
                    <Icon name="check" size={20} color="#fff" />
                    <Text style={styles.submitButtonText}>Hoàn thành</Text>
                </TouchableOpacity>
            }

            <Modal
                transparent={true}
                visible={loading}
            >
                <View style={{
                    flex: 1,
                    backgroundColor: 'rgba(202,247,183,0.5)',
                    justifyContent: 'center',
                    alignItems: 'center'
                }}>
                    <ActivityIndicator size="large" color="green" />
                </View>
            </Modal>
        </View>

    );
}

const styles = StyleSheet.create({
    subject: {
        fontSize: 20,
        fontFamily: 'quolibet',
        fontWeight: 'bold',
        marginBottom: 10,
        color: '#2282F3',
        textAlign: 'center',
    },
    dateText: {
        fontSize: 20,
        fontFamily: 'quolibet',
        color: '#333',
        textAlign: 'center',
        marginBottom: 10,
    },
    content: {
        padding: 10,
    },
    imageContainer: {
        backgroundColor: 'white',
        padding: 20,
        marginBottom: 12,
        borderRadius: 10,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.3,
        shadowRadius: 2,
        elevation: 5,
    },
    img: {
        width: '100%',
        height: 200,

    },
    detailsContainer: {
        marginTop: 20,
        padding: 4,
        paddingHorizontal: 20,
        marginBottom: 5,
        backgroundColor: 'white',
        borderRadius: 10,

    },
    nameText: {
        color: '#2282F3',
        fontSize: 18,
        fontFamily: 'quolibet',
        fontWeight: '700',
        borderBottomWidth: 1,
        borderBottomColor: '#eee',
        paddingBottom: 10,
        marginLeft: 5,
    },
    infoText: {
        fontSize: 14,
        letterSpacing: 1.5,
        color: '#333',
        marginVertical: 10,
        fontWeight: 'bold',
    },
    statusText: {
        fontSize: 16,
        letterSpacing: 1.5,
        marginVertical: 10,
        fontWeight: 'bold',
    },
    submitButton: {
        width: '50%',
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'lightgreen',
        padding: 15,
        borderRadius: 15,
        marginBottom: 20,
        alignSelf: 'center'
    },
    submitButtonText: {
        color: 'whitesmoke',
        marginLeft: 10,
        fontSize: 18,
        fontWeight: 'bold',

    },
});