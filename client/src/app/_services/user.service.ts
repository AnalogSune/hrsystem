import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appuser';
import { Document } from '../_models/document';
import { ScheduleEntry, ScheduleSearchDto } from '../_models/scheduleEntry';
// import { Tasks } from '../_models/tasks';

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

  getUsersByDepartment(id: number) {
    return this.http.get<AppUser[]>(this.baseUrl + 'users/department/' + id);
  }

  searchUsers(param: string) {
    return this.http.get<AppUser[]>(this.baseUrl + 'users/search/' + param);
  }

  getSchedule(searchDto:ScheduleSearchDto) {
    let params = new HttpParams();
    return this.http.post<ScheduleEntry[]>(this.baseUrl + 'calendar/get', searchDto);
  }

  uploadPhoto(file: File)
  {
    const formData: FormData = new FormData();
    formData.append('image', file);

    return this.http.post(this.baseUrl + 'users/image', formData);
  }

  uploadDocument(file: File) {
    const formData: FormData = new FormData();
    formData.append('file', file);
    return this.http.post(this.baseUrl + 'users/file', formData);
  }

  getDocuments(): Observable<Document[]> {
    return this.http.get<Document[]>(this.baseUrl + 'users/file');
  }

  deleteDocuments(fileId: number) {
    return this.http.delete(this.baseUrl + 'users/file/' + fileId);
  }

  
}
