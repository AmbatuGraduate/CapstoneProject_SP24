import React from "react";
import { View, Text, StyleSheet, ScrollView } from "react-native";
import { Icon } from '@rneui/themed';

export default function ReportDetails({ route }) {

    const { reportId, reportBody, reportSubject, reportImpact, reportStatus, reportResponse, expectedResolutionDate, actualResolutionDate } = route.params;

    let cleanedReportSubject = reportSubject.replace(/\[Report\]/g, '').trim();

    let cleanedReportBody = reportBody.replace(/Report ID: .*|Expected Resolution Date: .*|Report Impact: .*/g, '');

    const impactLevels = {
        0: 'Thấp',
        1: 'Vừa',
        2: 'Cao'
    };

    const impactColors = {
        0: '#ADF35B',
        1: 'orange',
        2: 'red'
    };

    const statusBackground = {
        'UnResolved': 'pink',
        'Resolved': '#8BE78B',
    };

    const reportStatuses = {
        'UnResolved': 'Chưa xử lý',
        'Resolved': 'Đã xử lý',
    };

    const statusColors = {
        'UnResolved': 'red',
        'Resolved': 'green',
    };

    return (
        <ScrollView
            style={styles.container}
        >
            <View style={styles.overview}>
                <Text style={styles.subject}>{cleanedReportSubject}</Text>
                <View style={styles.impactContainer}>
                    <Text style={{ color: '#2282F3', fontSize: 18, fontWeight: '600' }}>Mức độ ảnh hưởng</Text>
                    <View style={{ flexDirection: 'row' }}>
                        <Text style={[styles.impactText, { color: impactColors[reportImpact] }]}>{impactLevels[reportImpact]}</Text>
                        <Icon style={{ marginLeft: 10 }} name="warning" type="Ionicons" size={20} color={impactColors[reportImpact]} />
                    </View>
                </View>

                <View style={styles.impactContainer}>
                    <Text style={{ color: '#2282F3', fontSize: 18, fontWeight: '600' }}>Trạng thái</Text>
                    <Text style={[styles.statusText, { color: statusColors[reportStatus] }]}>{reportStatuses[reportStatus]}</Text>
                </View>

            </View>


            <View style={styles.overview}>
                <Text style={{ color: '#2282F3', fontSize: 18, fontWeight: '600' }}>Nội dung</Text>
                <Text style={styles.bodyText}>{cleanedReportBody}</Text>
                <Text style={[styles.bodyText, styles.dateText]}>Cần giải quyết trước -  {expectedResolutionDate}</Text>
            </View>

            {/* if response != null, add display response */}
            <View style={styles.overview}>
                <Text style={{ color: '#2282F3', fontSize: 18, fontWeight: '600' }}>Phản hồi</Text>
                {reportResponse

                    ? <View>
                        <Text style={styles.bodyText}>{reportResponse}</Text>
                        <Text style={styles.resDate}>Phản hồi ngày: {actualResolutionDate} </Text>
                    </View>
                    : <Text style={styles.noRes}>Chưa có phản hồi...</Text>
                }
            </View>


        </ScrollView >
    )
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20, // Increase padding
    },
    overview: {
        backgroundColor: '#F5F5F5', // Lighter background color
        padding: 30, // Increase padding
        marginBottom: 20, // Increase margin
        borderRadius: 15, // Increase border radius
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 2 }, // Increase shadow offset
        shadowOpacity: 0.25, // Decrease shadow opacity
        shadowRadius: 3.84,
        elevation: 5,
    },
    subject: {
        fontSize: 24, // Increase font size
        fontFamily: 'quolibet',
        fontWeight: 'bold',
        marginBottom: 15, // Increase margin
    },
    impactContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginVertical: 10,
        borderBottomWidth: 1,
        borderBottomColor: '#eee',
        paddingBottom: 10,
    },
    impactText: {
        fontSize: 18,
        fontFamily: 'quolibet',
        fontWeight: 'bold',
    },
    statusText: {
        fontSize: 16,
        fontFamily: 'quolibet',
        fontWeight: 'bold',
        color: '#333',
        padding: 6,
        borderRadius: 15,
    },
    bodyText: {
        fontSize: 18,
        fontFamily: 'quolibet',
        color: '#333',
        lineHeight: 24,
        paddingTop: 30
    },
    dateText: {
        fontSize: 14, // Increase font size
        fontWeight: 'bold', // Make it bold
        marginTop: 15, // Add margin top for more space above the date
    },
    noRes: {
        fontSize: 18,
        fontFamily: 'quolibet',
        color: 'grey',
        marginTop: 10,
    },
    resDate: {
        fontSize: 16,
        fontFamily: 'quolibet',
        color: '#838383',
        fontWeight: 'bold',
        marginTop: 10,
    }
})
