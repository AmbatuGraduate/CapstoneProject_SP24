import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { CULTIVAR_LIST, TREE_DETAIL, TREE_UPDATE, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor, dayFormat } from "../../utils";
import { useCookies } from "react-cookie";

export const UpdateTree = () => {
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const [data, setData] = useState<any>();
  const [token, setToken] = useCookies(["accessToken"]);
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
      label: "Mã cây",
      formType: "input",
      key: "treeCode",
      defaultValue: data?.treeCode,
      disabled: true,
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
      label: "Giống cây",
      formType: "select",
      key: "cultivarId",
      optionExtra: {
        url: CULTIVAR_LIST,
        _key: "cultivarName",
        _value: "cultivarId",
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
      label: "Ghi chú",
      formType: "textarea",
      key: "note",
      defaultValue: data?.note,
    },
  ];

  const handleSubmit = async (data: Record<string, unknown>) => {
    await useApi.put(TREE_UPDATE, {
      ...data,
      plantTime: dateConstructor(data.plantTime),
      updateBy: JSON.parse(token.accessToken).name,
      isExist: true,
    });
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
