import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {}
  loggedIn: boolean;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }
  
  email() {
    return localStorage.getItem('email');
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

  logout(){
    localStorage.removeItem('email');
    localStorage.removeItem('token');
  }

}
