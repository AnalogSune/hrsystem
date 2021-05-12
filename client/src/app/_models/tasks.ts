import { AppUser } from "./appuser";

export interface TaskCreationDTO {
    title? : string;
    description? : string;
    startTime? : Date;
    duration? : number;
    employeeId? : number;
}

export interface Task {
    title? : string;
    description? : string;
    startTime? : Date;
    endTime? : Date;
    duration? : number;
    employee? : AppUser;
    subTasks?: Array<SubTask>;
}

export interface SubTaskCreationDto {
    taskId?: number;
    description?: string;
}

export interface SubTask {
    id?: number;
    taskId?: number;
    description?: string;
    taskStatus?: number;
}