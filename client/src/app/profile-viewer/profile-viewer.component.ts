import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, } from '@angular/router';
import { AppUser } from '../_models/appuser';
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
  ngOnInit() {
    this.fetchUser();

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
        });
      }
      else
      {
        this.userService.getUser(params.userId).subscribe(u => {
          this.user = u;
          console.log(this.user);
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
}
