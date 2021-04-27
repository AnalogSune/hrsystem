import { Component, OnInit } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-company',
  templateUrl: './company.component.html',
  styleUrls: ['./company.component.css']
})
export class CompanyComponent implements OnInit {
  users: AppUser[] = undefined;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.userService.getUsers().subscribe(u => {
      this.users = u;
      console.log(u);
    }, error => {
      console.log(error);
    })
  }

}
