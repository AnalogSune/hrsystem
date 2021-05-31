import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ReplaySubject } from 'rxjs';
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

  private currentUserSource = new ReplaySubject<AppUser>(1);
  currentUser = this.currentUserSource.asObservable();

  constructor(private http: HttpClient, private userService: UserService) 
  { 
    if (this.isLoggedIn()) {
      this.updateCurrentUser();
    }
  }

  private updateCurrentUser() {
    this.decodedToken = this.jwtHelper.decodeToken(localStorage.getItem('token'));
    this.userService.getUser(this.decodedToken.nameid).subscribe(user => {
      this.setCurrentUser(user);
      localStorage.setItem('departmentId', user.inDepartment.id.toString());
    }, error => {
      console.log(error);
    });
  }

  setCurrentUser(user: AppUser) {
    localStorage.setItem('user', JSON.stringify(user));
  }

  getCurrentUser(): AppUser {
    return JSON.parse(localStorage.getItem('user'));
  }

  getUserId(): number {
    return this.decodedToken.nameid
  }

  isAdmin(): boolean {
    if (this.decodedToken) return this.decodedToken.isadmin == "True"? true: false;
    return false;
  }

  sendPasswordChangeEmail(email: string) {
    return this.http.post(this.baseUrl + 'account/password/mail/' + email, {});
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


  changePassword(id: number, password: string, headers?: HttpHeaders) {
    return this.http.post(this.baseUrl + "account/password/" + id + "/" + password, {}, { headers });
  }

  isLoggedIn(): boolean {
    const token = localStorage.getItem('token');
    if (token != null)
      return !this.jwtHelper.isTokenExpired(token);
    return false;
  }
}
