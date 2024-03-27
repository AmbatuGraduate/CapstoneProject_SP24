import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { REPORT_LIST, TREE_DELETE, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { dayFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { useRef } from "react";

import { MdAddCircleOutline } from "react-icons/md";

export const ManageReport = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  // TODO get list

  const handleDelete = async (id: string) => {
    await useApi.delete(TREE_DELETE.replace(":id", id));
    ref.current?.reload();
  };

  const columns: Column[] = [
    {
      header: "Chỉnh sửa",
      accessorFn(row) {
        return (
          <div>
            <button type="button" className="btn btn-click" onClick={() => {}}>
              <ModalDelete handleDelete={() => handleDelete(row.reportId)} />
            </button>
          </div>
        );
      },
    },
    {
      header: "Mã báo cáo",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            <Link
              className="linkCode"
              style={{ fontWeight: "bold" }}
              to={`/manage-report/${row.reportId}`}
            >
              {row.reportId}
            </Link>
          </h6>
        );
      },
    },
    {
      header: "Người gửi",
      accessorFn(longRow) {
        return <h6>{longRow.issuerGmail}</h6>;
      },
    },
    {
      header: "Trạng thái",
      accessorFn(row) {
        return <h6>{row.status}</h6>;
      },
    },
    {
      header: "Mức độ ảnh hưởng",
      accessorFn(row) {
        return <h6>{row.reportImpact}</h6>;
      },
    },
    {
      header: "Ngày giải quyết",
      accessorFn(row) {
        return <h6>{dayFormat(row.expectedResolutionDate)}</h6>;
      },
    },
    {
      header: "Ngày đã giải quyết",
      accessorFn(row) {
        return <h6>{dayFormat(row.actualResolutionDate)}</h6>;
      },
    },
  ];

  return (
    <div>
      <ListView
        ref={ref}
        listURL={REPORT_LIST}
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
            Thêm báo cáo
          </Button>
        }
        filter={(row) => {
          return row.isExist == true;
        }}
      />
    </div>
  );
};
