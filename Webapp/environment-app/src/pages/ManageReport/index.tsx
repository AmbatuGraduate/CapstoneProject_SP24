import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { REPORT_LIST, TREE_DELETE, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { ReportImpact, ReportStatus, dayFormat } from "../../utils";
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
      header: "Người gửi",
      accessorFn(longRow) {
        return (
          <h6 className="shortText">
            <Link
              className="linkCode"
              style={{ fontWeight: "bold" }}
              to={`/manage-report/${longRow.reportId}`}
            >
              {longRow.issuerGmail}
            </Link>
          </h6>
        );
      },
      width: "10%",
    },
    {
      header: "Trạng thái",
      accessorFn(row) {
        return (
          <h6
            className="shortText"
            style={{
              color: ReportStatus(row.status).color,
              fontWeight: "bold",
            }}
          >
            {ReportStatus(row.status).text}
          </h6>
        );
      },
    },
    {
      header: "Mức độ ảnh hưởng",
      accessorFn(row) {
        return (
          <h6
            className="shortText"
            style={{
              color: ReportImpact(row.reportImpact).color,
              fontWeight: "bold",
            }}
          >
            {ReportImpact(row.reportImpact).text}
          </h6>
        );
      },
    },
    {
      header: "Cần giải quyết trước",
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
            onClick={() => navigate("create")}
          >
            <MdAddCircleOutline className="iconAdd" />
            Thêm báo cáo
          </Button>
        }
        transform={(data) => data?.result?.value?.map((i) => i.report) || []}
      />
    </div>
  );
};
