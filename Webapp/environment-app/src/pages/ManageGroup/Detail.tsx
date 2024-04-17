import { useEffect, useRef, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import {
  TREE_TRIM_SCHEDULE_DELETE,
  useApi,
  GROUP_DETAIL,
  DEPARTMENT_EMPLOYEE,
  EMPLOYEE_DELETE,
} from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { Column } from "../../Components/ListView/Table";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { roleFormat } from "../../utils";
import { ListView } from "../../Components/ListView";
import { useCookies } from "react-cookie";


export const DetailGroup = () => {
  const { email = "" } = useParams();
  const [token] = useCookies(["accessToken"]);
  const [data, setData] = useState<any>();
  const [loading, setLoading] = useState<boolean>(true);

  const ref = useRef<any>();

  const handleDelete = async (email: string) => {
    await useApi.delete(EMPLOYEE_DELETE.replace(":email", email));
    ref.current?.reload();
  };

  const columns: Column[] = [
    {
      header: "",
      accessorFn(row) {
        return (
          <div>
            <button type="button" className="btn btn-click" onClick={() => { }}>
              <ModalDelete handleDelete={() => handleDelete(row.email)} />
            </button>
          </div>
        );
      },
      width: "1%",
    },
    {
      header: "Tên Nhân Viên",
      accessorFn(row) {
        return (
          <h6 className="shortText linkDiv" style={{ margin: 'auto' }}>
            <Link
              className="linkCode"
              style={{ fontWeight: "bold" }}
              to={`/manage-employee/email=${row.email}`}
            >
              {row.name}
            </Link>
          </h6>
        );
      },
      width: "10%",
    },
    {
      header: "Email",
      accessorFn(row) {
        return <h6 className="shortText">{row.email}</h6>;
      },
      width: "15%",
    },
    {
      header: "Số Điện Thoại",
      accessorFn(row) {
        return <h6 className="shortText">{row.phoneNumber}</h6>;
      },
      width: "10%",
    },
    {
      header: "Chức Vụ",
      accessorFn(row) {
        return <h6 className="shortText">{roleFormat(row.role).text}</h6>;
      },
      width: "10%",
    },
    {
      header: "Ảnh",
      accessorFn(row) {
        if (row.picture == "") {
          return <h6 className="shortText"><img src="https://i.imgur.com/CfPvx7O.jpg" /></h6>;
        } else {
          return <h6 className="shortText"><img src={row.picture} /></h6>;
        }
      },
      width: "10%",
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
              <Link to={`/manage-group/${data?.value?.email}/update`}>
                <Button className="btnLink" variant="success">
                  Cập Nhật
                </Button>
              </Link>
            )}
          </div>
        </div>
      </div>

      {/* ----------------------------------------------------------------------------------------------- */}
      {/* employee of group */}
      <div>
        <ListView
          ref={ref}
          listURL={DEPARTMENT_EMPLOYEE.replace(":groupEmail", data?.value?.email)}
          columns={columns}
        />
      </div>
    </div>
  );
};
