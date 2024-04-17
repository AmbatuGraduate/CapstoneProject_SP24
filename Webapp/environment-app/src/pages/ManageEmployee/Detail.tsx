import { useEffect, useRef, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { TREE_TRIM_SCHEDULE_DELETE, EMPLOYEE_DETAIL, useApi, EMPLOYEE_SCHEDULE } from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { Column } from "../../Components/ListView/Table";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { taskStatus, timeFormat, dayFormat, roleFormat } from "../../utils";
import { ListView } from "../../Components/ListView";

export const DetailEmployee = () => {
  const navigate = useNavigate();
  const { email = " " } = useParams();
  const [data, setData] = useState<any>();
  const [loading, setLoading] = useState<boolean>(true);
  const handleNavigate = () => {
    navigate("/manage-employee");
  };

  const ref = useRef<any>();

  const handleDelete = async (id: string) => {
    await useApi.delete(TREE_TRIM_SCHEDULE_DELETE.replace(":id", id));
    ref.current?.reload();
  };

  const columns: Column[] = [
    {
      header: "",
      accessorFn(row) {
        return (
          <div>
            <button type="button" className="btn btn-click" onClick={() => { }}>
              <ModalDelete handleDelete={() => handleDelete(row.myEvent.id)} />
            </button>
          </div>
        );
      },
      width: "2%",
    },
    {
      header: "Thời Gian",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            {timeFormat(row.myEvent.start) + "-" + timeFormat(row.myEvent.end)}
          </h6>
        );
      },
      width: "10%",
    },
    {
      header: "Ngày Làm",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            {dayFormat(row.myEvent.end)}
          </h6>
        );
      },
      width: "10%",
    },
    {
      header: "Tiêu Đề",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            <Link
              className="linkCode"
              style={{ fontWeight: "bold", textAlign: "center" }}
              to={`/manage-treetrim-schedule/${row.myEvent.id}`}
            >
              {row.myEvent.summary}
            </Link>
          </h6>
        );
      },
      width: "20%",
    },
    {
      header: "Địa Chỉ Cụ Thể",
      accessorFn(row) {
        return <h6 style={{ padding: '0 1rem' }}>{row.myEvent.location}</h6>;
      },
      width: "40%",
    },
    {
      header: "Trạng Thái",
      accessorFn(row) {
        return (
          <h6
            className="shortText"
            style={{
              color: taskStatus(
                row.myEvent.extendedProperties.privateProperties.JobWorkingStatus
              ).color,
              fontWeight: "bold",
            }}
          >
            {
              taskStatus(
                row.myEvent.extendedProperties.privateProperties.JobWorkingStatus
              ).text
            }
          </h6>
        );
      },
    },
  ];

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
    <div>
      {/* ----------------------------------------------------------------------------------------------- */}
      <div className="main-layout row">
        <h4 className="title">Thông Tin Chi Tiết Nhân Viên</h4>
        <hr className="line" />


        <div className="profileContainer">
          <div className="profileImage">
            <img src={data?.picture || '../assets/imgs/avatar.jpg'} alt="userAvatar" />
            <Link to={`/manage-employee/${data?.email}/update`}>
              <Button className="btnLink" variant="success" style={{ width: '100%', fontWeight: 'bold' }}>
                Cập Nhật Thông Tin
              </Button>
            </Link>
          </div>
          <div className="profileInfo">
            <p className="employeeName">{data?.name}</p>
            <p className="employeeRole">{roleFormat(data?.role).text}</p>

            <p className="employeeCode"><span className="infoTextLabel">Địa chỉ email</span> <span className="infoText">{data?.email}</span></p>
            <p className="employeeCode"><span className="infoTextLabel">Số điện thoại</span> <span className="infoText">{data?.phoneNumber}</span></p>
            <p className="employeeCode"><span className="infoTextLabel">Bộ phận</span> <span className="infoText">{data?.department}</span></p>
            <p className="employeeCode"><span className="infoTextLabel">Địa chỉ thường trú</span> <span className="infoText">{data?.address}</span></p>

          </div>
        </div>
      </div>

      {/* ----------------------------------------------------------------------------------------------- */}
      {/* schedule */}
      <div>
        <ListView
          ref={ref}
          listURL={EMPLOYEE_SCHEDULE.replace(":email", data?.email)}
          columns={columns}
        />
      </div>
    </div>
  );
};
