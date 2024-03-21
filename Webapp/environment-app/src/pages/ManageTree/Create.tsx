import { useNavigate } from "react-router-dom";
import { CULTIVAR_LIST, TREE_ADD, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor, user } from "../../utils";

export const CreateTree = () => {
  const navigate = useNavigate();

  const fields: Field[] = [
    {
      label: "Mã cây",
      formType: "input",
      key: "treeCode",
      required: true,
    },
    // {
    //   label: "Quận",
    //   formType: "select",
    //   key: "quan",
    //   defaultValue: 6,
    //   options: [
    //     {
    //       key: "Thanh Khê",
    //       value: 1,
    //     },
    //     {
    //       key: "Hải Châu",
    //       value: 2,
    //     },
    //     {
    //       key: "Ngũ Hành Sơn",
    //       value: 3,
    //     },
    //     {
    //       key: "Sơn Trà",
    //       value: 4,
    //     },
    //     {
    //       key: "Liên Chiểu",
    //       value: 5,
    //     },
    //     {
    //       key: "Cẩm Lệ",
    //       value: 6,
    //     },
    //   ],
    // },
    {
      label: "Tuyến đường",
      formType: "input",
      key: "treeLocation",
    },
    {
      label: "Đường kính thân",
      formType: "number",
      key: "bodyDiameter",
    },
    {
      label: "Tán lá",
      formType: "number",
      key: "leafLength",
    },
    {
      label: "Thời gian trồng",
      formType: "date",
      key: "plantTime",
    },
    {
      label: "Thời gian cắt",
      formType: "date",
      key: "cutTime",
    },
    {
      label: "Khoảng thời gian cắt",
      formType: "input",
      key: "intervalCutTime",
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
    },
    {
      label: "Ghi chú",
      formType: "input",
      key: "note",
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    const u = user();
    await useApi.post(TREE_ADD, {
      ...data,
      cutTime: dateConstructor(data.cutTime),
      plantTime: dateConstructor(data.plantTime),
      updateDate: new Date(),
      updateBy: u?.name,
      createBy: u?.name,
    });
    console.log("CreateTree", data);
  };

  return (
    <div className="form-cover">
      <h4>Create tree</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-tree")}
      />
    </div>
  );
};
