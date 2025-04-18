import { Link } from "react-router-dom";
import { DELETE_REPORT, REPORT_BY_USER, REPORT_LIST, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { ReportImpact, ReportStatus, dayFormat } from "../../utils";
import { useRef } from "react";
import { useCookies } from "react-cookie";
import ModalDelete from "../../Components/Modals/ModalDelete";

export const ManageReport = () => {
  const [token] = useCookies(["accessToken"]);
  const isAdmin = JSON.parse(token.accessToken).role === "Admin" || "HR" || "Manager";
  const isUser = JSON.parse(token.accessToken);
  const handleDelete = async (id: string) => {
    await useApi.delete(DELETE_REPORT.replace(":id", id));
    ref.current?.reload();
  };


  const email = isUser.email;
  const ref = useRef<any>();
  // TODO get list

  const columns: Column[] = [
    {
      header: "",
      accessorFn(row) {
        return (
          <div>
            <button type="button" className="btn btn-click" onClick={() => { }}>
              <ModalDelete handleDelete={() => handleDelete(row.id)} />
            </button>
          </div>
        );
      },
      width: "1%",
    },
    {
      header: "Người Gửi",
      accessorFn(longRow) {
        console.log(longRow)
        return (
          <h6 className="shortText linkDiv" style={{ margin: 'auto' }}>
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
      width: "15%",
    },
    {
      header: "Tiêu Đề",
      accessorFn(row) {
        const modifiedSubject = row.reportSubject.replace("[Report]", "");
        return <h6 style={{ padding: '0 1rem' }}>{modifiedSubject}</h6>;
      },
      width: "20%",
    },
    {
      header: "Cần Giải Quyết Trước",
      accessorFn(row) {
        return (
          <h6 className="shortText">{dayFormat(row.expectedResolutionDate)}</h6>
        );
      },
      width: "10%",
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
      width: "10%",
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
      width: "10%",
    },
  ];

  const sortReports = (reports) => {
    return reports.sort((a, b) => {
      // Sắp xếp theo mức độ ảnh hưởng
      if (ReportImpact(a.reportImpact).text !== ReportImpact(b.reportImpact).text) {
        return ReportImpact(a.reportImpact).text.localeCompare(ReportImpact(b.reportImpact).text);
      }
      // Nếu cùng mức độ ảnh hưởng, sắp xếp theo trạng thái
      return ReportStatus(a.reportStatus).text.localeCompare(ReportStatus(b.reportStatus).text);
    });
  };

  return (
    <div>
      <ListView
        ref={ref}
        listURL={isAdmin ? REPORT_LIST : REPORT_BY_USER.replace(":email", email)}
        columns={columns}
        transform={(data: any) => sortReports(data?.value?.map((i) => i.reportFormat) || [])}
      />
    </div>
  );
};