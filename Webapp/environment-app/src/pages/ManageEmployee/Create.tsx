import { useNavigate } from "react-router-dom";
import { Field, FormBase } from "../../Components/FormBase";

export const CreateCultivar = () => {
  const navigate = useNavigate();

  const fields: Field[] = [
    {
      label: "Giống cây",
      formType: "input",
      key: "cultivarName",
      required: true,
    },
    {
      label: "Loại cây",
      formType: "textarea",
      key: "treeTypeId",
      required: true,
    },
  ];

  const handleSubmit = (data: Record<string, unknown>) => {
    //TODO call api and return list
    console.log("CreateCultivar", data);
  };

  return (
    <FormBase
      fields={fields}
      onSave={handleSubmit}
      onCancel={() => navigate("/manage-cultivar")}
    />
  );
};
