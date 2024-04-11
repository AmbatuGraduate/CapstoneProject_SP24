import React, { useEffect, useState, useRef } from "react";
import { Button, Modal } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { TREE_TRIM_SCHEDULE, useApi, TREE_TRIM_SCHEDULE_DELETE, EMPLOYEE_LIST, TREE_TRIM_SCHEDULE_UPDATE, TREE_TRIM_SCHEDULE_DETAIL } from "../../Api";
import { ListView } from "../../Components/ListView";
import { dayFormat, taskStatus, timeFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { MdAddCircleOutline } from "react-icons/md";

export const ManageTreeTrimSchedule = () => {
  const [modalPosition, setModalPosition] = useState({ top: 0, left: 0 });
  const [employees, setEmployees] = useState<employee[]>([]);
  const [selectedEmployeeId, setSelectedEmployeeId] = useState("");
  const [hoveringRows, setHoveringRows] = useState({});
  const [showModal, setShowModal] = useState(false);
  const [selectedRowId, setSelectedRowId] = useState(null); // Thêm state mới để lưu trữ id của hàng được chọn
  const navigate = useNavigate();
  const ref = useRef<any>();

  type employee = {
    id: string;
    email: string;
    name: string;
  };

  useEffect(() => {
    async function fetchEmployees() {
      try {
        const response = await useApi.get(EMPLOYEE_LIST);
        setEmployees(response.data);
      } catch (error) {
        console.error("Error fetching employees: ", error);
      }
    }
    fetchEmployees();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      // Kiểm tra xem đã chọn nhân viên chưa
      if (!selectedEmployeeId) {
        throw new Error("Vui lòng chọn nhân viên");
      }

      // Lấy thông tin nhân viên được chọn từ danh sách nhân viên
      const selectedEmployee = employees.find((employee) => employee.email === selectedEmployeeId);

      if (!selectedEmployee) {
        throw new Error("Không tìm thấy thông tin của nhân viên được chọn.");
      }

      // Tạo đối tượng attendee từ thông tin nhân viên
      const attendee = {
        name: selectedEmployee.name,
        email: selectedEmployee.email
      };

      // Lấy dữ liệu hiện tại của hàng được chọn
      // @ts-ignore or @ts-expect-error
      const response = await useApi.get(TREE_TRIM_SCHEDULE_DETAIL.replace(":id", selectedRowId));
      const selectedRowData = response.data.myEvent;

      const updatedRowData = {
        summary: selectedRowData.summary,
        description: selectedRowData.description,
        location: selectedRowData.location,
        start: {
          dateTime: new Date(selectedRowData.start).toISOString()
        },
        end: {
          dateTime: new Date(selectedRowData.end).toISOString()
        },
        attendees: [
          {
            name: attendee.name,
            email: attendee.email
          }
        ]
      };


      // Gửi yêu cầu cập nhật lịch cắt tỉa
      // @ts-ignore or @ts-expect-error
      await useApi.post(TREE_TRIM_SCHEDULE_UPDATE.replace(":id", selectedRowId), updatedRowData);

      // Reload danh sách sau khi cập nhật thành công
      ref.current.reload();
      setShowModal(false); // Đóng modal sau khi gửi yêu cầu thành công
    } catch (error) {
      console.error("Lỗi khi xử lý dữ liệu nhân viên:", error);
      // Xử lý lỗi tại đây (ví dụ: hiển thị thông báo lỗi cho người dùng)
    }
  };


  const calculateModalPosition = (event) => {
    const buttonRect = event.target.getBoundingClientRect();

    // Tính toán vị trí của modal
    const top = buttonRect.top - 32;
    const left = buttonRect.width + buttonRect.left;

    // Cập nhật vị trí của modal
    setModalPosition({ top, left });
  };

  const handleMouseEnter = (id) => {
    setHoveringRows((prevHoveringRows) => ({
      ...prevHoveringRows,
      [id]: true,
    }));
  };

  const handleMouseLeave = (id) => {
    setHoveringRows((prevHoveringRows) => ({
      ...prevHoveringRows,
      [id]: false,
    }));
  };

  const handleDelete = async (id) => {
    await useApi.delete(TREE_TRIM_SCHEDULE_DELETE.replace(":id", id));
    ref.current.reload();
  };

  const columns = [
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
      width: "8%",
    },
    {
      header: "Ngày Làm",
      accessorFn(row) {
        return (
          <h6 className="shortText">
            {dayFormat(row.end)}
          </h6>
        );
      },
      width: "10%",
    },
    {
      header: "Tiêu Đề",
      accessorFn(row) {
        return (
          <h6 className="linkDiv">
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
        const isRowHovering = hoveringRows[row.id];

        if (isRowHovering) {
          return (
            <h6
              style={{ fontWeight: "bold", cursor: "pointer", color: "#FB6D48" }}
              onMouseLeave={() => handleMouseLeave(row.id)}
              onClick={(event) => {
                setSelectedRowId(row.id); // Lưu id của hàng được chọn
                calculateModalPosition(event);
                setShowModal(true);
              }}
            >
              Thêm nhân viên
            </h6>
          );
        } else {
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
          } else {
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
      width: "25%",
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
          <>
            <Button
              variant="success"
              style={{
                backgroundColor: "hsl(94, 59%, 35%)",
                border: "none",
                padding: "0.5rem 1rem",
              }}
              onClick={() => navigate("/manage-treetrim-schedule/create")}
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
                <form onSubmit={handleSubmit}>
                  <label>
                    Chọn nhân viên:
                    <select
                      style={{ width: "300px" }}
                      value={selectedEmployeeId}
                      onChange={(e) => setSelectedEmployeeId(e.target.value)}
                    >
                      <option value="">Chọn nhân viên</option>
                      {employees.map((employee) => (
                        <option key={employee.id} value={employee.email}>
                          {employee.name}
                        </option>
                      ))}
                    </select>
                  </label>
                  <button type="submit">Xác nhận</button>
                </form>
              </Modal.Body>
            </Modal>
          </>
        }
      />
    </div>
  );
};
