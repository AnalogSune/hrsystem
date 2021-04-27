export interface Role {
    roleName?: string;
}

export interface Department {
    id: number;
    name?: string;
    departmentRoles?: Role[];
}
