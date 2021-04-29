import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { Document } from '../_models/document';
import { Clipboard } from '@angular/cdk/clipboard';
import * as FileSaver from 'file-saver';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.css']
})
export class DocumentsComponent implements OnInit {

  documents: Document[] = [];

  constructor(private userService: UserService, private clipboard: Clipboard) { }

  ngOnInit() {
    this.fetchDocuments();
  }

  fetchDocuments() {
    this.userService.getDocuments().subscribe(docs => {
      this.documents = docs;
    }, error => {
      console.log(error);
    });
  }

  copyToCp(txt: string) {
    console.log(txt);
    console.log(this.clipboard.copy(txt));
  }

  uploadDocument(evt) {
    const file:File = evt.target.files[0];
    this.userService.uploadDocument(file).subscribe(res => {
      console.log(res);
      this.fetchDocuments();
    }, error => {
      console.log(error);
    });
  }

  deleteDocument(id: number) {
    this.userService.deleteDocuments(id).subscribe(res => {
      console.log(res);
      this.fetchDocuments();
    }, error => {
      console.log(error);
    });
  }

  downloadFile(url: string, name: string) {
    FileSaver.saveAs(url, name);
  }

}
