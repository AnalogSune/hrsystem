import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth.service';


@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {

  constructor(private authorizeService : AuthService) { }

  ngOnInit() {
  }

  isAdmin() : boolean {
    return this.authorizeService.isAdmin();
  }

}
