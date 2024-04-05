import dayjs from "dayjs";

export const dayFormat = (day: string | Date) => {
  return dayjs(day).format("DD-MM-YYYY");
};

export const dateConstructor = (day: string) => {
  const [d, m, y] = day.split("/").map(Number);
  return new Date(y, m - 1, d)
}

export const timeFormat = (day: string | Date) => {
  return dayjs(day).format("HH:mm");
};

export const taskStatus = (status) => {
  switch (status) {
    case "None":
      return { text: "", color: "" };
    case "Not Start":
      return { text: "Chưa Thực Hiện", color: "black" };
    case "In Progress":
      return { text: "Đang Làm", color: "orange" };
    case "Done":
      return { text: "Đã Hoàn Thành", color: "green" };
    case "Late":
      return { text: "Đã Trễ", color: "red" };
    default:
      return { text: status, color: "" };
  }
};

const ReportImpactType = {
  LOW: 0,
  MEDIUM: 1,
  HIGH: 2
}


export const ReportImpact = (reportImpact) => {
  switch (reportImpact) {
    case ReportImpactType.LOW:
      return { text: "Thấp", color: "grey" };
    case ReportImpactType.MEDIUM:
      return { text: "Trung bình", color: "orange" };
    case ReportImpactType.HIGH:
      return { text: "Cao", color: "red" };
    default:
      return { text: reportImpact, color: "" };
  }
}

export const ReportStatus = (reportStatus: string): { text: string, color: string } => {
  switch (reportStatus) {
    case "UnResolved":
      return { text: "Chưa được xử lý", color: "red" };
    case "Resolved":
      return { text: "Đã được xử lý", color: "green" };
    default: return { text: "Chưa được xử lý", color: "red" };
  }
}