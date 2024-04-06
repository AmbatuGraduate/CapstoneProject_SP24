import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { TREE_DETAIL, useApi } from "../../Api";
import { dayFormat } from "../../utils";
import "./style.scss";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import SimpleMap from "./MapIntergration";

export const DetailTree = () => {
  const navigate = useNavigate();
  const { id = "" } = useParams();
  const { email = " " } = useParams();
  const [data, setData] = useState<any>();
  const [loading, setLoading] = useState<boolean>(true);
  const handleNavigate = () => {
    navigate("/manage-tree");
  };

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
    <div className="main-layout row">
      <div className="map col-md-6">
        <SimpleMap />
      </div>
      <div className="detail-content col-md-6">
        <div className="detail-cover">
          <div className="detail-content-parent">
            <div className="detail-content-child-label">Mã Số Cây: </div>
            <div className="detail-content-child-value">{data?.treeCode}</div>
          </div>
        </div>

        <div className="detail-cover">
          <div className="detail-content-parent">
            <div className="detail-content-child-label">Tên Đường: </div>
            <div className="detail-content-child-value">{data?.streetName}</div>
          </div>
        </div>

        <div className="detail-cover">
          <div className="detail-content-parent">
            <div className="detail-content-child-label">Loại Cây: </div>
            <div className="detail-content-child-value">{data?.treeType}</div>
          </div>
        </div>

        <div className="detail-cover">
          <div className="detail-content-parent">
            <div className="detail-content-child-label">Đường Kính Thân: </div>
            <div className="detail-content-child-value">
              {data?.bodyDiameter}
            </div>
          </div>
          <div className="detail-content-parent">
            <div className="detail-content-child-label">Tán Lá: </div>
            <div className="detail-content-child-value">{data?.leafLength}</div>
          </div>
        </div>

        <div className="detail-cover">
          <div className="detail-content-parent">
            <div className="detail-content-child-label">Thời Điểm Trồng: </div>
            <div className="detail-content-child-value">
              {dayFormat(data?.plantTime)}
            </div>
          </div>
          <div className="detail-content-parent">
            <div className="detail-content-child-label">Thời Điểm Cắt: </div>
            <div className="detail-content-child-value">
              {dayFormat(data?.cutTime)}
            </div>
          </div>
        </div>

        <div className="detail-cover">
          <div className="detail-content-parent">
            <div className="detail-content-child-label">Người Phụ Trách: </div>
            <Link to={`/manage-employee/email=${data?.user}`}>
              <div className="detail-content-child-value">{data?.user}</div>
            </Link>
          </div>
        </div>

        <div className="button-cover grid">
          <Button
            className="btnCancel"
            variant="danger"
            onClick={handleNavigate}
          >
            Trở Về
          </Button>
          <Link to={`/manage-tree/${data?.treeCode}/update`}>
            <Button className="btnLink" variant="success">
              Cập Nhật
            </Button>
          </Link>
        </div>
      </div>
    </div>
  );
};
