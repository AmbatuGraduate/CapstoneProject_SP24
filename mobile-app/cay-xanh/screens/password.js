import React from "react";
import { View, Text, StyleSheet, Dimensions, TextInput } from 'react-native';
import { MaterialIcons } from '@expo/vector-icons';
import FlatButton from "../shared/button";
import Toast from 'react-native-root-toast';

export default function Password() {

    return (
        <View style={styles.container}>

            {/* ------------------------------------------------------------------- */}
            {/* PROFILE PICTURE Container*/}

            <View style={styles.imageContainer} onPress={() => alert('Yaay!')}>
                <MaterialIcons size={72} name="lock" ></MaterialIcons>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* text Container*/}

            <View style={styles.nameContainer}>
                <Text style={{ fontSize: 16, fontFamily: 'nunito-bold', textAlign: 'center', }}>Đổi mật khẩu</Text>
                <Text style={{ fontSize: 14, fontFamily: 'nunito-regular', textAlign: 'center', }}>Nhập mật khẩu cũ và mật khẩu mới phía dưới</Text>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* password info Container*/}

            <View style={styles.infoContainer}>
                <View style={styles.infoSection}>
                    <Text style={{
                        fontSize: 14,
                        color: '#777D7E',
                        fontFamily: 'nunito-bold',
                    }}>Mật khẩu cũ</Text>

                    <TextInput secureTextEntry={true} style={styles.info} placeholder='*********'></TextInput>
                </View>
                <View style={styles.infoSection}>
                    <Text style={{
                        fontSize: 14,
                        color: '#777D7E',
                        fontFamily: 'nunito-bold',
                    }}>Mật khẩu mới</Text>
                    <TextInput secureTextEntry={true} style={styles.info} placeholder='*********'></TextInput>
                </View>
                <View style={styles.infoSection}>
                    <Text style={{
                        fontSize: 14,
                        color: '#777D7E',
                        fontFamily: 'nunito-bold',
                    }}>Nhập lại mật khẩu mới</Text>
                    <TextInput secureTextEntry={true} style={styles.info} placeholder='*********'></TextInput>
                </View>
            </View>

            {/* ------------------------------------------------------------------- */}
            {/* Edit profile Container */}
            <FlatButton text='Đổi mật khẩu' onPress={() => alert('pass')}></FlatButton>
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
    nameContainer: {
        alignItems: 'center',
        paddingHorizontal: 80
    },
    imageContainer: {
        alignSelf: 'center',
        alignItems: 'center',
        justifyContent: 'center',
        overflow: 'hidden',
        width: Dimensions.get('window').width * 0.33,
        height: Dimensions.get('window').width * 0.33,
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