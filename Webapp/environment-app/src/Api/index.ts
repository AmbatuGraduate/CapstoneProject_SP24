import axiosClient from "../Api/axiosClient";
export const useApi = axiosClient;


export const EMPLOYEE_LIST = "/User/GetGoogleUsers/GetGoogleUsers";
export const EMPLOYEE_DETAIL = "/User/GetGoogleUser?:email";
export const EMPLOYEE_ADD = "/User/AddGoogleUser";
export const EMPLOYEE_DELETE = "/User/DeleteGoogleUser";
export const EMPLOYEE_SCHEDULE = "/Calendar/GetCalendarEventsByAttendeeEmail?calendarTypeEnum=1&attendeeEmail=:email";

export const TREE_LIST = "/Tree/Get";
export const TREE_DETAIL = "/Tree/GetByTreeCode/:id";
export const TREE_ADD = "Tree/Add";
export const TREE_UPDATE = "Tree/Update/:id";
export const TREE_DELETE = "Tree/Delete/:id";
export const CULTIVAR_LIST = "/Cultivar/Get";
export const CULTIVAR_DETAIL = "/Cultivar/GetById/:id";
export const TREE_TYPE_LIST = "/TreeType/";

export const TREE_TRIM_SCHEDULE = "/Calendar/GetAllCalendarEvents?calendarTypeEnum=1";
export const TREE_TRIM_SCHEDULE_DELETE = "/Calendar/DeleteCalendarEvent?calendarTypeEnum=1&eventId=:id";
export const TREE_TRIM_SCHEDULE_DETAIL = "/Calendar/GetEventById?calendarTypeEnum=1&eventId=:id";

export const GARBAGE_COLLECTION_SCHEDULE = "/Calendar/GetAllCalendarEvents?calendarTypeEnum=2";

export const ClEANING_SCHEDULE = "/Calendar/GetAllCalendarEvents?calendarTypeEnum=3";

export const STREET_LIST = "Street/Get"
export const LOGIN = "/auth/google"

export const REPORT_LIST =  "/Report/GetReportFormats"
export const CREATE_REPORT = "/Report/CreateReport"
export const DETAIL_REPORT = "/Report/GetReportById/?id=:id"
export const RESPONSE_REPORT = "/Report/ResponseReport"

