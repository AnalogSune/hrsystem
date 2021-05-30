import { Component, ComponentRef, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { CreateMeetingComponent } from '../create-meeting/create-meeting.component';
import { MeetingsTableComponent } from '../meetings-table/meetings-table.component';
import { Department } from '../_models/department';
import { Meeting } from '../_models/meetings';
import { AdminService } from '../_services/admin.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { MeetingService } from '../_services/meeting.service';

@Component({
  selector: 'app-meetings',
  templateUrl: './meetings.component.html',
  styleUrls: ['./meetings.component.css']
})
export class MeetingsComponent implements OnInit {

  departments : Department[] = undefined;

  constructor(private authService: AuthService, private alertifyService: AlertifyService,
    private meetingService: MeetingService, private adminService: AdminService, private dialog: MatDialog) { }
    
  @ViewChild("meetingsTable") meetingsTable: MeetingsTableComponent;
  @ViewChild("trainingTable") trainingTable: MeetingsTableComponent;
  @ViewChild( MatPaginator ) paginator: MatPaginator;

  checkDay = (d: Date | null): boolean => {
    let now = new Date();
    now.setDate(new Date().getDate() - 1);
    return d >= now;
  }

  ngOnInit() {
    this.getDepartments();
  }

  getDepartments() {
    this.adminService.getDepartments().subscribe(r => {
      this.departments = r;
    }, error => {
      this.alertifyService.error('Unable to retrive departments!', error);
    })
  }

  createMeeting(meeting: Meeting) {
    this.meetingService.createMeeting(meeting).subscribe(r  => {
      if (r.meetingType == 0)
        this.trainingTable.addNewMeet(r);
      else
        this.meetingsTable.addNewMeet(r);
      this.alertifyService.success("Meeting Created");
    }, error => {
      this.alertifyService.error("Unable to create meeting!", error);
    })
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

  openDialog(){
      const ref = this.dialog.open(CreateMeetingComponent, {width: 'auto', data: this.departments});
      ref.afterClosed().subscribe(r => {
        if (r)
          this.createMeeting(r);
      });
  }

}
