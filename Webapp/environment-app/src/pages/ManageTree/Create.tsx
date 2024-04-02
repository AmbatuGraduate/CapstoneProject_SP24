import { useNavigate } from "react-router-dom";
import { CULTIVAR_LIST, TREE_ADD, TREE_TYPE_LIST, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor } from "../../utils";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const CreateTree = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [isLoading, setIsLoading] = useState(false);
  const [cutTime, setCutTime] = useState<Date | null>(null);
  const [plantTime, setPlantTime] = useState<Date | null>(null);
  const [intervalCutTime, setIntervalCutTime] = useState<number>(0);
  const [token, setToken] = useCookies(["accessToken"]);
  const [address, setAddress] = useState<string | null>("");

  const fields: Field[] = [
    {
      label: "Mã cây",
      formType: "input",
      key: "treeCode",
      googleAddress: false,
      required: true,
      placeholder: "Ví dụ: 15_CD5_HX_CL",
    },
    {
      label: "Tuyến đường",
      formType: "input",
      key: "treeLocation",
      googleAddress: true,
      value: address,
      onChange: (e) => {
        setAddress(e.target.value);
      },
    },
    {
      label: "Đường kính thân (cm)",
      formType: "number",
      key: "bodyDiameter",
      googleAddress: false,
      placeholder: "Ví dụ: 150",
    },
    {
      label: "Tán lá (cm)",
      formType: "number",
      key: "leafLength",
      googleAddress: false,
      placeholder: "Ví dụ: 150",
    },
    {
      label: "Thời điểm trồng",
      formType: "date",
      key: "plantTime",
      selected: plantTime || new Date(),
      googleAddress: false,
      onChange: (e) => {
        setPlantTime(e.target.value);
      },
    },
    {
      label: "Thời điểm cắt",
      formType: "date",
      key: "cutTime",
      selected: cutTime || new Date(),
      affectValue: intervalCutTime,
      affectDate: plantTime || new Date(),
      googleAddress: false,
      onChange: (e) => {
        const plantTimeValue = data["plantTime"];
        const plantTime = dateConstructor(plantTimeValue);
        const intervalCutTime = parseInt(e.target.value) || 0;
        const newCutTime = new Date(
          plantTime.getFullYear(),
          plantTime.getMonth() + intervalCutTime,
          plantTime.getDate()
        );
        newCutTime.setMonth(newCutTime.getMonth() + intervalCutTime);
        setCutTime(newCutTime);
      },
    },
    {
      label: "Khoảng thời gian cắt",
      formType: "input",
      key: "intervalCutTime",
      value: intervalCutTime,
      googleAddress: false,
      onChange: (e) => {
        setIntervalCutTime(e.target.value);
        // const plantTimeValue = data["plantTime"];
        // const plantTime = dateConstructor(plantTimeValue);
        const newCutTime = new Date();
        newCutTime.setMonth(newCutTime.getMonth() + 3);
        // newCutTime.setMonth(newCutTime.getMonth() + intervalCutTime);
        setCutTime(newCutTime);
      },
    },
    // {
    //   label: "Loại cây",
    //   formType: "select",
    //   key: "treeTypeId",
    //   optionExtra: {
    //     url: TREE_TYPE_LIST,
    //     _key: "treeTypeName",
    //     _value: "treeTypeId",
    //   },
    // },
    {
      label: "Giống cây",
      formType: "select",
      key: "cultivarId",
      optionExtra: {
        url: CULTIVAR_LIST,
        _key: "cultivarName",
        _value: "cultivarId",
      },
      googleAddress: false,
    },
    {
      label: "Ghi chú",
      formType: "textarea",
      key: "note",
      googleAddress: false,
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    try {
      await useApi.post(TREE_ADD, {
        ...data,
        cutTime: dateConstructor(data.cutTime),
        plantTime: dateConstructor(data.plantTime),
        updateBy: JSON.parse(token.accessToken).name,
        createBy: JSON.parse(token.accessToken).name,
        updateDate: new Date(),
        isExist: true,
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
        onCancel={() => navigate("/manage-tree")}
      />
    </div>
  );
};
