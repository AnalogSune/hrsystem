export enum ScheduleType 
{
    Working, DayOff, WorkFromHome, SickDay
}

export class ScheduleEntry {
    id?: number;
    employeeId: number;
    startDate: Date;
    endDate: Date;
    type: ScheduleType;
    createNewEntry: boolean = true;
    workShiftId: number;
}

export class ScheduleSearchDto {
    constructor(startDate: Date, endDate: Date, employeeid?: number, type?: ScheduleType) {
        this.startDate = startDate;
        this.endDate = endDate;
        this.employeeId = employeeid;
        this.type = type;
    }
    startDate: Date;
    endDate: Date;
    employeeId?: number;
    type?: ScheduleType;
}