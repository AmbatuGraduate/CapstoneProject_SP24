import { useNavigate } from "react-router-dom";
import { TREE_TRIM_SCHEDULE_ADD, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor } from "../../utils";
import { useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const CreateTreeTrimSchedule = () => {
    const navigate = useNavigate();
    const ref = useRef<any>();
    const [, setIsLoading] = useState(false);
    const [token] = useCookies(["accessToken"]);

    const fields: Field[] = [
        {
            label: "Tiêu Đề",
            formType: "input",
            key: "sumary",
        },
        {
            label: "Địa Chỉ",
            formType: "input",
            key: "location",
            placeholder: "Ví dụ: 29 Sơn Thủy Đông 2, Hòa Hải, Ngũ Hành Sơn",
        },
        {
            label: "Bắt Đầu Từ",
            formType: "datetime",
            key: "start.dateTime",
        },
        {
            label: "Kết Thúc Trước",
            formType: "datetime",
            key: "end.dateTime",
        },
        {
            label: "Cây Cần Cẳt",
            formType: "input",
            key: "treeId",
        },
        {
            label: "Nhân Viên Thực Hiện",
            formType: "input",
            key: "",
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
        await useApi.post(TREE_TRIM_SCHEDULE_ADD, {
            ...data,
        });
        ref.current?.reload();
        navigate("/manage-treetrim-schedule");
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