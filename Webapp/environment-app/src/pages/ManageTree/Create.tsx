import { useNavigate } from "react-router-dom";
import { Field, FormBase } from "../../Components/FormBase";
import {
  CULTIVAR_LIST,
  STREET_LIST,
  TREE_ADD,
  TREE_TYPE_LIST,
  TREE_UPDATE,
  useApi,
} from "../../Api";
import { useState } from "react";
import { dayFormat } from "../../utils";

export const CreateTree = () => {
  const navigate = useNavigate();
  const CalendarComponent = () => {
    const [calendar, setCalendar] = useState("");
    const handleSelect = (date) => {
      console.log(date);
      setCalendar(dayFormat(date));
    };
  };

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
      formType: "select",
      key: "streetId",
      optionExtra: {
        url: STREET_LIST,
        _key: "streetName",
        _value: "streetId",
      },
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
      formType: "input",
      key: "plantTime",
    },
    {
      label: "Thời gian cắt",
      formType: "input",
      key: "cutTime",
    },
    {
      label: "Khoảng thời gian cắt",
      formType: "input",
      key: "intervalCutTime",
    },
    {
      label: "Loại cây",
      formType: "select",
      key: "treeTypeId",
      optionExtra: {
        url: TREE_TYPE_LIST,
        _key: "treeTypeName",
        _value: "treeTypeId",
      },
    },
    {
      label: "Giống cây",
      formType: "select",
      key: "cultivarId",
      optionExtra: {
        url: CULTIVAR_LIST,
        _key: "cultivarName",
        _value: "treeTypeId",
      },
    },
    {
      label: "Ghi chú",
      formType: "input",
      key: "note",
    },
    {
      label: "Người tạo",
      formType: "input",
      key: "createBy",
    },
    {
      label: "Cập nhật bởi",
      formType: "input",
      key: "updateBy",
    },
    {
      label: "Ngày tạo",
      formType: "input",
      key: "updateDate",
    },
    {
      label: "Tình trạng cây",
      formType: "select",
      key: "isExist",
      defaultValue: 2,
      options: [
        {
          key: "Cần cắt",
          value: 1,
        },
        {
          key: "Đã cắt",
          value: 2,
        },
      ],
    },
  ];

  const handleSubmit = async (data: Record<string, unknown>) => {
    await useApi.post(TREE_ADD, data);
    console.log("CreateTree", data);
  };

  return (
    <FormBase
      fields={fields}
      onSave={handleSubmit}
      onCancel={() => navigate("/manage-tree")}
    />
  );
};
