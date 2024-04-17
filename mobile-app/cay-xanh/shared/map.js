import React, { useState, useEffect } from "react";
import { View, StyleSheet } from "react-native";
import MapView, { PROVIDER_GOOGLE, Marker } from 'react-native-maps';
import Geocoder from "react-native-geocoding";


Geocoder.init('AIzaSyD6azBbhclWDNTysvGxe9fk6A0s97mFOq8');

export default function SharedMap({ route }) {
    const locationParam = route.params.issueLocation || route.params.address;

    const [location, setLocation] = useState(null);
    useEffect(() => {
        if (locationParam) {
            Geocoder.from(locationParam)
                .then(json => {
                    var location = json.results[0].geometry.location;
                    setLocation(location);
                })
                .catch(error => console.warn(error));
        }
    }, [locationParam]);

    return (
        <View style={styles.container}>
            {location && (
                <MapView
                    provider={PROVIDER_GOOGLE}
                    style={styles.map}
                    initialRegion={{
                        latitude: location.lat,
                        longitude: location.lng,
                        latitudeDelta: 0.0922,
                        longitudeDelta: 0.0421,
                    }}
                >
                    <Marker
                        coordinate={{
                            latitude: location.lat,
                            longitude: location.lng,
                        }}
                        title="Vị trí sự cố"
                    />
                </MapView>
            )}
        </View>
    );
}

const styles = StyleSheet.create({
    container: {
        flex: 1,
    },
    map: {
        ...StyleSheet.absoluteFillObject,
    },
});