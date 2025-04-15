import React, { useState, useEffect } from "react";
import { View, Text, Image, StyleSheet, Dimensions, TouchableHighlight } from 'react-native';
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
                setUser(JSON.parse(userString));
            }
        });
    }, []);

    return (
        <LinearGradient
            colors={['rgba(255, 255, 255, 0.6)', 'rgba(254, 252, 234, 0.5)']}
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
                    <Icon name="email" size={20} style={styles.icon} color={'#31A8E7'} />
                    <View>
                        <Text style={styles.Label}>Email</Text>
                        <Text style={styles.info} numberOfLines={1}>{user?.email}</Text>
                    </View>

                </View>
                <View style={styles.infoSection}>
                    <Icon name="group" size={20} style={styles.icon} color={'#FEEEB7'} />
                    <View>
                        <Text style={styles.Label}>Bộ phận</Text>
                        <Text style={styles.info}>{user?.department}</Text>
                    </View>

                </View>
                <View style={styles.infoSection}>
                    <Icon name="user-circle-o" type="font-awesome" size={20} color={'#FCCCE0'} style={styles.icon} />
                    <View>
                        <Text style={styles.Label}>Chức vụ</Text>
                        <Text style={styles.info}>Nhân viên</Text>
                    </View>

                </View>
                <View style={styles.infoSection}>
                    <Icon name="phone" size={20} style={styles.icon} color={'#ABF0D7'} />
                    <View>
                        <Text style={styles.Label}>Số điện thoại</Text>
                        <Text style={styles.info}>{user?.phoneNumber}</Text>
                    </View>

                </View>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Edit profile Container */}
            {/* <TouchableOpacity style={styles.submitButton} onPress={() => { navigation.navigate('ChangePassword') }}>
                <Icon name="edit" size={20} color="#fff" />
                <Text style={styles.submitButtonText}>Đổi mật khẩu</Text>
            </TouchableOpacity> */}
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
        padding: 15,
        borderRadius: 50,
        borderWidth: 5,
        borderColor: 'rgba(246,246,246,0.4)',
        marginRight: 20,

    },
    infoContainer: {
        padding: 20,
    },
    infoSection: {
        paddingVertical: 15,
        flexDirection: 'row',
    },
    info: {
        fontSize: 20,
        color: '#333',
        fontFamily: 'quolibet',
        flexWrap: 'nowrap',
        overflow: 'hidden',
        borderColor: 'lightgray',
        borderRadius: 10,
        paddingVertical: 10,
        letterSpacing: 0.75,
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
        fontSize: 16,
        color: '#CDD3D6',
        fontFamily: 'quolibet',
        marginBottom: 5,
        letterSpacing: 1,
    },
});