import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Request } from '../_models/requests';

@Injectable({
  providedIn: 'root'
})
export class RequestService {

  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  makeRequest(request: Request) {
    return this.http.post(this.baseUrl + 'requests', request);
  }

  getRequests(employeeid: number) {
    return this.http.get<Request[]>(this.baseUrl + 'requests/history/' + employeeid);
  }


}
