import { MaterialIcons } from '@expo/vector-icons';
import React from "react";
import { StyleSheet, Text, View, ImageBackground } from "react-native";

/*************************************************************
**                      CUSTOM HEADER                       **
**                  CREATED BY: LE ANH QUAN                 **
*************************************************************/


export default function Header({ navigation, title, customDrawerProp }) {
    const openMenu = () => {
        navigation.openDrawer();
    }
    return (
        <ImageBackground source={require('../assets/game_bg.png')} style={styles.header}>
            <MaterialIcons style={styles.icon} name="menu" size={28} onPress={openMenu} />
            <Text style={styles.headerText}>{title}</Text>
            <View style={styles.notificationContainer}>
                <MaterialIcons style={styles.icon} name="notifications" size={28} />
            </View>
        </ImageBackground>
    )
}

const styles = StyleSheet.create({
    header: {
        width: '100%',
        flexDirection: 'row',
        backgroundColor: '#fff',
        justifyContent: 'space-between',
        alignItems: 'center',
        paddingHorizontal: 10,
    },
    headerText: {
        fontFamily: 'nunito-regular',
        fontWeight: 'bold',
        fontSize: 16,
        color: '#333',
        letterSpacing: 1,
    },
    icon: {
        borderColor: '#333',
        color: '#f1356d',
    },
    notificationContainer: {
        width: 40,
        alignItems: 'center',
        marginRight: '5%',
    },
});
