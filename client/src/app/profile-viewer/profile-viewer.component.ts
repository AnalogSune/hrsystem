import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, } from '@angular/router';
import { AppUser } from '../_models/appuser';
import { Department, Role } from '../_models/department';
import { AdminService } from '../_services/admin.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-profile-viewer',
  templateUrl: './profile-viewer.component.html',
  styleUrls: ['./profile-viewer.component.css']
})
export class ProfileViewerComponent implements OnInit {

  user: AppUser;
  password: string;
  departments: Map<number, Department> = new Map<number, Department>();
  userDepartmentId: number = -1;
  userRoleId: number = -1;

  constructor(private authService: AuthService, private userService: UserService,
    private routerParams: ActivatedRoute, private adminService: AdminService, private alertify: AlertifyService) { }

  public get roles(): Role[] {
    return this.departments.get(this.userDepartmentId)?.departmentRoles;
  }

  public get roleid(): number {
    let dep = this.departments.get(this.userDepartmentId);
    if (dep.departmentRoles.find(r => {r.id == this.userRoleId}))
      return this.userRoleId;
    else 
      return dep.departmentRoles[0]?.id;
  }

  public set roleid(id: number) {
    this.userRoleId = id;
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
      let userid = 0;
      if (params.userId == undefined)
        userid = this.authService.decodedToken.nameid;
      else
        userid = params.userId;
        
        this.userService.getUser(userid).subscribe(u => {
          this.user = u;
          if (u.inDepartment)
          {
            this.userDepartmentId = u.inDepartment?.id || -1;
            this.userRoleId = u.role?.id || -1;
          }
        });
    }, error => {
      this.alertify.error('Unable to retrieve query parameters!', error);
    });
  }

  uploadPhoto(event) {
    const file:File = event.target.files[0];
    this.userService.uploadPhoto(file).subscribe(next=>{
      this.fetchUser();
      this.alertify.success('Photo uploaded successfully!');
    }, error => {
      this.alertify.error('Unable to upload photo!', error);
    });
  }

  changePassword(id: number) {
    this.adminService.changePassword(id, this.password).subscribe(next => {
      this.alertify.success('Password changed successfully!');
    }, error => {
      this.alertify.error('Unable to change password!', error);
    });
  }

  updateDepartment() {
    if (this.hasRoles())
    {
      this.adminService.changeUserDepartment(this.user.id, this.userDepartmentId).subscribe(next => {
        this.userRoleId = this.departments.get(this.userDepartmentId).departmentRoles[0].id;
        this.updateRole();
      }, error => {
        this.alertify.error('Unable to change the departments!', error);
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
      this.userService.updateUser(this.user).subscribe(x => {
        this.user = x;
        this.alertify.success("User Updated!");
        this.fetchUser();
      }, error => {
        this.alertify.error("Could not Update user!");
      })
    }, error => {
      this.alertify.error('Unable to change role!', error);
    })
  }

  getDepartments() {
    this.adminService.getDepartments().subscribe(deps => {
      this.departments.clear();
      deps.forEach(d => {
        this.departments.set(d.id, d);
      });
    }, error => {
      this.alertify.error('Unable to retrieve the departments!', error);
    })
  }

  saveChanges() {
    this.updateDepartment();
  }
}
