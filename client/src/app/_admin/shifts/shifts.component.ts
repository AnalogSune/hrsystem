import { Time } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Shift } from 'src/app/_models/shift';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-shifts',
  templateUrl: './shifts.component.html',
  styleUrls: ['./shifts.component.css']
})
export class ShiftsComponent implements OnInit {
  newShift: Shift = {};
  shifts: Shift[];

  constructor(private adminService: AdminService) { }

  ngOnInit() {
    this.getShifts();
  }

  submit() {
    this.newShift.startTime.setMinutes(this.newShift.startTime.getMinutes() - this.newShift.startTime.getTimezoneOffset());
    this.adminService.createWorkShift(this.newShift).subscribe(shift => {
      console.log(shift);
      this.getShifts();
    }, error => {
      console.log(error);
    })
  }

  getShifts() {
    this.adminService.getWorkShifts().subscribe(shifts => {
      console.log(shifts);
      this.shifts = shifts;
    }, error => {
      console.log(error);
    })
  }

  deleteShift(id: number) {
    this.adminService.deleteWorkShift(id).subscribe(res => {
      console.log(res);
      if (res == true)
        this.getShifts();
    }, error => {
      console.log(error);
    })
  }

}
