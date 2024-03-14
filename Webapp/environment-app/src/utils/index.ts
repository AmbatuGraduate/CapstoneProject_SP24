import dayjs from "dayjs";

export const dayFormat = (day: string | Date) => {
  return dayjs(day).format("DD-MM-YYYY");
};

