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

        {/* ===================================================================== */}
        <div className="profileContainer">
          <div className="profileImage">
            <img src={JSON.parse(token.accessToken).image || '../assets/imgs/avatar.jpg'} alt="userAvatar" />
          </div>
          <div className="profileInfo">
            <p className="employeeName">{JSON.parse(token.accessToken).name}</p>
            <p className="employeeRole">{JSON.parse(token.accessToken).role}</p>

            <p className="employeeCode"><span className="infoTextLabel">Địa chỉ email</span> <span className="infoText">{JSON.parse(token.accessToken).email}</span></p>
            <p className="employeeCode"><span className="infoTextLabel">Số điện thoại</span> <span className="infoText">{data?.phoneNumber}</span></p>
            <p className="employeeCode"><span className="infoTextLabel">Bộ phận</span> <span className="infoText">{JSON.parse(token.accessToken).department}</span></p>
            <p className="employeeCode"><span className="infoTextLabel">Địa chỉ thường trú</span> <span className="infoText">{data?.address}</span></p>

          </div>

        </div>

        {/* ===================================================================== */}
      </div>
    </div>
  );
};
