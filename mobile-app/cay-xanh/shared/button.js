import React from "react";
import { StyleSheet, TouchableOpacity, Text, View } from "react-native";
import { LinearGradient } from 'expo-linear-gradient';
import { Icon } from '@rneui/themed';


export default function FlatButton({ text, onPress, iconName }) {
    return (
        <TouchableOpacity onPress={onPress}>
            <LinearGradient
                colors={["#F5F5F5", "#F5F5F5"]}
                style={styles.button}
                start={{ x: 0, y: 0.5 }}
                end={{ x: 1, y: 0.5 }}
            >
                <View></View>
                <Text style={styles.buttonText}>{text}</Text>
                <View style={styles.iconContainer}>
                    <Icon name={iconName} size={24} color="white" />
                </View>
            </LinearGradient>
        </TouchableOpacity>
    );
}


const styles = StyleSheet.create({
    button: {
        borderRadius: 28,
        marginVertical: 15,
        paddingVertical: 8,
        paddingHorizontal: 10,
        backgroundColor: '#87a080',
        width: '60%',
        alignSelf: 'center',
        borderColor: '#f1356d',
        borderWidth: 2,
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
    },
    buttonText: {
        color: '#f1356d',
        fontSize: 20,
        textAlign: 'center',
        fontFamily: 'nunito-bold',
    },
    iconContainer: {
        backgroundColor: '#f1356d',
        borderRadius: 50, // This will make the background circular
        padding: 5,
    }
})