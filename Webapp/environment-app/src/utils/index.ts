import dayjs from "dayjs";

export const dayFormat = (day: string | Date) => {
  return dayjs(day).format("DD-MM-YYYY");
};

export const dateConstructor = (day: string) => {
  const [d, m, y] = day.split("/").map(Number);
  return new Date(y, m-1, d)
}

export const timeFormat = (day: string | Date) => {
  return dayjs(day).format("HH:mm");
};

export const taskStatus = (status) => {
  switch (status) {
    case "None":
      return { text: "", color: "" };
    case "Not Start":
      return { text: "Chưa Thực Hiện", color: "red" };
    case "In Progress":
      return { text: "Đang Làm", color: "orange" };
    case "Done":
      return { text: "Đã Hoàn Thành", color: "green" };
    default:
      return { text: status, color: "" };
  }
};