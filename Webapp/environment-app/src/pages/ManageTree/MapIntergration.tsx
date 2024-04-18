import GoogleMapReact, { Coords } from "google-map-react";
import Marker from "../../../public/assets/marker.svg";
import { KeyboardEventHandler, useEffect, useRef, useState } from "react";
import axios from "axios";

<script
  src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBz_TEfwMOJ-vvMd4Z8r0F365Z9YdUMQiw&v=weekly"
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
type SimpleMapProps = {
  location?: string;
};
export default function SimpleMap(props: SimpleMapProps) {
  const [treeLocation, setTreeLocation] = useState<MarkerType>({
    lat: 16.041871,
    lng: 108.216446,
  });

  const fetch = async () => {
    const addressURI = encodeURI(props?.location || "Da nang");
    const res = await axios.get(
      `https://maps.googleapis.com/maps/api/geocode/json?address=${addressURI}&key=AIzaSyBz_TEfwMOJ-vvMd4Z8r0F365Z9YdUMQiw`
    );
    const data = res.data;
    console.log(data);
    if (data) {
      if (data) setTreeLocation(data.results?.[0].geometry.location);
    }
  };

  useEffect(() => {
    fetch();
  }, []);

  return (
    // Important! Always set the container height explicitly
    <div
      style={{
        height: "100%",
        width: "100%",
        padding: "3rem",
      }}
    >
      <GoogleMapReact
        bootstrapURLKeys={{ key: "AIzaSyD6azBbhclWDNTysvGxe9fk6A0s97mFOq8" }}
        defaultZoom={15}
        center={treeLocation}
      >
        {
          <AnyReactComponent
            lat={treeLocation.lat} // Lấy tọa độ lat từ treeLocation
            lng={treeLocation.lng} // Lấy tọa độ lng từ treeLocation
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
  const autocompleteInputRef = useRef<HTMLInputElement>(null);

  let autocomplete: google.maps.places.Autocomplete;

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
    console.log("sadasd");
    if (e.keyCode !== 13) return;
    // const addressURI = encodeURI(address);
    // const res = await axios.get(
    //   `https://maps.googleapis.com/maps/api/geocode/json?address=${addressURI}&key=AIzaSyBqQfxxgCjLvTq9tCGjnjHxCVnX3acWXmY`
    // );
    // const data = res.data;
    // console.log(data);
    // if (data) {
    //   setLocation(data.result[0].geometry.location);
    // }
    const center = { lat: 16.047079, lng: 108.20623 };
    const defaultBounds = {
      north: center.lat + 0.1,
      south: center.lat - 0.1,
      east: center.lng + 0.1,
      west: center.lng - 0.1,
    };
    const input = e.target as HTMLInputElement;
    const options = {
      bounds: defaultBounds,
      componentRestrictions: { country: "VN" },
      fields: ["address_components", "geometry", "icon", "name"],
      strictBounds: false,
    };
    const autocomplete = new window.google.maps.places.Autocomplete(
      input,
      options
    );

    autocomplete.addListener("place_changed", () => {
      const place = autocomplete.getPlace();
      console.log(place);
      if (place.geometry && place.geometry.location) {
        const latitude = place.geometry.location.lat();
        const longitude = place.geometry.location.lng();

        setLocation({ lat: latitude, lng: longitude });
        console.log("Latitude:", latitude);
        console.log("Longitude:", longitude);
      }
    });
  };

  return (
    <div className="google-map">
      <div className="form-group address">
        {/* <label htmlFor="exampleInputEmail1">Tuyến đường</label> */}
        <input
          ref={autocompleteInputRef}
          className="form-control"
          id="exampleInputEmail1"
          aria-describedby="emailHelp"
          placeholder="Nhập địa chỉ"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
          onKeyDown={(e) => onKeyDown(e)}
        />
        {/* <small id="emailHelp" className="form-text text-muted">
          We'll never share your email with anyone else.
        </small> */}
      </div>
      <GoogleMapReact
        bootstrapURLKeys={{ key: "AIzaSyBz_TEfwMOJ-vvMd4Z8r0F365Z9YdUMQiw" }}
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
