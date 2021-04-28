import { coerceCssPixelValue } from '@angular/cdk/coercion';
import { Component, OnInit } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { AdminService } from '../_services/admin.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent implements OnInit {
  users: AppUser[] = undefined;

  constructor(private userService: UserService, private authService: AuthService, private adminService: AdminService) { }

  ngOnInit() {
    this.update();
  }

  update() {
    this.userService.getUsers().subscribe(u => {
      this.users = u;
      console.log(u);
    }, error => {
      console.log(error);
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
      console.log(d);
      this.update();
    }, error => {
      console.log(error);
    });
  }

}
