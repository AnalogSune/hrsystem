import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { CalendarComponent } from '../calendar/calendar.component';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {

  @ViewChild("calendar") calendar: CalendarComponent;

  constructor(private authService: AuthService) { }

  isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  ngAfterViewInit(): void {
    if (this.isLoggedIn())
    {
    if (!this.authService.isAdmin())
      this.calendar.maxHeight = "60vh";
    else
      this.calendar.maxHeight="45vh";
    }
  }

  ngOnInit() {
  }

}
