import { StyleSheet, Dimensions } from "react-native";

export const profileStyle = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: 'white',
        paddingVertical: 30,
        justifyContent: 'space-between',
    },
    imageContainer: {
        alignSelf: 'center',
        alignItems: 'center',
        justifyContent: 'center',
        overflow: 'hidden',
        width: Dimensions.get('window').width * 0.33,
        height: Dimensions.get('window').width * 0.33,
    },
    imageContainer: {
        alignSelf: 'center',
        overflow: 'hidden',
        shadowColor: "#333",
        shadowOffset: {
            width: 0,
            height: 6,
        },
        shadowOpacity: 0.37,
        shadowRadius: 7.49,
        elevation: 12,
        backgroundColor: 'white',
        width: Dimensions.get('window').width * 0.33,
        height: Dimensions.get('window').width * 0.33,
        borderRadius: Math.round(Dimensions.get('window').width + Dimensions.get('window').height) / 2,
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
