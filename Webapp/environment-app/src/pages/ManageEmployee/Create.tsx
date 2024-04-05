import { useNavigate } from "react-router-dom";
import { EMPLOYEE_ADD, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor } from "../../utils";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const CreateEmployee = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [token] = useCookies(["accessToken"]);

  const fields: Field[] = [
    {
      label: "Họ",
      formType: "input",
      key: "familyName",
      placeholder: "Ví dụ: Nguyễn",
    },
    {
      label: "Tên",
      formType: "input",
      key: "name",
      placeholder: "Ví dụ: Văn A",
    },
    {
      label: "Email",
      formType: "input",
      key: "email",
      placeholder: "Ví dụ: ANV@cayxanh.vn",
    },
    {
      label: "Mật Khẩu",
      formType: "input",
      key: "password",
    },
    {
      label: "Số Điện Thoại",
      formType: "input",
      key: "phone",
      placeholder: "Ví dụ: 0123456789",
    },
    {
      label: "Địa Chỉ",
      formType: "input",
      key: "address",
    },
    {
      label: "Ngày Sinh",
      formType: "date",
      key: "birthDate",
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);
    await useApi.post(EMPLOYEE_ADD, {
      ...data,
      birthDate: dateConstructor(data.birthDate),
    });
    ref.current?.reload();
    navigate("/manage-employee");
  };

  return (
    <div className="form-cover">
      <h4>Thêm Nhân Viên</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-employee")}
      />
    </div>
  );
};