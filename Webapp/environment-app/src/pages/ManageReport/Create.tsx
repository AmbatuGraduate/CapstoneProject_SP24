import { useNavigate } from "react-router-dom";
import { CREATE_REPORT, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor } from "../../utils";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const CreateReport = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [token] = useCookies(["accessToken"]);

  const fields: Field[] = [
    {
      value: JSON.parse(token.accessToken).email,
      label: "Người gửi",
      formType: "input",
      key: "issuerEmail",
    },
    {
      label: "Tiêu đề",
      formType: "input",
      key: "reportSubject",
    },
    {
      label: "Nội dung",
      formType: "input",
      key: "reportBody",
    },
    {
      label: "Cần giải quyết trước",
      formType: "date",
      key: "expectedResolutionDate",
    },
    {
      label: "Mức độ ảnh hưởng",
      formType: "select",
      key: "reportImpact",
      options: [
        {
          key: "Thấp",
          value: 0,
        },
        {
          key: "Trung bình",
          value: 1,
        },
        {
          key: "Cao",
          value: 2,
        },
      ],
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);
    await useApi.post(CREATE_REPORT, {
      ...data,
      expectedResolutionDate: dateConstructor(data.expectedResolutionDate),
    });
    ref.current?.reload();
    navigate("/manage-report");
  };

  return (
    <div className="form-cover">
      <h4>Thêm báo cáo</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-report")}
      />
    </div>
  );
};