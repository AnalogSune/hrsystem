import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, } from '@angular/router';
import { AppUser } from '../_models/appuser';
import { Department, Role } from '../_models/department';
import { AdminService } from '../_services/admin.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-profile-viewer',
  templateUrl: './profile-viewer.component.html',
  styleUrls: ['./profile-viewer.component.css']
})
export class ProfileViewerComponent implements OnInit {

  constructor(private authService: AuthService, private userService: UserService,
    private routerParams: ActivatedRoute, private adminService: AdminService) { }

  user: AppUser;
  password: string;
  departments: Department[] = [];
  userDepartment: Department = {};
  userRole: Role = {};

  public get roles(): Role[] {
    return this.userDepartment.departmentRoles;
  }  

  ngOnInit() {
    this.fetchUser();
    this.getDepartments();
  }

  isUser(): boolean {
    return this.authService.decodedToken.nameid == this.user.id;
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  fetchUser() {
    this.routerParams.queryParams.subscribe(params => {
      if (params.userId == undefined)
      {
        this.userService.getUser(this.authService.decodedToken.nameid).subscribe(u => {
          this.user = u;
          this.userDepartment = u.inDepartment || {};
          this.userRole = u.role || {};
        });
      }
      else
      {
        this.userService.getUser(params.userId).subscribe(u => {
          this.user = u;
          this.userDepartment = u.inDepartment || {};
          this.userRole = u.role || {};
        });
      }
    });
  }

  uploadPhoto(event) {
    const file:File = event.target.files[0];
    this.userService.uploadPhoto(file).subscribe(next=>{
      this.fetchUser();
    });
  }

  changePassword(id: number) {
    this.adminService.changePassword(id, this.password).subscribe(next => {
      console.log(next);
    }, error => {
      console.log(error);
    });
  }

  updateDepartment() {
    this.adminService.changeUserDepartment(this.user.id, this.userDepartment.id).subscribe(next => {
      console.log(next);
      this.fetchUser();
    }, error => {
      console.log(error);
    })
  }

  updateRole() {
    this.adminService.changeUserRole(this.user.id, this.userRole.id).subscribe(next => {
      console.log(next);
    }, error => {
      console.log(error);
    })
  }

  getDepartments() {
    this.adminService.getDepartments().subscribe(deps => {
      this.departments = deps;
    }, error => {
      console.log(error);
    })
  }
}
