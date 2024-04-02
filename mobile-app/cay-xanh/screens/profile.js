import React, { useState, useEffect } from "react";
import { View, Text, Image, StyleSheet, Dimensions, TouchableHighlight, TouchableOpacity, Animated, Easing } from 'react-native';
import { Icon } from '@rneui/themed';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { LinearGradient } from 'expo-linear-gradient';
import { api } from "../shared/api";

/*************************************************************
**_________________ PROFILE SCREEN OF APP __________________**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/

export default function Profile({ navigation }) {

    const [user, setUser] = useState(null);

    // check if user is logged in
    useEffect(() => {
        AsyncStorage.getItem("@user").then(async (userString) => {
            if (userString !== null) {
                let user = JSON.parse(userString);
                var email = user?.email;
                var token = user?.token; // get token from user object
                try {
                    const res = await api.get(`https://vesinhdanang.xyz:7024/api/User/GetGoogleUser?email=${email}`, {
                        headers: {
                            "Content-Type": "application/json",
                            "Authorization": `Bearer ${token}`,
                            "Client-Type": "Mobile"
                        },
                    });

                    // append user department to user object
                    user = {
                        ...user,
                        department: res.data.department,
                        phoneNumber: res.data.phoneNumber,
                        role: res.data.role
                    };
                    setUser(user);
                    await AsyncStorage.setItem("@user", JSON.stringify(user)); // update user data in AsyncStorage
                } catch (error) {
                    console.log('There has been a problem with fetch operation: ', error.message);
                }
            }
        });
    }, []);

    return (
        <LinearGradient
            colors={['rgba(255, 255, 255, 0.6)', 'rgba(197, 252, 234, 0.5)']}
            start={{ x: 0, y: 0 }}
            end={{ x: 0, y: 1 }}
            style={styles.container}
        >

            {/* ------------------------------------------------------------------- */}
            {/* PROFILE PICTURE Container*/}
            <View style={{ padding: 20 }}>
                <TouchableHighlight style={styles.imageContainer} onPress={() => alert('Yaay!')}>
                    {user ? (
                        <Image
                            style={styles.img}
                            source={{ uri: user.picture }}
                        />
                    ) : (
                        <View></View>
                    )}
                </TouchableHighlight>

                {/* ------------------------------------------------------------------- */}
                {/* NAME Container*/}
                <View style={styles.nameContainer}>
                    <Text style={{ fontWeight: 'bold', fontSize: 24 }}>{user?.name}</Text>
                </View>

            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Details info Container*/}
            <View style={styles.infoContainer}>
                <View style={styles.infoSection}>
                    <Text style={styles.Label}>Email</Text>
                    <Text style={styles.info} numberOfLines={1}>{user?.email}</Text>
                </View>
                <View style={styles.infoSection}>
                    <Text style={styles.Label}>Bộ phận</Text>
                    <Text style={styles.info}>{user?.department}</Text>
                </View>
                <View style={styles.infoSection}>
                    <Text style={styles.Label}>Chức vụ</Text>
                    <Text style={styles.info}>{user?.role}</Text>
                </View>
                <View style={styles.infoSection}>
                    <Text style={styles.Label}>Số điện thoại</Text>
                    <Text style={styles.info}>{user?.phoneNumber}</Text>
                </View>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Edit profile Container */}
            <TouchableOpacity style={styles.submitButton} onPress={() => { navigation.navigate('ChangePassword') }}>
                <Icon name="edit" size={20} color="#fff" />
                <Text style={styles.submitButtonText}>Đổi mật khẩu</Text>
            </TouchableOpacity>
        </LinearGradient>
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        justifyContent: 'flex-start',
        backgroundColor: '#f5f5f5',
    },
    nameContainer: {
        alignItems: 'center',
        padding: 10, // Add padding
    },
    imageContainer: {
        alignSelf: 'center',
        overflow: 'hidden',
        shadowColor: '#333',
        shadowOffset: {
            width: 0,
            height: 10,
        },
        shadowOpacity: 0.37,
        shadowRadius: 7.49,
        elevation: 12,
        width: Dimensions.get('window').width * 0.25,
        height: Dimensions.get('window').width * 0.25,
        borderRadius: Math.round(Dimensions.get('window').width + Dimensions.get('window').height) / 2,
        marginBottom: 15,
    },
    img: {
        width: '100%',
        height: '100%',
        borderRadius: Math.round(Dimensions.get('window').width + Dimensions.get('window').height) / 2, // Add border radius
    },
    icon: {
        color: 'blue',
        marginRight: 20,
        marginLeft: 10
    },
    infoContainer: {
        padding: 30,
    },
    infoSection: {
        paddingVertical: 15,

    },
    info: {
        fontSize: 20,
        color: '#333',
        fontFamily: 'quolibet',
        flexWrap: 'nowrap',
        overflow: 'hidden',
        borderBottomWidth: 1,
        borderColor: 'lightgray',
        borderRadius: 10,
        padding: 10,
    },
    submitButton: {
        width: '50%',
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'lightgreen',
        padding: 10,
        borderRadius: 15,
        marginBottom: 20,
        position: 'absolute',
        bottom: 0,
        alignSelf: 'center',
        shadowColor: '#000', // Add shadow
        shadowOffset: {
            width: 0,
            height: 2,
        },
        shadowOpacity: 0.25,
        shadowRadius: 3.84,
        elevation: 5,
    },
    submitButtonText: {
        color: 'whitesmoke',
        marginLeft: 10,
        fontSize: 18,
        fontWeight: 'bold',
    },
    Label: {
        fontSize: 18,
        color: '#2282F3',
        fontFamily: 'quolibet',
        marginBottom: 5,
        fontWeight: 'bold',
    },
});