import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Document } from '../_models/document';
import { Clipboard } from '@angular/cdk/clipboard';
import * as FileSaver from 'file-saver';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.css']
})
export class DocumentsComponent implements OnInit {

  documents: Document[] = [];

  constructor(private userService: UserService, private clipboard: Clipboard,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.fetchDocuments();
  }

  fetchDocuments() {
    this.userService.getDocuments().subscribe(docs => {
      this.documents = docs;
    }, error => {
      this.alertify.error('Unable to retrieve document list!', error);
    });
  }

  copyToCp(txt: string) {
    if (!this.clipboard.copy(txt))
      this.alertify.error('Failed to copy!');
    else 
      this.alertify.success('Copied to clipboard');
  }

  uploadDocument(evt) {
    const file:File = evt.target.files[0];
    this.userService.uploadDocument(file).subscribe(res => {
      this.alertify.success('Document uploaded!');
      this.fetchDocuments();
    }, error => {
      this.alertify.error('Unable to upload document!', error);
    });
  }

  deleteDocument(id: number) {
    this.userService.deleteDocuments(id).subscribe(res => {
      this.alertify.success('Document deleted!');
      this.fetchDocuments();
    }, error => {
      this.alertify.error('Unable to delete document!', error);
    });
  }

  downloadFile(url: string, name: string) {
    FileSaver.saveAs(url, name);
  }

}
