import React, { useState } from "react";
import { StyleSheet, View, Text, ScrollView } from 'react-native';
import FlatButton from "../shared/button";
import AsyncStorage from "@react-native-async-storage/async-storage";
import Toast from 'react-native-toast-message';
import { Icon } from '@rneui/themed';


/*************************************************************
**_________________ TASK DETAILS SCREEN ____________________**
**                  CREATED BY: LE ANH QUAN                 **

*************************************************************/

export default function TaskDetails({ route }) {

    const [updatedStatus, setUpdatedStatus] = useState(route.params.status); // Create a state variable for the status

    const { key, summary, description, address, start, status, trees } = route.params;

    // ----------------- Update task status -----------------
    const updateStatus = () => {
        try {
            AsyncStorage.getItem("@accessToken").then(token => {
                // local test: http://vesinhdanang.xyz/AmbatuGraduate_API/
                // server: https://192.168.1.7:45455/
                const url = new URL('https://vesinhdanang.xyz:7024/api/Calendar/UpdateJobWorkingStatus');
                url.searchParams.append('jobWorkingStatus', 2); // 2 corresponds to 'Done' in enum
                url.searchParams.append('eventId', key);

                fetch(url, {
                    method: 'POST',
                    headers: {
                        Accept: 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`,
                        "Client-Type": "Mobile"
                    },
                })
                    .then(response => response.json())
                    .then(responseJson => {
                        console.log(responseJson);
                        if (!responseJson.isError) {
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
                        Toast.show({
                            type: 'error',
                            text1: 'Lỗi xảy ra',
                            text2: 'Vui lòng thử lại sau',
                            visibilityTime: 3000,
                            autoHide: true,
                            topOffset: 30,
                            bottomOffset: 40,
                        });
                        setUpdatedStatus('Done');
                    });
            });
        } catch (error) {
            console.error(error);
        }
    }


    // ----------------- GET DATA FROM PREVIOUS SCREEN -----------------

    let treeArray = trees.split(",");
    treeArray = treeArray.filter(item => item);

    const dateObject = new Date(start);
    const monthNames = ["01", "02", "03", "04", "05", "06",
        "07", "08", "09", "10", "11", "12"
    ];
    const formattedDate = `${dateObject.getDate()} - ${monthNames[dateObject.getMonth()]} - ${dateObject.getFullYear()}`;

    const getStatusColor = (status) => {
        switch (status) {
            case 'Not Started':
                return '#FFD1DF';
            case 'In Progress':
                return '#FDFA72';
            case 'Done':
                return '#E2FFE3';
            default:
                return '#E8E8E8';
        }
    }

    const getStatusTextColor = (status) => {
        switch (status) {
            case 'Not Started':
                return 'coral';
            case 'In Progress':
                return 'darkblue';
            case 'Done':
                return 'green';
            default:
                return 'black';
        }
    }

    return (
        <View style={{ flex: 1 }}>
            <ScrollView contentContainerStyle={styles.content}>

                <View style={styles.content}>
                    {/* ANH */}

                    <View style={styles.detailsContainer}>
                        <Text style={styles.subject}>{summary}</Text>
                        {treeArray.map((item, index) => (
                            <Text key={index} style={styles.infoText}>{item}</Text>
                        ))}
                        <Text style={styles.infoText}>{formattedDate}</Text>

                    </View>

                    {/* DIA CHI */}
                    <View style={styles.detailsContainer}>
                        <Text style={styles.nameText}>Địa chỉ</Text>

                        <Text style={styles.infoText}>{address}</Text>
                    </View>

                    {/* Ghi chu */}
                    <View style={styles.detailsContainer}>
                        <Text style={styles.nameText}>Ghi chú</Text>

                        <Text style={styles.infoText}>{description}</Text>
                    </View>

                    {/* TRANG THAI */}
                    <View style={[styles.detailsContainer, { backgroundColor: getStatusColor(updatedStatus) }]}>
                        <Text style={styles.nameText}>Trạng thái</Text>
                        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                            <Text style={[styles.statusText, { color: getStatusTextColor(updatedStatus) }]}>{updatedStatus}</Text>
                            {updatedStatus === 'Done' && <Icon name='check' type='font-awesome' color='green' />}
                        </View>
                    </View>
                </View>
                <Toast />

            </ScrollView>
            {
                updatedStatus !== 'Done' &&
                <FlatButton style={{
                    position: 'absolute',
                    bottom: 0,
                    width: '100%',
                    backgroundColor: '#2282F3',
                    borderRadius: 0,
                }} text='Hoàn thành' iconName="check" onPress={() => updateStatus()}></FlatButton>
            }

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
        marginTop: 10,
        padding: 4,
        paddingHorizontal: 20,
        marginBottom: 5,
        backgroundColor: 'white',
        borderRadius: 10,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.3,
        shadowRadius: 2,
        elevation: 5,
    },
    nameText: {
        color: '#2282F3',
        fontSize: 18,
        fontFamily: 'quolibet',
        fontWeight: '700',
        borderBottomWidth: 1,
        borderBottomColor: '#eee',
        paddingBottom: 10,
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
});