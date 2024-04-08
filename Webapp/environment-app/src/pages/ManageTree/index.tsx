import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { TREE_DELETE, TREE_LIST, useApi } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { dayFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { useRef, useState } from "react";

import { BiSolidEdit } from "react-icons/bi";
import { MdAddCircleOutline } from "react-icons/md";

export const ManageTree = () => {
  const navigate = useNavigate();
  // const [selectedRows, setSelectedRows] = useState<string[]>([]);
  const ref = useRef<any>();

  // const handleCheckboxChange = (e, id) => {
  //   const { checked } = e.target;
  //   // Thêm hoặc xóa id của dòng khỏi danh sách được chọn tùy thuộc vào trạng thái của checkbox
  //   setSelectedRows((prevSelectedRows) => {
  //     if (checked) {
  //       return [...prevSelectedRows, id];
  //     } else {
  //       return prevSelectedRows.filter((rowId) => rowId !== id);
  //     }
  //   });
  // };
  // const handleDeleteSelected = async () => {
  //   // Thực hiện xóa các dòng được chọn
  //   for (const id of selectedRows) {
  //     await useApi.delete(TREE_DELETE.replace(":id", id));
  //   }
  //   // Sau khi xóa, làm mới dữ liệu
  //   ref.current?.reload();
  //   // Đặt lại danh sách dòng được chọn về trạng thái ban đầu
  //   setSelectedRows([]);
  // };
  const handleDelete = async (id: string) => {
    await useApi.delete(TREE_DELETE.replace(":id", id));
    ref.current?.reload();
  };

  const columns: Column[] = [
    // {
    //   header: "",
    //   accessorFn(row) {
    //     return (
    //       <input
    //         type="checkbox"
    //         onChange={(e) => handleCheckboxChange(e, row.id)}
    //         checked={selectedRows.includes(row.id)}
    //       />
    //     );
    //   },
    // },
    {
      header: "",
      accessorFn(row) {
        return (
          <div>
            <button type="button" className="btn btn-click" onClick={() => { }}>
              <ModalDelete handleDelete={() => handleDelete(row.treeCode)} />
            </button>
          </div>
        );
      },
      width: "2%",
    },
    {
      header: "Mã Số Cây",
      accessorFn(row) {
        return (
          <h6 className="shortText">
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
        return <h6>{longRow.streetName}</h6>;
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
    // {
    //   header: "Đường kính thân",
    //   accessorFn(row) {
    //     return <h6 className="shortText">{row.bodyDiameter}</h6>
    //   },
    // },
    // {
    //   header: "Tán lá",
    //   accessorFn(row) {
    //     return <h6 className="shortText">{row.leafLength}</h6>
    //   },
    // },
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
            <MdAddCircleOutline className="iconAdd" />
            Thêm Cây
          </Button>
        }
        // filter={(row) => {
        //   return row.isExist == true;
        // }}
      />
    </div>
  );
};
