import React, { useEffect, useState, useRef } from "react";
import { Button, Modal } from "react-bootstrap";
import { Link, useNavigate } from "react-router-dom";
import { TREE_TRIM_SCHEDULE, useApi, TREE_TRIM_SCHEDULE_DELETE, TREE_TRIM_SCHEDULE_UPDATE, TREE_TRIM_SCHEDULE_DETAIL, DEPARTMENT_EMPLOYEE } from "../../Api";
import { ListView } from "../../Components/ListView";
import { dayFormat, taskStatus, timeFormat } from "../../utils";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { MdAddCircleOutline } from "react-icons/md";
import Swal from "sweetalert2";

export const ManageTreeTrimSchedule = () => {
  const [modalPosition, setModalPosition] = useState({ top: 0, left: 0 });
  const [employees, setEmployees] = useState<employee[]>([]);
  const [hoveringRows, setHoveringRows] = useState({});
  const [showModal, setShowModal] = useState(false);
  const [selectedRowId, setSelectedRowId] = useState(null); // Thêm state mới để lưu trữ id của hàng được chọn
  const navigate = useNavigate();
  const ref = useRef<any>();

  const [selectedEmployeeIds, setSelectedEmployeeIds] = useState<string[]>([]);


  type employee = {
    id: string;
    email: string;
    name: string;
  };

  useEffect(() => {
    async function fetchEmployees() {
      try {
        const response = await useApi.get(DEPARTMENT_EMPLOYEE.replace(":groupEmail", "cayxanh@vesinhdanang.xyz"));
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
      if (selectedEmployeeIds.length === 0) {
        throw new Error("Vui lòng chọn nhân viên");
      }

      // Lấy thông tin nhân viên được chọn từ danh sách nhân viên
      const selectedEmployees = employees.filter((employee) => selectedEmployeeIds.includes(employee.email));



      if (selectedEmployees.length === 0) {
        throw new Error("Không tìm thấy thông tin của nhân viên được chọn.");
      }

      // Tạo mảng attendee từ thông tin nhân viên
      const attendees = selectedEmployees.map(employee => ({
        name: employee.name,
        email: employee.email
      }));


      // Lấy dữ liệu hiện tại của hàng được chọn
      // @ts-ignore or @ts-expect-error
      const response = await useApi.get(TREE_TRIM_SCHEDULE_DETAIL.replace(":id", selectedRowId));
      const selectedRowData = response.data.myEvent;
      const updatedRowData = {
        summary: selectedRowData.summary,
        description: selectedRowData.description ? selectedRowData.description : "",
        location: selectedRowData.location,
        start: {
          dateTime: new Date(selectedRowData.start).toISOString()
        },
        end: {
          dateTime: new Date(selectedRowData.end).toISOString()
        },
        attendees: attendees
      };

      console.log("data: " + JSON.stringify(updatedRowData));

      Swal.fire({
        title: 'Đang cập nhật...',
        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        }
      });


      // Gửi yêu cầu cập nhật lịch cắt tỉa
      // @ts-ignore or @ts-expect-error

      await useApi.post(TREE_TRIM_SCHEDULE_UPDATE.replace(":id", selectedRowId), updatedRowData);
      Swal.close();
      Swal.fire('Thành công', 'Đã thêm nhân viên vào lịch.', 'success');

      // Reload danh sách sau khi cập nhật thành công
      ref.current.reload();
      setShowModal(false); // Đóng modal sau khi gửi yêu cầu thành công
    } catch (error) {
      console.error("Lỗi khi xử lý dữ liệu nhân viên:", error);
      Swal.fire({
        icon: 'error',
        title: 'Không Thành Công',
        text: "Lỗi khi cập nhật nhân viên cho lịch",
      });
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
      width: "1%",
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
            {dayFormat(row.start)}
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
              style={{ fontWeight: "bold", cursor: "pointer", color: "#FB6D48", padding: '0 1rem' }}
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
            return <h6 style={{ padding: '0 1rem' }}>{row.attendees[0].fullName}</h6>;
          } else if (
            row.attendees &&
            row.attendees.length == 2 &&
            row.attendees[0].user &&
            row.attendees[0].user.email
          ) {
            return <h6 style={{ padding: '0 1rem' }}>{row.attendees[0].fullName},{row.attendees[1].fullName}</h6>;
          } else if (
            row.attendees &&
            row.attendees.length > 2 &&
            row.attendees[0].user &&
            row.attendees[0].user.email
          ) {
            return <h6 style={{ padding: '0 1rem' }}>{row.attendees[0].fullName},{row.attendees[1].fullName},...</h6>;
          } else {
            return <h6 onMouseEnter={() => handleMouseEnter(row.id)} style={{ color: "orange", padding: '0 1rem' }}>Cần thêm nhân viên thực hiện</h6>;
          }
        }
      },
      width: "20%",
    },
    {
      header: "Địa Chỉ Cụ Thể",
      accessorFn(row) {
        return <h6 style={{ padding: '0 1rem' }}>{row.location}</h6>;
      },
      width: "30%",
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
      width: "10%",
    },
  ];

  const transformData = (data) => {
    // Sắp xếp các lịch theo ngày từ mới tới cũ
    // @ts-ignore or @ts-expect-error
    return data.sort((a, b) => new Date(b.start) - new Date(a.start));
  };

  return (
    <div>
      <ListView
        ref={ref}
        listURL={TREE_TRIM_SCHEDULE}
        columns={columns}
        transform={transformData}
        bottom={
          <>
            <Button
              variant="success"
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
                display: "flex",
                flexDirection: "column",
                justifyContent: "space-between"
              }}>
              <Modal.Header closeButton>
                <Modal.Title>Thêm Nhân Viên Thực Hiện</Modal.Title>
              </Modal.Header>
              <Modal.Body>
                <form onSubmit={handleSubmit}>
                  <label>
                    Chọn nhân viên:
                    {employees.map((employee) => (
                      <div key={employee.id} style={{ margin: "10px 0" }}>
                        <input
                          type="checkbox"
                          id={employee.email}
                          value={employee.email}
                          checked={selectedEmployeeIds.includes(employee.email)}
                          onChange={(e) => {
                            if (e.target.checked) {
                              setSelectedEmployeeIds([...selectedEmployeeIds, e.target.value]);
                            } else {
                              setSelectedEmployeeIds(selectedEmployeeIds.filter(id => id !== e.target.value));
                            }
                          }}
                          style={{ marginRight: "10px" }}
                        />
                        <label htmlFor={employee.email}>{employee.name}</label>
                      </div>
                    ))}
                  </label>
                  <div style={{ display: "flex", justifyContent: "center", paddingBottom: "20px" }}>
                    <button className="addEmployeeBtn" type="submit" style={{
                      padding: "10px 20px",
                      borderRadius: "5px",
                      backgroundColor: "#007bff",
                      color: "white",
                      border: "none"
                    }}>
                      Xác nhận</button>
                  </div>
                </form>
              </Modal.Body>

            </Modal>
          </>
        }
      />
    </div>
  );
};
