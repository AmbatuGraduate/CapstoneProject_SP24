import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { EMPLOYEE_DETAIL, EMPLOYEE_UPDATE, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dayFormat } from "../../utils";
import { useCookies } from "react-cookie";

export const UpdateEmployee = () => {
  const navigate = useNavigate();
  const { email = "" } = useParams();
  const [data, setData] = useState<any>();
  const [token] = useCookies(["accessToken"]);
  const [address, setAddress] = useState<string | null>("");

  const fetch = async () => {
    try {
      const data = await useApi.get(EMPLOYEE_DETAIL.replace(":email", "email=" + email));
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
      label: "Tên Nhân Viên",
      formType: "input",
      key: "name",
      defaultValue: data?.name,
    },
    {
      label: "Ngày Sinh",
      formType: "input",
      key: "birthDate",
      defaultValue: dayFormat(data?.birthDate),
    },
    {
      label: "Email",
      formType: "input",
      key: "email",
      defaultValue: data?.email,
    },
    {
      label: "Số Điện Thoại",
      formType: "input",
      key: "phoneNumber",
      defaultValue: data?.phoneNumber,
    },
    {
      label: "Bộ Phận",
      formType: "input",
      key: "department",
      defaultValue: data?.department,
    },
    {
      label: "Chức Vụ",
      formType: "input",
      key: "role",
      defaultValue: data?.role,
    },

    {
      label: "Địa Chỉ Thường Trú",
      formType: "input",
      key: "address",
      defaultValue: data?.address,
      // googleAddress: true,
      // value: address,
      // onChange: (e) => {
      //   setAddress(e.target.value);
      // },
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    await useApi.put(EMPLOYEE_UPDATE, {
      ...data,
    });
    console.log("UpdateTree", data);
  };

  return (
    <div className="form-cover">
      <h4>Cập Nhật Thông Tin Nhân Viên</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-employee")}
      />
    </div>
  );
};
