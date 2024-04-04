import { useNavigate, useParams } from "react-router-dom";
import { RESPONSE_REPORT, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";

export const ResponseReport = () => {
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);

  const fields: Field[] = [
    {
      label: "Phản hồi ",
      formType: "input",
      key: "response",
      placeholder: "Nhập nội dung phản hồi",
    },
    {
      label: "Trạng thái",
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
      reportID: id,
    });
    ref.current?.reload();
    navigate("/manage-report");
  };

  return (
    <div className="form-cover">
      <h4>Phản hồi</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-report")}
      />
    </div>
  );
};
