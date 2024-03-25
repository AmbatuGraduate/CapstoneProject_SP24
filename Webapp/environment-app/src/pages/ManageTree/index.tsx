import { Button } from "react-bootstrap";
import { BiSolidEdit } from "react-icons/bi";
import { Link, useNavigate } from "react-router-dom";
import { TREE_DELETE, TREE_LIST, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { dayFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { useRef } from "react";

export const ManageTree = () => {
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
            <Link to={`/manage-tree/${row?.treeCode}/update`}>
              <button type="button" className="btn btn-click">
                <BiSolidEdit />
              </button>
            </Link>
            <button type="button" className="btn btn-click" onClick={() => {}}>
              <ModalDelete handleDelete={() => handleDelete(row.treeCode)} />
            </button>
          </div>
        );
      },
    },
    {
      header: "Mã số cây",
      accessorFn(row) {
        return <Link className="linkCode" to={`/manage-tree/${row.treeCode}`}>{row.treeCode}</Link>;
      },
    },
    { header: "Tuyến đường", accessorKey: "streetName", align: "left" },
    { header: "Giống cây", accessorKey: "cultivar", align: "left" },
    { header: "Đường kính thân", accessorKey: "bodyDiameter", align: "left" },
    { header: "Tán lá", accessorKey: "leafLength", align: "left" },
    {
      header: "Thời điểm cắt tỉa gần nhất",
      accessorFn(row) {
        return <h6>{dayFormat(row.cutTime)}</h6>;
      },
    },
    {
      header: "Trạng thái",
      accessorFn(row) {
        const status = row.isCut ? "Đã cắt" : "Cần Cắt";
        const color = row.isCut ? "green" : "red";
        return <span style={{ color, fontWeight: "bold" }}>{status}</span>;
      },
    },
  ];

  return (
    <div>
      <ListView
        ref={ref}
        listURL={TREE_LIST}
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
            Thêm cây
          </Button>
        }
        filter={(row) => {
          return row.isExist == true;
        }}
      />
    </div>
  );
};
