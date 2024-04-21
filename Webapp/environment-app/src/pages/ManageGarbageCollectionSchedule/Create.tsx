import { useNavigate } from "react-router-dom";
import { DEPARTMENT_EMPLOYEE, DEPARTMENT_LIST, EMPLOYEE_LIST, GARBAGE_COLLECTION_ADD, useApi, } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";
import Swal from "sweetalert2";

export const CreateGarbageCollectionSchedule = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [departmentEmail, setDepartmentEmail] = useState<any>();
  const [address, setAddress] = useState<string | null>("");

  const fields: Field[] = [
    {
      label: "Bộ Phận",
      formType: "select",
      keyName: "departmentEmail",
      optionExtra: {
        url: DEPARTMENT_LIST,
        _key: "name",
        _value: "email",
      },
      onChange: (value) => setDepartmentEmail(value),
    },
    {
      label: "Nhân Viên Thực Hiện",
      formType: "select",
      keyName: "attendees.email",
      optionExtra: {
        url: DEPARTMENT_EMPLOYEE.replace(":groupEmail", departmentEmail),
        _key: "name",
        _value: "email",
      },
    },
    {
      label: "Tiêu Đề",
      formType: "input",
      keyName: "summary",
      placeholder: "Nhập tiêu đề"
    },
    {
      label: "Địa Chỉ",
      formType: "input",
      keyName: "location",
      googleAddress: true,
      value: address,
      onChange: (e) => {
        setAddress(e.target.value);
      },
      placeholder: "Nhập địa chỉ",
    },
    {
      label: "Bắt Đầu Từ",
      formType: "datetime",
      keyName: "start.dateTime",
      defaultValue: new Date(),
    },
    {
      label: "Kết Thúc Trước",
      formType: "datetime",
      keyName: "end.dateTime",
      defaultValue: new Date(),
    },
    {
      label: "Ghi Chú",
      formType: "textarea",
      keyName: "description",
      placeholder: "Ví dụ: Cần lưu ý...",
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);

    const formattedStartDateTime = data["start.dateTime"];
    const formattedEndDateTime = data["end.dateTime"];
    try {
      // Lấy danh sách nhân viên từ API
      const employeeListResponse = await useApi.get(EMPLOYEE_LIST);
      const employeeList = employeeListResponse.data; // Giả sử dữ liệu trả về là một mảng

      // Lấy thông tin nhân viên thực hiện từ data
      const selectedEmployee = data["attendees.email"];

      // Tìm nhân viên được chọn trong danh sách nhân viên và lấy thông tin của họ
      const selectedEmployeeInfo = employeeList.find(
        (employee: any) => employee.email === selectedEmployee
      );

      // Kiểm tra xem nhân viên có tồn tại trong danh sách không
      if (!selectedEmployeeInfo) {
        throw new Error("Không tìm thấy thông tin của nhân viên được chọn.");
      }

      // Tạo đối tượng attendee chứa tên và email của nhân viên
      const attendee = {
        name: selectedEmployeeInfo.name,
        email: selectedEmployee,
      };

      const requestData = {
        ...data,
        start: { dateTime: formattedStartDateTime },
        end: { dateTime: formattedEndDateTime },
        attendees: [attendee], // Sử dụng thông tin nhân viên thu được
        treeId: "",
      };

      delete requestData["start.dateTime"];
      delete requestData["end.dateTime"];
      Swal.fire({
        title: 'Đang thêm lịch...',
        allowEscapeKey: false,
        allowOutsideClick: false,
        didOpen: () => {
          Swal.showLoading();
        }
      });
      await useApi.post(GARBAGE_COLLECTION_ADD, requestData);
      Swal.close();
      Swal.fire(
        'Thành công!',
        'Thêm lịch thu gom mới thành công!',
        'success'
      );
      ref.current?.reload();
      navigate("/manage-garbagecollection-schedule");
    } catch (error) {
      console.error("Lỗi khi xử lý dữ liệu nhân viên:", error);
      Swal.fire(
        'Lỗi!',
        'Lỗi khi thêm lịch thu gom! Vui lòng thử lại sau.',
        'error'
      );
      setIsLoading(false);
      // Xử lý lỗi tại đây (ví dụ: hiển thị thông báo lỗi cho người dùng)
    }
  };

  return (
    <div className="form-cover">
      <h4>Thêm Lịch Thu Gom Rác</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-garbagecollection-schedule")}
      />
    </div>
  );
};
