import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs/operators';
import { AppUser } from '../_models/appuser';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  jwtHelper = new JwtHelperService();
  baseUrl = 'http://localhost:5000/api/';
  user: AppUser;

  constructor(private http: HttpClient) { this.updateCurrentUser() }

  login(model: any) {
    return this.http.post(this.baseUrl + 'account/login/', model)
    .pipe(
      map((response: any) => {
        const user = response;
        if (user) {
          localStorage.setItem('token', user.token);
        }
      })
    );
  }

  updateCurrentUser() {
    if (this.isLoggedIn())
    {
      let id = this.jwtHelper.decodeToken(localStorage.getItem('token')).nameid;
      this.getUser(id).subscribe(u => {
        this.user = u;
      }, error => {
        console.error(error);
      });
    }
  }

  getUser(id: number) {
    let h = new HttpHeaders().set('Authorization','Bearer ' + localStorage.getItem('token'));
    return this.http.get<AppUser>(this.baseUrl + 'users/' + id, {headers: h});
  }

  isLoggedIn() {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }
}
