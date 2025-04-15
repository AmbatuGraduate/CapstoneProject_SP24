import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { TREE_DELETE, TREE_LIST, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { dayFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { useRef } from "react";
import { useCookies } from "react-cookie";

import { MdAddCircleOutline } from "react-icons/md";


export const ManageTree = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [token] = useCookies(["accessToken"]);
  const handleDelete = async (id: string) => {
    await useApi.delete(TREE_DELETE.replace(":id", id));
    ref.current?.reload();
  };

  const columns: Column[] = [
    {
      header: "",
      accessorFn(row) {
        return (
          (JSON.parse(token.accessToken).role != "HR") && (
            <div>
              <button type="button" className="btn btn-click" onClick={() => { }}>
                <ModalDelete handleDelete={() => handleDelete(row.treeCode)} />
              </button>
            </div>
          )
        );
      },
      width: "1%",
    },
    {
      header: "Mã Số Cây",
      accessorFn(row) {
        return (
          <h6 className="shortText linkDiv" style={{ margin: 'auto' }}>
            <Link
              className="linkCode"
              style={{ fontWeight: "bold" }}
              to={`/manage-tree/${row.treeCode}`}
            >
              {row.treeCode}
            </Link>
          </h6>
        );
      },
      width: "10%",
    },
    {
      header: "Địa Chỉ Cụ Thể",
      accessorFn(longRow) {
        return <h6 style={{ padding: '0 1rem' }}>{longRow.streetName}</h6>;
      },
      width: "35%",
    },
    {
      header: "Loại Cây",
      accessorFn(row) {
        return <h6 className="shortText">{row.treeType}</h6>;
      },
      width: "15%",
    },
    {
      header: "Thời Điểm Cắt Tiếp Theo",
      accessorFn(row) {
        return <h6 className="shortText">{dayFormat(row.cutTime)}</h6>;
      },
      width: "15%",
    },
    {
      header: "Trạng Thái",
      accessorFn(row) {
        const status = row.isCut ? "Đã cắt" : "Cần Cắt";
        const color = row.isCut ? "green" : "red";
        return (
          <h6 className="shortText" style={{ color, fontWeight: "bold" }}>
            {status}
          </h6>
        );
      },
      width: "10%",
    },
  ];

  const transformData = (data: any) => {
    // Filter các cây với trạng thái isCut là false trước
    const notCutTrees = data.filter((tree: any) => !tree.isCut);
    // Filter các cây với trạng thái isCut là true
    const cutTrees = data.filter((tree: any) => tree.isCut);
    // Nối hai mảng lại với nhau, đảm bảo các cây chưa cắt sẽ xuất hiện trước
    return [...notCutTrees, ...cutTrees];
  };

  return (
    <div>
      <ListView
        ref={ref}
        listURL={TREE_LIST}
        columns={columns}
        transform={transformData}
        bottom={
          ((JSON.parse(token.accessToken).role == "Manager" || JSON.parse(token.accessToken).role == "Admin") && (
            <Button
              variant="success"
              onClick={() => navigate("/manage-tree/create")}
            >
              <MdAddCircleOutline className="iconAdd" />
              Thêm Cây
            </Button>
          )
          )

        }
      />
    </div>
  );
};
