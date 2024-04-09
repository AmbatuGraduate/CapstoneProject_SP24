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
      label: "Ngày Sinh",
      formType: "date",
      key: "birthDate",
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

    // Ensure birthDate is in the correct format (dd/mm/yyyy)
    const [day, month, year] = data.birthDate.split('/').map(Number);
    const parsedDate = new Date(year, month - 1, day);

    // Check if parsedDate is a valid date
    if (isNaN(parsedDate.getTime())) {
      console.error('Invalid date format for birthDate');
      setIsLoading(false);
      return;
    }

    // Convert birthDate to ISO string
    const birthDate = parsedDate.toISOString();

    const requestData = {
      ...data,
      birthDate: birthDate,
    };

    try {
      // Call the API to add employee
      await useApi.post(EMPLOYEE_ADD, requestData);

      // Reload the ref if available
      if (ref.current) {
        ref.current.reload();
      }

      // Navigate to manage-employee page
      navigate("/manage-employee");
    } catch (error) {
      console.error("Error while adding employee:", error);
      setIsLoading(false); // Reset loading state if there's an error
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
