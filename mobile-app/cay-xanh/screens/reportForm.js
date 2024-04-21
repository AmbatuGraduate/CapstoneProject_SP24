import React, { useState, useRef } from "react";
import { FlatList, View, Text, StyleSheet, TextInput, TouchableOpacity, Modal, ActivityIndicator, Button, Image, TouchableHighlight } from "react-native";
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
import * as Location from 'expo-location';
import Geocoder from "react-native-geocoding";



Geocoder.init('AIzaSyDpDsjnmVkeWs0myDwrK0dHgj_fFMXYAIo');

const ReportSchema = yup.object({
    reportSubject: yup.string().required('Nhập tiêu đề báo cáo').min(4, 'Ít nhất 4 ký tự'),
    reportBody: yup.string().required('Nhập nội dung chi tiết báo cáo').min(4, 'Ít nhất 4 ký tự'),
    reportImpact: yup.string().required('Mức độ ảnh hưởng là bắt buộc').test('is-impact', 'Chọn mức độ ảnh hưởng', (value) => {
        const parsedValue = parseInt(value, 10);
        return parsedValue >= 0 && parsedValue <= 2;
    }),
    expectedResolutionDate: yup.date().required('Hãy nhập ngày cần giải quyết').test('is-future', 'Ngày cần giải quyết phải trong tương lai', (value) => {
        return value >= new Date();
    }),
});

export default function ReportForm({ onFormSuccess }) {
    // date picker
    const [date, setDate] = React.useState(new Date());
    const [showDatePicker, setShowDatePicker] = useState(false);
    const [loading, setLoading] = useState(false);
    const [isCameraVisible, setCameraVisible] = useState(false);
    const [isCameraReady, setIsCameraReady] = useState(false);
    const [issueLocation, setIssueLocation] = useState('');
    const autocompleteRef = useRef(null);
    const [modalVisible, setModalVisible] = useState(false);
    const [locationOption, setLocationOption] = useState(null);

    const getLocation = async () => {
        let { status } = await Location.requestForegroundPermissionsAsync();
        if (status !== 'granted') {
            alert('Thiết bị đã từ chối quyền đọc địa chỉ của ứng dụng!');
            return;
        }

        let location = await Location.getCurrentPositionAsync({});
        const { latitude, longitude } = location.coords;

        // Reverse geocoding
        Geocoder.from(latitude, longitude)
            .then(json => {
                var addressComponent = json.results[0].formatted_address;
                console.log(addressComponent);
                setIssueLocation(addressComponent);
            })
            .catch(error => console.warn(error));
    };


    let cameraRef = useRef();

    const handleCameraReady = () => {
        setIsCameraReady(true);
    };

    const selectImage = (props) => {
        let options = {
            title: 'Chọn hoặc chụp ảnh',
            storageOptions: {
                skipBackup: true,
                path: 'images',
            },
            mediaType: 'photo',
            includeBase64: true,
            selectionLimit: 0,
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

    const requestCameraPermissions = async () => {
        const { status } = await Camera.requestCameraPermissionsAsync();
        if (status === 'granted') {
            setCameraVisible(true);
        } else {
            console.log('Camera permission not granted');
        }
    };

    const takePhoto = async (props) => {
        if (cameraRef.current && isCameraReady) {
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
    };

    return (
        <View style={styles.container}>

            <Formik
                initialValues={{
                    reportSubject: '',
                    reportBody: '',
                    issueLocation: issueLocation,
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
                    api.post('https://vesinhdanang.xyz:7024/api/Report/CreateReport', {
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


                        <FlatList
                            showsVerticalScrollIndicator={false}
                            showsHorizontalScrollIndicator={false}
                            overScrollMode='never'
                            data={[{ key: 'form' }]}
                            padding={7}

                            renderItem={() => (
                                <>
                                    <Text style={styles.inputLabel}>
                                        Tiêu đề báo cáo
                                    </Text>
                                    <TextInput
                                        style={styles.input}
                                        placeholder="Ví dụ: Rác thải không được xử lý"
                                        onChangeText={props.handleChange('reportSubject')}
                                        value={props.values.reportSubject}
                                        onBlur={props.handleBlur('reportSubject')}
                                    />
                                    <Text style={styles.errorText}>
                                        {props.touched.reportSubject && props.errors.reportSubject}
                                    </Text>

                                    <Text style={styles.inputLabel}>
                                        Vị trí cụ thể
                                    </Text>



                                    <Modal
                                        animationType="slide"
                                        transparent={true}
                                        visible={modalVisible}
                                        onRequestClose={() => {
                                            setModalVisible(!modalVisible);
                                        }}
                                    >
                                        <View style={styles.centeredView}>
                                            <View style={styles.modalView}>
                                                <TouchableHighlight
                                                    style={styles.modalButton}
                                                    onPress={() => {
                                                        setLocationOption('manual');
                                                        setModalVisible(!modalVisible);
                                                    }}
                                                >
                                                    <View style={styles.modalButtonText}>
                                                        <Icon style={styles.modalIcon} name="edit" type="font-awesome" size={20} color="palegreen" />
                                                        <Text style={styles.textStyle}>Nhập vị trí</Text>
                                                    </View>
                                                </TouchableHighlight>

                                                <TouchableHighlight
                                                    style={styles.modalButton}
                                                    onPress={() => {
                                                        getLocation();
                                                        setLocationOption('current');
                                                        setModalVisible(!modalVisible);
                                                        props.setFieldValue('issueLocation', issueLocation);

                                                    }}
                                                >
                                                    <View style={styles.modalButtonText}>
                                                        <Icon style={styles.modalIcon} name="map-marker" type="font-awesome" size={20} color="lightgreen" />
                                                        <Text style={styles.textStyle}>Vị trí hiện tại</Text>
                                                    </View>

                                                </TouchableHighlight>
                                            </View>
                                        </View>
                                    </Modal>
                                    <TouchableOpacity style={styles.buttonMapStyle} onPress={() => setModalVisible(true)}>
                                        <Icon name="map" size={30} color="green" />
                                        <Text style={styles.buttonMapText}>Chọn vị trí</Text>
                                    </TouchableOpacity>

                                    {locationOption === 'manual' && (
                                        <GooglePlacesAutocomplete
                                            placeholder='Ví dụ: 123 Nguyễn Văn Linh, Đà Nẵng'
                                            keepResultsAfterBlur={true}
                                            onFail={error => console.error(error)}
                                            onPress={(data, details = null) => {
                                                console.log('Pressed!');
                                                props.setFieldValue('issueLocation', data.description);
                                            }}
                                            query={{
                                                key: 'AIzaSyDpDsjnmVkeWs0myDwrK0dHgj_fFMXYAIo',
                                                language: 'vi',
                                                components: 'country:vn',
                                                location: '16.0544,108.2022', // danang
                                                radius: '50000',
                                            }}
                                            fetchDetails={true}
                                            styles={{
                                                textInputContainer: styles.inputAddress,
                                            }}
                                        />
                                    )}

                                    {locationOption === 'current' && (
                                        <View style={styles.currentLocation}>
                                            <Text style={styles.currentLocationText}>{issueLocation}</Text>
                                        </View>
                                    )}

                                    {/* <GooglePlacesAutocomplete
                                        placeholder='Ví dụ: 123 Nguyễn Văn Linh, Đà Nẵng'
                                        keepResultsAfterBlur={true}
                                        onFail={error => console.error(error)}
                                        onPress={(data, details = null) => {
                                            console.log('Pressed!');
                                            setIssueLocation(data.description);
                                        }}
                                        query={{
                                            key: 'AIzaSyDpDsjnmVkeWs0myDwrK0dHgj_fFMXYAIo',
                                            language: 'vi',
                                            components: 'country:vn',
                                            location: '16.0544,108.2022', // danang
                                            radius: '50000',
                                        }}
                                        fetchDetails={true}
                                        styles={{
                                            textInputContainer: styles.inputAddress,
                                        }}
                                        textInputProps={{
                                            value: issueLocation,
                                            onChangeText: (text) => setIssueLocation(text)
                                        }}
                                    />
                                    <Text style={{ alignSelf: 'center' }}>Hoặc</Text>
                                    <View>
                                        <Button title="Vị trí hiện tại" onPress={getLocation} />
                                    </View>
                                    <Text>
                                        {issueLocation}</Text> */}



                                    <Text style={styles.errorText}>
                                        {props.touched.issueLocation && props.errors.issueLocation}
                                    </Text>
                                    <View style={styles.impact}>
                                        <Text style={styles.inputLabel}>Mức độ ảnh hưởng</Text>
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
                                        <View style={{
                                            height: 3,
                                            width: props.values.reportImpact === '0' ? '33%' : props.values.reportImpact === '1' ? '66%' : '100%',
                                            backgroundColor: props.values.reportImpact === '0' ? 'green' : props.values.reportImpact === '1' ? 'orange' : props.values.reportImpact === '2' ? 'red' : 'white',
                                        }} />
                                    </View>
                                    <Text style={styles.errorText}>{props.touched.reportImpact && props.errors.reportImpact}</Text>
                                    <Text style={styles.inputLabel}>Cần giải quyết trước ngày</Text>
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
                                    <Text style={styles.inputLabel}>
                                        Nội dung báo cáo
                                    </Text>
                                    <TextInput
                                        multiline
                                        style={[styles.bodyInput, { height: Math.max(100, props.values.reportBodyHeight) }]}
                                        placeholder='Ví dụ: Rác thải không được xử lý ở khu vực công viên 29/3, Đà Nẵng'
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
                                            onPress={requestCameraPermissions}
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
                                        <Text style={styles.submitButtonText}>Gửi Báo Cáo</Text>
                                    </TouchableOpacity>
                                </>
                            )}
                        />
                        {isCameraVisible && (
                            <Camera
                                ref={cameraRef}
                                style={StyleSheet.absoluteFillObject}
                                onCameraReady={handleCameraReady}

                            >
                                <View style={{
                                    flex: 1,
                                    flexDirection: 'column',
                                    justifyContent: 'flex-end',
                                    alignItems: 'center',
                                    marginBottom: 20,
                                }}>
                                    <TouchableOpacity
                                        style={{
                                            position: 'absolute',
                                            left: 0,
                                            bottom: 0,
                                            width: 70,
                                            height: 70,
                                            borderRadius: 35,
                                            justifyContent: 'center',
                                            alignItems: 'center',
                                        }}
                                        onPress={() => setCameraVisible(false)}
                                    >
                                        <Icon name="remove" type="font-awesome" size={48} color='rgba(255,255,255,0.4)' />
                                    </TouchableOpacity>
                                    <TouchableOpacity
                                        style={{
                                            width: 70,
                                            height: 70,
                                            borderRadius: 35,
                                            backgroundColor: 'rgba(216,216,216, 0.1)',
                                            justifyContent: 'center',
                                            alignItems: 'center',
                                        }}
                                        onPress={() => takePhoto(props)}
                                    >
                                        <Icon name="circle-o" type="font-awesome" size={64} color='rgba(255,255,255,0.4)' />
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


        </View >
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
        fontSize: 14,
        marginBottom: 10,
        backgroundColor: 'white',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.3,
        shadowRadius: 2,
        elevation: 5,
        borderRadius: 10,
        borderWidth: 1.5,
        borderColor: 'grey',
        paddingLeft: 20,
    },
    inputAddress: {
        borderColor: '#ddd',
        padding: 2,
        fontSize: 14,
        marginBottom: 10,
        backgroundColor: 'white',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.3,
        shadowRadius: 2,
        elevation: 5,
        borderRadius: 10,
        borderWidth: 1.5,
        borderColor: 'grey',
        paddingLeft: 8,
    },
    bodyInput: {
        borderWidth: 1,
        borderColor: '#ddd',
        padding: 10,
        fontSize: 14,
        marginBottom: 10,
        backgroundColor: 'white',
        shadowColor: '#000',
        shadowOffset: { width: 0, height: 1 },
        shadowOpacity: 0.3,
        shadowRadius: 2,
        elevation: 5,
        borderRadius: 10,
        borderWidth: 2,
        borderColor: '#333',
    },
    impact: {
        justifyContent: 'space-between',
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
        padding: 12,
        borderRadius: 15,
        marginVertical: 20,
        width: '70%',
        alignSelf: 'center',
    },
    submitButtonText: {
        color: '#fff',
        marginLeft: 10,
        fontSize: 16,
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
    inputLabel: {
        fontSize: 15,
        fontWeight: 'bold',
        marginBottom: 5,

    },
    centeredView: {
        flex: 1,
        justifyContent: "center",

    },
    modalView: {
        margin: 2,
        backgroundColor: "rgba(0, 0, 0, 0.6)",
        borderRadius: 4,
        padding: 20,
        alignItems: "center",
    },
    modalText: {
        marginBottom: 15,
        textAlign: "center",
        fontSize: 18,
        fontWeight: 'bold',
    },
    modalButton: {
        padding: 10,
        marginTop: 5,
        backgroundColor: "rgba(0, 0, 0, 1)",
        width: '100%',

    },
    modalButtonText: {
        color: "white",
        fontWeight: "bold",
        textAlign: "center",
        flexDirection: 'row',
        alignItems: 'center',
        justifyContent: 'center',
    },
    textStyle: {
        color: "whitesmoke",
        textAlign: "center",
        fontSize: 22,
        paddingLeft: 10,
        fontFamily: 'quolibet',
    },
    buttonMapStyle: {
        flexDirection: 'row',
        alignItems: 'center',
        backgroundColor: '#DDDDDD',
        padding: 10,
        borderRadius: 5,
        marginBottom: 10,
        justifyContent: 'center',
    },
    buttonMapText: {
        marginLeft: 10,
        fontSize: 16,
        fontWeight: 'bold',
        color: 'darkgreen',
    },
    currentLocation: {
        padding: 10,
        borderRadius: 5,
    },
    currentLocationText: {
        fontSize: 16,
        fontWeight: '500',
        color: 'green',
        textDecorationLine: 'underline',
    },
});