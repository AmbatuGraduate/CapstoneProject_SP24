import React, { useState } from "react";
import { StyleSheet, View, Text, Image, ScrollView } from 'react-native';
import FlatButton from "../shared/button";
import AsyncStorage from "@react-native-async-storage/async-storage";
import Toast from 'react-native-toast-message';
import { Icon } from '@rneui/themed';
import { LinearGradient } from 'expo-linear-gradient';


/*************************************************************
**_________________ TASK DETAILS SCREEN ____________________**
**                  CREATED BY: LE ANH QUAN                 **

*************************************************************/

export default function TaskDetails({ route }) {

    const [updatedStatus, setUpdatedStatus] = useState(route.params.status); // Create a state variable for the status

    // ----------------- Update task status -----------------
    const updateStatus = () => {
        try {
            AsyncStorage.getItem("@accessToken").then(token => {
                // local test: http://vesinhdanang.xyz/AmbatuGraduate_API/
                // server: https://192.168.1.7:45455/
                const url = new URL('http://vesinhdanang.xyz/api/Calendar/UpdateJobWorkingStatus');
                url.searchParams.append('token', token);
                url.searchParams.append('jobWorkingStatus', 2); // 2 corresponds to 'Done' in enum
                url.searchParams.append('eventId', key);

                fetch(url, {
                    method: 'POST',
                    headers: {
                        Accept: 'application/json',
                        'Content-Type': 'application/json',
                        'Authorization': `Bearer ${token}`
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
                        console.error(error);
                    });
            });
        } catch (error) {
            console.error(error);
        }
    }


    // ----------------- GET DATA FROM PREVIOUS SCREEN -----------------
    const { key, description, address, start, status, img } = route.params;
    const dateObject = new Date(start);
    const monthNames = ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6",
        "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"
    ];
    const formattedDate = `${dateObject.getDate()} / ${monthNames[dateObject.getMonth()]} / ${dateObject.getFullYear()}`;

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
        <LinearGradient
            colors={['rgba(197, 252, 234, 0.5)', 'lightgray']}
            start={{ x: 0, y: 0 }}
            end={{ x: 0, y: 1 }}
            style={styles.content}
        >
            <ScrollView contentContainerStyle={styles.content}>

                <View style={styles.content}>
                    {/* ANH */}
                    <View style={styles.imageContainer}>
                        <Image
                            style={styles.img}
                            source={{ uri: img }}
                        />
                    </View>
                    {/* DIA CHI */}
                    <View style={styles.detailsContainer}>
                        <Text style={styles.nameText}>Địa chỉ</Text>

                        <Text style={styles.infoText}>{address}</Text>
                    </View>

                    {/* THONG TIN CHI TIET */}
                    <View style={styles.detailsContainer}>
                        <Text style={styles.nameText}>Thông tin chi tiết</Text>
                        <Text style={styles.infoText}>{description}</Text>
                    </View>

                    {/* THONG TIN CHI TIET */}
                    <View style={styles.detailsContainer}>
                        <Text style={styles.nameText}>Thời gian</Text>
                        <Text style={styles.infoText}>{formattedDate}</Text>
                    </View>

                    {/* TRANG THAI */}
                    <View style={[styles.detailsContainer, { backgroundColor: getStatusColor(updatedStatus) }]}>
                        <Text style={styles.nameText}>Trạng thái</Text>
                        <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                            <Text style={[styles.statusText, { color: getStatusTextColor(updatedStatus) }]}>{updatedStatus}</Text>
                            {updatedStatus === 'Done' && <Icon name='check' type='font-awesome' color='green' />}
                        </View>
                    </View>


                    <FlatButton style={{
                        bottom: 10,
                        left: 0,
                        right: 0
                    }} text='Hoàn thành' iconName="check" onPress={() => updateStatus()}></FlatButton>

                </View>
                <Toast />

            </ScrollView>

        </LinearGradient>

    );
}

const styles = StyleSheet.create({
    content: {
        padding: 10,
    },
    imageContainer: {
        margin: 15,
        marginBottom: 30,
        overflow: 'hidden',
        shadowColor: "#aaa",
        shadowOffset: {
            width: 0,
            height: 6,
        },
        shadowOpacity: 0.37,
        shadowRadius: 7.49,
        elevation: 12,
        backgroundColor: 'white',
        borderRadius: 15
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
        backgroundColor: 'whitesmoke',
        borderRadius: 10,
        borderWidth: 1,
        borderColor: 'skyblue',
    },
    nameText: {
        fontSize: 18,
        color: 'green',
        textTransform: 'uppercase',
        fontFamily: 'nunito-bold'
    },
    infoText: {
        fontSize: 16,
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