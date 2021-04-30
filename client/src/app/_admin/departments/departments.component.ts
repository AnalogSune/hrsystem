import { Component, OnInit } from '@angular/core';
import { isEmpty } from 'rxjs/operators';
import { Department, Role } from 'src/app/_models/department';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

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

  constructor(private adminService: AdminService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.update();
  }

  update() {
    this.adminService.getDepartments().subscribe(deps => {
      this.departments = deps;
      if (deps[0] != undefined)
        this.department = deps[0]; 
    });
  }

  addRole() {
    if (this.department != undefined && this.rolename)
    {
      this.adminService.addRole(this.department.id, this.rolename).subscribe(res => {
        this.alertify.success('Role added successfully!');
        this.department = res;
      }, error => {
        this.alertify.error('Unable to add role!', error);
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
        this.alertify.success('New department created!');
        this.update();
      }, error => {
        this.alertify.error('Unable to create department!', error);
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
      this.alertify.success('Role deleted!');
      this.update();
    }, error => {
      this.alertify.error('Unable to delete role!', error);
    });
  }

  deleteDepartment()
  {
    if (this.department)
    {
      this.adminService.removeDepartment(this.department.id).subscribe(next => {
        this.department = null;
        this.update();
        this.alertify.success('Department deleted!');
      }, error => {
        this.alertify.error('Unable to delete department!', error);
      });
    }
  }
}
