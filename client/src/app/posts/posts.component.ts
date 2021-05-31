import { Component, OnInit } from '@angular/core';
import { AdminService } from '../_services/admin.service';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  postContent: string;
  postsMade: any;
  constructor(private adminService: AdminService, private authService: AuthService,
    private alertify: AlertifyService) { }

  ngOnInit() {
    this.getPosts();
  }

  submitPost() {
    if (this.postContent)
    this.adminService.makePost({publisherid: this.authService.getCurrentUser().id, content: this.postContent}).subscribe(next => {
      this.getPosts();
      this.postContent = ""
      this.alertify.success("New post was added");
    }, error => {
      this.alertify.error('Unable to post message!', error);
    })
  }

  getPosts() {
    this.adminService.getPosts().subscribe(next => {
      this.postsMade = next;
    }, error => {
      this.alertify.error('Unable to retrieve posts!', error);
    });
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

}
