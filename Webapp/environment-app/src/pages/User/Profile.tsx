import { useEffect, useRef, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { TREE_TRIM_SCHEDULE_DELETE, EMPLOYEE_DETAIL, useApi } from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { useCookies } from "react-cookie";

export const Profile = () => {
  const [token] = useCookies(["accessToken"]);
  const { email = " " } = useParams();
  const [data, setData] = useState<any>();
  const [loading, setLoading] = useState<boolean>(true);

  const ref = useRef<any>();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await useApi.get(EMPLOYEE_DETAIL.replace(":email", email));
        setData(response.data);
      } catch (error) {
        console.error("Error fetching employee detail:", error);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [email]);

  return loading ? (
    <ClipLoader
      className="spinner"
      color={"hsl(94, 59%, 35%)"}
      loading={loading}
      size={60}
    />
  ) : (
    <div >
      <div className="main-layout row ">
        <h4 className="title">Hồ sơ cá nhân</h4>
        <hr className="line" />
        <div className="image col-md-2 ">
          <div><img src={JSON.parse(token.accessToken).image || '../assets/imgs/avatar.jpg'} alt="userAvatar" /></div>
        </div>
        <div className="detail-content col-md-10">
          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Tên Nhân Viên: </div>
              <div className="detail-content-child-value">
                {JSON.parse(token.accessToken).name}
              </div>
            </div>
          </div>

          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Email: </div>
              <div className="detail-content-child-value">
                {JSON.parse(token.accessToken).email}
              </div>
            </div>
          </div>

          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Số Điện Thoại: </div>
              <div className="detail-content-child-value">
                {data?.phoneNumber}
              </div>
            </div>
          </div>

          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">
                Bộ Phận:{" "}
              </div>
              <div className="detail-content-child-value">
                {JSON.parse(token.accessToken).department}
              </div>
            </div>
          </div>
          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Chức Vụ: </div>
              <div className="detail-content-child-value">
                {JSON.parse(token.accessToken).role}
              </div>
            </div>
          </div>

          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">
                Địa Chỉ Thường Trú:{" "}
              </div>
              <div className="detail-content-child-value">
                {data?.address}
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};
