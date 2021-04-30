import { DatePipe } from '@angular/common';
import { Inject } from '@angular/core';
import { AfterViewInit, ChangeDetectorRef, Component, ComponentRef, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { ScheduleType, ScheduleEntry, ScheduleSearchDto } from '../_models/scheduleEntry';
import { Shift } from '../_models/shift';
import { AdminService } from '../_services/admin.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';


class DateRange {
    startDate?: Date = null;
    endDate?: Date = null;
    employeeId: number;

    selectDate(date: Date, employeeId: number): void {
        if (this.startDate == null || this.endDate != null ||
            date.valueOf() < this.startDate.valueOf() ||
            this.employeeId != employeeId) {

            this.employeeId = employeeId;
            this.startDate = date;
            this.endDate = null;
        }
        else if (this.endDate == null) {
            this.endDate = date;
        }
    }

    isInRange(date: Date): boolean {
        if (!this.startDate || !this.endDate) return false;
        return date.valueOf() >= this.startDate.valueOf() && 
            date.valueOf() <= this.endDate.valueOf();
    }

    isValid(): boolean {
        return this.startDate!=null && this.endDate!=null;
    }

    clear() {
        this.startDate = null;
        this.endDate = null;
    }
}

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {
    dates: Date[];
    scheduleEntries: ScheduleEntry[];
    employees: AppUser[];
    datesSelected: DateRange = new DateRange();
    workShifts: Shift[] = [];
    workShiftId: number;
    scheduleType: ScheduleType = 0;

    calendarColors: string[] = ['green', 'red', 'yellow', 'blue', "cyan"]

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

    constructor(private userService: UserService, private datePipe: DatePipe, private authService: AuthService, private adminService: AdminService) 
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
        if (this.isAdmin())
            this.getShifts();
    }

    isAdmin() {
        return this.authService.isAdmin();
    }

    private getShifts() {
        this.adminService.getWorkShifts().subscribe(shifts => {
            this.workShifts = shifts;
            if (shifts.length > 0)
                this.workShiftId = shifts[0].id;
            else
                this.workShiftId = null;
        }, error => {
            console.log(error);
        })
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
        if (this.datesSelected.isInRange(date) && this.datesSelected.employeeId == id) 
            return this.calendarColors[4];
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
        if (this.isAdmin())
            this.datesSelected.selectDate(date, employeeId);
    }

    addCalendarEntry(create: boolean = true) {
        if (!this.datesSelected.isValid() ||
            (!this.workShiftId && this.scheduleType == ScheduleType.Working && create)) return;
        let entry: ScheduleEntry = {
            startDate: this.datesSelected.startDate,
            endDate: this.datesSelected.endDate,
            employeeId: this.datesSelected.employeeId,
            type: this.scheduleType,
            workShiftId: this.workShiftId,
            createNewEntry: create
        };
        this.adminService.addCalendarEntry(entry).subscribe(res => {
            console.log(res);
            this.updateDates()
            this.datesSelected.clear();
        }, error => {
            console.log(error);
        });
    }
}