import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  searchParam: string;
  usersfound : string[] = [""];
  usersId:  Map<string, number> = new Map();

  constructor(private userService: UserService, private router: Router,
    private alertify: AlertifyService) { }

  ngOnInit() {
  }

  search() {
    this.userService.searchUsers(this.searchParam).subscribe(users => {
      this.usersfound = [];
      if (users != undefined)
      {
        users.forEach(u => {
          if (u.fName && u.lName)
          {
            this.usersfound.push(u.fName + ' ' + u.lName);
            this.usersId[u.fName + ' ' + u.lName] = u.id;
          }
          else
          {
            this.usersfound.push(u.email);
            this.usersId[u.email] = u.id;
          }
        });
      }

    }, error => {
      this.alertify.error('Failed to perform search!', error);
    })
  }

  submit() {
    this.router.navigate(['/profile'], {queryParams: {'userId': this.usersId[this.searchParam]}});
  }

}
