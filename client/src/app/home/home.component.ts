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

  ngAfterViewInit(): void {
    if (!this.authService.isAdmin())
      this.calendar.maxHeight = "60vh";
    else
      this.calendar.maxHeight="45vh";
  }

  ngOnInit() {
  }

}
