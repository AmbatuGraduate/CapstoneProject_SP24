import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { GROUP_LIST } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { ReportImpact, ReportStatus, dayFormat } from "../../utils";
import { useRef } from "react";

import { MdAddCircleOutline } from "react-icons/md";

export const ManageGroup = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  // TODO get list

  // const handleDelete = async (id: string) => {
  //   await useApi.delete(TREE_DELETE.replace(":id", id));
  //   ref.current?.reload();
  // };

  const columns: Column[] = [
    {
      header: "Email",
      accessorFn(longRow) {
        return (
          <h6 className="shortText">
            <Link
              className="linkCode"
              style={{ fontWeight: "bold" }}
              to={`/manage-group/${longRow.id}`}
            >
              {longRow.email}
            </Link>
          </h6>
        );
      },
    },
    {
      header: "Tên bộ phận",
      accessorFn(row) {
        return <h6>{row.name}</h6>;
      },
    },
    {
      header: "Mô tả",
      accessorFn(row) {
        return <h6 className="shortText">{row.description}</h6>;
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
            onClick={() => navigate("/manage-report/create")}
          >
            <MdAddCircleOutline className="iconAdd" />
            Thêm báo cáo
          </Button>
        }
      />
    </div>
  );
};
