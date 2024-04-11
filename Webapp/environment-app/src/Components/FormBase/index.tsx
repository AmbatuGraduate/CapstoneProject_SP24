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
  formType: "input" | "select" | "textarea" | "number" | "date" | "jsx" | "datetime";
  options?: Option[];
  required?: boolean;
  disabled?: boolean;
  optionExtra?: OptionExtra;
  onChange?: (event: React.ChangeEvent<any>) => void;
  onRender?: React.ReactNode;
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
    const { formType, options, key, disabled, optionExtra, onRender, ...rest } = props;
    const _disabled = mode == "view" ? true : disabled;
    const [_options, setOptions] = useState<Option[]>();
    const [startDate, setStartDate] = useState<Date | null>(
      props.selected || new Date()
    );

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
            rows={4}
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
      case "jsx":
        return onRender;
      case "datetime":
        return (
          <DatePicker
            selected={startDate}
            onChange={(date) => setStartDate(date)}
            showTimeSelect
            timeFormat="HH:mm"
            timeIntervals={15}
            timeCaption="Time"
            dateFormat="HH:mm dd/MM/yyyy "
            className="datepicker"
            name={key}
            disabled={_disabled}
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
      if (f.key === "reportImpact") {
        data[f.key] = Number((e.target as any)?.[f.key].value);
      }
      if (f.key === "status") {
        data[f.key] = Number((e.target as any)?.[f.key].value);
      }
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
        <div className="btnPosi">
          <Button className="btnSave" type="submit">
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
