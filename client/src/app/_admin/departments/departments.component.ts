import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { isEmpty } from 'rxjs/operators';
import { Department, Role } from 'src/app/_models/department';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { CreateDepartmentComponent } from '../create-department/create-department.component';

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

  constructor(private adminService: AdminService, private alertify: AlertifyService, private dialog: MatDialog) { }

  ngOnInit() {
    this.update();
  }

  update() {
    this.adminService.getDepartments().subscribe(deps => {
      this.departments = deps;
      if (deps.length > 0)
      {
        if (!this.department)
          this.department = deps[0];
        else
          this.department = deps.find((d) => {return d.id == this.department.id;})
      }

    });
  }

  addRole() {
    if (this.department != undefined && this.rolename)
    {
      this.adminService.addRole(this.department.id, this.rolename).subscribe(res => {
        this.alertify.success('Role added successfully!');
        this.department = res;
        this.update();
      }, error => {
        this.alertify.error('Unable to add role!', error);
      });
      this.rolename = ""
    }
    else {
      this.alertify.error('Please enter role name');
    }
  }

  addDepartment(name: string) {
    if (name)
    {
      let newDepartment: Department = {name: name};
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
        this.department = undefined;
        this.update();
        this.alertify.success('Department deleted!');
      }, error => {
        this.alertify.error('Unable to delete department!', error);
      });
    }
  }

  openDialog() {
    const ref = this.dialog.open(CreateDepartmentComponent);
    ref.afterClosed().subscribe(r => {
      this.addDepartment(r);
    })
  }

  confirmDelete() {
    this.alertify.confirm('Are you sure you want to delete this department?', () => {
      this.deleteDepartment();
    });
  }

  confirmDeleteRole(id: number) {
    this.alertify.confirm('Are you sure you want to delete this role?', () => {
      this.deleteRole(id);
    });
  }
}
