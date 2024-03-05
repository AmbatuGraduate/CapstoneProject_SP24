import React from "react";
import { StyleSheet, View, Text, Image } from 'react-native';
import FlatButton from "../shared/button";
/*************************************************************
**_________________ TASK DETAILS SCREEN ____________________**
**                  CREATED BY: LE ANH QUAN                 **

*************************************************************/

export default function TaskDetails({ route }) {

    const { key, description, address, start, img } = route.params;

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
                <Text style={styles.infoText}>{address}</Text>
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
                <Text style={styles.infoText}>{description}</Text>
            </View>

            {/* THONG TIN CHI TIET */}
            <View style={styles.detailsContainer}>
                <Text style={styles.nameText}>Thời gian</Text>
                <View
                    style={{
                        borderBottomColor: 'black',
                        borderBottomWidth: 1,
                    }}
                />
                <Text style={styles.infoText}>{start}</Text>
            </View>


            <FlatButton style={{
                position: 'absolute',
                bottom: 10,
                left: 0,
                right: 0
            }} text='Hoàn thành' onPress={() => { console.log('btn pressed') }}></FlatButton>

        </View>
    );
}

const styles = StyleSheet.create({
    content: {
        flex: 1,
        padding: 20,
    },
    imageContainer: {
        margin: 20,
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
        marginTop: 15,
        padding: 8,
        paddingHorizontal: 20,
        marginBottom: 10,
    },
    nameText: {
        fontSize: 18,
        color: '#333',
        textTransform: 'uppercase',
        fontFamily: 'nunito-bold'
    },
    infoText: {
        fontSize: 16,
        letterSpacing: 1.5,
        color: '#666',
        marginVertical: 10,
        // fontFamily: 'nunito-regular',
    },
});