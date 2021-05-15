export enum MeetingType{
    Training, Meeting
}

export interface Meeting {
    description?: string;
    departmentId?: number;
    date?: Date;
    durationHours?: number;
    meetingType?: MeetingType;
    meetingLink?: string;
}

export interface MeetingSearchDto {
    departmentId?: number;
    meetingType?: MeetingType;
}