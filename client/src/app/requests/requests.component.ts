import { Component, OnInit } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { Request, RequestSearch, RequestStatus } from '../_models/requests';
import { AdminService } from '../_services/admin.service';
import { AuthService } from '../_services/auth.service';
import { RequestService } from '../_services/request.service';

class RequestForm
{
  constructor() {}
  startDay: string = "";
  endDay: string = "";
  requestType: number;
}

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {

  constructor(private authService: AuthService, private requestService: RequestService, private adminService: AdminService) { }
  formModel: RequestForm = new RequestForm();
  requestHistory: Request[];
  requestSearch: RequestSearch = {requestStatus: 0, employeeId: null, requestType: null};
  users: AppUser[] = [];

  ngOnInit() {
    this.getHistory();
    this.getUsers();
  }

  submit()
  {
    let endDate = new Date(this.formModel.endDay);
    let startDate = new Date(this.formModel.startDay);

    let diff =  (endDate.getFullYear() - startDate.getFullYear()) * 365 + 
                (endDate.getMonth() - startDate.getMonth()) * 30 + 
                (endDate.getDate() - startDate.getDate()) + 1;

    const requestDto: Request = {
      employeeId: this.authService.currentUser.id,
      date: new Date(this.formModel.startDay),
      endDate: new Date(this.formModel.endDay),
      requestType:this.formModel.requestType,
      status: 0
    };
    
    this.requestService.makeRequest(requestDto).subscribe(next => {
      this.getHistory();
    }, error => {
      console.log(error);
    });
  }

  requestType(type : number) : string{
    switch(type){
      case 1:
        return "Day Off"
      case 2:
        return "Work From Home"
      case 3:
        return "Sick Day"
        
      default: return "Error"
    }
  }

  status(status: number): string {
    switch(status) {
      case 0:
        return "Pending";
      case 1:
        return "Accepted";
      default:
        return "decliened";
    }
  }

  getHistory()
  {
    this.requestService.getRequests(this.authService.decodedToken.nameid).subscribe(res => {
      this.requestHistory = res;
    });
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

  search() {
    
    console.log(this.requestSearch);
    this.adminService.searchRequests(this.requestSearch).subscribe(reqs => {
      this.requestHistory = reqs;
      console.log(reqs);
    }, error => {
      console.log(error);
    });
  }

  getUsers() {
    this.adminService.getUsersWithPending().subscribe(users => {
      console.log(users);
      this.users = users;
    })
  }

  changeStatus(id: number, status: RequestStatus) {
    this.adminService.updateRequest(id, status).subscribe(res => {
      console.log(res);
      this.search();
    }, error => {
      console.log(error);
    })
  }

}