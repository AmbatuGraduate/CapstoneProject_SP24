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
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập họ cho nhân viên",
      required: true,
    },
    {
      label: "Tên",
      formType: "shortInput",
      keyName: "familyName",
      placeholder: "Ví dụ: Văn A",
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập tên cho nhân viên",
      required: true,
    },
    {
      label: "Email",
      formType: "input",
      keyName: "email",
      defaultValue: "@vesinhdanang.xyz",
      pattern: /^[^\s@]+@vesinhdanang\.xyz$/,
      errorMessage: "Vui lòng nhập một địa chỉ email hợp lệ có đuôi @vesinhdanang.xyz",
      required: true,
    },
    {
      label: "Mật Khẩu",
      formType: "shortInput",
      keyName: "password",
      hiddenInput: "true",
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng mật khẩu",
      required: true,
    },
    {
      label: "Số Điện Thoại",
      formType: "shortInput",
      keyName: "phone",
      placeholder: "Ví dụ: 0123456789",
      pattern: /^\d{10,11}$/,
      errorMessage: "Vui lòng nhập đúng số điện thoại",
      required: true,
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
      placeholder: "Nhập địa chỉ",
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập địa chỉ",
      required: true,
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
      required: true,
    },
    {
      label: "Chức Vụ",
      formType: "select",
      keyName: "userRole",
      defaultValue: 1,
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
          key: "Quản Lý Nhân Sự",
          value: 4,
        },
      ],
      required: true,
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    try {

      Swal.fire({
        title: 'Đang thêm tài khoản nhân viên...',
        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        }
      });

      await useApi.post(EMPLOYEE_ADD, {
        ...data,
        userRole: Number(data?.userRole)
      });
      Swal.close();
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
