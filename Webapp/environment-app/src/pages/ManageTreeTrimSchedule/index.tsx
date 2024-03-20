import { Button } from "react-bootstrap";
import { BiSolidEdit } from "react-icons/bi";
import { Link, useNavigate } from "react-router-dom";
import { TREE_LIST } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { dayFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";

export const ManageTreeTrimSchedule = () => {
  const navigate = useNavigate();
  // TODO get list

  const columns: Column[] = [
    {
      header: "Chỉnh sửa",
      accessorFn(row) {
        return (
          <div >
            <Link to={`/manage-tree/${row?.treeCode}/update`}>
              <button type="button" className="btn btn-click">
                <BiSolidEdit />
              </button>
            </Link>
            <button type="button" className="btn btn-click" onClick={() => { }}>
              <ModalDelete />
            </button>
          </div>
        );
      },
    },
    {
      header: "Thời gian",
      accessorFn(row) {
        return <h6>{dayFormat(row.cutTime)}</h6>;
      },
    },
    {
      header: "Tiêu đề",
      accessorFn(row) {
        return <Link className="linkCode" to={`/manage-tree/${row.treeCode}`}>{row.treeCode}</Link>;
      },
    },
    { header: "Vị trí", accessorKey: "streetName", align: "left" },

    {
      header: "Trạng thái",
      accessorFn(row) {
        const status = row.isCut ? "Đã hoàn thành" : "Chưa hoàn thành";
        const color = row.isCut ? "green" : "red";
        return <span style={{ color, fontWeight: "bold" }}>{status}</span>;
      },
    },
  ];

  return (
    <div>
      <ListView
        listURL={TREE_LIST}
        columns={columns}
        bottom={
          <Button
            variant="success"
            style={{ backgroundColor: "hsl(94, 59%, 35%)", border: "none", padding: "0.5rem 1rem" }}
            onClick={() => navigate("/manage-tree/create")}
          >
            Thêm lịch
          </Button>
        }
      />
    </div>
  );
};
