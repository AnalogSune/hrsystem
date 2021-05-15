import { AfterViewInit, Component, Input, OnInit } from '@angular/core';
import { Meeting, MeetingSearchDto } from '../_models/meetings';
import { AdminService } from '../_services/admin.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { MeetingService } from '../_services/meeting.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { ViewChild } from '@angular/core';

@Component({
  selector: 'app-meetings-table',
  templateUrl: './meetings-table.component.html',
  styleUrls: ['./meetings-table.component.css']
})

export class MeetingsTableComponent implements OnInit {

  dataSource: MatTableDataSource<Meeting>;
  displayedColumns: string[] = ['description', 'date', 'duration', 'meetingLink']
  @Input() meetingType: number = 0;

  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private authService: AuthService, private alertifyService: AlertifyService,
    private meetingService: MeetingService, private adminService: AdminService) { }

  ngOnInit() {
    setTimeout(()=>{this.dataSource.paginator = this.paginator;}, 200);
    this.update();
  }

  update() {
    let search: MeetingSearchDto = {meetingType: this.meetingType};
    if (!this.authService.isAdmin()) {
      search.departmentId = Number.parseInt(localStorage.getItem('departmentId'));
    }
    this.meetingService.getMeetings(search).subscribe(r => {
      this.dataSource = new MatTableDataSource<Meeting>(r);
    }, error => {
      this.alertifyService.error("unable to retrieve meetings", error);
    })
  }

}