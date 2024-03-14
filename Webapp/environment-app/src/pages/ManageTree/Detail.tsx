import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { TREE_DETAIL, useApi } from "../../Api";

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

  return <div>'{JSON.stringify(data)}</div>;
};
