import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { TREE_TRIM_SCHEDULE } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { taskStatus, timeFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { MdAddCircleOutline } from "react-icons/md";

export const ManageTreeTrimSchedule = () => {
  const navigate = useNavigate();

  const columns: Column[] = [
    {
      header: "",
      accessorFn(row) {
        return (
          <div>
            <button type="button" className="btn btn-click" onClick={() => {}}>
              <ModalDelete />
            </button>
          </div>
        );
      },
      width: "5%",
    },
    {
      header: "Thời Gian",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            {timeFormat(row.start) + "-" + timeFormat(row.end)}
          </h6>
        );
      },
      width: "10%",
    },
    {
      header: "Tiêu Đề",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            <Link
              className="linkCode"
              style={{ fontWeight: "bold", textAlign: "center" }}
              to={`/manage-tree/${row.treeCode}`}
            >
              {row.summary}
            </Link>
          </h6>
        );
      },
      width: "10%",
    },
    {
      header: "Nhân Viên Thực Hiện",
      accessorFn(row) {
        return <h6>{row.attendees}</h6>;
      },
      width: "20%",
    },
    {
      header: "Địa Chỉ Cụ Thể",
      accessorFn(row) {
        return <h6>{row.location}</h6>;
      },
      width: "40%",
    },
    {
      header: "Trạng Thái",
      accessorFn(row) {
        return (
          <h6
            className="shortText"
            style={{
              color: taskStatus(
                row.extendedProperties.privateProperties.JobWorkingStatus
              ).color,
              fontWeight: "bold",
            }}
          >
            {
              taskStatus(
                row.extendedProperties.privateProperties.JobWorkingStatus
              ).text
            }
          </h6>
        );
      },
    },
  ];

  return (
    <div>
      <ListView
        listURL={TREE_TRIM_SCHEDULE}
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
            Thêm lịch
          </Button>
        }
      />
    </div>
  );
};