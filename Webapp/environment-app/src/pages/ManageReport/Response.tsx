import { useNavigate } from "react-router-dom";
import { RESPONSE_REPORT, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";

export const ResponseReport = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);

  const fields: Field[] = [
    {
      label: "Phản Hồi ",
      formType: "input",
      key: "response",
      placeholder: "Chưa có phản hồi nào"
    },
    {
      label: "Trạng Thái",
      formType: "select",
      key: "status",
      options: [
        {
          key: "Chưa được xử lý",
          value: 0,
        },
        {
          key: "Đã xử lý",
          value: 1,
        },
      ],
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);
    await useApi.post(RESPONSE_REPORT, {
      ...data,
    });
    ref.current?.reload();
    navigate("/manage-report");
  };

  return (
    <div className="form-cover">
      <h4>Phản Hồi</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-report")}
      />
    </div>
  );
};