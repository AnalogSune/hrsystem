import { Component, OnInit } from '@angular/core';
import { AppUser } from 'src/app/_models/appuser';
import { Form } from '@angular/forms';
import { AuthService } from 'src/app/_services/auth.service';
import { AdminService } from 'src/app/_services/admin.service';
import { Department, Role } from 'src/app/_models/department';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { stringify } from '@angular/compiler/src/util';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private adminService: AdminService, private alertify: AlertifyService) { }

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
        this.alertify.success('User was registered successfully!');
      }, error => {
        this.alertify.error('Failed to register user!', error);
      });
    else
    this.alertify.error("Passwords don't match!");
  }

  getDepartments() {
    this.adminService.getDepartments().subscribe(deps => {
      this.departments.clear();
      deps.forEach(d => {
        this.departments.set(d.id, d);
      });
      this.updateDepartment();
    }, error => {
      this.alertify.error('Unable to load departments!', error);
    })
  }

  updateDepartment() {
    if (this.departments && this.user.departmentId)
      this.roles =this.departments.get(this.user.departmentId)?.departmentRoles;
  }

  departmentsExist(): boolean {
    return this.departments.size > 0;
  }

  rolesExist(): boolean {
    return this.roles.length > 0;
  }
}
