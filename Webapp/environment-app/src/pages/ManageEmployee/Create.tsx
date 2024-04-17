import { useNavigate } from "react-router-dom";
import { DEPARTMENT_LIST, EMPLOYEE_ADD, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";
import Swal from "sweetalert2";

export const CreateEmployee = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [address, setAddress] = useState<string | null>("");

  const fields: Field[] = [
    {
      label: "Họ",
      formType: "shortInput",
      keyName: "name",
      placeholder: "Ví dụ: Nguyễn",
    },
    {
      label: "Tên",
      formType: "shortInput",
      keyName: "familyName",
      placeholder: "Ví dụ: Văn A",
    },
    {
      label: "Email",
      formType: "input",
      keyName: "email",
      defaultValue: "@vesinhdanang.xyz"
    },
    {
      label: "Mật Khẩu",
      formType: "shortInput",
      keyName: "password",
      hiddenInput: "true",
    },
    {
      label: "Số Điện Thoại",
      formType: "shortInput",
      keyName: "phone",
      placeholder: "Ví dụ: 0123456789",
    },
    {
      label: "Địa Chỉ",
      formType: "input",
      keyName: "address",
      googleAddress: true,
      value: address,
      onChange: (e) => {
        setAddress(e.target.value);
      },
      placeholder: "Nhập địa chỉ"
    },
    {
      label: "Bộ Phận",
      formType: "select",
      keyName: "departmentEmail",
      optionExtra: {
        url: DEPARTMENT_LIST,
        _key: "name",
        _value: "email",
      },
    },
    {
      label: "Chức Vụ",
      formType: "select",
      keyName: "userRole",
      options: [
        {
          key: "Nhân Viên",
          value: 1,
        },
        {
          key: "Quản Lý",
          value: 2,
        },
        {
          key: "Quản lý Nhân Sự",
          value: 3,
        },
      ],
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    try {
      await useApi.post(EMPLOYEE_ADD, {
        ...data,
        userRole: Number(data?.userRole)
      });
      Swal.fire(
        'Thành công!',
        'Thêm nhân viên mới thành công!',
        'success'
      );
      ref.current?.reload();
      navigate("/manage-employee");
    } catch (error) {
      console.error("Error creating employee:", error);
      Swal.fire(
        'Lỗi!',
        'Lỗi khi thêm nhân viên! Vui lòng thử lại sau.',
        'error'
      );
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
