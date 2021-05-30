import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-create-department',
  templateUrl: './create-department.component.html',
  styleUrls: ['./create-department.component.css']
})
export class CreateDepartmentComponent implements OnInit {
  departmentname: string = "";

  constructor( private dialogRef: MatDialogRef<CreateDepartmentComponent>) { }

  ngOnInit() {
  }

  close() {
    this.dialogRef.close(this.departmentname);
  }

}
