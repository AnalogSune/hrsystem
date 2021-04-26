import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  constructor(private userService: UserService) { }
  searchParam: string;
  usersfound : string[] = [""];

  search() {
    this.userService.searchUsers(this.searchParam).subscribe(users => {
      this.usersfound = [];
      if (users != undefined)
      {
        users.forEach(u => {
          this.usersfound.push(u.email);
        });
      }

    })
  }

  ngOnInit() {
  }

}
