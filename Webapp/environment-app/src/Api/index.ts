import axiosClient from "../Api/axiosClient";
export const useApi = axiosClient;


export const EMPLOYEE_LIST = "/User/GetGoogleUsers/GetGoogleUsers";
export const EMPLOYEE_DETAIL = "/User/GetGoogleUser?:email";
export const EMPLOYEE_ADD = "/User/AddGoogleUser";
export const EMPLOYEE_UPDATE = "/User/UpdateGoogleUser";
export const EMPLOYEE_DELETE = "/User/DeleteGoogleUser?userEmail=:email";
export const EMPLOYEE_SCHEDULE = "/Calendar/GetCalendarEventsByAttendeeEmail?calendarTypeEnum=1&attendeeEmail=:email";

export const DEPARTMENT_LIST = "/Group/GetAllGroups";
export const DEPARTMENT_EMPLOYEE = "/Group/GetAllMembersOfGroup?groupEmail=:groupEmail";

export const TREE_LIST = "/Tree/Get";
export const TREE_DETAIL = "/Tree/GetByTreeCode/:id";
export const TREE_ADD = "Tree/Add";
export const TREE_UPDATE = "Tree/Update/:id";
export const TREE_DELETE = "Tree/Delete/:id";
export const TREE_TYPE_LIST = "/TreeType/";
export const TREE_ADDRESS = "/Tree/GetCut/:address"

export const TREE_TRIM_SCHEDULE = "/Calendar/GetAllCalendarEvents?calendarTypeEnum=1";
export const TREE_TRIM_SCHEDULE_DELETE = "/Calendar/DeleteCalendarEvent?calendarTypeEnum=1&eventId=:id";
export const TREE_TRIM_SCHEDULE_DETAIL = "/Calendar/GetEventById?calendarTypeEnum=1&eventId=:id";
export const TREE_TRIM_SCHEDULE_ADD = "/Calendar/AddCalendarEvent?calendarTypeEnum=1";
export const TREE_TRIM_SCHEDULE_UPDATE = "/Calendar/UpdateCalendarEvent?calendarTypeEnum=1&eventId=:id";

export const GARBAGE_COLLECTION_SCHEDULE = "/Calendar/GetAllCalendarEvents?calendarTypeEnum=2";
export const GARBAGE_COLLECTION_DELETE = "/Calendar/DeleteCalendarEvent?calendarTypeEnum=2&eventId=:id";
export const GARBAGE_COLLECTION_DETAIL = "/Calendar/GetEventById?calendarTypeEnum=2&eventId=:id";
export const GARBAGE_COLLECTION_ADD = "/Calendar/AddCalendarEvent?calendarTypeEnum=2";
export const GARBAGE_COLLECTION_UPDATE = "/Calendar/UpdateCalendarEvent?calendarTypeEnum=2&eventId=:id";

export const ClEANING_SCHEDULE = "/Calendar/GetAllCalendarEvents?calendarTypeEnum=3";
export const ClEANING_SCHEDULE_DELETE = "/Calendar/DeleteCalendarEvent?calendarTypeEnum=3&eventId=:id";
export const ClEANING_SCHEDULE_DETAIL = "/Calendar/GetEventById?calendarTypeEnum=3&eventId=:id";
export const ClEANING_SCHEDULE_ADD = "/Calendar/AddCalendarEvent?calendarTypeEnum=3";
export const ClEANING_SCHEDULE_UPDATE = "/Calendar/UpdateCalendarEvent?calendarTypeEnum=3&eventId=:id";

export const STREET_LIST = "Street/Get"
export const LOGIN = "/auth/google"

export const REPORT_LIST = "/Report/GetReportFormats"
export const REPORT_BY_USER = "Report/GetReportsByUser?email=:email"
export const CREATE_REPORT = "/Report/CreateReport"
export const DETAIL_REPORT = "/Report/GetReportById/?id=:id"
export const RESPONSE_REPORT = "/Report/ResponseReport"
export const DELETE_REPORT = "/Report/DeleteReport?id=:id"

// export const GROUP_LIST = "/Group/GetAllGroups"
// export const GROUP_EMPLOYEE = "/Group/GetAllMembersOfGroup/?groupEmail=:email"
export const GROUP_DELETE = "/Group/DeleteGroup?groupEmail=:email"
export const GROUP_ADD = "/Group/AddGroup"
export const GROUP_DETAIL = "/Group/GetGroupByGroupEmail?groupEmail=:email"
export const GROUP_UPDATE = "/Group/UpdateGroup"