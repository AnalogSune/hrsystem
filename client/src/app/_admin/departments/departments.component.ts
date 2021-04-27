import { Component, OnInit } from '@angular/core';
import { Department, Role } from 'src/app/_models/department';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css']
})
export class DepartmentsComponent implements OnInit {

  department: Department;
  rolename: string;

  departments: Department[];

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.adminService.getDepartments().subscribe(deps => {
      this.departments = deps;
      if (deps[0] != undefined)
        this.department = deps[0]; 
      console.log(deps);
    });
  }

  submit() {
    console.log(this.department);
    if (this.department != undefined && this.department.departmentRoles != undefined)
    this.adminService.createDepartment(this.department).subscribe(next => {
      console.log(next);
    }, error => {
      console.log(error);
    });
  }

  addRole() {
    this.department.departmentRoles.push({roleName: this.rolename});
    this.rolename = "";
    this.adminService.updateDepartment(this.department).subscribe(dep => {
      console.log(dep);
    });
  }

  selectedDep(dep: Department) {
    this.department = dep;
  }


}
