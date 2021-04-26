import { ScheduleType } from "./scheduleEntry";

export interface Request {
    employeeId: number;
    date: Date;
    endDate: Date;
    requestType: ScheduleType;
    status: number;
}