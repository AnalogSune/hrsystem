export interface Role {
    id?: number;
    roleName?: string;
}

export interface Department {
    id?: number;
    name?: string;
    departmentRoles?: Role[];
}
