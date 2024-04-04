import GoogleMapReact, { Coords } from "google-map-react";
import Marker from "../../../public/assets/marker.svg";
import { KeyboardEventHandler, useState } from "react";
import "./style.scss";
import axios from "axios";

<script
  src="https://maps.googleapis.com/maps/api/js?key=AIzaSyB41DRUbKWJHPxaFjMAwdrzWzbVKartNGg&callback=initMap&v=weekly"
  defer
></script>;
const AnyReactComponent = (props: any) => (
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
  // const [treeLocation, setTreeLocation] = useState<MarkerType>({
  //   lat: 0.0,
  //   lng: 0,
  // });
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
          />
        }
      </GoogleMapReact>
    </div>
  );
}

type GoogleMapProps = {
  onChange?: (location: Coords) => void;
};
export function GoogleMap(props: GoogleMapProps) {
  const [show, setShow] = useState(false);
  const [location, setLocation] = useState<Coords>({
    lat: 16.041871,
    lng: 108.216446,
  });

  const [address, setAddress] = useState<string>("");

  const defaultProps = {
    center: {
      lat: 16.041871,
      lng: 108.216446,
    },
    zoom: 15,
  };

  const onSave = () => {
    props.onChange && props.onChange(location);
    setShow(false);
  };
  console.log(location);

  if (!show)
    return (
      <button
        type="button"
        className="btn btn-click"
        onClick={() => setShow(true)}
      >
        Open
      </button>
    );

  const onKeyDown = async (e: any) => {
    if (e.keyCode !== 13) return;
    const addressURI = encodeURI(address);
    const res = await axios.get(
      `https://maps.googleapis.com/maps/api/geocode/json?address=${addressURI}&key=AIzaSyBqQfxxgCjLvTq9tCGjnjHxCVnX3acWXmY`
    );
    const data = res.data;
    if (data) {
      setLocation(data.result[0].geometry.location);
    }
  };

  return (
    // Important! Always set the container height explicitly
    <div className="google-map">
      <div className="form-group address">
        <label htmlFor="exampleInputEmail1">Address</label>
        <input
         
          className="form-control"
          id="exampleInputEmail1"
          aria-describedby="emailHelp"
          placeholder="Enter email"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
          onKeyDown={(e) => onKeyDown(e)}
        />
        <small id="emailHelp" className="form-text text-muted">
          We'll never share your email with anyone else.
        </small>
      </div>
      <GoogleMapReact
        bootstrapURLKeys={{ key: "AIzaSyBqQfxxgCjLvTq9tCGjnjHxCVnX3acWXmY" }}
        defaultCenter={defaultProps.center}
        center={location}
        defaultZoom={defaultProps.zoom}
        onClick={(value) => setLocation(value)}
      >
        {<AnyReactComponent {...location} />}
      </GoogleMapReact>
      <button
        type="button"
        className="btn btn-success btn-save"
        onClick={onSave}
      >
        Save
      </button>
    </div>
  );
}
