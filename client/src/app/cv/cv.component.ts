import { Component, OnInit } from '@angular/core';
import { CV } from '../_models/CV';
import { AdminService } from '../_services/admin.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-cv',
  templateUrl: './cv.component.html',
  styleUrls: ['./cv.component.css']
})
export class CvComponent implements OnInit {
  cvModel: CV = {};

  constructor(private adminService: AdminService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  uploadCV(event) {
    this.cvModel.cvFile = event.target.files[0];
  }

  submit() {
    this.adminService.uploadCV(this.cvModel).subscribe(res => {
      this.alertify.success('CV uploaded');
    }, error => {
      this.alertify.error('Failed to upload cv', error);
    });
  }
}
