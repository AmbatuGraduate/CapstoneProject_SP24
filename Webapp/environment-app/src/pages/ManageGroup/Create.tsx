import { useNavigate } from "react-router-dom";
import { EMPLOYEE_LIST, TREE_ADD, useApi } from "../../Api";
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
      formType: "input",
      keyName: "email",
      required: true,
      placeholder: "Ví dụ: quanlyvesinh@vesinhdanang.xyz",
    },
    {
      label: "Tên Bộ Phận",
      formType: "input",
      keyName: "name",
      placeholder: "Ví dụ: Bộ phận quản lý..",
    },
    {
      label: "Mô tả",
      formType: "input",
      keyName: "description",
      placeholder: "Ví dụ: Bộ phận quản lý..",
    },
    {
      label: "Nhân viên",
      formType: "select",
      keyName: "members",
      placeholder: "Ví dụ: abc@vesinhdanang.xyz",
      optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "email",
        _value: "email",
      },
    },
    {
      label: "Quản lý",
      formType: "shortInput",
      keyName: "owners",
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    try {
      await useApi.post(TREE_ADD, {
        ...data,
        adminCreated: true,
      });
      Swal.fire(
        'Thành công!',
        'Thêm bộ phận mới thành công!',
        'success'
      );
      ref.current?.reload();
      navigate("/manage-tree");
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
