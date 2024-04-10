import { useNavigate } from "react-router-dom";
import { TREE_ADD, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const CreateGroup = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [token] = useCookies(["accessToken"]);

  const fields: Field[] = [
    {
      label: "Email",
      formType: "input",
      key: "email",
      required: true,
      placeholder: "Ví dụ: quanlyvesinh@vesinhdanang.xyz",
    },
    {
      label: "Tên Bộ Phận",
      formType: "input",
      key: "name",
      placeholder: "Ví dụ: Bộ phận quản lý..",
    },
    {
      label: "Mô tả",
      formType: "input",
      key: "description",
      placeholder: "Ví dụ: Bộ phận quản lý..",
    },
    {
      label: "Nhân viên",
      formType: "input",
      key: "members",
      placeholder: "Ví dụ: abc@vesinhdanang.xyz",
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    try {
      await useApi.post(TREE_ADD, {
        ...data,
        adminCreated: true,
        owners: JSON.parse(token.accessToken).role == "Admin"
      });
      ref.current?.reload();
      navigate("/manage-tree");
    } catch (error) {
      console.error("Error creating tree:", error);
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <div className="form-cover">
      <h4>Thêm cây</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate(-1)}
      />
    </div>
  );
};
