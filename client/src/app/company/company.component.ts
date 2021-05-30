import { coerceCssPixelValue, coerceStringArray } from '@angular/cdk/coercion';
import { Component, OnInit } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { Department } from '../_models/department';
import { AdminService } from '../_services/admin.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent implements OnInit {
  users: AppUser[] = undefined;
  departments: Department[] = undefined;
  departmentName: string = "All";

  constructor(private userService: UserService, private authService: AuthService, 
    private adminService: AdminService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.filterByDepartment(null);
  }

  filterByDepartment(id?: number, depName: string = "All Departments")
  {
    this.departmentName = depName;
    if (id == null)
    {
      this.userService.getUsers().subscribe(u => {
        this.users = u;
      }, error => {
        this.alertify.error('Unable to retrieve users!', error);
      })
    }
    else
    {
      this.userService.getUsersByDepartment(id).subscribe( u => {
        this.users = u;
      }, error => {
        this.alertify.error('Unable to retrieve users!', error);
      })
    }

    this.adminService.getDepartments().subscribe(u => {
      this.departments = u;
    }, error => {
      this.alertify.error('Unable to retrieve departments!', error);
    })
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  isUser(id: number): boolean {
    return this.authService.decodedToken.nameid == id;
  }

  deleteUser(id: number): void {
    this.adminService.deleteUser(id).subscribe(d => {
      this.alertify.success('User deleted!');
      this.filterByDepartment(null);
    }, error => {
      this.alertify.error('Unable to delete user!', error);
    });
  }

  confirmDelete(id: number){
    this.alertify.confirm("Are you sure you want to delete this user?", () =>{
      this.deleteUser(id)
    })
  }

}
