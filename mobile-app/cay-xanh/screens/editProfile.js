import React from "react";
import { View, Text, StyleSheet, Dimensions, TextInput, TouchableHighlight, Image } from 'react-native';
import FlatButton from "../shared/button";

export default function EditProfile() {

    return (
        <View style={styles.container}>

            {/* ------------------------------------------------------------------- */}
            {/* PROFILE PICTURE Container*/}

            <TouchableHighlight style={styles.imageContainer} onPress={() => alert('Yaay!')}>
                <Image
                    style={styles.img}
                    source={{ uri: 'https://sm.ign.com/ign_nordic/cover/a/avatar-gen/avatar-generations_prsz.jpg' }}
                />
            </TouchableHighlight>

            {/* ------------------------------------------------------------------- */}
            {/* ------------------------------------------------------------------- */}
            {/* password info Container*/}

            <View style={styles.infoContainer}>
                <View style={styles.infoSection}>
                    <Text style={{
                        fontSize: 14,
                        color: '#777D7E',
                        fontFamily: 'nunito-bold',
                    }}>Số điện thoại</Text>

                    <TextInput style={styles.info} placeholder='Ví dụ: 0123456789'></TextInput>
                </View>
                <View style={styles.infoSection}>
                    <Text style={{
                        fontSize: 14,
                        color: '#777D7E',
                        fontFamily: 'nunito-bold',
                    }}>Ngày sinh</Text>
                    <TextInput style={styles.info} placeholder='Ví dụ: 12/02/2022'></TextInput>
                </View>
                <View style={styles.infoSection}>
                    <Text style={{
                        fontSize: 14,
                        color: '#777D7E',
                        fontFamily: 'nunito-bold',
                    }}>Họ Tên</Text>
                    <TextInput style={styles.info} placeholder='Ví dụ: Lê Anh Quân'></TextInput>
                </View>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Edit profile Container */}
            <FlatButton text='Chỉnh sửa' onPress={() => alert('pass')}></FlatButton>
        </View >
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: 'white',
        paddingVertical: 30,
        justifyContent: 'space-between',
    },
    imageContainer: {
        alignSelf: 'center',
        alignItems: 'center',
        justifyContent: 'center',
        overflow: 'hidden',
        width: Dimensions.get('window').width * 0.33,
        height: Dimensions.get('window').width * 0.33,
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
    },
    img: {
        width: '100%',
        height: '100%',
    },
    iconBtn: {
        color: '#98BD98',
    },
    infoSection: {
        alignItems: 'center',
        justifyContent: 'center',
        marginVertical: 10,
        paddingVertical: 10,
    },
    info: {
        fontSize: 16,
        color: '#777D7E',
        fontFamily: 'nunito-regular',
        backgroundColor: '#F2F3F4',
        borderRadius: 16,
        width: Dimensions.get('window').width * 0.75,
        height: Dimensions.get('window').width * 0.1,
        textAlign: 'center',
    },

})