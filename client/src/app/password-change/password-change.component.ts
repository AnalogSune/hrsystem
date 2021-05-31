import { HttpHeaders, HttpParams } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-password-change',
  templateUrl: './password-change.component.html',
  styleUrls: ['./password-change.component.css']
})
export class PasswordChangeComponent implements OnInit {
  password: string;
  passwordConfirm: string;
  private token: string;
  
  constructor(private route: ActivatedRoute, private authService: AuthService,
    private router: Router, private alertify: AlertifyService) { }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      this.token = params.token;
      if (!this.token || this.authService.jwtHelper.isTokenExpired(this.token))
        this.router.navigate([""]);
    });
  }

  submit() {
    if (this.password !== this.passwordConfirm)
    {
      this.alertify.error("Passwords don't match!");
      return;
    }

    let h = new HttpHeaders( {
      Authorization: "Bearer " + this.token
    });
    this.authService.changePassword(this.authService.jwtHelper.decodeToken(this.token).nameid, this.password, h).subscribe(r => {
      if (r)
      {
        this.alertify.success("Password was changed successfully!");
      }
    }, error => {
      this.alertify.error("Error changing password", error)
    });
    this.router.navigate([""]);
  }

}
