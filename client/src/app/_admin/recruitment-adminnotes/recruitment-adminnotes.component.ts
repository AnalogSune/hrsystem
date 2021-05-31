import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-recruitment-adminnotes',
  templateUrl: './recruitment-adminnotes.component.html',
  styleUrls: ['./recruitment-adminnotes.component.css']
})
export class RecruitmentAdminnotesComponent implements OnInit {

  constructor( private dialogRef: MatDialogRef<RecruitmentAdminnotesComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {notes: string}) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(this.data.notes);
  }

}
