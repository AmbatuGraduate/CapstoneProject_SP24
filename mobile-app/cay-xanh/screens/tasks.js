import React, { useState } from "react";
import { StyleSheet, View, Text, FlatList, Image, TouchableOpacity } from 'react-native';

export default function TasksList({ navigation }) {
    const [trees, setTrees] = useState([
        { name: 'cay bang', img: 'https://www.canhquan.net/Content/Images/FileUpload/2018/2/p1030345_500_03%20(1)-1.jpg', key: '1' },
        { name: 'cay phuong', img: 'https://upload.wikimedia.org/wikipedia/commons/8/87/Royal_Poinciana.jpg', key: '2' },
    ]);

    return (
        <View>
            <FlatList
                data={trees}
                renderItem={({ item }) => (
                    <TouchableOpacity style={styles.records} onPress={() => {
                        navigation.navigate('TaskDetails', {
                            key: item.key,
                            name: item.name,
                            img: item.img
                        })
                    }}>
                        <Text>{item.name}</Text>
                        <Image
                            style={{ width: '100%', height: '80%' }}
                            source={{ uri: item.img }}></Image>
                    </TouchableOpacity>
                )}>

            </FlatList>
        </View>
    )
}
const styles = StyleSheet.create({
    records: {
        height: 100,
        borderWidth: 1,
        borderColor: '#333',
        borderRadius: 8,
        margin: 20,
        padding: 16

    }
})