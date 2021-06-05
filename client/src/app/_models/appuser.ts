import { Department, Role } from "./department";

export interface AppUser
{
    id?: number;
    password?: string;
    email?: string;
    fName?: string;
    lName?: string;
    phoneNumber?: string;
    address?: string;
    role?: Role;
    roleId?: number;
    inDepartment?: Department;
    departmentId?: number;
    pictureUrl?: string;
    daysOffLeft?: number;
    workedFromHome?: number;
    country?: string;
    dateOfBirth?: Date;
    nationality?: string;
    isAdmin?: boolean;
}

export interface UserUpdate
{
    id?: number;
    email?: string;
    fName?: string;
    lName?: string;
    pictureUrl?: string;
    phoneNumber?: string;
    address?: string;
    roleId?: number;
    departmentId?: number;
    daysOffLeft?: number;
    workedFromHome?: number;
    country?: string;
    dateOfBirth?: Date;
    nationality?: string;
    isAdmin?: boolean;
}