import { useEffect, useState } from "react";
import { CULTIVAR_DETAIL, useApi } from "../../api";
import { Field, FormBase } from "../../components/FormBase";


type Input = {
  cultivarName?: string;
  note?: string;
};
export const UpdateCultivar = () => {
  const [data, setData] = useState<Input | null>(null);

  const fetch = async () => {
    try {
      const data = await useApi.get(CULTIVAR_DETAIL.replace(":id", "cultivarName"));
      console.log(data);
    } catch (error) {
      console.log(error);
    }
    setData({ cultivarName: "ASAJSA_@!@_13123", note: "NOTEEEEE" });
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
      required: true,
      defaultValue: data?.cultivarName,
    },
    {
      label: "Ghi chú",
      formType: "textarea",
      key: "note",
      required: true,
    },
    {
      label: "Đường kính thân",
      formType: "number",
      key: "ob",
    },
  ];

  const handleSubmit = (data: Record<string, unknown>) => {
    //TODO call api and return list
    console.log("CreateTree", data);
  };

  return <FormBase fields={fields} onSave={handleSubmit} />;
};
