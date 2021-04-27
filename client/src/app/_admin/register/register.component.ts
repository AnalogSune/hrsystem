import { Component, OnInit } from '@angular/core';
import { AppUser } from 'src/app/_models/appuser';
import { Form } from '@angular/forms';
import { AuthService } from 'src/app/_services/auth.service';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  constructor(private adminService: AdminService) { }

  user: AppUser = {};
  passConfirm: string;

  ngOnInit() {
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

}
