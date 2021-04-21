import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  loginInfo: any = {}
  constructor(private authService: AuthService, private router: Router) { }

  ngOnInit() {
  }

  login()
  {
    this.authService.login(this.loginInfo).subscribe(next => {
      console.log('Yey');
    }, error => {
      console.error('Nay')
    }, () => {
      this.router.navigate(['']);
    });
  }


}
