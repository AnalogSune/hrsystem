import { Component, OnInit } from '@angular/core';
import { Department, Role } from 'src/app/_models/department';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-departments',
  templateUrl: './departments.component.html',
  styleUrls: ['./departments.component.css']
})
export class DepartmentsComponent implements OnInit {

  department: Department = {name:"", departmentroles: []};
  rolename: string;

  constructor(private adminService: AdminService) { }

  ngOnInit() {
  }

  submit() {
    console.log(this.department);
    if (this.department != undefined && this.department.departmentroles != undefined)
    this.adminService.createDepartment(this.department).subscribe(next => {
      console.log(next);
    }, error => {
      console.log(error);
    });
  }

  addRole() {
    this.department.departmentroles.push({rolename: this.rolename});
    this.rolename = "";
  }

}
