import { useState, useEffect } from "react";
import { useApi } from "../../Api";
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import Table, { Column } from "../ListView/Table";

type DataResponse = {
    data?: unknown[];
    date: string; // Assuming each data item has a 'date' property
};

type Props = {
    listURL: string;
    columns: Column[];
};

export const CalendarView = (props: Props) => {
    const { listURL, columns } = props;

    const [data, setData] = useState<DataResponse | null>(null);
    const [loading, setLoading] = useState(true);
    const [selectedDate, setSelectedDate] = useState(new Date());
    const [currentPage, setCurrentPage] = useState(1);

    const fetchData = async () => {
        setLoading(true);
        try {
            const res = await useApi.get(listURL);
            const _data = await res.data;
            setData(_data);
        } catch (error) {
            console.log(error);
        }
        setLoading(false);
    };

    useEffect(() => {
        fetchData();
    }, []);

    const handleDateChange = (value: Date | Date[] | null, event: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        if (Array.isArray(value)) {
            // If value is a range of dates, select the first date in the range
            // If value is an array of dates, select the first date in the array
            setSelectedDate(value[0]);
        } else if (value instanceof Date) {
            setSelectedDate(value);
        }
    };

    const filteredData = data?.filter(
        (item) => new Date(item.date).toDateString() === selectedDate.toDateString()
    );

    return (
        <div>
            <Calendar
                onChange={handleDateChange}
                value={selectedDate}
            />
            {loading ? (
                <p>Loading...</p>
            ) : (
                <Table
                    data={filteredData}
                    currentPage={currentPage}
                    pageCount={Math.ceil((filteredData?.length || 0) / 10)}
                    loading={loading}
                    columns={columns}
                    onChange={setCurrentPage}
                />
            )}
        </div>
    );
};