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
  departments: Map<number, Department> = new Map<number, Department>();
  userDepartmentId: number = -1;
  userRoleId: number = -1;

  public get roles(): Role[] {
    return this.departments.get(this.userDepartmentId)?.departmentRoles;
  }  

  ngOnInit() {
    this.fetchUser();
    if (this.isAdmin())
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
          if (u.inDepartment)
          {
            this.userDepartmentId = u.inDepartment.id || -1;
            this.userRoleId = u.role.id || -1;
          }
        });
      }
      else
      {
        this.userService.getUser(params.userId).subscribe(u => {
          this.user = u;
          if (u.inDepartment)
          {
            this.userDepartmentId = u.inDepartment.id || -1;
            this.userRoleId = u.role.id || -1;
          }
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
    if (this.hasRoles())
    {
      this.adminService.changeUserDepartment(this.user.id, this.userDepartmentId).subscribe(next => {
        console.log(next);
        this.userRoleId = this.departments.get(this.userDepartmentId).departmentRoles[0].id;
        this.updateRole();
      }, error => {
        console.log(error);
      });
    }
  }

  hasRoles(): boolean {
    if (this.userDepartmentId >= 0  && this.departmentsExist())
      return this.departments.get(this.userDepartmentId)?.departmentRoles.length > 0;

    return false;
  }

  departmentsExist(): boolean {
    return this.departments.size > 0;
  }

  updateRole() {
    this.adminService.changeUserRole(this.user.id, this.userRoleId).subscribe(next => {
      console.log(next);
      this.fetchUser();
    }, error => {
      console.log(error);
    })
  }

  getDepartments() {
    this.adminService.getDepartments().subscribe(deps => {
      this.departments.clear();
      deps.forEach(d => {
        this.departments.set(d.id, d);
      });
    }, error => {
      console.log(error);
    })
  }
}
