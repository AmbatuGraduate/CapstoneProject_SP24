import React, { useState, useEffect } from "react";
import { View, Text, Image, StyleSheet, Dimensions, TouchableHighlight, TouchableOpacity } from 'react-native';
import { MaterialIcons } from '@expo/vector-icons';
import FlatButton from "../shared/button";
import AsyncStorage from '@react-native-async-storage/async-storage';

/*************************************************************
**_________________ PROFILE SCREEN OF APP __________________**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/

export default function Profile({ navigation }) {

    const [user, setUser] = useState(null);
    // check if user is logged in
    useEffect(() => {
        AsyncStorage.getItem("@user").then(user => {
            if (user !== null) {
                setUser(JSON.parse(user));
                console.log(user);
            }
        });
    }, []);


    return (
        <View style={styles.container}>

            {/* ------------------------------------------------------------------- */}
            {/* PROFILE PICTURE Container*/}

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
                <Text style={{ fontSize: 16 }}>Ngu Hanh Son, Da Nang</Text>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Details info Container*/}

            <View style={styles.infoContainer}>
                <View style={styles.infoSection}>
                    <MaterialIcons style={styles.icon} size={24} name="account-circle" ></MaterialIcons>
                    <Text style={styles.info} numberOfLines={1}>{user?.email}</Text>
                    <View></View>
                    <View></View>
                </View>
                <View style={styles.infoSection}>
                    <MaterialIcons style={styles.icon} size={24} name="calendar-today" ></MaterialIcons>
                    <Text style={styles.info}>Ngày Sinh</Text>
                    <View></View>
                    <View></View>
                </View>
                <View style={styles.infoSection}>
                    <MaterialIcons style={styles.icon} size={24} name="local-phone" ></MaterialIcons>
                    <Text style={styles.info}>0123456789</Text>
                    <View></View>
                    <View></View>
                </View>
                {/* Functional Features */}
                {/* Password  */}
                <TouchableOpacity style={styles.infoSection} onPress={() => { navigation.navigate('ChangePassword') }}>
                    <MaterialIcons style={styles.icon} size={24} name="lock" ></MaterialIcons>
                    <Text style={styles.info}>Đổi mật khẩu </Text>
                    <View></View>
                    <View></View>
                    <View></View>
                    <MaterialIcons style={styles.iconBtn} size={24} name="cached" ></MaterialIcons>
                </TouchableOpacity>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Edit profile Container */}
            <FlatButton text='Chỉnh sửa' onPress={() => { navigation.navigate('EditProfile') }}></FlatButton>
        </View >
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: 'white',
        paddingVertical: 30,
        justifyContent: 'space-between',
        paddingHorizontal: 20,
    },
    nameContainer: {
        alignItems: 'center',
        marginBottom: 20,
    },
    imageContainer: {
        alignSelf: 'center',
        overflow: 'hidden',
        shadowColor: "#333",
        shadowOffset: {
            width: 0,
            height: 6,
        },
        shadowOpacity: 0.37,
        shadowRadius: 7.49,
        elevation: 12,
        backgroundColor: 'white',
        width: Dimensions.get('window').width * 0.33,
        height: Dimensions.get('window').width * 0.33,
        borderRadius: Math.round(Dimensions.get('window').width + Dimensions.get('window').height) / 2,
        marginBottom: 20,
    },
    img: {
        width: '100%',
        height: '100%',
    },
    icon: {
        color: '#98BD98',
        marginRight: 36,
        marginLeft: 10
    },
    iconBtn: {
        color: '#98BD98',
    },
    // information
    infoContainer: {
        marginTop: 20,
    },
    infoSection: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        borderBottomColor: '#d3d3d3',
        borderBottomWidth: 1,
        paddingHorizontal: 0,
        paddingVertical: 15,
    },
    info: {
        flex: 1,
        fontSize: 16,
        color: '#777D7E',
        fontFamily: 'nunito-regular',
        flexWrap: 'nowrap', // Prevent text from wrapping
        overflow: 'hidden', // Hide overflow text
        width: '90%', // Set a specific width
    },
});