import { useEffect, useRef, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import {
  TREE_TRIM_SCHEDULE_DELETE,
  useApi,
  EMPLOYEE_SCHEDULE,
  GROUP_DETAIL,
} from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { Column } from "../../Components/ListView/Table";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { taskStatus, timeFormat, dayFormat } from "../../utils";
import { ListView } from "../../Components/ListView";
import { useCookies } from "react-cookie";


export const DetailGroup = () => {
  const { email = "" } = useParams();
  const [token] = useCookies(["accessToken"]);
  const [data, setData] = useState<any>();
  const [loading, setLoading] = useState<boolean>(true);

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
        return <h6 className="shortText">{dayFormat(row.myEvent.end)}</h6>;
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
                row.myEvent.extendedProperties.privateProperties
                  .JobWorkingStatus
              ).color,
              fontWeight: "bold",
            }}
          >
            {
              taskStatus(
                row.myEvent.extendedProperties.privateProperties
                  .JobWorkingStatus
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
        const response = await useApi.get(
          GROUP_DETAIL.replace(":email", email)
        );
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
      <div className="main-layout row ">
        <h4 className="title">Xem Thông Tin Chi Tiết Bộ Phận</h4>
        <hr className="line" />
        <div className="detail-content col-md-10">
          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Tên Bộ Phận: </div>
              <div className="detail-content-child-value">{data?.value?.name}</div>
            </div>
          </div>

          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Email: </div>
              <div className="detail-content-child-value">{data?.value?.email}</div>
            </div>
          </div>

          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Mô Tả: </div>
              <div className="detail-content-child-value">
                {data?.value?.description}
              </div>
            </div>
          </div>

          <div className="detail-cover">
            <div className="detail-content-parent">
              <div className="detail-content-child-label">Nhân viên: </div>
              <div className="detail-content-child-value">
                {data?.value?.directMembersCount}
              </div>
            </div>
          </div>

          <div className="button-cover grid">
            {(JSON.parse(token.accessToken).role == "Admin" || JSON.parse(token.accessToken).role == "HR") && (
              <Link to={`/manage-employee/${data?.email}/update`}>
                <Button className="btnLink" variant="success">
                  Cập Nhật
                </Button>
              </Link>
            )}
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
