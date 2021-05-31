import { Component, OnInit, ViewChild } from '@angular/core';
import { CV } from 'src/app/_models/CV';
import { AdminService } from 'src/app/_services/admin.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { AuthService } from 'src/app/_services/auth.service';
import * as FileSaver from 'file-saver';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { RecruitmentCoverletterComponent } from '../recruitment-coverletter/recruitment-coverletter.component';
import { RecruitmentAdminnotesComponent } from '../recruitment-adminnotes/recruitment-adminnotes.component';

@Component({
  selector: 'app-recruitment',
  templateUrl: './recruitment.component.html',
  styleUrls: ['./recruitment.component.css']
})
export class RecruitmentComponent implements OnInit {
  cvs: CV[] = [];
  displayedColumns: string[] = ['fullName', 'email', 'cover', 'notes', 'cvFile'];
  dataSource: MatTableDataSource<CV>;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private _authService : AuthService, private _adminService : AdminService, private _alertifyService : AlertifyService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.getCVs();
  }

  getCVs() {
    this._adminService.getCVs().subscribe(cv => {
      this.cvs = cv;
      this.dataSource = new MatTableDataSource<CV>(cv);
      this.dataSource.paginator = this.paginator;
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

  openCoverDialog(coverLetter: string) {
   this.dialog.open(RecruitmentCoverletterComponent, {data:{letter: coverLetter}});    
  }
  
  openNotesDialog(notes: string, cv: CV) {
    const ref = this.dialog.open(RecruitmentAdminnotesComponent, {data:{notes: notes}});
    ref.afterClosed().subscribe(r => {
      if (r)
      {
        cv.adminNote = r;
        this.updateCV(cv);
      }
    })
  }

}
