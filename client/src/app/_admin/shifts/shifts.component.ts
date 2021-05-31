import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Shift } from 'src/app/_models/shift';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { CreateShiftComponent } from '../create-shift/create-shift.component';

@Component({
  selector: 'app-shifts',
  templateUrl: './shifts.component.html',
  styleUrls: ['./shifts.component.css']
})
export class ShiftsComponent implements OnInit {
  shifts: Shift[];

  constructor(private adminService: AdminService, private alertify: AlertifyService, private dialog: MatDialog) { }

  ngOnInit() {
    this.getShifts();
  }

  createShift(shift: Shift) {
    if (!shift.startTime || !shift.name || !shift.duration) return;
    shift.startTime.setMinutes(shift.startTime.getMinutes() - shift.startTime.getTimezoneOffset());
    this.adminService.createWorkShift(shift).subscribe(shift => {
      this.alertify.success('Shift created!');
      this.getShifts();
    }, error => {
      this.alertify.error('Unable to create shift!', error);
    })
  }

  getShifts() {
    this.adminService.getWorkShifts().subscribe(shifts => {
      this.shifts = shifts;
    }, error => {
      this.alertify.error('Unable to retrieve shifts!', error);
    })
  }

  deleteShift(id: number) {
    this.adminService.deleteWorkShift(id).subscribe(res => {
      this.getShifts();
      this.alertify.success('Shift deleted!');
    }, error => {
      this.alertify.error('Unable to delete shift!', error);
    })
  }

  openDialog() {
    const ref = this.dialog.open(CreateShiftComponent);
    ref.afterClosed().subscribe(r => {
      if (r)
        this.createShift(r);
    });
  }

}
