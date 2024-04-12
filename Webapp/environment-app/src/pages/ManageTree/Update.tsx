import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  EMPLOYEE_LIST,
  TREE_DETAIL,
  TREE_TYPE_LIST,
  TREE_UPDATE,
  useApi,
} from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor, dayFormat } from "../../utils";
import { useCookies } from "react-cookie";

export const UpdateTree = () => {
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const [data, setData] = useState<any>();
  const [token] = useCookies(["accessToken"]);
  const [address, setAddress] = useState<string | null>("");

  const fetch = async () => {
    try {
      const data = await useApi.get(TREE_DETAIL.replace(":id", id));
      setData(data.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetch();
  }, []);

  const fields: Field[] = [
    {
      label: "Mã Cây",
      formType: "input",
      keyName: "treeCode",
      defaultValue: data?.treeCode,
      disabled: true,
    },
    {
      label: "Tuyến Đường",
      formType: "input",
      keyName: "treeLocation",
      defaultValue: data?.streetName,
      // googleAddress: true,
      // value: address,
      // onChange: (e) => {
      //   setAddress(e.target.value);
      // },
    },
    {
      label: "Loại Cây",
      formType: "select",
      keyName: "treeTypeId",
      optionExtra: {
        url: TREE_TYPE_LIST,
        _key: "treeTypeName",
        _value: "treeTypeId",
      },
    },
    {
      label: "Đường Kính Thân",
      formType: "number",
      keyName: "bodyDiameter",
      defaultValue: data?.bodyDiameter,
    },
    {
      label: "Tán Lá",
      formType: "number",
      keyName: "leafLength",
      defaultValue: data?.leafLength,
    },
    {
      label: "Thời Điểm Trồng",
      formType: "date",
      keyName: "plantTime",
      defaultValue: dayFormat(data?.plantTime),
    },
    {
      label: "Khoảng Thời Gian Cắt",
      formType: "number",
      keyName: "intervalCutTime",
      defaultValue: dayFormat(data?.intervalCutTime),
    },
    {
      label: "Ghi Chú",
      formType: "textarea",
      keyName: "note",
      defaultValue: data?.note,
    },
    {
      label: "Người Phụ Trách",
      formType: "select",
      keyName: "email",
      optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "email",
        _value: "email",
      },
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    await useApi.put(TREE_UPDATE.replace(":id", id), {
      ...data,
      plantTime: dateConstructor(data.plantTime),
    });
    console.log("UpdateTree", data);
    navigate(-1);
  };

  return (
    <div className="form-cover">
      <h4>Cập Nhật Cây</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate(-1)}
      />
    </div>
  );
};
