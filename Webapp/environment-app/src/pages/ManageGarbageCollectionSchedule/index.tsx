import { Button, Modal } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { GARBAGE_COLLECTION_SCHEDULE, useApi, GARBAGE_COLLECTION_DELETE } from "../../Api";
import { ListView } from "../../Components/ListView";
import { Column } from "../../Components/ListView/Table";
import { taskStatus, timeFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { useRef, useState } from "react";

import { MdAddCircleOutline } from "react-icons/md";

export const ManageGarbageCollectionSchedule = () => {
  const [modalPosition, setModalPosition] = useState({ top: 0, left: 0 });

  const calculateModalPosition = (event) => {
    const buttonRect = event.target.getBoundingClientRect();

    // Tính toán vị trí của modal
    const top = buttonRect.top - 32;
    const left = buttonRect.width + buttonRect.left;

    // Cập nhật vị trí của modal
    setModalPosition({ top, left });
  };


  const [hoveringRows, setHoveringRows] = useState({});
  const [showModal, setShowModal] = useState(false);
  const navigate = useNavigate();

  // Hàm xử lý sự kiện khi chuột di chuột vào hàng
  const handleMouseEnter = (id) => {
    setHoveringRows((prevHoveringRows) => ({
      ...prevHoveringRows,
      [id]: true,
    }));
  };

  // Hàm xử lý sự kiện khi chuột di ra khỏi hàng
  const handleMouseLeave = (id) => {
    setHoveringRows((prevHoveringRows) => ({
      ...prevHoveringRows,
      [id]: false,
    }));
  };

  const ref = useRef<any>();

  const handleDelete = async (id: string) => {
    await useApi.delete(GARBAGE_COLLECTION_DELETE.replace(":id", id));
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
          <h6 >
            <Link
              className="linkCode"
              style={{ fontWeight: "bold", textAlign: "center" }}
              to={`/manage-garbagecollection-schedule/${row.id}`}
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
        const isRowHovering = hoveringRows[row.id];

        if (isRowHovering) {
          return ( // Hiển thị "Thêm nhân viên" khi chuột di chuyển vào
            <h6
              style={{ fontWeight: "bold", cursor: "pointer", color: "#FB6D48" }}
              onMouseLeave={() => handleMouseLeave(row.id)}
              onClick={(event) => {
                calculateModalPosition(event); // Tính toán vị trí của modal khi nhấn vào nút
                setShowModal(true); // Hiển thị modal
              }}
            >
              Thêm nhân viên
            </h6>
          );
        } else {
          // Kiểm tra và hiển thị dữ liệu như ban đầu khi không hover
          if (
            row.attendees &&
            row.attendees.length == 1 &&
            row.attendees[0].user &&
            row.attendees[0].user.email
          ) {
            return <h6 >{row.attendees[0].fullName}</h6>;
          } else if (
            row.attendees &&
            row.attendees.length == 2 &&
            row.attendees[0].user &&
            row.attendees[0].user.email
          ) {
            return <h6 >{row.attendees[0].fullName},{row.attendees[1].fullName}</h6>;
          } else if (
            row.attendees &&
            row.attendees.length > 2 &&
            row.attendees[0].user &&
            row.attendees[0].user.email
          ) {
            return <h6 >{row.attendees[0].fullName},{row.attendees[1].fullName},...</h6>;
          }

          else {
            return <h6 onMouseEnter={() => handleMouseEnter(row.id)} style={{ color: "orange" }}>Cần thêm nhân viên thực hiện</h6>;
          }
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
        listURL={GARBAGE_COLLECTION_SCHEDULE}
        columns={columns}
        bottom={
          <>
            <Button
              variant="success"
              style={{
                backgroundColor: "hsl(94, 59%, 35%)",
                border: "none",
                padding: "0.5rem 1rem",
              }}
              onClick={() => navigate("/manage-garbagecollection-schedule/create")}
            >
              <MdAddCircleOutline className="iconAdd" />
              Thêm Lịch
            </Button>

            <Modal show={showModal} onHide={() => setShowModal(false)} backdrop={false}
              style={{
                top: modalPosition.top,
                left: modalPosition.left,
                width: "350px",
              }}>
              <Modal.Header closeButton>
                <Modal.Title>Thêm Nhân Viên Thực Hiện</Modal.Title>
              </Modal.Header>
              <Modal.Body>
                {/* Form để nhập tên nhân viên */}
                <form >
                  <label>
                    <input style={{ width: "300px" }} type="text" />
                  </label>
                </form>
                <button type="submit">Xác nhận</button>
              </Modal.Body>
            </Modal>
          </>
        }
      />
    </div>
  );
};