import { Component, OnInit } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { Request, RequestSearch, RequestStatus } from '../_models/requests';
import { AdminService } from '../_services/admin.service';
import { AuthService } from '../_services/auth.service';
import { RequestService } from '../_services/request.service';
import { AlertifyService } from '../_services/alertify.service';

class RequestForm
{
  constructor() {}
  startDay: string = "";
  endDay: string = "";
  requestType: number = 1;
}

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {

  constructor(private authService: AuthService, private requestService: RequestService, private adminService: AdminService,
    private alertify: AlertifyService) { }
  formModel: RequestForm = new RequestForm();
  requestHistory: Request[];
  requestSearch: RequestSearch = {requestStatus: null, employeeId: null, requestType: null};
  users: Map<number, AppUser> = new Map<number, AppUser>();

  ngOnInit() {
    if (!this.isAdmin())
      this.getHistory();
    else
    {
      this.search();
      this.getUsers();
    }
  }

  submit()
  {
    let endDate = new Date(this.formModel.endDay);
    let startDate = new Date(this.formModel.startDay);

    let diff =  (endDate.getFullYear() - startDate.getFullYear()) * 365 + 
                (endDate.getMonth() - startDate.getMonth()) * 30 + 
                (endDate.getDate() - startDate.getDate()) + 1;
    
    if (diff > this.authService.currentUser.daysOffLeft && this.formModel.requestType == 1)
    {
      this.alertify.error("You are asking for more days than you have available!");
      return;
    }

    if (this.formModel.requestType == 0 || !this.formModel.startDay || !this.formModel.endDay)
    {
      this.alertify.error("Invalid request");
      return;
    }

    const requestDto: Request = {
      employeeId: this.authService.currentUser.id,
      date: new Date(this.formModel.startDay),
      endDate: new Date(this.formModel.endDay),
      requestType:this.formModel.requestType,
      status: 0
    };
    
    this.requestService.makeRequest(requestDto).subscribe(next => {
      this.alertify.success('Request made successfully!');
      this.getHistory();
    }, error => {
      this.alertify.error('Unable make request!', error);
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
        return "Declined";
    }
  }

  getHistory()
  {
    this.requestService.getRequests(this.authService.decodedToken.nameid).subscribe(res => {
      this.requestHistory = res;
    }, error => {
      this.alertify.error('Unable to retrieve request history!', error);
    });
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

  search() {
    this.adminService.searchRequests(this.requestSearch).subscribe(reqs => {
      this.requestHistory = reqs;
    }, error => {
      this.alertify.error('Unable to perform search!', error);
    });
  }

  getUsers() {
    this.adminService.getUsersWithPending().subscribe(users => {
      users.forEach(u => {
        this.users.set(u.id, u);
      })
      console.log(users);
    }, error => {
      this.alertify.error('Unable to retrieve users!', error);
    })
  }

  getUserName(id): string {
    if (this.users.get(id) != undefined)
    return this.users.get(id).fName + ' ' + this.users.get(id).lName;
  }

  changeStatus(id: number, status: RequestStatus) {
    this.adminService.updateRequest(id, status).subscribe(res => {
      this.alertify.success('Status Changed!');
      this.search();
    }, error => {
      this.alertify.error('Unable to change status!', error);
    })
  }

}
