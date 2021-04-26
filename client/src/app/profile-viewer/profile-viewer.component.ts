import { Component, Input, OnInit } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-profile-viewer',
  templateUrl: './profile-viewer.component.html',
  styleUrls: ['./profile-viewer.component.css']
})
export class ProfileViewerComponent implements OnInit {

  constructor(private authService: AuthService, private userService: UserService) { }

  @Input() user: AppUser;

  ngOnInit() {
    if (this.user == undefined)
      this.userService.getUser(this.authService.decodedToken.nameid).subscribe(u => {
        this.user = u;
      });

  }

}
