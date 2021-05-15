import { Component, ComponentRef, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
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
  meetingCreationModel: Meeting = {};

  departments : Department[] = undefined;

  constructor(private authService: AuthService, private alertifyService: AlertifyService,
    private meetingService: MeetingService, private adminService: AdminService) { }
    
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

  createMeeting() {
    this.meetingService.createMeeting(this.meetingCreationModel).subscribe(r => {
      this.meetingsTable.update();
      this.trainingTable.update();
      this.alertifyService.success("Meeting Created");
    }, error => {
      this.alertifyService.error("Unable to create meeting!", error);
    })
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

}
