export enum CalendarType 
{
    Working, DayOff, WorkFromHome, SickDay
}

export class ScheduleEntry {
    id: number;
    employeeId: number;
    startDate: Date;
    endDate: Date;
    type: CalendarType;
}

export class ScheduleSearchDto {
    constructor(startDate: Date, endDate: Date, employeeid?: number, type?: CalendarType) {
        this.startDate = startDate;
        this.endDate = endDate;
        this.employeeId = employeeid;
        this.type = type;
    }
    startDate: Date;
    endDate: Date;
    employeeId?: number;
    type?: CalendarType;
}