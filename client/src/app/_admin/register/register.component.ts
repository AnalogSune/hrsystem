import { Component, OnInit } from '@angular/core';
import { AppUser } from 'src/app/_models/appuser';
import { Form } from '@angular/forms';
import { AuthService } from 'src/app/_services/auth.service';
import { AdminService } from 'src/app/_services/admin.service';
import { Department, Role } from 'src/app/_models/department';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private adminService: AdminService) { }

  user: AppUser = {};
  passConfirm: string;
  departments:  Map<number, Department> = new Map<number, Department>();
  roles: Role[] = [];

  ngOnInit() {
    this.getDepartments();
  }

  register() {
    if (this.user.password == this.passConfirm)
      this.adminService.register(this.user).subscribe(res => {
        console.log(this.user);
        console.log(res);
      }, error => {
        console.log(error);
      });
    else
      console.log("Passwords don't match!");
  }

  getDepartments() {
    this.adminService.getDepartments().subscribe(deps => {
      // this.departments = deps;
      console.log(deps);
      this.departments.clear();
      deps.forEach(d => {
        this.departments.set(d.id, d);
      });
      this.updateDepartment();
    }, error => {
      console.log(error);
    })
  }

  updateDepartment() {
    if (this.departments && this.user.departmentId)
      this.roles =this.departments.get(this.user.departmentId)?.departmentRoles;

      console.log(this.roles);
  }

  departmentsExist(): boolean {
    return this.departments.size > 0;
  }

  rolesExist(): boolean {
    return this.roles.length > 0;
  }
}
