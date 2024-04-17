import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import {
    EMPLOYEE_LIST,
  GROUP_DETAIL,
  GROUP_UPDATE,
  useApi,
} from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";

export const UpdateGroup = () => {
  const navigate = useNavigate();
  const { email = "" } = useParams();
  const [data, setData] = useState<any>();

  const fetch = async () => {
    try {
      const data = await useApi.get(GROUP_DETAIL.replace(":email", email));
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
      label: "Email",
      formType: "input",
      keyName: "email",
      defaultValue: data?.value?.email,
      disabled: true,
    },
    {
      label: "Tên Bộ Phận",
      formType: "input",
      keyName: "name",
      defaultValue: data?.value?.name,
    },
    {
      label: "Mô tả",
      formType: "input",
      keyName: "description",
      defaultValue: data?.value?.description,
    },
    {
      label: "Nhân viên",
      formType: "select",
      keyName: "members",
      defaultValue: data?.value?.members,
      optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "email",
        _value: "email",
      },
    },
    {
        label: "Quản lý",
        formType: "select",
        keyName: "owners",
        defaultValue: data?.value?.owners,
        optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "email",
        _value: "email",
      },
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

    await useApi.post(GROUP_UPDATE.replace(":email", email), {
      ...data,
    });
    console.log("UpdateTree", data);
    navigate(-1);
  };

  return (
    <div className="form-cover">
      <h4>Cập Nhật Bộ Phận</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate(-1)}
      />
    </div>
  );
};
