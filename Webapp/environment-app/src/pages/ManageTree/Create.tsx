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
  const [plantTime, setPlantTime] = useState<Date | null>(null);
  const [intervalCutTime, setIntervalCutTime] = useState<number>(0);
  const [address, setAddress] = useState<string | null>("");
  useEffect(() => {
    console.log("rerender");
  }, []);

  const cutTime = () => {
    const newCutTime = new Date(plantTime || new Date());
    newCutTime.setMonth(newCutTime.getMonth() + intervalCutTime * 3);
    return newCutTime;
  };

  const fields: Field[] = [
    {
      label: "Mã Cây",
      formType: "input",
      keyName: "treeCode",
      googleAddress: false,
      required: true,
      placeholder: "Ví dụ: 15_CD5_HX_CL",
    },
    {
      label: "Tuyến Đường",
      formType: "input",
      keyName: "treeLocation",
      googleAddress: true,
      value: address,
      onChange: (e) => {
        setAddress(e.target.value);
      },
    },
    {
      label: "Đường Kính Thân (cm)",
      formType: "number",
      keyName: "bodyDiameter",
      googleAddress: false,
      placeholder: "Ví dụ: 50",
    },
    {
      label: "Tán Lá (cm)",
      formType: "number",
      keyName: "leafLength",
      googleAddress: false,
      placeholder: "Ví dụ: 150",
    },
    {
      label: "Thời Điểm Trồng",
      formType: "date",
      keyName: "plantTime",
      selected: plantTime || new Date(),
      googleAddress: false,
      onChange: (date) => {
        setPlantTime(date);
      },
    },
    {
      label: "Thời Điểm Cắt",
      formType: "date",
      keyName: "cutTime",
      selected: cutTime(),
      affectValue: intervalCutTime * 3,
      // affectDate: plantTime || new Date(),
      googleAddress: false,
    },
    {
      label: "Khoảng Thời Gian Cắt (Tháng)",
      formType: "number",
      keyName: "intervalCutTime",
      value: intervalCutTime,
      googleAddress: false,
      onChange: (value) => setIntervalCutTime(Number(value || 0)),
    },
    {
      label: "Loại Cây",
      formType: "select",
      keyName: "treeTypeId",
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
      keyName: "note",
      googleAddress: false,
      placeholder: "Ví dụ: Cần lưu ý...",
    },
    {
      label: "Người Phụ Trách",
      formType: "select",
      keyName: "email",
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
        cutTime: data.cutTime,
        plantTime: data.plantTime,
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
