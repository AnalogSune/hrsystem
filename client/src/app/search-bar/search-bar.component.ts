import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  constructor(private userService: UserService, private router: Router) { }
  searchParam: string;
  usersfound : string[] = [""];
  usersId:  Map<string, number> = new Map();

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

    })
  }

  submit() {
    this.router.navigate(['/profile'], {queryParams: {'userId': this.usersId[this.searchParam]}});
  }

}
