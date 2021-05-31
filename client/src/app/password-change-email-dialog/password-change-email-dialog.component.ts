import { Component, OnInit } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-password-change-email-dialog',
  templateUrl: './password-change-email-dialog.component.html',
  styleUrls: ['./password-change-email-dialog.component.css']
})
export class PasswordChangeEmailDialogComponent implements OnInit {
  email: string = "";
  constructor(private dialogRef: MatDialogRef<PasswordChangeEmailDialogComponent>,
    private authService: AuthService, private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  send() {
    if (this.email)
    this.authService.sendPasswordChangeEmail(this.email).subscribe(r => {
      if (r)
        this.alertifyService.success("Email was sent successfully!");
      else
        this.alertifyService.error("Wrong email!");
    }, error => {
      this.alertifyService.error("Unable to send email!", error);
    })
    this.dialogRef.close();
  }

}
