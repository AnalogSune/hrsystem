import { DatePipe } from '@angular/common';
import { stringify } from '@angular/compiler/src/util';
import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { Shift } from 'src/app/_models/shift';

@Component({
  selector: 'app-create-shift',
  templateUrl: './create-shift.component.html',
  styleUrls: ['./create-shift.component.css']
})
export class CreateShiftComponent implements OnInit {
  newShift: Shift = {}

  

  shiftFormGroup = new FormGroup({
    durationControl: new FormControl(1, [Validators.required, Validators.min(1), Validators.max(23), this.decimalValidation]),
    name: new FormControl(0, [Validators.required]),
    startTime: new FormControl(0, [Validators.required])
  });

  decimalValidation(control: AbstractControl): ValidationErrors | null {
    const str: string = control?.value?.toString();
    if (!str) return;
    if (str.includes(".") || str.includes(",")) 
      return {invalidNumber: "Can't be a decimal number!"}
    return null;
  }

  
  public set shiftTime(v : string) {
    if (!v) return;
    this.newShift.startTime = new Date();
    let timez: string[] = v.split(":");
    this.newShift.startTime.setHours(Number.parseInt(timez[0]), Number.parseInt(timez[1]));
  }
  
  
  constructor( private dialogRef: MatDialogRef<CreateShiftComponent>) { }

  ngOnInit() {
  }

  submit() {
    this.dialogRef.close(this.newShift);
  }

}
