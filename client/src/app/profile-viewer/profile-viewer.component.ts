import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, } from '@angular/router';
import { AppUser } from '../_models/appuser';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-profile-viewer',
  templateUrl: './profile-viewer.component.html',
  styleUrls: ['./profile-viewer.component.css']
})
export class ProfileViewerComponent implements OnInit {

  constructor(private authService: AuthService, private userService: UserService,
    private routerParams: ActivatedRoute) { }

  user: AppUser;

  ngOnInit() {
    this.fetchUser();

  }

  isUser(): boolean {
    return this.authService.decodedToken.nameid == this.user.id;
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
}
