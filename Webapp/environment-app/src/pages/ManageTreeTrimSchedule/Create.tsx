import { useNavigate } from "react-router-dom";
import { DEPARTMENT_EMPLOYEE, DEPARTMENT_LIST, EMPLOYEE_LIST, TREE_LIST, TREE_TRIM_SCHEDULE_ADD, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const CreateTreeTrimSchedule = () => {
    const navigate = useNavigate();
    const ref = useRef<any>();
    const [, setIsLoading] = useState(false);
    const [token] = useCookies(["accessToken"]);
    const [departmentEmail, setDepartmentEmail] = useState<string | null>(null);

    const fields: Field[] = [
        {
            label: "Tiêu Đề",
            formType: "input",
            key: "summary",
        },
        {
            label: "Cây Cần Cẳt",
            formType: "select",
            key: "treeId",
            optionExtra: {
                url: TREE_LIST,
                _key: "treeCode",
                _value: "treeCode",
            },
        },
        {
            label: "Bắt Đầu Từ",
            formType: "datetime",
            key: "start.dateTime",
        },
        {
            label: "Địa Chỉ",
            formType: "input",
            key: "location",
            placeholder: "Ví dụ: 29 Sơn Thủy Đông 2, Hòa Hải, Ngũ Hành Sơn",
        },
        {
            label: "Kết Thúc Trước",
            formType: "datetime",
            key: "end.dateTime",
        },
        {
            label: "Bộ Phận",
            formType: "select",
            key: "departmentEmail",
            optionExtra: {
                url: DEPARTMENT_LIST,
                _key: "name",
                _value: "email",
            },
            onChange: (e) => setDepartmentEmail(e.target.value),
        },
        {
            label: "Nhân Viên Thực Hiện",
            formType: "select",
            key: "attendees.email",
            optionExtra: {
                url: DEPARTMENT_EMPLOYEE.replace(':groupEmail', departmentEmail),
                _key: "value.name",
                _value: "value.email",
            },
        },
        {
            label: "Ghi Chú",
            formType: "textarea",
            key: "description",
            placeholder: "Ví dụ: Cần lưu ý...",
        },
    ];

    const handleSubmit = async (data: Record<string, any>) => {
        setIsLoading(true);

        // Process start dateTime
        const startDateTimeParts = data["start.dateTime"].split(" "); // Split date and time
        const startDateParts = startDateTimeParts[1].split("/"); // Split day, month, and year
        const formattedStartDateTime = `${startDateParts[2]}-${startDateParts[1]}-${startDateParts[0]}T${startDateTimeParts[0]}:00+07:00`;

        // Process end dateTime
        const endDateTimeParts = data["end.dateTime"].split(" "); // Split date and time
        const endDateParts = endDateTimeParts[1].split("/"); // Split day, month, and year
        const formattedEndDateTime = `${endDateParts[2]}-${endDateParts[1]}-${endDateParts[0]}T${endDateTimeParts[0]}:00+07:00`;

        try {
            // Lấy danh sách nhân viên từ API
            const employeeListResponse = await useApi.get(EMPLOYEE_LIST);
            const employeeList = employeeListResponse.data; // Giả sử dữ liệu trả về là một mảng

            // Lấy thông tin nhân viên thực hiện từ data
            const selectedEmployee = data["attendees.email"];

            // Tìm nhân viên được chọn trong danh sách nhân viên và lấy thông tin của họ
            const selectedEmployeeInfo = employeeList.find((employee: any) => employee.email === selectedEmployee);

            // Kiểm tra xem nhân viên có tồn tại trong danh sách không
            if (!selectedEmployeeInfo) {
                throw new Error("Không tìm thấy thông tin của nhân viên được chọn.");
            }

            // Tạo đối tượng attendee chứa tên và email của nhân viên
            const attendee = {
                name: selectedEmployeeInfo.name,
                email: selectedEmployee
            };

            const requestData = {
                ...data,
                start: { dateTime: formattedStartDateTime },
                end: { dateTime: formattedEndDateTime },
                attendees: [attendee], // Sử dụng thông tin nhân viên thu được
            };

            delete requestData["start.dateTime"];
            delete requestData["end.dateTime"];

            await useApi.post(TREE_TRIM_SCHEDULE_ADD, requestData);
            ref.current?.reload();
            navigate("/manage-treetrim-schedule");
        } catch (error) {
            console.error("Lỗi khi xử lý dữ liệu nhân viên:", error);
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