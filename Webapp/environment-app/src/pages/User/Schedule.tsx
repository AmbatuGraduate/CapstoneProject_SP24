import { useRef, useState } from "react";
import { Link } from "react-router-dom";
import { TREE_TRIM_SCHEDULE_DELETE, useApi, EMPLOYEE_SCHEDULE } from "../../Api";
import { ClipLoader } from "react-spinners";
import { Column } from "../../Components/ListView/Table";
import ModalDelete from "../../Components/Modals/ModalDelete";
import { taskStatus, timeFormat, dayFormat, roleFormat } from "../../utils";
import { ListView } from "../../Components/ListView";
import { useCookies } from "react-cookie";

export const Schedule = () => {
    const [loading] = useState<boolean>(true);
    const [token] = useCookies(["accessToken"]);
    const ref = useRef<any>();

    const columns: Column[] = [
        {
            header: "Thời Gian",
            accessorFn(row) {
                return (
                    <h6 className="shortText">
                        {timeFormat(row.myEvent.start) + "-" + timeFormat(row.myEvent.end)}
                    </h6>
                );
            },
            width: "10%",
        },
        {
            header: "Ngày Làm",
            accessorFn(row) {
                return (
                    <h6 className="shortText">
                        {dayFormat(row.myEvent.end)}
                    </h6>
                );
            },
            width: "10%",
        },
        {
            header: "Tiêu Đề",
            accessorFn(row) {
                return (
                    <h6 className="shortText">
                        <Link
                            className="linkCode"
                            style={{ fontWeight: "bold", textAlign: "center" }}
                            to={`/manage-treetrim-schedule/${row.myEvent.id}`}
                        >
                            {row.myEvent.summary}
                        </Link>
                    </h6>
                );
            },
            width: "20%",
        },
        {
            header: "Địa Chỉ Cụ Thể",
            accessorFn(row) {
                return <h6>{row.myEvent.location}</h6>;
            },
            width: "40%",
        },
        {
            header: "Trạng Thái",
            accessorFn(row) {
                return (
                    <h6
                        className="shortText"
                        style={{
                            color: taskStatus(
                                row.myEvent.extendedProperties.privateProperties.JobWorkingStatus
                            ).color,
                            fontWeight: "bold",
                        }}
                    >
                        {
                            taskStatus(
                                row.myEvent.extendedProperties.privateProperties.JobWorkingStatus
                            ).text
                        }
                    </h6>
                );
            },
        },
    ];


    return loading ? (
        <ClipLoader
            className="spinner"
            color={"hsl(94, 59%, 35%)"}
            loading={loading}
            size={60}
        />
    ) : (
        <div>
            {/* ----------------------------------------------------------------------------------------------- */}
            {/* schedule */}
            <div>
                <ListView
                    ref={ref}
                    listURL={EMPLOYEE_SCHEDULE.replace(":email", JSON.parse(token.accessToken).email)}
                    columns={columns}
                />
            </div>
        </div>
    );
};
