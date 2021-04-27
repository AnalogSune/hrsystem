export interface AppUser
{
    id?: number;
    password?: string;
    email?: string;
    fname?: string;
    lname?: string;
    phonenumber?: string;
    address?: string;
    roleid?: number;
    departmentid?: number;
    pictureurl?: string;
    daysoffleft?: number;
    workedfromhone?: number;
    country?: string;
    dateofbirth?: Date;
    nationality?: string;
    isadmin?: boolean;
}