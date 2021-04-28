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
    inDepartment?: Department;
    pictureUrl?: string;
    daysOffLeft?: number;
    workedFromHome?: number;
    country?: string;
    dateOfBirth?: Date;
    nationality?: string;
    isAdmin?: boolean;
}