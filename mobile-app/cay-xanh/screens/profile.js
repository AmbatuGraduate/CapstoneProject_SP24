import React, { useState } from "react";
import { View, Text, Image, StyleSheet, Dimensions, TouchableHighlight, TouchableOpacity } from 'react-native';
import { MaterialIcons } from '@expo/vector-icons';

/*************************************************************
**_________________ PROFILE SCREEN OF APP __________________**
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/

export default function Profile({ navigation }) {

    return (
        <View style={styles.container}>

            {/* ------------------------------------------------------------------- */}
            {/* PROFILE PICTURE */}

            <TouchableHighlight style={styles.imageContainer} onPress={() => alert('Yaay!')}>
                <Image
                    style={styles.img}
                    source={{ uri: 'https://sm.ign.com/ign_nordic/cover/a/avatar-gen/avatar-generations_prsz.jpg' }}
                />
            </TouchableHighlight>

            {/* ------------------------------------------------------------------- */}
            {/* NAME */}

            <View style={styles.name}>
                <Text style={{ fontWeight: 'bold', fontSize: 24 }}>Quan Le Anh</Text>
                <Text style={{ fontSize: 16 }}>Ngu Hanh Son, Da Nang</Text>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Details  */}

            <View style={styles.infoContainer}>
                <View style={styles.infoSection}>
                    <Text style={styles.label}>Tên đăng nhập</Text>
                    <Text style={styles.info}>aobnao9</Text>
                </View>
                <View style={styles.infoSection}>
                    <Text style={styles.label}>Số điện thoại</Text>
                    <Text style={styles.info}>012345678</Text>
                </View>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* FEATURES */}

            <View>
                {/* Info  */}
                <TouchableOpacity style={styles.profileNavigator} onPress={() => { navigation.navigate('ChangePassword') }}>
                    <View style={{ flexDirection: 'row', }}>
                        <MaterialIcons style={styles.icon} size={20} name="person" ></MaterialIcons>
                        <Text style={styles.buttonText}> Thay đổi thông tin cá nhân </Text>
                    </View>

                    <MaterialIcons style={styles.icon} size={20} name="navigate-next" ></MaterialIcons>
                </TouchableOpacity>

                {/* Password  */}
                <TouchableOpacity style={styles.profileNavigator} onPress={() => { navigation.navigate('ChangePassword') }}>
                    <View style={{ flexDirection: 'row', }}>
                        <MaterialIcons style={styles.icon} size={20} name="lock" ></MaterialIcons>
                        <Text style={styles.buttonText}> Đổi mật khẩu </Text>
                    </View>

                    <MaterialIcons style={styles.icon} size={20} name="navigate-next" ></MaterialIcons>
                </TouchableOpacity>

            </View>


        </View >
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20
    },
    name: {
        alignItems: 'center',
        marginBottom: 20
    },
    imageContainer: {
        alignSelf: 'center',
        margin: 20,
        overflow: 'hidden',
        shadowColor: "#000",
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
    },
    img: {
        width: '100%',
        height: '100%',
    },
    profileNavigator: {
        borderBottomColor: '#999DA0',
        borderBottomWidth: 1,
        flexDirection: 'row',
        justifyContent: 'space-between',
        paddingTop: 25,
        marginRight: 15,
        marginLeft: 15
    },
    buttonText: {
        fontSize: 16,
        color: '#777D7E',
    },
    icon: {
        color: '#98BD98',
    },
    infoContainer: {
        padding: 12,
        margin: 5
    },
    label: {
        color: 'lightgrey',
        fontWeight: 'bold',
        paddingTop: 20,
        paddingBottom: 10
    },
    info: {
        fontWeight: 'bold'
    },
    infoSection: {
        borderBottomColor: '#999DA0',
        borderBottomWidth: 1,
        paddingBottom: 5
    }
})