import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { EMPLOYEE_LIST, EMPLOYEE_DELETE, useApi, DEPARTMENT_EMPLOYEE } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { useRef } from "react";
import { roleFormat } from "../../utils";

import { MdAddCircleOutline } from "react-icons/md";
import { useCookies } from "react-cookie";

export const ManageEmployee = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [token] = useCookies(["accessToken"]);

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
      width: "2%",
    },
    {
      header: "Tên Nhân Viên",
      accessorFn(row) {
        return (
          <h6 className="shortText linkDiv">
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
      header: "Số Điện Thoại",
      accessorFn(row) {
        return <h6 className="shortText">{row.phoneNumber}</h6>;
      },
      width: "10%",
    },
    {
      header: "Bộ Phận",
      accessorFn(row) {
        return <h6 className="shortText">{row.department}</h6>;
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
        if (row.picture == null) {
          return <h6 className="shortText"><img src="../assets/imgs/avatar.jpg" /></h6>;
        } else {
          return <h6 className="shortText"><img src={row.picture} /></h6>;
        }
      },
      width: "10%",
    },

  ];

  const listURL = JSON.parse(token.accessToken).role === "Admin" ? EMPLOYEE_LIST : DEPARTMENT_EMPLOYEE.replace(':groupEmail', JSON.parse(token.accessToken).departmentEmail);

  return (
    <div>
      <ListView
        ref={ref}
        listURL={listURL}
        columns={columns}
        bottom={
          <Button
            variant="success"
            onClick={() => navigate("/manage-employee/create")}
          >
            <MdAddCircleOutline className="iconAdd" />
            Thêm Nhân Viên
          </Button>
        }
        filter={(row) => {
          return row.role != "Admin";
        }}
      />
    </div>
  );
};
