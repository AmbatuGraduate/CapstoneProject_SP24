import { Button } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { GROUP_LIST } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { useRef } from "react";

import { MdAddCircleOutline } from "react-icons/md";
import { useCookies } from "react-cookie";

export const ManageGroup = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [token] = useCookies(["accessToken"]);
  // TODO get list

  // const handleDelete = async (id: string) => {
  //   await useApi.delete(TREE_DELETE.replace(":id", id));
  //   ref.current?.reload();
  // };

  const columns: Column[] = [
    {
      header: "Email",
      accessorFn(longRow) {
        return (
          <h6 className="shortText">
            <Link
              className="linkCode"
              style={{ fontWeight: "bold" }}
              to={`/manage-group/${longRow.id}`}
            >
              {longRow.email}
            </Link>
          </h6>
        );
      },
      width: "30%",
    },
    {
      header: "Tên bộ phận",
      accessorFn(row) {
        return <h6 className="shortText">{row.name}</h6>;
      },
      width: "30%",
    },
    {
      header: "Mô tả",
      accessorFn(row) {
        return <h6 className="shortText">{row.description}</h6>;
      },
      width: "40%",
    },
  ];

  return (
    <div>
      <ListView
        ref={ref}
        listURL={GROUP_LIST}
        columns={columns}
        bottom={
          (JSON.parse(token.accessToken).role == "Admin") && (
            <Button
              variant="success"
              onClick={() => navigate(-1)}
            >
              <MdAddCircleOutline className="iconAdd" />
              Thêm Bộ Phận
            </Button>
          )
        }
      />
    </div>
  );
};
