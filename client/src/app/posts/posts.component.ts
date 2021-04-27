import { Component, OnInit } from '@angular/core';
import { AdminService } from '../_services/admin.service';
import { AuthService } from '../_services/auth.service';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.css']
})
export class PostsComponent implements OnInit {
  postContent: string;
  postsMade: any;
  constructor(private adminService: AdminService, private authService: AuthService ) { }

  ngOnInit() {
    this.getPosts();
  }

  submitPost() {
    this.adminService.makePost({publisherid: this.authService.currentUser.id, content: this.postContent}).subscribe(next => {
      this.getPosts();
    })
  }

  getPosts() {
    this.adminService.getPosts().subscribe(next => {
      this.postsMade = next;
    });
  }

  isAdmin(): boolean {
    return this.authService.isAdmin();
  }

}
