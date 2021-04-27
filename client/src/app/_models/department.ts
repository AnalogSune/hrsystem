export interface Role {
    rolename?: string;
}

export interface Department {
    name?: string;
    departmentroles?: Role[];
}
