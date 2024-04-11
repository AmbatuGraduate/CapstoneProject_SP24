import { useNavigate } from "react-router-dom";
import { EMPLOYEE_LIST, TREE_ADD, TREE_TYPE_LIST, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor } from "../../utils";
import { useEffect, useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const CreateTree = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [cutTime, setCutTime] = useState<Date | null>(null);
  const [plantTime, setPlantTime] = useState<Date | null>(null);
  const [intervalCutTime, setIntervalCutTime] = useState<number>(0);
  const [token] = useCookies(["accessToken"]);
  const [address, setAddress] = useState<string | null>("");
  useEffect(() => {
    console.log("rerender");
  }, []);

  const fields: Field[] = [
    {
      label: "Mã Cây",
      formType: "input",
      key: "treeCode",
      googleAddress: false,
      required: true,
      placeholder: "Ví dụ: 15_CD5_HX_CL",
    },
    {
      label: "Tuyến Đường",
      formType: "input",
      key: "treeLocation",
      googleAddress: true,
      value: address,
      onChange: (e) => {
        setAddress(e.target.value);
      },
    },
    {
      label: "Đường Kính Thân (cm)",
      formType: "number",
      key: "bodyDiameter",
      googleAddress: false,
      placeholder: "Ví dụ: 50",
    },
    {
      label: "Tán Lá (cm)",
      formType: "number",
      key: "leafLength",
      googleAddress: false,
      placeholder: "Ví dụ: 150",
    },
    {
      label: "Thời Điểm Trồng",
      formType: "date",
      key: "plantTime",
      selected: plantTime || new Date(),
      googleAddress: false,
      onChange: (e) => {
        setPlantTime(e.target.value);
      },
    },
    {
      label: "Thời Điểm Cắt",
      formType: "date",
      key: "cutTime",
      selected: cutTime || new Date(),
      affectValue: intervalCutTime,
      affectDate: plantTime || new Date(),
      googleAddress: false,
    },
    {
      label: "Khoảng Thời Gian Cắt (Tháng)",
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
        newCutTime.setMonth(newCutTime.getMonth() + intervalCutTime);
        setCutTime(newCutTime);
      },
    },
    {
      label: "Loại Cây",
      formType: "select",
      key: "treeTypeId",
      optionExtra: {
        url: TREE_TYPE_LIST,
        _key: "treeTypeName",
        _value: "treeTypeId",
      },
      googleAddress: false,
    },
    {
      label: "Ghi Chú",
      formType: "textarea",
      key: "note",
      googleAddress: false,
      placeholder: "Ví dụ: Cần lưu ý...",
    },
    {
      label: "Người Phụ Trách",
      formType: "select",
      key: "email",
      optionExtra: {
        url: EMPLOYEE_LIST,
        _key: "email",
        _value: "email",
      },
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
