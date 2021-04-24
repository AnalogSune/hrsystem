import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { CalendarType, ScheduleEntry, ScheduleSearchDto } from '../_models/scheduleEntry';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {
    dates: Date[];
    startDate: Date = new Date();
    scheduleEntries: ScheduleEntry[];
    employees: AppUser[];

    constructor(private userService: UserService) {}

    ngOnInit(): void {
        this.dates = this.getDates();

        this.userService.getUsers()
        .subscribe(next => {
            this.employees = next;
        })
        this.updateDates();
    }

    private updateDates() {
        this.dates = this.getDates();
        let enddate = new Date(this.startDate);
        enddate.setMonth(enddate.getMonth() + 1);
        let v = new ScheduleSearchDto(this.startDate, enddate);
        this.userService.getSchedule(v)
        .subscribe(next => {
            this.scheduleEntries = next;
            console.log(next);
        });
    }

    nextMonth() {
        this.startDate.setMonth(this.startDate.getMonth() + 1);
        this.updateDates();
    }

    prevMonth() {
        this.startDate.setMonth(this.startDate.getMonth() - 1);
        this.updateDates();
    }

    getDates(): Array<Date> {
        let dates = new Array<Date>();
        
        let now: Date = new Date(this.startDate);
        for (let i = 0; i < 30; i++)
        {
            dates.push(new Date(now));
            now.setDate(now.getDate() + 1);
        }
        return dates;
    }

    getColor(date: Date, id: number) {
        for (let i = 0; i < this.scheduleEntries.length; i++)
        {
            if (this.scheduleEntries[i].employeeId == id && new Date(this.scheduleEntries[i].startDate) <= date &&
            new Date(this.scheduleEntries[i].endDate) >= date)
            {
                switch(this.scheduleEntries[i].type)
                {
                case CalendarType.DayOff:
                    return 'red';
                case CalendarType.SickDay:
                    return 'yellow';
                case CalendarType.WorkFromHome:
                    return 'brown';
                case CalendarType.Working:
                    return 'green';
                default:
                    return 'white';
                }
            }
        }
    }
    
}