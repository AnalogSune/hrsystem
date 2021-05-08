import { Component, OnInit } from '@angular/core';
import { CV } from 'src/app/_models/CV';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import * as FileSaver from 'file-saver';

@Component({
  selector: 'app-recruitment',
  templateUrl: './recruitment.component.html',
  styleUrls: ['./recruitment.component.css']
})
export class RecruitmentComponent implements OnInit {
  cvs: CV[] = [];

  constructor(private _authService : AuthService, private _adminService : AdminService, private _alertifyService : AlertifyService) { }

  ngOnInit() {
    this.getCVs();
  }

  getCVs() {
    this._adminService.getCVs().subscribe(cv => {
      this.cvs = cv;
    }, error => {
      this._alertifyService.error('Unable to retrieve users!', error);
    })
  }

  downloadCV(url) {
    FileSaver.saveAs(url);
  }

  updateCV(cv: CV) {
    this._adminService.updateCV(cv.id, cv.adminNote).subscribe(next => {
      this._alertifyService.success('CV Updated');
    }, error => {
      this._alertifyService.error('Unable to update!', error);
    });
  }

  deleteCV(id) {
    this._adminService.deleteCV(id).subscribe(next => {
      this._alertifyService.success('CV Deleted');
      this.getCVs();
    }, error => {
      this._alertifyService.error('Unable to delete CV', error);
    })
  }

}
