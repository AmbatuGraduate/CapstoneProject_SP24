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
    // {
    //   header: "Chỉnh sửa",
    //   accessorFn(row) {
    //     return (
    //       <div>
    //         <button type="button" className="btn btn-click" onClick={() => {}}>
    //           <ModalDelete handleDelete={() => handleDelete(row.id)} />
    //         </button>
    //       </div>
    //     );
    //   },
    // },
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
      width: "25%",
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
    {
      header: "Cần Giải Quyết Trước",
      accessorFn(row) {
        return <h6 className="shortText">{dayFormat(row.expectedResolutionDate)}</h6>;
      },
      width: "15%",
    },
    {
      header: "Ngày Đã Giải Quyết",
      accessorFn(row) {
        return <h6 className="shortText">{dayFormat(row.actualResolutionDate)}</h6>;
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
            Thêm Báo Cáo
          </Button>
        }
        transform={(data: any) => data?.value?.map((i) => i.reportFormat) || []}
      />
    </div>
  );
};