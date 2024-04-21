import React, { useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { useApi } from "../../Api";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import "./style.scss";

import { FaRegCalendarAlt } from "react-icons/fa";

export type Field = {
  label: string;
  keyName: string;
  defaultValue?: any;
  affectValue?: any;
  affectDate?: Date;
  googleAddress?: boolean;
  selected?: Date;
  value?: any;
  placeholder?: string;
  formType:
    | "input"
    | "shortInput"
    | "select"
    | "textarea"
    | "number"
    | "date"
    | "jsx"
    | "datetime"
    | "multi-select";
  options?: Option[];
  required?: boolean;
  disabled?: boolean;
  optionExtra?: OptionExtra;
  onChange?: (value: any) => void;
  onRender?: React.ReactNode;
  setFormData?: any;
  formData?: any;
  hiddenInput?: any;
} & Partial<Props>;

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

const FormType = (props: Field) => {
  const {
    formType,
    options,
    keyName,
    disabled,
    optionExtra,
    onRender,
    mode,
    formData,
    setFormData,
    onChange,
    defaultValue = "",
    ...rest
  } = props;

  const _disabled = mode == "view" ? true : disabled;
  const [_options, setOptions] = useState<Option[]>();

  const [places] = useState([]);

  useEffect(() => {
    if (optionExtra) {
      fetchDataForFormSelect(optionExtra);
    }
  }, [JSON.stringify(optionExtra)]);

  useEffect(() => {
    if (props.googleAddress) {
      const input = document.getElementById("pac-input");
      if (input instanceof HTMLInputElement) {
        const center = { lat: 16.047079, lng: 108.20623 };
        const defaultBounds = {
          north: center.lat + 0.1,
          south: center.lat - 0.1,
          east: center.lng + 0.1,
          west: center.lng - 0.1,
        };
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
          // console.log(place);
          console.log(formData[keyName]);
          if (place.geometry && place.geometry.location) {
            const latitude = place.geometry.location.lat();
            const longitude = place.geometry.location.lng();
            // console.log("Latitude:", latitude);
            // console.log("Longitude:", longitude);
            const addressComponents = place.address_components;
            let address = "";
            // Iterate through address components to construct the address string
            addressComponents?.forEach((component) => {
              address += component.long_name + ", ";
            });
            // Remove trailing comma and space
            address = address.slice(0, -2);
            console.log("Selected Address:", address);
            setFormData((prev) => ({ ...prev, [keyName]: address }));
          }
        });
        // console.log(places);
      }
    }
  }, [formData[keyName], props.value]);

  const fetchDataForFormSelect = async (option: OptionExtra) => {
    const res = await useApi.get(option.url);
    const _options = res.data?.map((obj: any) => ({
      key: obj[option._key],
      value: obj[option._value],
    }));
    setOptions(_options);
    setFormData((prev) => ({ ...prev, [keyName]: _options[0]?.value }));
  };

  switch (formType) {
    case "input":
      return (
        <Form.Control
          id={props.googleAddress == true ? "pac-input" : ""}
          type="text"
          {...rest}
          name={keyName}
          value={formData[keyName]}
          onChange={(e) => {
            console.log("formbase", formData[keyName]);
            // onChange && onChange(e);
            setFormData((prev) => ({ ...prev, [keyName]: e.target.value }));
          }}
          disabled={_disabled}
        />
      );
    case "shortInput":
      return (
        <Form.Control
          id={props.googleAddress == true ? "pac-input" : ""}
          as="input"
          type={props.hiddenInput ? "password" : "text"}
          {...rest}
          name={keyName}
          value={formData[keyName]}
          onChange={(e) =>
            setFormData((prev) => ({ ...prev, [keyName]: e.target.value }))
          }
          disabled={_disabled}
        />
      );
    case "number":
      return (
        <Form.Control
          type="number"
          {...rest}
          name={keyName}
          disabled={_disabled}
          value={formData[keyName]}
          onChange={(e) => {
            setFormData((prev) => ({
              ...prev,
              [keyName]: Number(e.target.value || 0),
            }));
            onChange && onChange(e.target.value || 0);
          }}
        />
      );
    case "textarea":
      return (
        <Form.Control
          {...rest}
          as="textarea"
          rows={4}
          name={keyName}
          disabled={_disabled}
          value={formData[keyName]}
          onChange={(e) =>
            setFormData((prev) => ({ ...prev, [keyName]: e.target.value }))
          }
        />
      );
    case "select":
      return (
        <Form.Select
          name={keyName}
          {...rest}
          disabled={_disabled}
          onChange={(e) => {
            onChange && onChange(e.target.value);
            setFormData((prev) => ({ ...prev, [keyName]: e.target.value }));
          }}
        >
          {(optionExtra ? _options : options)?.map((option, idx) => (
            <option key={idx} value={option.value}>
              {option.key}
            </option>
          ))}
        </Form.Select>
      );

    case "multi-select":
      return (
        <Form.Select
          name={keyName}
          {...rest}
          disabled={_disabled}
          multiple
          onChange={(e) => {
            onChange && onChange(e.target.value);
            setFormData((prev) => ({ ...prev, [keyName]: e.target.value }));
          }}
        >
          {(optionExtra ? _options : options)?.map((option, idx) => (
            <option key={idx} value={option.value}>
              {option.key}
            </option>
          ))}
        </Form.Select>
      );

    case "date":
      return (
        <div className="date-picker-container">
          <DatePicker
            selected={formData[keyName] ? new Date(formData[keyName]) : null}
            onChange={(date) => {
              console.log("date", date);
              setFormData((prev) => ({ ...prev, [keyName]: date }));
            }}
            className="datepicker"
            name={keyName}
            disabled={_disabled}
            dateFormat="dd/MM/yyyy"
          />
          <FaRegCalendarAlt className="calendar-icon" />
        </div>


      );
    case "jsx":
      return onRender;
    case "datetime":
      return (
        <div className="date-picker-container">
          <DatePicker
            selected={formData[keyName] ? new Date(formData[keyName]) : null}
            onChange={(date) => {
              onChange && onChange(date);
              setFormData((prev) => ({ ...prev, [keyName]: date }));
            }}
            showTimeSelect
            timeFormat="HH:mm"
            timeIntervals={15}
            timeCaption="Time"
            dateFormat="HH:mm dd/MM/yyyy "
            className="datepicker"
            name={keyName}
            disabled={_disabled}
          />
          <FaRegCalendarAlt className="calendar-icon" />
        </div>

      );
    default:
      return (
        <Form.Control
          type="text"
          {...rest}
          name={keyName}
          disabled={_disabled}
          value={formData[keyName]}
          onChange={(e) =>
            setFormData((prev) => ({ ...prev, [keyName]: e.target.value }))
          }
        />
      );
  }
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
  const [formData, setFormData] = useState<Record<string, any>>({});

  useEffect(() => {
    const _data = { ...formData };
    fields.forEach(
      (f) =>
        (_data[f.keyName] = f.value
          ? f.value
          : _data[f.keyName]
          ? _data[f.keyName]
          : f.defaultValue)
    );
    setFormData(_data);
  }, [JSON.stringify(fields)]);

  const handleSubmit = () => {
    onSave && onSave(formData);
    console.log(formData);
  };

  return (
    <Form className="form-base">
      {fields.map((f, idx) => {
        // console.log("rerender FormType", f.label);
        const groupClassName = `custom-group-${f.formType}`;
        return (
          <Form.Group className={`mb-3 ${groupClassName}`} key={idx}>
            <Form.Label>{f.label}</Form.Label>
            <FormType
              setFormData={setFormData}
              formData={formData}
              {...f}
              keyName={f.keyName}
            />
          </Form.Group>
        );
      })}
      {mode == "create&update" ? (
        <div className="btnPosi">
          <Button className="btnSave" type="button" onClick={handleSubmit}>
            Lưu
          </Button>
          <Button className="btnCancel" variant="danger" onClick={onCancel}>
            Hủy
          </Button>
        </div>
      ) : (
        <div className="btnPosi">
          <Button className="btnSave" variant="info" onClick={navigateUpdate}>
            Cập Nhật
          </Button>
          <Button className="btnCancel" variant="danger" onClick={backPage}>
            Trở Về
          </Button>
        </div>
      )}
    </Form>
  );
};
