import { Button } from "react-bootstrap";
import { BiSolidEdit } from "react-icons/bi";
import { Link, useNavigate } from "react-router-dom";
import { TREE_TRIM_SCHEDULE } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { dayFormat, taskStatus, timeFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";

export const ManageGarbageCollectionSchedule = () => {
  const navigate = useNavigate();

  const columns: Column[] = [
    // {
    //   header: "Chỉnh sửa",
    //   accessorFn(row) {
    //     return (
    //       <div >
    //         <Link to={`/manage-tree/${row?.treeCode}/update`}>
    //           <button type="button" className="btn btn-click">
    //             <BiSolidEdit />
    //           </button>
    //         </Link>
    //         <button type="button" className="btn btn-click" onClick={() => { }}>
    //           <ModalDelete />
    //         </button>
    //       </div>
    //     );
    //   },
    // },
    {
      header: "Thời gian",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            {timeFormat(row.myEvent.start) + "-" + timeFormat(row.myEvent.end)}
          </h6>
        );
      },
    },
    {
      header: "Tiêu đề",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            <Link
              className="linkCode"
              style={{ fontWeight: "bold", textAlign: "center" }}
              to={`/manage-tree/${row.treeCode}`}
            >
              {row.myEvent.summary}
            </Link>
          </h6>
        );
      },
    },
    {
      header: "Vị trí",
      accessorFn(row) {
        return <h6>{row.myEvent.location}</h6>;
      },
    },
    {
      header: "Trạng thái",
      accessorFn(row) {
        return (
          <h6
            className="shortText"
            style={{
              color: taskStatus(
                row.myEvent.extendedProperties.privateProperties
                  .JobWorkingStatus
              ).color,
              fontWeight: "bold",
            }}
          >
            {
              taskStatus(
                row.myEvent.extendedProperties.privateProperties
                  .JobWorkingStatus
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
            Thêm lịch
          </Button>
        }
      />
    </div>
  );
};
