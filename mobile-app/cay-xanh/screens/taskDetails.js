import React from "react";
import { StyleSheet, View, Text, FlatList, Image, TouchableOpacity } from 'react-native';

export default function TaskDetails({ navigation, route }) {

    const { key, name, img } = route.params;

    return (
        <View>
            <Text>{name}</Text>
            <Image
                style={{ width: '100%', height: '80%' }}
                source={{ uri: img }}></Image>
        </View>
    )
}