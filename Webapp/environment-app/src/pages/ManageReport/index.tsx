import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { REPORT_LIST } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { ReportImpact, ReportStatus, dayFormat } from "../../utils";
import { useRef } from "react";

import { MdAddCircleOutline } from "react-icons/md";

export const ManageReport = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  // TODO get list

  // const handleDelete = async (id: string) => {
  //   await useApi.delete(TREE_DELETE.replace(":id", id));
  //   ref.current?.reload();
  // };

  const columns: Column[] = [
    {
      header: "Người Gửi",
      accessorFn(longRow) {
        return (
          <h6 className="shortText">
            <Link
              className="linkCode"
              style={{ fontWeight: "bold" }}
              to={`/manage-report/${longRow.id}`}
            >
              {longRow.issuerEmail}
            </Link>
          </h6>
        );
      },
      width: "19%",
    },
    {
      header: "Tiêu Đề",
      accessorFn(row) {
        const modifiedSubject = row.reportSubject.replace("[Report]", "");
        return <h6>{modifiedSubject}</h6>;
      },
      width: "8%",
    },
    {
      header: "Cần Giải Quyết Trước",
      accessorFn(row) {
        return (
          <h6 className="shortText">{dayFormat(row.expectedResolutionDate)}</h6>
        );
      },
      width: "19%",
    },
    {
      header: "Trạng Thái",
      accessorFn(row) {
        return (
          <h6
            className="shortText"
            style={{
              color: ReportStatus(row.reportStatus).color,
              fontWeight: "bold",
            }}
          >
            {ReportStatus(row.reportStatus).text}
          </h6>
        );
      },
      width: "15%",
    },
    {
      header: "Mức Độ Ảnh Hưởng",
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
      width: "20%",
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
            onClick={() => navigate("/manage-report/create")}
          >
            <MdAddCircleOutline className="iconAdd" />
            Thêm báo cáo
          </Button>
        }
        transform={(data: any) => data?.value?.map((i) => i.reportFormat) || []}
      />
    </div>
  );
};
