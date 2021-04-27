import { DatePipe } from '@angular/common';
import { Inject } from '@angular/core';
import { AfterViewInit, ChangeDetectorRef, Component, ComponentRef, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { ScheduleType, ScheduleEntry, ScheduleSearchDto } from '../_models/scheduleEntry';
import { UserService } from '../_services/user.service';


@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {
    dates: Date[];
    scheduleEntries: ScheduleEntry[];
    employees: AppUser[];

    calendarColors: string[] = ['green', 'red', 'yellow', 'blue']

    @Input() view: 'week' | 'month' = 'week';
    @Input() startDate: Date = new Date();
    @Input() showViews: boolean = true;

    public get numDays(): number {
        return (this.view == 'week'? 7 : 30);
    }

    public get endDate(): Date {
        let endDate = new Date(this.startDate);
        if (this.view == 'week')
            endDate.setDate(this.startDate.getDate() + 7);
        else
            endDate.setMonth(this.startDate.getMonth() + 1);
        endDate.setDate(endDate.getDate() - 1);
        return endDate;
    }

    public get dateRangeStr(): string {
        
        return this.datePipe.transform(this.startDate, 'dd-MMMM-y') + ' - ' + this.datePipe.transform(this.endDate, 'dd-MMMM-y');
    }

    constructor(private userService: UserService, private datePipe: DatePipe) 
    {
    }

    ngOnInit(): void {
        this.dates = this.getDates();
        this.startDate = new Date();
        this.userService.getUsers()
        .subscribe(next => {
            this.employees = next;
        });
        this.updateDates();
    }

    private updateDates() {
        this.dates = this.getDates();
        this.userService.getSchedule(new ScheduleSearchDto(this.startDate, this.endDate))
        .subscribe(next => {
            this.scheduleEntries = next;
        });
    }

    nextMonth() {
        if (this.view == 'week')
            this.startDate.setDate(this.startDate.getDate() + 7);
        else
            this.startDate.setMonth(this.startDate.getMonth() + 1);
        this.updateDates();
    }

    prevMonth() {
        if (this.view == 'week')
            this.startDate.setDate(this.startDate.getDate() - 7);
        else
            this.startDate.setMonth(this.startDate.getMonth() - 1);
        this.updateDates();
    }

    getDates(): Array<Date> {
        let dates = new Array<Date>();
        
        let now: Date = new Date(this.startDate);
        for (let i = 0; i < this.numDays; i++)
        {
            dates.push(new Date(now));
            now.setDate(now.getDate() + 1);
        }
        return dates;
    }

    changeView(view: 'week' | 'month')
    {
        this.view = view;
        this.updateDates();
    }

    getColor(date: Date, id: number): string {
        return this.calendarColors[this.getScheduleType(date, id)];
    }

    getScheduleType(date: Date, id: number): ScheduleType {
        for (let i = 0; i < this.scheduleEntries.length; i++)
        {
            let sDate = new Date(this.scheduleEntries[i].startDate);
            let nDate = new Date(this.scheduleEntries[i].endDate);
            nDate.setDate(nDate.getDate() + 1);

            if (this.scheduleEntries[i].employeeId == id && sDate <= date &&
            nDate >= date)
            {
                return this.scheduleEntries[i].type;
            }
        }
    }
    
    dateClicked(date: Date, employeeId: number) {
        console.log(date + ', id:' + employeeId);
    }
}