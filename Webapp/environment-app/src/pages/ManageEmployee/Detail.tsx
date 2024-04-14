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
        return <h6>{row.myEvent.location}</h6>;
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
    <div >
      <div className="main-layout row ">
        <h4 className="title">Xem Thông Tin Chi Tiết Nhân Viên</h4>
        <hr className="line" />
        <div className="image col-md-2 ">
          <div><img src={data?.picture || '../assets/imgs/avatar.jpg'} alt="userAvatar" /></div>
        </div>
        <div className="detail-content col-md-10">
          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Tên Nhân Viên: </div>
              <div className="detail-content-child-value">
                {data?.name}
              </div>
            </div>
          </div>

          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Email: </div>
              <div className="detail-content-child-value">
                {data?.email}
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
                {data?.department}
              </div>
            </div>
          </div>
          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Chức Vụ: </div>
              <div className="detail-content-child-value">
                {roleFormat(data?.role).text}
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

          <div className="button-cover grid">
            <Button
              className="btnCancel"
              variant="danger"
              onClick={handleNavigate}
            >
              Trở Về
            </Button>
            <Link to={`/manage-employee/${data?.email}/update`}>
              <Button className="btnLink" variant="success">
                Bảo Mật
              </Button>
            </Link>
            <Link to={`/manage-employee/${data?.email}/update`}>
              <Button className="btnLink" variant="success">
                Cập Nhật
              </Button>
            </Link>
          </div>
        </div>
      </div>


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
