import {  Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { AuthService } from '../_services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService) { }

  canActivate(): boolean {
    return this.authService.isLoggedIn();
  }

}

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuard implements CanActivate {

  constructor(private authService: AuthService) { }

  canActivate(): boolean {
    return this.authService.isLoggedIn() && this.authService.isAdmin();
  }

}