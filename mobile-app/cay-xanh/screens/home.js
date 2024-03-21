import React, { useState } from "react";
import { StyleSheet, View, Text, FlatList, Image, Modal } from 'react-native';


/*************************************************************
**__________________ HOME SCREEN OF APP ____________________**
**                  CREATED BY: LE ANH QUAN                 **

*************************************************************/

export default function Home() {

    const [trees, setTrees] = useState([
        { name: 'cay bang', img: 'https://www.canhquan.net/Content/Images/FileUpload/2018/2/p1030345_500_03%20(1)-1.jpg', key: '1' },
        { name: 'cay phuong', img: 'https://upload.wikimedia.org/wikipedia/commons/8/87/Royal_Poinciana.jpg', key: '2' },
    ]);


    return (
        <View>
            <FlatList
                data={trees}
                renderItem={({ item }) => (
                    <View style={styles.records}>
                        <Text>{item.name}</Text>
                        <Image
                            style={{ width: '100%', height: '80%' }}
                            source={{ uri: item.img }}></Image>
                    </View>
                )}>

            </FlatList>
            <Modal visible={false}>
                <View style={styles.modelContent}>
                    <Text>Model</Text>
                </View>
            </Modal>
        </View>
    )
}

const styles = StyleSheet.create({
    records: {
        height: 250,
        borderWidth: 1,
        borderColor: '#333',
        borderRadius: 8,
        margin: 20,
        padding: 16

    },
    modelContent: {
        flex: 1,
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: 'white',
        padding: 40,
        margin: 20
    }
})