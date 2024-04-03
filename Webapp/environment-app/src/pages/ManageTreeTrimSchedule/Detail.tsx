import { useEffect, useState } from "react";
import { Link, useNavigate, useParams } from "react-router-dom";
import { TREE_TRIM_SCHEDULE_DETAIL, useApi } from "../../Api";
import { ClipLoader } from "react-spinners";
import { Button } from "react-bootstrap";
import { dayFormat, timeFormat } from "../../utils";

export const DetailTreeTrimSchedule = () => {
    const navigate = useNavigate();
    const { id = "" } = useParams();
    const [data, setData] = useState<any>();
    const [loading, setLoading] = useState<boolean>(true);
    const handleNavigate = () => {
        navigate("/manage-treetrim-schedule");
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await useApi.get(TREE_TRIM_SCHEDULE_DETAIL.replace(":id", id));
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
            <div className="detail-content col-md-4">
                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">Nhân Viên Thực Hiện: </div>
                        <div className="detail-content-child-value">
                            {data?.myEvent.attendees.length <= 0 ? (
                                <div>Cần thêm nhân viên thực hiện</div>
                            ) : (
                                data?.myEvent.attendees.map(attendee => (
                                    <Link className="linkCode" to={`/manage-employee/email=${attendee.user.email}`} key={attendee.id}>{attendee.fullName}</Link>
                                ))
                            )}
                        </div>
                    </div>
                </div>
            </div>

            <div className="detail-content col-md-8">
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
                        <div className="detail-content-child-label">Cây Cần Cắt: </div>
                        <div className="detail-content-child-value">
                            {data?.myEvent.extendedProperties.privateProperties.Tree}
                        </div>
                    </div>
                </div>

                <div className="detail-cover">
                    <div className="detail-content-parent">
                        <div className="detail-content-child-label">
                            Tình Trạng Công Việc:{" "}
                        </div>
                        <div className="detail-content-child-value">
                            {data?.myEvent.extendedProperties.privateProperties.JobWorkingStatus}
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
                    <Link to={`/manage-tree/${data?.treeCode}/update`}>
                        <Button className="btnLink" variant="success">
                            Cập nhật
                        </Button>
                    </Link>
                </div>
            </div>
        </div>
    );
};
