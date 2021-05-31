import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { PasswordChangeEmailDialogComponent } from '../password-change-email-dialog/password-change-email-dialog.component';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  loginInfo: any = {}
  constructor(private authService: AuthService, private router: Router, private alertify: AlertifyService,
    private dialog: MatDialog) { }

  ngOnInit() {
  }

  login()
  {
    this.authService.login(this.loginInfo).subscribe(next => {
      this.alertify.success('You have logged in!');
    }, error => {
      this.alertify.error('Failed to log in!', error);      
    }, () => {
      this.router.navigate(['']);
    });
  }

  openDialog() {
    this.dialog.open(PasswordChangeEmailDialogComponent);
  }


}
