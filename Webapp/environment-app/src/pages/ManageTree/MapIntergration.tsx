import React, { useEffect, useState } from "react";
import GoogleMapReact from "google-map-react";
import { TREE_DETAIL, useApi } from "../../Api";
import Marker from "../../../public/assets/marker.svg";

<script
  src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg&callback=initMap&v=weekly"
  defer
></script>;
const AnyReactComponent = ({ text }) => (
  <div>
    <img src={Marker} style={{ width: "30px" }} alt="" />
  </div>
);

export type MarkerType = {
  lat: number;
  lng: number;
};

export default function SimpleMap() {
  // const [data, setData] = useState<any>();
  const [treeLocation, setTreeLocation] = useState<MarkerType>({
    lat: 0.0,
    lng: 0,
  });
  const defaultProps = {
    center: {
      lat: 16.041871,
      lng: 108.216446,
    },
    zoom: 15,
  };

  return (
    // Important! Always set the container height explicitly
    <div
      style={{
        height: "60vh",
        width: "100%",
        display: "flex",
        alignItems: "center",
      }}
    >
      <GoogleMapReact
        bootstrapURLKeys={{ key: "AIzaSyBqQfxxgCjLvTq9tCGjnjHxCVnX3acWXmY" }}
        defaultCenter={defaultProps.center}
        defaultZoom={defaultProps.zoom}
      >
        {
          <AnyReactComponent
            lat={16.041871} // Lấy tọa độ lat từ treeLocation
            lng={108.216446} // Lấy tọa độ lng từ treeLocation
            text="Tree Location" // Đặt text cho marker
          />
        }
      </GoogleMapReact>
    </div>
  );
}
