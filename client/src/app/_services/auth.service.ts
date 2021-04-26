import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appuser';
import { UserService } from './user.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  baseUrl = environment.baseUrl;
  currentUser: AppUser;

  constructor(private http: HttpClient, private userService: UserService) 
  { 
    if (this.isLoggedIn()) {
      this.updateCurrentUser();
    }
  }

  private updateCurrentUser() {
    this.decodedToken = this.jwtHelper.decodeToken(localStorage.getItem('token'));
    this.userService.getUser(this.decodedToken.nameid).subscribe(user => {
      this.currentUser = user;
    }, error => {
      console.log(error);
    });
  }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login/', model)
    .pipe(
      map((response: any) => {
        if (response) {
          localStorage.setItem('token', response.token);
          localStorage.setItem('email', response.email);
          this.updateCurrentUser();
        }
      })
    );
  }

  isLoggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
