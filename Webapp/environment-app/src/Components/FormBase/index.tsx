import React, { useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useApi } from "../../Api";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "./style.scss";

export type Field = {
  label: string;
  key: string;
  defaultValue?: any;
  affectValue?: any;
  affectDate?: Date;
  googleAddress?: boolean;
  selected?: Date;
  value?: any;
  placeholder?: string;
  formType: "input" | "select" | "textarea" | "number" | "date";
  options?: Option[];
  required?: boolean;
  disabled?: boolean;
  optionExtra?: OptionExtra;
  onChange?: (event: React.ChangeEvent<any>) => void;
};

export type OptionExtra = {
  url: string;
  _key: string;
  _value: string;
};

export type Option = {
  key: string;
  value: any;
};

type Props = {
  fields: Field[];
  onSave?: (data: Record<string, unknown>) => void;
  onCancel?: () => void;
  backPage?: () => void;
  navigateUpdate?: () => void;
  mode?: "view" | "create&update";
};

export const FormBase = (props: Props) => {
  const {
    fields,
    onSave,
    onCancel,
    mode = "create&update",
    backPage,
    navigateUpdate,
  } = props;

  const FormType = ({ props }: { props: Field }) => {
    const { formType, options, key, disabled, optionExtra, ...rest } = props;
    const _disabled = mode == "view" ? true : disabled;
    const [_options, setOptions] = useState<Option[]>();
    const [startDate, setStartDate] = useState<Date | null>(
      props.selected || new Date()
    );
    const [places, setPlaces] = useState([]);

    useEffect(() => {
      if (optionExtra) {
        fetchDataForFormSelect(optionExtra);
      }
    }, []);

    useEffect(() => {
      const currentTime = props.affectDate || new Date();
      currentTime.setMonth(
        currentTime.getMonth() + (Number(props.affectValue) || 0)
      );
      setStartDate(currentTime);
    }, [props.affectValue]);

    useEffect(() => {
      if (props.googleAddress) {
        const center = { lat: 16.047079, lng: 108.20623 };
        // Create a bounding box with sides ~10km away from the center point
        const defaultBounds = {
          north: center.lat + 0.1,
          south: center.lat - 0.1,
          east: center.lng + 0.1,
          west: center.lng - 0.1,
        };
        const input = document.getElementById("pac-input");
        const options = {
          bounds: defaultBounds,
          componentRestrictions: { locality: "Da Nang" },
          fields: ["address_components", "geometry", "icon", "name"],
          strictBounds: false,
        };
        const autocomplete = new window.google.maps.places.Autocomplete(
          input,
          options
        );

        // Add event listener to handle place selection
        autocomplete.addListener("place_changed", () => {
          const place = autocomplete.getPlace();
          console.log(place); // Handle the selected place here
          if (place.geometry && place.geometry.location) {
            const latitude = place.geometry.location.lat();
            const longitude = place.geometry.location.lng();
            console.log("Latitude:", latitude);
            console.log("Longitude:", longitude);
            // Xử lý tọa độ latitude và longitude ở đây
          }
        });
        console.log(places);
      }
    }, [props.value]);

    const fetchDataForFormSelect = async (option: OptionExtra) => {
      const res = await useApi.get(option.url);
      const _options = res.data?.map((obj: any) => ({
        key: obj[option._key],
        value: obj[option._value],
      }));
      setOptions(_options);
    };

    switch (formType) {
      case "input":
        return (
          <Form.Control
            id={props.googleAddress == true ? "pac-input" : ""}
            type="text"
            {...rest}
            name={key}
            disabled={_disabled}
          />
        );
      case "number":
        return (
          <Form.Control
            type="number"
            {...rest}
            name={key}
            disabled={_disabled}
          />
        );
      case "textarea":
        return (
          <Form.Control
            {...rest}
            as="textarea"
            rows={3}
            name={key}
            disabled={_disabled}
          />
        );
      case "select":
        return (
          <Form.Select name={key} {...rest} disabled={_disabled}>
            {(optionExtra ? _options : options)?.map((option, idx) => (
              <option key={idx} value={option.value}>
                {option.key}
              </option>
            ))}
          </Form.Select>
        );

      case "date":
        return (
          <DatePicker
            selected={startDate}
            onChange={(date) => setStartDate(date)}
            className="datepicker"
            name={key}
            disabled={_disabled}
            dateFormat="dd/MM/yyyy"
          />
        );
      default:
        return (
          <Form.Control type="text" {...rest} name={key} disabled={_disabled} />
        );
    }
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const data: Record<string, unknown> = {};
    fields.forEach((f) => {
      data[f.key] = (e.target as any)?.[f.key].value;
    });
    console.log(data);
    onSave && onSave(data);
  };

  return (
    <Form onSubmit={handleSubmit} className="form-base">
      {fields.map((f, idx) => {
        return (
          <Form.Group className="mb-3" controlId={f.key} key={idx}>
            <Form.Label>{f.label}</Form.Label>
            <FormType props={f} />
          </Form.Group>
        );
      })}
      {mode == "create&update" ? (
        <div>
          <Button className="btnSave" type="submit">
            Lưu
          </Button>
          <Button className="btnCancel" variant="danger" onClick={onCancel}>
            Hủy
          </Button>
        </div>
      ) : (
        <div>
          <Button className="btnSave" variant="info" onClick={navigateUpdate}>
            Cập nhật
          </Button>
          <Button className="btnCancel" variant="danger" onClick={backPage}>
            Trở về
          </Button>
        </div>
      )}
    </Form>
  );
};