import { DatePipe } from '@angular/common';
import { Inject } from '@angular/core';
import { AfterViewInit, ChangeDetectorRef, Component, ComponentRef, Input, OnInit, ViewChild, ViewContainerRef } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { AppUser } from '../_models/appuser';
import { ScheduleType, ScheduleEntry, ScheduleSearchDto } from '../_models/scheduleEntry';
import { Shift } from '../_models/shift';
import { AdminService } from '../_services/admin.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { UserService } from '../_services/user.service';


class DateRange {
    startDate?: Date = null;
    endDate?: Date = null;
    employeeId: number;
    firstSelection: boolean = true;
    selectDate(date: Date, employeeId: number): void {
        if (this.firstSelection == true ||
            date.valueOf() < this.startDate.valueOf() ||
            this.employeeId != employeeId) {

            this.employeeId = employeeId;
            this.startDate = date;
            this.endDate = date;
            this.firstSelection = false;
        }
        else if (this.firstSelection == false) {
            this.endDate = date;
            this.firstSelection = true;
        }
    }

    isInRange(date: Date): boolean {
        if (!this.startDate || !this.endDate) return false;
        return date.valueOf() >= this.startDate.valueOf() && 
            date.valueOf() <= this.endDate.valueOf();
    }

    isStart(date: Date) {
        return date.getDate() == this.startDate?.getDate();
    }

    isEnd(date: Date) {
        return date.getDate() == this.endDate?.getDate();
    }

    isValid(): boolean {
        return this.startDate!=null && this.endDate!=null;
    }

    clear() {
        this.startDate = null;
        this.endDate = null;
        this.firstSelection = true;
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
    workShifts: Map<number, Shift> = new Map<number, Shift>();
    workShiftId: number;
    workShiftName: string= "asdasd";
    scheduleType: ScheduleType = 0;
    dataSource:  MatTableDataSource<Date>;
    calendarColors: string[] = ['workColor', 'doColor', 'wfhColor', 'sdColor', 'selColor']
    calendarColorsBg: string[] = ['workColor-bg', 'doColor-bg', 'wfhColor-bg', 'sdColor-bg', 'selColor-bg']

    @Input() view: 'week' | 'month' = 'month';

    public get _view() {
        return this.view
    }

    public set _view(value: 'week' | 'month') {
        this.view = value;
        this.updateDates();
    }

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

    constructor(private userService: UserService, private datePipe: DatePipe, private authService: AuthService, 
        private adminService: AdminService, private alertify: AlertifyService) 
    {
    }

    ngOnInit(): void {
        // this.dates = this.getDates();
        this.startDate = new Date();
        this.userService.getUsers()
        .subscribe(next => {
            this.employees = next;
        });
        this.updateDates();
        this.getShifts();
    }

    isAdmin() {
        return this.authService.isAdmin();
    }

    getShiftStr(date, id): string {
        let entry = this.getScheduleEntry(date, id);
        if (entry?.shift && (entry?.type == ScheduleType.Working || entry?.type == ScheduleType.WorkFromHome))
            return entry.shift.name + ' -> ' + this.datePipe.transform(entry.shift.startTime, 'HH:mm')
            + ' - ' + this.datePipe.transform(entry.shift.endTime, 'HH:mm');
        return '';
    }

    private getShifts() {
        this.adminService.getWorkShifts().subscribe(shifts => {
            shifts.forEach(s => {
                this.workShifts.set(s.id, s);
            }) ;
            if (shifts.length > 0)
                this.workShiftId = shifts[0].id;
            else
                this.workShiftId = null;
        }, error => {
            this.alertify.error('Unable to retrieve the work shifts!', error);
        })
    }

    private updateDates() {
        this.dates = this.getDates();
        this.dataSource = new MatTableDataSource<Date>(this.getDates());
        this.userService.getSchedule(new ScheduleSearchDto(this.startDate, this.endDate))
        .subscribe(next => {
            this.scheduleEntries = next;
        }, error => {
            this.alertify.error('Unable to retrieve the work schedule!', error)
        });
    }

    isEntryValid(): boolean {
        return ((this.scheduleType == 0 || this.scheduleType == 2) && this.workShiftId == null);
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

    getColor(date: Date, id: number): string {
        let classStr = "";
        if (this.datesSelected.isStart(date))
            classStr += "round-left "
        if (this.datesSelected.isEnd(date))
            classStr += "round-right "
        if (this.datesSelected.isInRange(date) && this.datesSelected.employeeId == id) 
            classStr += this.calendarColorsBg[4];
        else
            classStr += this.calendarColorsBg[this.getScheduleType(date, id)];
        return classStr;
    }

    getScheduleType(date: Date, id: number): ScheduleType {
        return this.getScheduleEntry(date, id)?.type;
    }

    getScheduleEntry(date: Date, id: number): ScheduleEntry {
        for (let i = 0; i < this.scheduleEntries.length; i++)
        {
            let sDate = new Date(this.scheduleEntries[i].startDate);
            let nDate = new Date(this.scheduleEntries[i].endDate);
            nDate.setDate(nDate.getDate() + 1);

            if (this.scheduleEntries[i].employeeId == id && sDate <= date &&
            nDate >= date)
            {
                return this.scheduleEntries[i];
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
            shiftId: this.workShiftId,
            createNewEntry: create
        };
        this.adminService.addCalendarEntry(entry).subscribe(res => {
            this.alertify.success('Schedule changed!');
            this.updateDates()
            this.datesSelected.clear();
        }, error => {
            this.alertify.error('Unable to add the calendar entry!', error);
        });
    }
}