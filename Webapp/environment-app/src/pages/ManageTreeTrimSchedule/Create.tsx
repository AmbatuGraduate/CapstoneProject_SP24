import { useNavigate } from "react-router-dom";
import {
  DEPARTMENT_EMPLOYEE,
  DEPARTMENT_LIST,
  EMPLOYEE_LIST,
  TREE_ADDRESS,
  TREE_TRIM_SCHEDULE_ADD,
  useApi,
} from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";
import Swal from "sweetalert2";

export const CreateTreeTrimSchedule = () => {
  const navigate = useNavigate();
  const ref = useRef<any>();
  const [, setIsLoading] = useState(false);
  const [departmentEmail, setDepartmentEmail] = useState<any>();
  const [address, setAddress] = useState<string | null>("");
  const [startTime, setStartTime] = useState<Date | null>(null);

  const endTime = () => {
    if (!startTime) return null; // Trả về null nếu không có startTime
    const newEndTime = new Date(startTime); // Sử dụng startTime làm tham số khởi tạo
    newEndTime.setMinutes(newEndTime.getMinutes() + 60); // Cộng thêm 00 phút
    return newEndTime;
  };

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
      required: true,
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
      required: true,
    },
    {
      label: "Tiêu Đề",
      formType: "input",
      keyName: "summary",
      placeholder: "Nhập tiêu đề",
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập tiêu đề công việc",
      required: true,
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
      pattern: /\S/, // Mẫu kiểm tra không được để trống
      errorMessage: "Vui lòng nhập địa chỉ",
      required: true,
    },
    {
      label: "Bắt Đầu Từ",
      formType: "datetime",
      keyName: "start.dateTime",
      defaultValue: new Date(),
      minDate: new Date(),
      onChange: (datetime) => {
        setStartTime(datetime);
      },
      required: true,
    },
    {
      label: "Kết Thúc Trước",
      formType: "datetime",
      keyName: "end.dateTime",
      defaultValue: new Date(),
      minDate: startTime || new Date(),
      value: endTime(),
      required: true,
    },
    {
      label: "Ghi Chú",
      formType: "textarea",
      keyName: "description",
      placeholder: "Ví dụ: Cần lưu ý...",
      defaultValue: "",
    },
  ];

  const handleSubmit = async (data: Record<string, any>) => {
    setIsLoading(true);
    console.log("data", data);
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
      const selectedAddress = data["location"];

      const treeListResponse = await useApi.get(
        TREE_ADDRESS.replace(":address", selectedAddress)
      );
      const treeList = treeListResponse.data;
      console.log(treeList.treeCode);

      // Tạo đối tượng attendee chứa tên và email của nhân viên
      const attendee = {
        name: selectedEmployeeInfo.name,
        email: selectedEmployee,
      };

      const treeCodes = treeList.map((tree: any) => tree.treeCode);
      const treeId = treeCodes.join(",");

      const requestData = {
        ...data,
        start: { dateTime: formattedStartDateTime },
        end: { dateTime: formattedEndDateTime },
        attendees: [attendee], // Sử dụng thông tin nhân viên thu được
        treeId: treeId,
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
      await useApi.post(TREE_TRIM_SCHEDULE_ADD, requestData);
      Swal.close();
      Swal.fire(
        'Thành công!',
        'Thêm lịch cắt tỉa mới thành công!',
        'success'
      );
      ref.current?.reload();
      navigate("/manage-treetrim-schedule");
    } catch (error) {
      console.error("Lỗi khi thêm lịch:", error);
      Swal.fire(
        'Lỗi!',
        'Lỗi khi thêm lịch cắt tỉa! Vui lòng thử lại sau.',
        'error'
      );
      setIsLoading(false);
      // Xử lý lỗi tại đây (ví dụ: hiển thị thông báo lỗi cho người dùng)
    }
  };

  return (
    <div className="form-cover">
      <h4>Thêm Lịch Cắt Tỉa</h4>
      <FormBase
        fields={fields}
        onSave={handleSubmit}
        onCancel={() => navigate("/manage-treetrim-schedule")}
      />
    </div>
  );
};
