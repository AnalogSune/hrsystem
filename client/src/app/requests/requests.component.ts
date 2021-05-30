import { Component, OnInit, ViewChild } from '@angular/core';
import { AppUser } from '../_models/appuser';
import { Request, RequestSearch, RequestStatus } from '../_models/requests';
import { AdminService } from '../_services/admin.service';
import { AuthService } from '../_services/auth.service';
import { RequestService } from '../_services/request.service';
import { AlertifyService } from '../_services/alertify.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {Sort} from '@angular/material/sort';

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

  displayedColumns: string[] = ['startDate', 'endDate', 'type', 'status']
  dataSource: MatTableDataSource<Request>;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  constructor(private authService: AuthService, private requestService: RequestService, private adminService: AdminService,
    private alertify: AlertifyService) { }

  formModel: RequestForm = new RequestForm();  
  requestSearch: RequestSearch = {requestStatus: null, employeeId: null, requestType: null};
  users: Map<number, AppUser> = new Map<number, AppUser>();

  ngOnInit() {

    setTimeout(()=>{this.dataSource.paginator = this.paginator;}, 200);
    if (!this.isAdmin())
      this.getHistory();
    else
    {
      this.displayedColumns.unshift('employee');
      this.displayedColumns.push('buttons');
      this.search();
      this.getUsers();
    }
  }

  daysFilter = (d: Date | null): boolean => {
    if (d == null) return false;
    return new Date(d.getFullYear(), d.getMonth(), d.getDate() + 1) >= (new Date());
  }

  submit()
  {
    if (this.formModel.requestType == 0 || !this.formModel.startDay || !this.formModel.endDay)
    {
      this.alertify.error("Invalid request");
      return;
    }

    let endDate = new Date(this.formModel.endDay);
    let startDate = new Date(this.formModel.startDay);
    endDate.setMinutes(-endDate.getTimezoneOffset());
    startDate.setMinutes(-startDate.getTimezoneOffset());
    let diff =  (endDate.getFullYear() - startDate.getFullYear()) * 365 + 
                (endDate.getMonth() - startDate.getMonth()) * 30 + 
                (endDate.getDate() - startDate.getDate()) + 1;

    
    if (diff > this.authService.getCurrentUser().daysOffLeft && this.formModel.requestType == 1)
    {
      this.alertify.error("You are asking for more days than you have available!");
      return;
    }
    
    if (endDate < startDate)
    {
      this.alertify.error("Invalid dates!");
      return;
    }

    const requestDto: Request = {
      employeeId: this.authService.getUserId(),
      date: new  Date(startDate),
      endDate: new  Date(endDate),
      requestType:this.formModel.requestType,
      status: 0
    };

    console.log(requestDto);
    
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
      this.dataSource = new MatTableDataSource<Request>(res);
      this.dataSource.paginator = this.paginator;
    }, error => {
      this.alertify.error('Unable to retrieve request history!', error);
    });
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

  search() {
    this.adminService.searchRequests(this.requestSearch).subscribe(reqs => {
      this.dataSource = new MatTableDataSource<Request>(reqs);
      this.dataSource.paginator = this.paginator;
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

  sortData(sort: Sort) {
    const data = this.dataSource.data.slice();
    if (!sort.active || sort.direction === '') {
      this.dataSource.data = data;
      return;
    }

    this.dataSource.data = data.sort((a, b) => {
      const isAsc = sort.direction === 'asc';
      switch (sort.active) {
        case 'employee': return compare(this.getUserName(a.employeeId), this.getUserName(b.employeeId), isAsc);
        case 'startDate': return compare(a.date, b.date, isAsc);
        case 'endDate': return compare(a.endDate, b.endDate, isAsc);
        case 'type': return compare(a.requestType, b.requestType, isAsc);
        case 'status': return compare(a.status, b.status, isAsc);
        default: return 0;
      }
    });
    

    function compare(a: number | string | Date, b: number | string | Date, isAsc: boolean) {
      return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
    }
  }

}
