import React, { useState, useEffect } from "react";
import { View, Text, Image, StyleSheet, Dimensions, TouchableHighlight, TouchableOpacity } from 'react-native';
import { MaterialIcons } from '@expo/vector-icons';
import FlatButton from "../shared/button";
import AsyncStorage from '@react-native-async-storage/async-storage';
import { LinearGradient } from 'expo-linear-gradient';


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
        <LinearGradient
            colors={['rgba(255, 255, 255, 0.6)', 'rgba(197, 252, 234, 0.5)']}
            start={{ x: 0, y: 0 }}
            end={{ x: 0, y: 1 }}
            style={styles.container}
        >

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
                    <Text style={styles.info}>Bộ phận</Text>
                    <View></View>
                    <View></View>
                </View>
                {/* Functional Features */}
                {/* Password  */}
                {/* <TouchableOpacity style={styles.infoSection} onPress={() => { navigation.navigate('ChangePassword') }}>
                    <MaterialIcons style={styles.icon} size={24} name="lock" ></MaterialIcons>
                    <Text style={styles.info}>Đổi mật khẩu </Text>
                    <View></View>
                    <View></View>
                    <View></View>
                    <MaterialIcons style={styles.iconBtn} size={24} name="cached" ></MaterialIcons>
                </TouchableOpacity> */}
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Edit profile Container */}
            <FlatButton text='Chỉnh sửa' iconName="edit" onPress={() => { navigation.navigate('EditProfile') }}></FlatButton>
        </LinearGradient>
    )
}

const colors = {
    primary: '#98BD98',
    secondary: '#d3d3d3',
    text: '#777D7E',
    background: 'white',
};

const spacing = {
    small: 10,
    medium: 20,
    large: 30,
};

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: colors.background,
        paddingVertical: spacing.large,
        justifyContent: 'space-between',
        paddingHorizontal: spacing.medium,
    },
    nameContainer: {
        alignItems: 'center',
        marginBottom: spacing.medium,
    },
    imageContainer: {
        alignSelf: 'center',
        overflow: 'hidden',
        shadowColor: colors.secondary,
        shadowOffset: {
            width: 0,
            height: spacing.small,
        },
        shadowOpacity: 0.37,
        shadowRadius: 7.49,
        elevation: 12,
        backgroundColor: colors.background,
        width: Dimensions.get('window').width * 0.33,
        height: Dimensions.get('window').width * 0.33,
        borderRadius: Math.round(Dimensions.get('window').width + Dimensions.get('window').height) / 2,
        marginBottom: spacing.medium,
    },
    img: {
        width: '100%',
        height: '100%',
    },
    icon: {
        color: colors.primary,
        marginRight: spacing.large,
        marginLeft: spacing.small
    },
    iconBtn: {
        color: colors.primary,
    },
    infoContainer: {
        marginTop: spacing.medium,
    },
    infoSection: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        borderBottomColor: colors.secondary,
        borderBottomWidth: 1,
        paddingHorizontal: 0,
        paddingVertical: spacing.medium,
    },
    info: {
        flex: 1,
        fontSize: 16,
        color: colors.text,
        fontFamily: 'nunito-regular',
        flexWrap: 'nowrap',
        overflow: 'hidden',
        width: '90%',
    },
});