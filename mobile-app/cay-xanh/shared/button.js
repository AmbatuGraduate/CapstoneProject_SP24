import React from "react";
import { StyleSheet, TouchableOpacity, Text, View } from "react-native";
import { LinearGradient } from 'expo-linear-gradient';

export default function FlatButton({ text, onPress }) {
    return (
        <TouchableOpacity onPress={onPress}>
            <LinearGradient
                colors={["#f1356d", "#f1356d"]}
                style={styles.button}
                start={{ x: 0, y: 0.5 }}
                end={{ x: 1, y: 0.5 }}
            >
                <Text style={styles.buttonText}>{text}</Text>
            </LinearGradient>
        </TouchableOpacity>
    );
}


const styles = StyleSheet.create({
    button: {
        borderRadius: 28,
        marginVertical: 15,
        paddingVertical: 14,
        paddingHorizontal: 10,
        backgroundColor: '#87a080',
        width: '100%',
        alignSelf: 'center',
    },
    buttonText: {
        color: 'white',
        fontWeight: 'bold',
        fontSize: 12,
        textAlign: 'center'
    }
})