import { AppUser } from "./appuser";

export interface TaskCreationDTO {
    title? : string;
    description? : string;
    startTime? : Date;
    duration? : number;
    employeeId? : number;
}

export interface Task {
    id?: number;
    title? : string;
    description? : string;
    startTime? : Date;
    endTime? : Date;
    duration? : number;
    employee? : AppUser;
    subTasks?: Array<SubTask>;
}

export interface SubTaskCreationDto {
    tasksId?: number;
    description?: string;
}

export interface SubTask {
    id?: number;
    tasksId?: number;
    description?: string;
    status?: number;
}

export interface TaskSearchDto {
    employeeId?: number;
    taskId?: number;
    status?: number;
    isOverdue?: boolean;
}