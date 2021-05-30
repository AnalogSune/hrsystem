import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { TaskCreationDTO } from '../_models/tasks';

@Component({
  selector: 'app-create-task',
  templateUrl: './create-task.component.html',
  styleUrls: ['./create-task.component.css']
})
export class CreateTaskComponent implements OnInit {

  constructor( public dialogRef: MatDialogRef<CreateTaskComponent>) { }

  taskCreation: TaskCreationDTO = {};

  ngOnInit() {
  }

  submit() {
    this.dialogRef.close(this.taskCreation);
  }
  
  userSelected(event) {
    this.taskCreation.employeeId = event;
  }
  
  daysFilter = (d: Date | null): boolean => {
    if (d == null) return false;
    return new Date(d.getFullYear(), d.getMonth(), d.getDate() + 1) >= (new Date());
  }

}
