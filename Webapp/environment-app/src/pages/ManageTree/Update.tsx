import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
  TREE_DETAIL,
  TREE_TYPE_LIST,
  TREE_UPDATE,
  useApi,
} from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useCookies } from "react-cookie";

export const UpdateTree = () => {
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const [data, setData] = useState<any>();
  const [token] = useCookies(["accessToken"]);

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
      formType: "shortInput",
      keyName: "treeCode",
      defaultValue: data?.treeCode,
      disabled: true,
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
      defaultValue: data?.plantTime,
    },
    {
      label: "Khoảng Thời Gian Cắt",
      formType: "number",
      keyName: "intervalCutTime",
      defaultValue: data?.intervalCutTime,
    },
    {
      label: "Người Phụ Trách",
      formType: "shortInput",
      keyName: "email",
      defaultValue: data?.user,
      disabled: true,
    },
    {
      label: "Ghi Chú",
      formType: "textarea",
      keyName: "note",
      defaultValue: data?.note,
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    const cutTime = () => {
      const newCutTime = new Date(data.plantTime || new Date());
      newCutTime.setMonth(
        newCutTime.getMonth() + Number(data.intervalCutTime || 0) * 3
      );
      return newCutTime;
    };

    await useApi.put(TREE_UPDATE.replace(":id", id), {
      ...data,
      plantTime: data.plantTime,
      // cutTime: cutTime(),
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
