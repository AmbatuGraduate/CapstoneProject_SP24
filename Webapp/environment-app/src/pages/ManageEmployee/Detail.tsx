import { useEffect, useState } from "react";
import { Field, FormBase } from "../../Components/FormBase";
import { useNavigate } from "react-router-dom";
import { CULTIVAR_DETAIL, useApi } from "../../Api";

type Input = {
  cultivarName?: string;
};
export const DetailCultivar = () => {
  const navigate = useNavigate();
  const [data, setData] = useState<Input | null>(null);

  const fetch = async () => {
    try {
      const data = await useApi.get(
        CULTIVAR_DETAIL.replace(":id", "cultivarName")
      );
      console.log(data);
    } catch (error) {
      console.log(error);
    }
    setData({ cultivarName: "ASAJSA_@!@_13123" });
    // TODO response data to setData
  };

  useEffect(() => {
    fetch();
  }, []);
  //TODO fetch api to get data

  const fields: Field[] = [
    {
      label: "Giống cây",
      formType: "input",
      key: "cultivarName",
      defaultValue: data?.cultivarName,
    },
    {
      label: "Ghi chú",
      formType: "textarea",
      key: "note",
    },
    {
      label: "Đường kính thân",
      formType: "number",
      key: "ob",
    },
  ];

  return (
    <FormBase
      fields={fields}
      mode="view"
      onCancel={() => navigate("/manage-cultivar")}
      navigateUpdate={() =>
        navigate(`/manage-cultivar/${data?.cultivarName}/update`)
      }
    />
  );
};
