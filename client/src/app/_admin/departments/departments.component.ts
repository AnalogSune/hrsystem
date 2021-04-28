import { Component, OnInit } from '@angular/core';
import { isEmpty } from 'rxjs/operators';
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
  departmentname: string;
  departments: Department[];

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.update();
  }

  update() {
    this.adminService.getDepartments().subscribe(deps => {
      this.departments = deps;
      if (deps[0] != undefined)
        this.department = deps[0]; 
      console.log(deps);
    });
  }

  addRole() {
    if (this.department != undefined && this.rolename)
    {
      this.adminService.addRole(this.department.id, this.rolename).subscribe(res => {
        console.log(res);
        this.department = res;
      }, error => {
        console.log(error);
      });
      this.rolename = ""
    }
  }

  addDepartment() {
    if (this.departmentname)
    {
      let newDepartment: Department = {name: ""};
      newDepartment.name = this.departmentname;
      this.adminService.createDepartment(newDepartment).subscribe(next => {
        console.log(next);
        this.update();
      }, error => {
        console.log(error);
      });
      this.departmentname = ""
    }
  }

  selectedDepartment(dep: Department) {
    this.department = dep;
  }

  deleteRole(id: number)
  {

    this.adminService.removeRole(id).subscribe(next => {
      this.update();
    }, error => {
      console.log(error);
    });
  }

  deleteDepartment()
  {
    if (this.department)
    {
      this.adminService.removeDepartment(this.department.id).subscribe(next => {
        this.department = null;
        this.update();
      }, error => {
        console.log(error);
      });
    }
  }
}
