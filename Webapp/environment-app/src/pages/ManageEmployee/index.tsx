import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { EMPLOYEE_LIST, EMPLOYEE_DELETE, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { useRef } from "react";

import { MdAddCircleOutline } from "react-icons/md";

export const ManageEmployee = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();


  const handleDelete = async (id: string) => {
    await useApi.delete(EMPLOYEE_DELETE.replace(":id", id));
    ref.current?.reload();
  };

  const columns: Column[] = [
    // {
    //   header: "",
    //   accessorFn(row) {
    //     return (
    //       <input
    //         type="checkbox"
    //         onChange={(e) => handleCheckboxChange(e, row.id)}
    //         checked={selectedRows.includes(row.id)}
    //       />
    //     );
    //   },
    // },
    {
      header: "",
      accessorFn(row) {
        return (
          <div>
            <button type="button" className="btn btn-click" onClick={() => { }}>
              <ModalDelete handleDelete={() => handleDelete(row.treeCode)} />
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
          <h6 className="shortText">
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
        return <h6 className="shortText">{row.role}</h6>;
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

  return (
    <div>
      <ListView
        ref={ref}
        listURL={EMPLOYEE_LIST}
        columns={columns}
        bottom={
          <Button
            variant="success"
            style={{
              backgroundColor: "hsl(94, 59%, 35%)",
              border: "none",
              padding: "0.5rem 1rem",
            }}
            onClick={() => navigate("/manage-tree/create")}
          >
            <MdAddCircleOutline className="iconAdd" />
            Thêm Nhân Viên
          </Button>
        }
      />
    </div>
  );
};
