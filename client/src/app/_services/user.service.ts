import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appuser';
import {ScheduleEntry, ScheduleSearchDto} from '../_models/scheduleEntry';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }

  getUser(id: number) {
    return this.http.get<AppUser>(this.baseUrl + 'users/' + id);
  }

  getUsers() {
    return this.http.get<AppUser[]>(this.baseUrl + 'users');
  }


  getSchedule(searchDto:ScheduleSearchDto) {
    let params = new HttpParams();
    return this.http.post<ScheduleEntry[]>(this.baseUrl + 'calendar/get', searchDto);
  }
}