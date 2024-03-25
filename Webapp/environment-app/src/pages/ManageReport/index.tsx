import { Button } from "react-bootstrap";
import { BiSolidEdit } from "react-icons/bi";
import { Link, useNavigate } from "react-router-dom";
import { REPORT_LIST, TREE_DELETE, TREE_LIST, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { dayFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { useRef } from "react";

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
            <Link to={`/manage-report/${row?.id}/update`}>
              <button type="button" className="btn btn-click">
                <BiSolidEdit />
              </button>
            </Link>
            <button type="button" className="btn btn-click" onClick={() => {}}>
              <ModalDelete handleDelete={() => handleDelete(row.id)} />
            </button>
          </div>
        );
      },
    },
    {
      header: "Mã báo cáo",
      accessorFn(row) {
        return (
          <Link className="linkCode" to={`/manage-report/${row.id}`}>
            {row.treeCode}
          </Link>
        );
      },
    },
    { header: "Tiêu đề", accessorKey: "streetName", align: "left" },
    { header: "Người gửi", accessorKey: "cultivar", align: "left" },
    { header: "Mức độ ảnh hưởng", accessorKey: "bodyDiameter", align: "left" },
    { header: "Trạng thái", accessorKey: "leafLength", align: "left" },
  ];

  return (
    <div>
      <ListView
        ref={ref}
        listURL={REPORT_LIST}
        columns={columns}
        filter={(row) => {
          return row.isExist == true;
        }}
      />
    </div>
  );
};
