import { Button } from "react-bootstrap";
import { BiSolidEdit } from "react-icons/bi";
import { Link, useNavigate } from "react-router-dom";
import { ROUTE_LIST, TREE_LIST } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { dayFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";

export const ManageRoute = () => {
  const navigate = useNavigate();
  // TODO get list

  const columns: Column[] = [
    {
      header: "Chỉnh sửa",
      accessorFn(row) {
        return (
          <div>
            <Link to={`/manage-route/${row?.streetId}/update`}>
              <button type="button" className="btn btn-click">
                <BiSolidEdit />
              </button>
            </Link>
            <button type="button" className="btn btn-click" onClick={() => {}}>
              <ModalDelete handleDelete={() => {}} />
            </button>
          </div>
        );
      },
    },
    {
      header: "Tên đường",
      accessorFn(row) {
        return (
          <Link className="linkCode" to={`/manage-route/${row.streetName}`}>
            {row.streetName}
          </Link>
        );
      },
    },
    { header: "Độ dài", accessorKey: "streetLength", align: "left" },
    { header: "Số nhà", accessorKey: "numberOfHouses", align: "left" },
    { header: "Loại tuyến đường", accessorKey: "streetTypeId", align: "left" },
    { header: "Nhóm dân cư", accessorKey: "residentialGroupId", align: "left" },
    { header: "Quận", accessorKey: "districtId", align: "left" },
  ];

  return (
    <div>
      <ListView
        listURL={ROUTE_LIST}
        columns={columns}
        bottom={
          <Button
            variant="success"
            onClick={() => navigate("/manage-route/create")}
          >
            Thêm
          </Button>
        }
      />
    </div>
  );
};
