import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Shift } from 'src/app/_models/shift';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';

@Component({
  selector: 'app-shifts',
  templateUrl: './shifts.component.html',
  styleUrls: ['./shifts.component.css']
})
export class ShiftsComponent implements OnInit {
  newShift: Shift = {};
  shifts: Shift[];

  constructor(private adminService: AdminService, private alertify: AlertifyService) { }

  ngOnInit() {
    this.getShifts();
  }

  submit() {
    this.newShift.startTime.setMinutes(this.newShift.startTime.getMinutes() - this.newShift.startTime.getTimezoneOffset());
    this.adminService.createWorkShift(this.newShift).subscribe(shift => {
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

}
