import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { ClEANING_SCHEDULE_DETAIL, useApi } from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { dayFormat, taskStatus, timeFormat } from "../../utils";
import SimpleMap from "../ManageTree/MapIntergration";

export const DetailCleaningSchedule = () => {
    const navigate = useNavigate();
    const { id = "" } = useParams();
    const [data, setData] = useState<any>();
    const [loading, setLoading] = useState<boolean>(true);
    const handleNavigate = () => {
        navigate("/manage-cleaning-schedule");
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await useApi.get(ClEANING_SCHEDULE_DETAIL.replace(":id", id));
                setData(response.data);
            } catch (error) {
                console.error("Error fetching tree trimm schedule detail:", error);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [id]);

    return loading ? (
        <ClipLoader
            className="spinner"
            color={"hsl(94, 59%, 35%)"}
            loading={loading}
            size={60}
        />
    ) : (
        <div className="main-layout row">
            <h4 className="title">Xem Thông Tin Chi Tiết Lịch Vệ Sinh Đô Thị</h4>
            <hr className="line" />
            
            <div className="detail-content col-md-8">
                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Bộ Phận Quản Lý: </div>
                        <div className="detail-content-child-value">
                            {data?.myEvent.extendedProperties.privateProperties.DepartmentEmail}
                        </div>
                    </div>
                </div>
                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Nhân Viên Thực Hiện: </div>
                        <div className="detail-content-child-value">
                            {data?.myEvent && data?.myEvent.attendees && data?.myEvent.attendees.length <= 0 ? (
                                <div>Cần thêm nhân viên thực hiện</div>
                            ) : (
                                data?.myEvent?.attendees?.map(attendee => (
                                    <Link className="linkCode" style={{ display: 'block' }} to={`/manage-employee/email=${attendee.user.email}`} key={attendee.id}>{attendee.fullName}</Link>
                                ))
                            )}
                        </div>

                    </div>
                </div>
                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Tiêu Đề: </div>
                        <div className="detail-content-child-value">
                            {data?.myEvent.summary}
                        </div>
                    </div>
                </div>

                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Vị Trí Cụ Thể: </div>
                        <div className="detail-content-child-value">
                            {data?.myEvent.location}
                        </div>
                    </div>
                    <div
                        style={{
                            alignSelf: 'center',
                            justifyContent: 'center',
                            margin: '4rem',
                            boxShadow: '0px 5px 10px rgba(0, 0, 0, 0.3)',

                        }}>
                        <SimpleMap location={data.myEvent.location} />
                    </div>
                </div>

                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Thời Gian Làm Việc</div>
                    </div>
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Từ: </div>
                        <div className="detail-content-child-value">
                            {timeFormat(data?.myEvent.start)}   {dayFormat(data?.myEvent.start)}
                        </div>
                    </div>
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Tới</div>
                        <div className="detail-content-child-value">
                            {timeFormat(data?.myEvent.end)}   {dayFormat(data?.myEvent.end)}
                        </div>
                    </div>
                </div>

                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Ghi Chú: </div>
                        <div className="detail-content-child-value">
                            {data?.myEvent.description}
                        </div>
                    </div>
                </div>

                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">
                            Tình Trạng Công Việc:{" "}
                        </div>
                        <div className="detail-content-child-value" style={{
                            color: taskStatus(
                                data?.myEvent.extendedProperties.privateProperties.JobWorkingStatus
                            ).color,
                            fontWeight: "bold",
                        }}>
                            {taskStatus(data?.myEvent.extendedProperties.privateProperties.JobWorkingStatus).text}
                        </div>
                    </div>
                </div>

                <div className="button-cover grid">
                    <Button
                        className="btnCancel"
                        variant="danger"
                        onClick={handleNavigate}
                    >
                        Trở về
                    </Button>
                    <Link to={`/manage-cleaning-schedule/${data?.myEvent.id}/update`}>
                        <Button className="btnLink" variant="success">
                            Cập nhật
                        </Button>
                    </Link>
                </div>
            </div>
        </div>
    );
};
