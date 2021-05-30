import { Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Department } from '../_models/department';
import { Meeting } from '../_models/meetings';

@Component({
  selector: 'app-create-meeting',
  templateUrl: './create-meeting.component.html',
  styleUrls: ['./create-meeting.component.css']
})
export class CreateMeetingComponent implements OnInit {
  meetingCreationModel: Meeting = {};
  departments: Department[] = [];

  constructor( private dialogRef: MatDialogRef<CreateMeetingComponent>,
    @Inject(MAT_DIALOG_DATA) private data: Department[]) {
      this.departments = data;
     }

  ngOnInit() {
  }

  createMeeting() {
    this.dialogRef.close(this.meetingCreationModel);
  }

}
