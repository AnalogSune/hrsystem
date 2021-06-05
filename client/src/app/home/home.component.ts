import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { CalendarComponent } from '../calendar/calendar.component';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private authService: AuthService) { }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  public get calendarHeight() : string {
    if (!this.authService.isAdmin())
       return "60vh";
      else
       return "45vh";
  }

  ngOnInit() {
  }

}
