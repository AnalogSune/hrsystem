import { ScheduleType } from "./scheduleEntry";

export enum RequestStatus {
    Pending, Accepted, Declined
}

export interface Request {
    id?: number;
    employeeId: number;
    date: Date;
    endDate: Date;
    requestType: ScheduleType;
    status: number;
}

export interface RequestSearch {
    employeeId?: number;
    requestStatus?: RequestStatus;
    requestType?: ScheduleType;
}