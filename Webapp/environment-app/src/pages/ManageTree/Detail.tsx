import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { TREE_DETAIL, useApi } from "../../Api";
import { dayFormat } from "../../utils";
import "./style.scss"
import { ClipLoader } from "react-spinners";

export const DetailTree = () => {
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const [data, setData] = useState<any>();
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    
    const fetchData = async () => {
      try {
        const response = await useApi.get(TREE_DETAIL.replace(":id", id));
        setData(response.data);
      } catch (error) {
        console.error("Error fetching tree detail:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [id]);

  return loading ? (
    <ClipLoader
      className="spinner"
      color={"hsl(94, 59%, 35%)"}
      loading={loading}
      size={60}
    />
  ) : (
    <div>
      <div className="main-layout">
        <div className="tree-detail-img">
          <img src="/assets/imgs/imgcondau4.jpg" alt="Admin Image" />
        </div>
        <div className="tree-detail-content">
          <div className="tree-detail-content-parent">
            <div className="tree-detail-content-child-label">Mã số cây: </div>
            <div className="tree-detail-content-child-value">{data?.treeCode}</div>
          </div>
          <div className="tree-detail-content-parent">
            <div className="tree-detail-content-child-label">Tên đường: </div>
            <div className="tree-detail-content-child-value">{data?.streetName}</div>
          </div>
          <div className="tree-detail-content-parent">
            <div className="tree-detail-content-child-label">Giống cây: </div>
            <div className="tree-detail-content-child-value">{data?.cultivar}</div>
          </div>
          <div className="tree-detail-content-parent">
            <div className="tree-detail-content-child-label">Đường kính thân: </div>
            <div className="tree-detail-content-child-value">{data?.bodyDiameter}</div>
          </div>
          <div className="tree-detail-content-parent">
            <div className="tree-detail-content-child-label">Tán lá: </div>
            <div className="tree-detail-content-child-value">{data?.leafLength}</div>
          </div>
          <div className="tree-detail-content-parent">
            <div className="tree-detail-content-child-label">Thời điểm trồng: </div>
            <div className="tree-detail-content-child-value">{dayFormat(data?.plantTime)}</div>
          </div>
          <div className="tree-detail-content-parent">
            <div className="tree-detail-content-child-label">Thời điểm cắt: </div>
            <div className="tree-detail-content-child-value">{dayFormat(data?.cutTime)}</div>
          </div>
        </div>
      </div>
    </div>
  );
};