import { useNavigate } from "react-router-dom";
import { DEPARTMENT_LIST, EMPLOYEE_ADD, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
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
      key: "name",
      placeholder: "Ví dụ: Nguyễn",
    },
    {
      label: "Tên",
      formType: "input",
      key: "familyName",
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
      label: "Bộ Phận",
      formType: "select",
      key: "departmentEmail",
      optionExtra: {
        url: DEPARTMENT_LIST,
        _key: "name",
        _value: "email",
      },
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    try {
      await useApi.post(EMPLOYEE_ADD, {
        ...data,
      });
      ref.current?.reload();
      navigate("/manage-employee");
    } catch (error) {
      console.error("Error creating employee:", error);
    } finally {
      setIsLoading(false);
    }
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
