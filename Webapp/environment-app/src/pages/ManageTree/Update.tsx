import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  CULTIVAR_LIST,
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
      console.log(id);
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
      label: "Mã cây",
      formType: "input",
      key: "treeCode",
      defaultValue: data?.treeCode,
    },
    {
      label: "Tuyến đường",
      formType: "input",
      key: "treeLocation",
      defaultValue: data?.treeLocation,
      googleAddress: true,
      value: address,
      onChange: (e) => {
        setAddress(e.target.value);
      },
    },
    {
      label: "Loại cây",
      formType: "select",
      key: "treeTypeId",
      optionExtra: {
        url: TREE_TYPE_LIST,
        _key: "treeTypeName",
        _value: "treeTypeId",
      },
    },
    {
      label: "Đường kính thân",
      formType: "number",
      key: "bodyDiameter",
      defaultValue: data?.bodyDiameter,
    },
    {
      label: "Tán lá",
      formType: "number",
      key: "leafLength",
      defaultValue: data?.leafLength,
    },
    {
      label: "Thời điểm trồng",
      formType: "date",
      key: "plantTime",
      defaultValue: dayFormat(data?.plantTime),
    },
    {
      label: "Khoảng thời gian cắt",
      formType: "number",
      key: "intervalCutTime",
      defaultValue: dayFormat(data?.intervalCutTime),
    },
    {
      label: "Người phụ trách",
      formType: "select",
      key: "userId",
      optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "name",
        _value: "id",
      },
    },
    {
      label: "Ghi chú",
      formType: "textarea",
      key: "note",
      defaultValue: data?.note,
    },
  ];

  const handleSubmit = async (data: Record<string, unknown>) => {
    await useApi.put(TREE_UPDATE.replace(":id", id), {
      ...data,
      plantTime: dateConstructor(data.plantTime),
      updateBy: JSON.parse(token.accessToken).name,
      isExist: true,
    });
    navigate(-1);
    console.log("UpdateTree", data);
  };

  return (
    <div className="form-cover">
      <h4>Cập nhật cây</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-tree")}
      />
    </div>
  );
};
