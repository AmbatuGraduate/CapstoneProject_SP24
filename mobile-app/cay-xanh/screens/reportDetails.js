import React from "react";
import { View, Text, StyleSheet, ScrollView, Image } from "react-native";
import { Icon } from '@rneui/themed';

export default function ReportDetails({ route }) {

    const { reportId, reportBody, reportImage, reportSubject, reportImpact, reportStatus, reportResponse, expectedResolutionDate, actualResolutionDate } = route.params;

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
        'UnResolved': '#FFA6A6', // light red
        'Resolved': '#8BE78B', // light green
    };

    const reportStatuses = {
        'UnResolved': 'Chưa xử lý',
        'Resolved': 'Đã xử lý',
    };

    const statusColors = {
        'UnResolved': '#D32F2F', // dark red
        'Resolved': '#388E3C', // dark green
    };

    return (
        <ScrollView
            style={styles.container}
        >
            <View style={styles.overview}>
                <Text style={styles.subject}>{cleanedReportSubject}</Text>
                <View style={styles.impactContainer}>
                    <Text style={[styles.label, { color: '#2282F3' }]}>Mức độ ảnh hưởng</Text>
                    <View style={{ flexDirection: 'row', alignItems: 'center' }}>
                        <Text style={[styles.impactText, { color: impactColors[reportImpact] }]}>{impactLevels[reportImpact]}</Text>
                        <Icon style={{ marginLeft: 10 }} name="warning" type="Ionicons" size={20} color={impactColors[reportImpact]} />
                    </View>
                </View>

                <View style={styles.impactContainer}>
                    <Text style={[styles.label, { color: '#2282F3' }]}>Trạng thái</Text>
                    <Text style={[styles.statusText, { backgroundColor: statusBackground[reportStatus], color: statusColors[reportStatus] }]}>{reportStatuses[reportStatus]}</Text>
                </View>
            </View>

            <View style={styles.overview}>
                {reportImage && <Image source={{ uri: reportImage }} style={styles.image} />}
                <Text style={[styles.label, { color: '#2282F3' }]}>Nội dung</Text>
                <Text style={styles.bodyText}>{cleanedReportBody}</Text>
                {reportStatus !== 'Resolved' && (
                    <Text style={styles.dateText}>Cần giải quyết trước - {expectedResolutionDate}</Text>
                )}
            </View>

            <View style={styles.overview}>
                <Text style={[styles.label, { color: '#2282F3' }]}>Phản hồi</Text>
                {reportResponse ? (
                    <View>
                        <Text style={styles.bodyText}>{reportResponse}</Text>
                        <Text style={styles.resDate}>Phản hồi ngày: {actualResolutionDate}</Text>
                    </View>
                ) : (
                    <Text style={styles.noRes}>Chưa có phản hồi...</Text>
                )}
            </View>
        </ScrollView>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
        padding: 20,
    },
    overview: {
        backgroundColor: '#F5F5F5',
        paddingHorizontal: 20,
        paddingVertical: 10,
        borderRadius: 15,
    },
    subject: {
        fontSize: 24,
        fontFamily: 'quolibet',
        fontWeight: 'bold',
        marginBottom: 10,
        color: '#2282F3',
        textAlign: 'center',
    },
    impactContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        borderBottomWidth: 1,
        borderBottomColor: '#eee',
        paddingBottom: 10,
    },
    label: {
        fontSize: 16,
        fontFamily: 'quolibet',
        fontWeight: '600',
        fontWeight: 'bold',
        paddingTop: 20,
        borderBottomWidth: 1,
        borderBottomColor: 'lightgrey',
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
        paddingVertical: 6,
        paddingHorizontal: 12,
        borderRadius: 15,
    },
    bodyText: {
        fontSize: 16,
        lineHeight: 24,
        color: '#333',
        letterSpacing: 0.5,
    },
    dateText: {
        fontSize: 16,
        fontFamily: 'quolibet',
        fontWeight: 'bold',
        backgroundColor: '#FFD700',
        padding: 5,
        borderRadius: 15,
        textAlign: 'center',
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
    },
    image: {
        width: '100%',
        height: 200,
        borderRadius: 15,
        marginTop: 10,
    },
});
