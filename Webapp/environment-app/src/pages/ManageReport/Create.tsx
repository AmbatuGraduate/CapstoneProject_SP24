import { useNavigate } from "react-router-dom";
import { CREATE_REPORT, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { ReportImpact, dateConstructor } from "../../utils";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const CreateReport = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [token] = useCookies(["accessToken"]);

  const fields: Field[] = [
    {
      label: "Tiêu Đề",
      formType: "input",
      keyName: "reportSubject",
    },
    {
      label: "Nội Dung",
      formType: "input",
      keyName: "reportBody",
    },
    {
      label: "Cần Giải Quyết Trước",
      formType: "date",
      keyName: "expectedResolutionDate",
    },
    {
      label: "Mức Độ Ảnh Hưởng",
      formType: "select",
      keyName: "reportImpact",
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
    {
      label: "Hình ảnh",
      formType: "input",
      keyName: "reportImages",
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);
    await useApi.post(CREATE_REPORT, {
      ...data,
      issuerEmail: JSON.parse(token.accessToken).email,
      expectedResolutionDate: dateConstructor(data.expectedResolutionDate),
      reportImpact: Number(data?.reportImpact)
    });
    ref.current?.reload();
    navigate("/manage-report");
  };

  return (
    <div className="form-cover">
      <h4>Thêm Báo Cáo</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-report")}
      />
    </div>
  );
};
