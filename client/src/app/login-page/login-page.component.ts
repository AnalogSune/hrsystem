import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  loginInfo: any = {}
  constructor(private accountService: AccountService, private router: Router) { }

  ngOnInit() {
  }

  login()
  {
    this.accountService.login(this.loginInfo).subscribe(next => {
      console.log('Yey');
    }, error => {
      console.error('Nay')
    }, () => {
      this.router.navigate(['']);
    });
  }


}
