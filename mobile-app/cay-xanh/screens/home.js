import React, { useState } from "react";
import { StyleSheet, View, Text, FlatList, TouchableOpacity } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import { Icon } from '@rneui/themed';



export default function Home() {
    const tasks = ['Task 1', 'Task 2', 'Task 3'];
    const currentDate = new Date();
    const navigation = useNavigation();

    const handlePress = () => {
        navigation.navigate('Tasks');
    };
    return (
        <View style={styles.container}>
            <Text style={styles.titleText}>
                Hôm nay, {currentDate.toLocaleDateString('vi-VN')}
            </Text>
            <View style={{ flexDirection: 'row' }}>
                <Icon name="bell-o" type="font-awesome" size={22} color="blue" />
                <Text style={styles.subtitleText}>
                    Thông báo
                </Text>

            </View>


            {/* so cong viec */}
            <View style={styles.notif}>
                {tasks.length > 0 ? (
                    <TouchableOpacity style={styles.taskButton} onPress={handlePress}>
                        <Icon name="calendar-check-o" type="font-awesome" size={20} color="green" />
                        <Text style={styles.taskText}>
                            Bạn có {tasks.length} công việc {tasks.length > 1 ? '' : ''}hôm nay. Nhấn để xem.
                        </Text>
                    </TouchableOpacity>
                ) : (
                    <Text style={styles.noTasksText}>Chưa có công việc nào</Text>
                )}
            </View>

            <View style={styles.hrline}></View>

            {/* thong bao */}
            <View style={styles.notif}>
                <View style={styles.taskButton}>
                    <Text style={styles.taskText}>
                        Bạn có {tasks.length} công việc {tasks.length > 1 ? '' : ''}bị trễ.
                    </Text>
                </View>
                <View style={styles.taskButton}>
                    <Text style={styles.taskText}>
                        Bạn có {tasks.length} công việc {tasks.length > 1 ? '' : ''}bị trễ.
                    </Text>
                </View>
                <View style={styles.taskButton}>
                    <Text style={styles.taskText}>
                        Bạn có {tasks.length} công việc {tasks.length > 1 ? '' : ''}bị trễ.
                    </Text>
                </View>
                <View style={styles.taskButton}>
                    <Text style={styles.taskText}>
                        Bạn có {tasks.length} công việc {tasks.length > 1 ? '' : ''}bị trễ.
                    </Text>
                </View>
                <View style={styles.taskButton}>
                    <Text style={styles.taskText}>
                        Bạn có {tasks.length} công việc {tasks.length > 1 ? '' : ''}bị trễ.
                    </Text>
                </View>
            </View>


        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 24,
        backgroundColor: '#f5f5f5',
    },
    titleText: {
        fontSize: 16,
        fontWeight: 'bold',
        color: '#B8C1F2',
        marginBottom: 20,
        fontFamily: 'quolibet',
    },
    subtitleText: {
        fontSize: 20,
        fontWeight: 'bold',
        color: '#4557B6',
        marginBottom: 10,
        fontFamily: 'quolibet',
        marginLeft: 10,
    },
    taskButton: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#fff',
        padding: 16,
        borderRadius: 10,
        marginVertical: 10,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 },
        shadowOpacity: 0.1,
        shadowRadius: 2,
        elevation: 3,
    },
    taskText: {
        marginLeft: 10,
        fontSize: 16,
        color: 'grey',

    },
    noTasksText: {
        fontSize: 18,
        color: '#333',
        fontStyle: 'italic',
        textAlign: 'center',
    },
    hrline: {
        borderBottomColor: '#BABABA',
        borderBottomWidth: 1,
        marginVertical: 20,
        width: '90%',
        alignSelf: 'center',
    },
});