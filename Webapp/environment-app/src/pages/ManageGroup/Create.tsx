import { useNavigate } from "react-router-dom";
import { EMPLOYEE_LIST, GROUP_ADD, TREE_ADD, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";
import Swal from "sweetalert2";

export const CreateGroup = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [token] = useCookies(["accessToken"]);

  const fields: Field[] = [
    {
      label: "Email",
      formType: "shortInput",
      keyName: "email",
      required: true,
      defaultValue: "@vesinhdanang.xyz",
      pattern: /^[^\s@]+@vesinhdanang\.xyz$/,
      errorMessage: "Vui lòng nhập một địa chỉ email hợp lệ có đuôi @vesinhdanang.xyz",
    },
    {
      label: "Quản lý",
      formType: "select",
      keyName: "owners",
      optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "email",
        _value: "email",
      },
      required: true,
    },
    {
      label: "Tên Bộ Phận",
      formType: "input",
      keyName: "name",
      required: true,
      placeholder: "Ví dụ: Bộ phận quản lý..",
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập tên bộ phận",
    },
    {
      label: "Mô tả",
      formType: "textarea",
      keyName: "description",
      placeholder: "Ví dụ: Bộ phận quản lý..",
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập mô tả cho bộ phận",
      required: true,
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    try {
      Swal.fire({
        title: 'Đang thêm bộ phận...',
        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        }
      });
      await useApi.post(GROUP_ADD, {
        ...data,
        members: data.owners,
        adminCreated: true,
      });
      Swal.close();
      Swal.fire(
        'Thành công!',
        'Thêm bộ phận mới thành công!',
        'success'
      );
      ref.current?.reload();
      navigate("/manage-group");
    } catch (error) {
      console.error("Error creating tree:", error);
      Swal.fire(
        'Lỗi!',
        'Lỗi khi thêm bộ phận! Vui lòng thử lại sau.',
        'error'
      );
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="form-cover">
      <h4>Thêm bộ phận</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate(-1)}
      />
    </div>
  );
};
