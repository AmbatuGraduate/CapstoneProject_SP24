import { MaterialIcons } from '@expo/vector-icons';
import React from "react";
import { StyleSheet, Text, View } from "react-native";

/*************************************************************
**                      CUSTOM HEADER                       **
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/


export default function Header({ navigation, title }) {
    const openMenu = () => {
        navigation.openDrawer();
    }
    return (

        <View>
            <MaterialIcons name="menu" size={28} onPress={openMenu} ></MaterialIcons>
            <Text>{title}</Text>
        </View>
    )
}