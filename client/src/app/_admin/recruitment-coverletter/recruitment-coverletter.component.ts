import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-recruitment-coverletter',
  templateUrl: './recruitment-coverletter.component.html',
  styleUrls: ['./recruitment-coverletter.component.css']
})
export class RecruitmentCoverletterComponent implements OnInit {

  constructor( private dialogRef: MatDialogRef<RecruitmentCoverletterComponent>,
    @Inject(MAT_DIALOG_DATA) public data: {letter: string}) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close();
  }

}
