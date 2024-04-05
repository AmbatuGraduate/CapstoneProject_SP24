import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { TREE_DETAIL, TREE_TYPE_LIST, TREE_UPDATE, useApi } from "../../Api";
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
      key: "treeCode",
      defaultValue: data?.treeCode,
      disabled: true,
    },
    {
      label: "Tuyến Đường",
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
      label: "Loại Cây",
      formType: "select",
      key: "treeTypeId",
      optionExtra: {
        url: TREE_TYPE_LIST,
        _key: "treeTypeName",
        _value: "v",
      },
    },
    {
      label: "Đường Kính Thân",
      formType: "number",
      key: "bodyDiameter",
      defaultValue: data?.bodyDiameter,
    },
    {
      label: "Tán Lá",
      formType: "number",
      key: "leafLength",
      defaultValue: data?.leafLength,
    },
    {
      label: "Thời Điểm Trồng",
      formType: "date",
      key: "plantTime",
      defaultValue: dayFormat(data?.plantTime),
    },
    {
      label: "Khoảng Thời Gian Cắt",
      formType: "number",
      key: "intervalCutTime",
      defaultValue: dayFormat(data?.intervalCutTime),
    },
    {
      label: "Ghi Chú",
      formType: "textarea",
      key: "note",
      defaultValue: data?.note,
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
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
