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

        <View style={styles.header}>
            <MaterialIcons style={styles.icon} name="menu" size={28} onPress={openMenu} ></MaterialIcons>
            <Text style={styles.headerText}>{title}</Text>
        </View>
    )
}

const styles = StyleSheet.create({
    header: {
        width: '100%',
        height: '100%',
        flex: 1,
        flexDirection: 'row',
        backgroundColor: '#fff'
    },
    headerText: {
        fontWeight: 'bold',
        fontSize: 20,
        color: '#333',
        letterSpacing: 1,
    },
    icon: {
        borderColor: '#333'
    },
    headerImg: {
        width: 26,
        height: 26,
        marginHorizontal: 10,
    },
    headerTitle: {
        flexDirection: 'row',
    }
});