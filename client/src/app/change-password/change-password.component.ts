import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  password: string = "";
  confirmPassword: string = "";
  constructor(private dialogRef: MatDialogRef<ChangePasswordComponent>, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  close() {
    if (this.password === this.confirmPassword)
      this.dialogRef.close(this.password);
    else
      this.alertify.error("Passwords don't match!")
  }

}
