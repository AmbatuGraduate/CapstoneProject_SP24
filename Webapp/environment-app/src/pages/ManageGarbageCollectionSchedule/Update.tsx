import { Link, useNavigate, useParams } from "react-router-dom";
import { GARBAGE_COLLECTION_DETAIL, GARBAGE_COLLECTION_UPDATE, useApi } from "../../Api";
import { Field, FormBase } from "../../Components/FormBase";
import { dateConstructor } from "../../utils";
import { useEffect, useRef, useState } from "react";
import { useCookies } from "react-cookie";

export const UpdateGarbageCollectionSchedule = () => {
    const navigate = useNavigate();
    const { id = "" } = useParams();
    const [data, setData] = useState<any>();
    const ref = useRef<any>();
    const [, setIsLoading] = useState(false);
    const [token] = useCookies(["accessToken"]);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await useApi.get(GARBAGE_COLLECTION_DETAIL.replace(":id", id));
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
            key: "sumary",
            defaultValue: data?.myEvent.summary,
        },
        {
            label: "Địa Chỉ",
            formType: "input",
            key: "location",
            defaultValue: data?.myEvent.location,
        },
        {
            label: "Bắt Đầu Từ",
            formType: "datetime",
            key: "start.dateTime",
            defaultValue: data?.myEvent.start,
        },
        {
            label: "Kết Thúc Trước",
            formType: "datetime",
            key: "end.dateTime",
        },
        {
            label: "Nhân Viên Thực Hiện",
            formType: "input",
            key: "attendees",
            defaultValue: data?.myEvent.attendees[0].fullName,
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
        await useApi.post(GARBAGE_COLLECTION_UPDATE, {
            ...data,
        });
        ref.current?.reload();
        navigate("/manage-garbagecollection-schedule");
    };

    return (
        <div className="form-cover">
            <h4>Cập Nhật Lịch Thu Gom Rác</h4>
            <FormBase
                fields={fields}
                onSave={handleSubmit}
                onCancel={() => navigate("/manage-garbagecollection-schedule")}
            />
        </div>
    );
};