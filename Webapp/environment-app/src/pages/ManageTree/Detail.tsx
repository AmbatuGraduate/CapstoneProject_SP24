import { useEffect, useState } from "react";
import { TREE_DETAIL, useApi } from "../../api";
import { Field, FormBase } from "../../components/FormBase";
import { useNavigate, useParams } from "react-router-dom";
import { dayFormat } from "../../utils";

export const DetailTree = () => {
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const [data, setData] = useState<any>();

  const fetch = async () => {
    try {
      const data = await useApi.get(TREE_DETAIL.replace(":id", id));
      setData(data.data);
    } catch (error) {
      console.log(error);
    }
  };

  useEffect(() => {
    fetch();
  }, []);

  const fields: Field[] = [
    {
      label: "Mã cây",
      formType: "input",
      key: "treeCode",
      defaultValue: data?.treeCode,
    },
    {
      label: "Quận",
      formType: "select",
      key: "quan",
      defaultValue: 3,
      options: [
        {
          key: "Thanh Khê",
          value: 1,
        },
        {
          key: "Hải Châu",
          value: 2,
        },
        {
          key: "Ngũ Hành Sơn",
          value: 3,
        },
        {
          key: "Sơn Trà",
          value: 4,
        },
        {
          key: "Liên Chiểu",
          value: 5,
        },
        {
          key: "Cẩm Lệ",
          value: 6,
        },
      ],
    },
    {
      label: "Tuyến đường",
      formType: "input",
      key: "streetName",
      defaultValue: data?.streetName,
    },
    {
      label: "Giống cây",
      formType: "input",
      key: "cultivar",
      defaultValue: data?.cultivar,
    },
    {
      label: "Đường kính thân",
      formType: "number",
      key: "bodyDiameter",
      defaultValue: data?.bodyDiameter,
    },
    {
      label: "Tán lá",
      formType: "number",
      key: "leafLength",
      defaultValue: data?.leafLength,
    },
    {
      label: "Thời điểm trồng",
      formType: "input",
      key: "plantTime",
      defaultValue: dayFormat(data?.plantTime),
    },
    {
      label: "Thời điểm cắt tỉa gần nhất",
      formType: "input",
      key: "cutTime",
      defaultValue: dayFormat(data?.cutTime),
    },
    {
      label: "Ghi chú",
      formType: "textarea",
      key: "note",
      defaultValue: data?.note,
    },
  ];

  return (
    <FormBase
      fields={fields}
      mode="view"
      backPage={() => navigate("/manage-tree")}
      navigateUpdate={() => navigate(`/manage-tree/${data?.treeCode}/update`)}
    />
  );
};
