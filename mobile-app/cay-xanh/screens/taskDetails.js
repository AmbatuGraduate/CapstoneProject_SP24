import React from "react";
import { StyleSheet, View, Text, Image, Button } from 'react-native';
import FlatButton from "../shared/button";
/*************************************************************
**_________________ TASK DETAILS SCREEN ____________________**
**                  CREATED BY: LE ANH QUAN                 **

*************************************************************/

export default function TaskDetails({ route }) {

    const { key, name, img } = route.params;

    return (
        // NOI DUNG CHI TIET 
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
                <View
                    style={{
                        borderBottomColor: 'black',
                        borderBottomWidth: 1,
                    }}
                />
                <Text style={styles.infoText}>Quận: Ngũ Hành Sơn</Text>
                <Text style={styles.infoText}>Đường: Nam Kỳ Khởi Nghĩa</Text>
                <Text style={styles.infoText}>Số nhà: 001</Text>
            </View>

            {/* THONG TIN CHI TIET */}
            <View style={styles.detailsContainer}>
                <Text style={styles.nameText}>Thông tin chi tiết</Text>
                <View
                    style={{
                        borderBottomColor: 'black',
                        borderBottomWidth: 1,
                    }}
                />
                <Text style={styles.infoText}>{name}</Text>
                <Text style={styles.infoText}>Cây rễ sâu</Text>
                <Text style={styles.infoText}>Bàng Đài Loan</Text>
                <Text style={styles.infoText}>Thòi điểm trồng: 19/10/2019</Text>
                <Text style={styles.infoText}>Thòi điểm chăm sóc: 19/10/2021</Text>
            </View>
            <FlatButton style={{ bottom: 0 }} text='Hoàn thành' onPress={() => { console.log('btn pressed') }}></FlatButton>

        </View>
    );
}

const styles = StyleSheet.create({
    content: {
        flex: 1,
    },
    imageContainer: {
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
        borderRadius: 8
    },
    img: {
        width: '100%',
        height: 200,
    },
    detailsContainer: {
        marginTop: 15,
        padding: 8,
        paddingHorizontal: 20,
        marginBottom: 10,
    },
    nameText: {
        fontSize: 18,
        fontFamily: 'nunito-bold'
    },
    infoText: {
        fontFamily: 'nunito-regular'
    }
});