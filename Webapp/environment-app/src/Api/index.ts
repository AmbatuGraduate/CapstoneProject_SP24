import axiosClient from "./axiosClient";
export const useApi = axiosClient;

export const TREE_LIST = "/Tree/Get";
export const TREE_DETAIL = "/Tree/GetByTreeCode/:id";
export const TREE_ADD = "Tree/Add";
export const TREE_UPDATE = "Tree/Update/:id";
export const TREE_DELETE = "Tree/Delete/:id";
export const CULTIVAR_LIST = "/Cultivar/Get";
export const CULTIVAR_DETAIL = "/Cultivar/GetById/:id";
export const TREE_TYPE_LIST = "/TreeType/";
export const TREE_TRIM_SCHEDULE = "/ScheduleTreeTrim/GetCalendarEvents";

export const STREET_LIST = "Street/Get"
export const LOGIN = "/auth/google"