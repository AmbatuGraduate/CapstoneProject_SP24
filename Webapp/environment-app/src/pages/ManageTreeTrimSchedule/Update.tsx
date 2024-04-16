import { useNavigate, useParams } from "react-router-dom";
import { DEPARTMENT_EMPLOYEE, EMPLOYEE_LIST, TREE_TRIM_SCHEDULE_DETAIL, TREE_TRIM_SCHEDULE_UPDATE, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { useEffect, useRef, useState } from "react";
import Swal from "sweetalert2";

export const UpdateTreeTrimSchedule = () => {
    const navigate = useNavigate();
    const { id = "" } = useParams();
    const [data, setData] = useState<any>();
    const ref = useRef<any>();
    const [, setIsLoading] = useState(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await useApi.get(TREE_TRIM_SCHEDULE_DETAIL.replace(":id", id));
                setData(response.data);
            } catch (error) {
                console.error("Error fetching tree trimm schedule detail:", error);
            }
        };

        fetchData();
    }, [id]);

    const fields: Field[] = [
        {
            label: "Tiêu Đề",
            formType: "input",
            keyName: "summary",
            defaultValue: data?.myEvent.summary,
        },
        {
            label: "Địa Chỉ",
            formType: "input",
            keyName: "location",
            defaultValue: data?.myEvent.location,
        },
        {
            label: "Bắt Đầu Từ",
            formType: "datetime",
            keyName: "start.dateTime",
            defaultValue: data?.myEvent.start,
        },
        {
            label: "Kết Thúc Trước",
            formType: "datetime",
            keyName: "end.dateTime",
            defaultValue: data?.myEvent.end,
        },
        {
            label: "Cây Cần Cẳt",
            formType: "input",
            keyName: "treeId",
            defaultValue: data?.myEvent.extendedProperties.privateProperties.Tree,
            disabled: true,
        },
        {
            label: "Bộ Phận",
            formType: "input",
            keyName: "departmentEmail",
            defaultValue: data?.myEvent.extendedProperties.privateProperties.DepartmentEmail,
            disabled: true,
        },
        {
            label: "Nhân Viên Thực Hiện",
            formType: "select",
            keyName: "attendees.email",
            optionExtra: {
                url: DEPARTMENT_EMPLOYEE.replace(':groupEmail', data?.myEvent.extendedProperties.privateProperties.DepartmentEmail),
                _key: "name",
                _value: "email",
            },
            defaultValue: data?.myEvent?.attendees[0]?.fullName,
        },
        {
            label: "Ghi Chú",
            formType: "textarea",
            keyName: "description",
            defaultValue: data?.myEvent?.description,
        },
    ];

    const handleSubmit = async (data: Record<string, any>) => {
        setIsLoading(true);

        // Process start dateTime
        const formattedStartDateTime = data["start.dateTime"];

        // Process end dateTime
        const formattedEndDateTime = data["end.dateTime"];

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
            delete requestData["attendees.email"]
            delete requestData["departmentEmail"]

            await useApi.post(TREE_TRIM_SCHEDULE_UPDATE.replace(":id", id), requestData);

            Swal.fire(
                'Thành công!',
                'Cập nhật lịch cắt tỉa thành công!',
                'success'
            );
            ref.current?.reload();
            navigate(-1)
        } catch (error) {
            console.error("Lỗi khi xử lý dữ liệu lịch:", error);
            Swal.fire(
                'Lỗi!',
                'Lỗi khi cập nhật lịch cắt tỉa! Vui lòng thử lại sau.',
                'error'
            );
            setIsLoading(false);
            // Xử lý lỗi tại đây (ví dụ: hiển thị thông báo lỗi cho người dùng)
        }
    };
    return (
        <div className="form-cover">
            <h4>Cập Nhật Lịch Cắt Tỉa</h4>
            <FormBase
                fields={fields}
                onSave={handleSubmit}
                onCancel={() => navigate(-1)}
            />
        </div>
    );
};