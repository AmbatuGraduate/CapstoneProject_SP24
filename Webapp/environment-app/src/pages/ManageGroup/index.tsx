import { Button } from "react-bootstrap";
import { Link, useNavigate, useParams } from "react-router-dom";
import { GROUP_DELETE, GROUP_LIST, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { useRef } from "react";
import { MdAddCircleOutline } from "react-icons/md";
import ModalDelete from "../../Components/Modals/ModalDelete";

export const ManageGroup = () => {
  const { departmentEmail = "" } = useParams();
  const navigate = useNavigate();
  const ref = useRef<any>();
  // TODO get list

  const handleDelete = async (id: string) => {
    await useApi.delete(GROUP_DELETE.replace(":email", departmentEmail));
    ref.current?.reload();
  };

  const columns: Column[] = [
    {
      header: "",
      accessorFn(row) {
        return (
          <div>
            <button type="button" className="btn btn-click" onClick={() => { }}>
              <ModalDelete handleDelete={() => handleDelete(row.departmentEmail)} />
            </button>
          </div>
        );
      },
      width: "8%",
    },
    {
      header: "Email",
      accessorFn(longRow) {
        return (
          <h6>
            <Link
              className="linkCode"
              style={{ fontWeight: "bold" }}
              to={`/manage-group/email=${departmentEmail}`}
            >
              {longRow.email}
            </Link>
          </h6>
        );
      },
      width: "8%",
    },
    {
      header: "Tên bộ phận",
      accessorFn(row) {
        return <h6 className="shortText">{row.name}</h6>;
      },
      width: "30%",
    },
    {
      header: "Mô tả",
      accessorFn(row) {
        return <h6 className="shortText">{row.description}</h6>;
      },
      width: "20%",
    },
    {
      header: "Số nhân viên",
      accessorFn(row) {
        return <h6 className="shortText">{row.directMembersCount}</h6>;
      },
    },
  ];

  return (
    <div>
      <ListView
        ref={ref}
        listURL={GROUP_LIST}
        columns={columns}
        bottom={
          <Button
            variant="success"
            style={{
              backgroundColor: "hsl(94, 59%, 35%)",
              border: "none",
              padding: "0.5rem 1rem",
            }}
            onClick={() => navigate("/manage-group/create")}
          >
            <MdAddCircleOutline className="iconAdd" />
            Thêm Bộ Phận
          </Button>
        }
      />
    </div>
  );
};
