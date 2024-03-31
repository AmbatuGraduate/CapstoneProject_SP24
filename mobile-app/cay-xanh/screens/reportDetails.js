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

    statusBackground = {
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
                    <Text style={[styles.statusText, { backgroundColor: statusBackground[reportStatus] }, { color: statusColors[reportStatus] }]}>{reportStatuses[reportStatus]}</Text>
                </View>

            </View>


            <View style={styles.overview}>
                <Text style={styles.bodyText}>{cleanedReportBody}</Text>
                <Text style={[styles.bodyText, styles.dateText]}>Cần giải quyết trước -  {expectedResolutionDate}</Text>
            </View>

            {/* if response != null, add display response */}
            <View style={styles.overview}>
                <Text style={styles.subject}>Phản hồi</Text>

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
        padding: 10,
    },
    overview: {
        backgroundColor: 'white',
        padding: 20,
        marginBottom: 12,
        borderRadius: 10,
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.3,
        shadowRadius: 2,
        elevation: 5,
    },
    subject: {
        fontSize: 22,
        fontFamily: 'quolibet',
        fontWeight: 'bold',
        marginBottom: 10,
    },
    statusText: {
        fontSize: 16,
        fontFamily: 'quolibet',
        fontWeight: 'bold',
        color: '#333',
        padding: 6,
        borderRadius: 15,
    },
    impactText: {
        fontSize: 16,
        fontFamily: 'quolibet',
        fontWeight: 'bold',
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
    bodyText: {
        fontSize: 18,
        fontFamily: 'quolibet',
        color: '#333',
        lineHeight: 24,
    },
    dateText: {
        color: '#2282F3',
        marginTop: 10,
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