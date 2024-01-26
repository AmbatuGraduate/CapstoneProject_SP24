import React from "react";
import { StyleSheet, TouchableOpacity, Text, View } from "react-native";
import { LinearGradient } from 'expo-linear-gradient';

export default function FlatButton({ text, onPress }) {
    return (
        <TouchableOpacity onPress={onPress}>
            <LinearGradient
                colors={["#a0c8a0", "#7d9d7d"]}
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
        paddingVertical: 14,
        paddingHorizontal: 10,
        backgroundColor: '#87a080',
        width: '40%',
        alignSelf: 'center',
    },
    buttonText: {
        color: 'white',
        fontWeight: 'bold',
        textTransform: 'uppercase',
        fontSize: 12,
        textAlign: 'center'
    }
})