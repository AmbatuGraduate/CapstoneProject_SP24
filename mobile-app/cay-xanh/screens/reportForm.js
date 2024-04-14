import React, { useState, useRef } from "react";
import { ScrollView, View, Text, StyleSheet, TextInput, TouchableOpacity, Modal, ActivityIndicator, Button, Image } from "react-native";
import { Formik } from "formik";
import { RadioButton } from 'react-native-paper';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { Icon } from '@rneui/themed';
import * as yup from 'yup';
import DateTimePicker from '@react-native-community/datetimepicker';
import { api } from "../shared/api";
import { launchImageLibrary } from 'react-native-image-picker';
import { Camera } from 'expo-camera';
import { GooglePlacesAutocomplete } from "react-native-google-places-autocomplete";



const ReportSchema = yup.object({
    reportSubject: yup.string().required('Tiêu đề không được bỏ trống').min(4, 'Ít nhất 4 ký tự'),
    reportBody: yup.string().required('Nội dung báo cáo không được bỏ trống').min(4, 'Ít nhất 4 ký tự'),
    issueLocation: yup.string().required('Địa điểm không được bỏ trống'),
    reportImpact: yup.string().required('Mức độ ảnh hưởng là bắt buộc').test('is-impact', 'Chọn mức độ ảnh hưởng', (value) => {
        const parsedValue = parseInt(value, 10);
        return parsedValue >= 0 && parsedValue <= 2;
    }),
    expectedResolutionDate: yup.date().required('Ngày dự kiến giải quyết là bắt buộc').test('is-future', 'Ngày dự kiến giải quyết phải trong tương lai', (value) => {
        return value >= new Date();
    }),
});

export default function ReportForm({ onFormSuccess }) {
    // date picker
    const [date, setDate] = React.useState(new Date());
    const [showDatePicker, setShowDatePicker] = useState(false);
    const [loading, setLoading] = useState(false);
    const [isCameraVisible, setCameraVisible] = useState(false);

    let cameraRef = useRef();

    const selectImage = (props) => {
        let options = {
            title: 'Chọn hoặc chụp ảnh',
            storageOptions: {
                skipBackup: true,
                path: 'images',
            },
            mediaType: 'photo',
            includeBase64: true,
            selectionLimit: 0, // set to 0 for unlimited selection
        };

        launchImageLibrary(options, (response) => {
            if (response.didCancel) {
                console.log('User cancelled image picker');
            } else if (response.error) {
                console.log('ImagePicker Error: ', response.error);
            } else if (response.assets) {
                const sources = response.assets.map(asset => `data:image/jpeg;base64,${asset.base64}`);
                props.setFieldValue('reportImages', [...props.values.reportImages, ...sources]);
            }
        });
    };

    const takePhoto = async (props) => {
        const { status } = await Camera.requestCameraPermissionsAsync();
        if (status === 'granted') {
            setCameraVisible(true)
            if (cameraRef.current) {
                try {
                    let photo = await cameraRef.current.takePictureAsync({
                        quality: 0.1,
                        base64: true,
                        exif: false
                    });
                    setCameraVisible(false);
                    const source = `data:image/jpeg;base64,${photo.base64}`;
                    props.setFieldValue('reportImages', [...props.values.reportImages, source]);

                } catch (error) {
                    console.log('Error taking picture: ', error);
                }
            } else {
                console.log('Camera ref is undefined or camera not ready');
            }
        } else {
            console.log('Camera permission not granted');
        }
    };

    return (

        <View style={styles.container}>

            <Formik
                initialValues={{
                    reportSubject: '',
                    reportBody: '',
                    issueLocation: '',
                    reportImages: [],
                    reportImpact: 'Thấp',
                    reportBodyHeight: 100,
                    expectedResolutionDate: new Date()
                }}
                validationSchema={ReportSchema}
                onSubmit={async (values, actions) => {
                    setLoading(true);
                    actions.resetForm();
                    const accessToken = await AsyncStorage.getItem("@accessToken");
                    const user = JSON.parse(await AsyncStorage.getItem("@user"));
                    const issuerEmail = user?.email;

                    // local test: http://192.168.1.7:45455/api/Report/GetReportsByUser?accessToken=
                    // server:     https://vesinhdanang.xyz:7024/api/Report/CreateReport
                    api.post('http://172.21.0.195:45455/api/Report/CreateReport', {
                        accessToken: accessToken,
                        issuerEmail: issuerEmail,
                        reportSubject: values.reportSubject,
                        reportBody: values.reportBody,
                        issueLocation: values.issueLocation,
                        reportImages: values.reportImages,
                        expectedResolutionDate: values.expectedResolutionDate,
                        reportImpact: parseInt(values.reportImpact, 10),
                    }, {
                        headers: {
                            'Content-Type': 'application/json',
                            'Authorization': `Bearer ${accessToken}`,
                            "Client-Type": "Mobile"
                        },
                    })
                        .then((response) => {
                            setLoading(false);
                            onFormSuccess();
                        })
                        .catch((error) => {
                            setLoading(false);
                            console.error('Error:', error);
                        });
                }}
            >
                {(props) => (
                    <>


                        <ScrollView
                            showsVerticalScrollIndicator={false}
                            showsHorizontalScrollIndicator={false}
                            overScrollMode='never'
                            padding={7}
                        >

                            <TextInput
                                style={styles.input}
                                placeholder='Tiêu đề'
                                onChangeText={props.handleChange('reportSubject')}
                                value={props.values.reportSubject}
                                onBlur={props.handleBlur('reportSubject')}
                            />
                            <Text style={styles.errorText}>
                                {props.touched.reportSubject && props.errors.reportSubject}
                            </Text>

                            <TextInput
                                style={styles.input}
                                placeholder='Địa điểm'
                                onChangeText={props.handleChange('issueLocation')}
                                value={props.values.issueLocation}
                                onBlur={props.handleBlur('issueLocation')}
                            />
                            <Text style={styles.errorText}>
                                {props.touched.reportSubject && props.errors.reportSubject}
                            </Text>


                            <View style={styles.impact}>
                                <Text style={styles.label}>Mức độ ảnh hưởng</Text>
                                <RadioButton.Group
                                    onValueChange={props.handleChange('reportImpact')}
                                    value={props.values.reportImpact}
                                    onBlur={props.handleBlur('reportImpact')}
                                >
                                    <View style={styles.radioButton}>
                                        <Text style={styles.radioButtonTextLow}>THẤP</Text>
                                        <RadioButton value="0" color='green' uncheckedColor='grey' />
                                    </View>
                                    <View style={styles.radioButton}>
                                        <Text style={styles.radioButtonTextMedium}>VỪA</Text>
                                        <RadioButton value="1" color='orange' uncheckedColor='grey' />

                                    </View>
                                    <View style={styles.radioButton}>
                                        <Text style={styles.radioButtonTextHigh}>CAO</Text>
                                        <RadioButton value="2" color='red' uncheckedColor='grey' />

                                    </View>
                                </RadioButton.Group>
                            </View>
                            <Text style={styles.errorText}>{props.touched.reportImpact && props.errors.reportImpact}</Text>
                            <Text style={styles.label}>Cần giải quyết trước</Text>
                            <View style={styles.dateContainer}>
                                <Text style={styles.dateValue}>
                                    {props.values.expectedResolutionDate.toLocaleDateString()}
                                </Text>
                                {Platform.OS === 'android' && (
                                    <TouchableOpacity onPress={() => setShowDatePicker(true)}>
                                        <Icon name="calendar" type='font-awesome' size={24} color="#2282F3" />
                                    </TouchableOpacity>
                                )}
                            </View>

                            {showDatePicker && (
                                <DateTimePicker
                                    value={props.values.expectedResolutionDate}
                                    mode="date"
                                    onBlur={props.handleBlur('expectedResolutionDate')}
                                    display="default"
                                    onChange={(event, selectedDate) => {
                                        setShowDatePicker(Platform.OS === 'ios');
                                        props.setFieldValue('expectedResolutionDate', selectedDate || props.values.expectedResolutionDate);
                                    }}
                                />
                            )}
                            <Text style={styles.errorText}>{props.touched.expectedResolutionDate && props.errors.expectedResolutionDate}</Text>

                            {/* body */}
                            <TextInput
                                multiline
                                style={[styles.bodyInput, { height: Math.max(100, props.values.reportBodyHeight) }]}
                                placeholder='Chi tiết báo cáo'
                                onChangeText={props.handleChange('reportBody')}
                                onContentSizeChange={(event) => {
                                    props.setFieldValue('reportBodyHeight', event.nativeEvent.contentSize.height)
                                }}
                                value={props.values.reportBody}
                                onBlur={props.handleBlur('reportBody')}
                            />
                            <Text style={styles.errorText}>{props.touched.reportBody && props.errors.reportBody}</Text>

                            {/* image */}

                            <View style={styles.fileStyle}>
                                <TouchableOpacity
                                    style={styles.imageButton}
                                    onPress={() => selectImage(props)}
                                >
                                    <Icon name="photograph" type="fontisto" size={24} color="green" />
                                </TouchableOpacity>

                                <TouchableOpacity
                                    style={styles.imageButton}
                                    onPress={() => takePhoto(props)} // take photo with camera
                                >
                                    <Icon name="camera" type="font-awesome" size={24} color="green" />
                                </TouchableOpacity>
                                <Text style={styles.fileText}>Thêm ảnh vào báo cáo</Text>
                            </View>


                            <View style={{ flexDirection: 'row', flexWrap: 'wrap' }}>
                                {props.values.reportImages && props.values.reportImages.map((image, index) => (
                                    <View key={index} style={styles.imageContainer}>
                                        <Image
                                            source={{ uri: image }}
                                            style={styles.image}
                                        />
                                        <TouchableOpacity
                                            style={styles.removeImageButton}
                                            onPress={() => {
                                                const newImages = [...props.values.reportImages];
                                                newImages.splice(index, 1);
                                                props.setFieldValue('reportImages', newImages);
                                            }}
                                        >
                                            <Icon name="remove" size={20} type="Ionicons" style={styles.rmvImg} color="#fff" />
                                        </TouchableOpacity>
                                    </View>
                                ))}
                            </View>


                            <TouchableOpacity style={styles.submitButton} onPress={props.handleSubmit}>
                                <Icon name="check" size={20} color="#fff" />
                                <Text style={styles.submitButtonText}>Gửi</Text>
                            </TouchableOpacity>

                        </ScrollView>
                        {isCameraVisible && (
                            <Camera
                                ref={cameraRef}
                                style={StyleSheet.absoluteFillObject}
                            >
                                <View style={{ flex: 1, backgroundColor: 'transparent', justifyContent: 'flex-end', alignItems: 'center' }}>
                                    <TouchableOpacity
                                        style={{
                                            marginBottom: 20,
                                            width: 70,
                                            height: 70,
                                            borderRadius: 35,
                                            backgroundColor: 'rgba(255,255,255, 0.1)',
                                            justifyContent: 'center',
                                            alignItems: 'center',
                                        }}
                                        onPress={() => takePhoto(props)}
                                    >
                                        <Icon name="circle-o" type="font-awesome" size={64} color='white' />
                                    </TouchableOpacity>
                                </View>
                            </Camera>
                        )}
                    </>
                )}


            </Formik>


            {/* loading screen */}
            <Modal
                transparent={true}
                visible={loading}
            >
                <View style={{
                    flex: 1,
                    backgroundColor: 'rgba(202,247,183,0.5)',
                    justifyContent: 'center',
                    alignItems: 'center'
                }}>
                    <ActivityIndicator size="large" color="green" />
                </View>
            </Modal>


        </View>
    );
}

const styles = StyleSheet.create({
    errorText: {
        color: '#FF0000', // Bright red color for better visibility
        marginBottom: 10,
        marginTop: -10,
        fontSize: 12, // Larger font size for better readability
        fontWeight: 'bold'
    },
    dateContainer: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: 10,
    },
    dateValue: {
        fontSize: 16,
        fontWeight: '500',
        color: '#2282F3',
        marginBottom: 10,
        backgroundColor: '#f0f0f0',
        padding: 10,
        borderRadius: 5,
    },
    container: {
        flex: 1,
        padding: 20,
        backgroundColor: '#f5f5f5',
    },
    input: {
        borderWidth: 1,
        borderColor: '#ddd',
        padding: 10,
        fontSize: 18,
        borderRadius: 6,
        marginBottom: 10,
        backgroundColor: 'white',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.3,
        shadowRadius: 2,
        elevation: 5,
    },
    bodyInput: {
        borderWidth: 1,
        borderColor: '#ddd',
        padding: 10,
        fontSize: 18,
        borderRadius: 6,
        marginBottom: 10,
        backgroundColor: 'white',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.3,
        shadowRadius: 2,
        elevation: 5,
    },
    impact: {
        justifyContent: 'space-between',
        marginBottom: 10,
        padding: 8,
        borderRadius: 6,

    },
    radioButton: {
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'space-between',
        padding: 10,
    },
    radioButtonTextLow: {
        fontSize: 16,
        fontWeight: '900',
        color: 'green',
    },
    radioButtonTextMedium: {
        fontSize: 16,
        fontWeight: '900',
        color: 'orange',
    },
    radioButtonTextHigh: {
        fontSize: 16,
        fontWeight: '900',
        color: 'red',
    },
    label: {
        marginBottom: 20,
        fontSize: 18,
        fontWeight: 'bold',
        color: 'grey',
    },
    submitButton: {
        flexDirection: 'row',
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: '#2282F3',
        padding: 10,
        borderRadius: 15,
        marginVertical: 20,
    },
    submitButtonText: {
        color: '#fff',
        marginLeft: 10,
        fontSize: 18,
        fontWeight: 'bold',

    },
    image: {
        width: 75,
        height: 75,
        borderRadius: 15,
        marginBottom: 10,
    },
    imageButton: {
        width: 50,
        height: 50,
        borderRadius: 25,
        alignItems: 'center',
        justifyContent: 'center',
    },
    imageContainer: {
        position: 'relative',
        paddingRight: 10,
    },
    removeImageButton: {
        position: 'absolute',
        right: 0,
        top: 0,
        width: 30,
        height: 30,
        borderRadius: 30,
        backgroundColor: 'rgba(0,0,0,0.5)',
        alignItems: 'center',
        justifyContent: 'center',
    },
    rmvImg: {
        borderRadius: 25,
    },
    fileStyle: {
        flexDirection: 'row',
        backgroundColor: 'whitesmoke',
        marginBottom: 10,
        paddingHorizontal: 10,
        justifyContent: 'space-between',
        borderRadius: 15,
        borderColor: '#C7C7C7',
        alignItems: 'center',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.1,
        shadowRadius: 1,
        elevation: 1,
    },
    fileText: {
        fontSize: 14,
        padding: 10,
        fontWeight: 'bold',
        color: 'grey',
    },
});