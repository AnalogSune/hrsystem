import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { AppUser } from '../_models/appuser';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
import {NgbTypeahead} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-search-bar',
  templateUrl: './search-bar.component.html',
  styleUrls: ['./search-bar.component.css']
})
export class SearchBarComponent implements OnInit {

  private _searchParam: string;
  usersfound : string[] = [""];
  usersId:  Map<string, number> = new Map();
  
  
  public get searchParam(): string {
    return this._searchParam;
  }
  public set searchParam(value: string) {
    this._searchParam = value;
    if (this.usersId[value])
      this.submit();
  }

  @Output() onSubmit: EventEmitter<number> = new EventEmitter();
  @Input() animate: boolean = true;

  constructor(private userService: UserService, private router: Router,
    private alertify: AlertifyService) { }

  ngOnInit() {
  }

  animateClass(): object {
    if (this.animate === true) return {}
    return {'width': '296px'}
  }

  filter(str: string) {
    const RexStr = /\<|\>|\"|\'|\&/g;
    str = (str + '').replace(RexStr,
            function(MatchStr){
                switch(MatchStr){
                    case "<":
                        return "&lt;";
                        break;
                    case ">":
                        return "&gt;";
                        break;
                    case "\"":
                        return "&quot;";
                        break;
                    case "'":
                        return "&#39;";
                        break;
                    case "&":
                        return "&amp;";
                        break;
                    case "/":
                        return "%2F";
                    default :
                        break;
                }
            }
        );
        return str;
  }

  search(event: KeyboardEvent) {
    if(event.key == "Enter") return;
    this.userService.searchUsers(this.filter(this._searchParam)).subscribe(users => {
      this.usersfound = [];
      if (users != undefined)
      {
        users.forEach(u => {
          if (u.fName && u.lName)
          {
            let nameToShow = u.fName + ' ' + u.lName + ' (' + u.email + ')';
            this.usersfound.push(nameToShow);
            this.usersId[nameToShow] = u.id;
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
    this.onSubmit.emit(this.usersId[this._searchParam]);
  }

}
