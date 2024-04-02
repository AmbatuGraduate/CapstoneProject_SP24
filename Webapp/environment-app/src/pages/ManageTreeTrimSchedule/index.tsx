import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { TREE_TRIM_SCHEDULE, useApi, TREE_TRIM_SCHEDULE_DELETE } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { taskStatus, timeFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { MdAddCircleOutline } from "react-icons/md";
import { useRef } from "react";

export const ManageTreeTrimSchedule = () => {
  const navigate = useNavigate();

  const ref = useRef<any>();

  const handleDelete = async (id: string) => {
    await useApi.delete(TREE_TRIM_SCHEDULE_DELETE.replace(":id", id));
    ref.current?.reload();
  };

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
      width: "2%",
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
              to={`/manage-treetrim-schedule/${row.id}`}
            >
              {row.summary}
            </Link>
          </h6>
        );
      },
      width: "15%",
    },
    {
      header: "Nhân Viên Thực Hiện",
      accessorFn(row) {
        // Kiểm tra nếu có ít nhất một người tham dự và email của người đầu tiên tồn tại
        if (row.attendees && row.attendees.length == 1 && row.attendees[0].user && row.attendees[0].user.email) {
          return <h6 className="shortText">{row.attendees[0].fullName}</h6>;
        } if (row.attendees && row.attendees.length > 1 && row.attendees[0].user && row.attendees[0].user.email) {
          return <h6 className="shortText">{row.attendees[0].fullName},...</h6>;
        } else {
          return <h6 className="shortText">Cần thên nhân viên thực hiện</h6>; // Hoặc bất kỳ giá trị nào thích hợp cho trường hợp không có email
        }
      },
      width: "20%",
    },
    {
      header: "Địa Chỉ Cụ Thể",
      accessorFn(row) {
        return <h6>{row.location}</h6>;
      },
      width: "35%",
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
        ref={ref}
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