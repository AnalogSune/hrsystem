import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appuser';
import { CV } from '../_models/CV';
import { Department } from '../_models/department';
import { makePostDto } from '../_models/makePostDto';
import { RequestSearch, RequestStatus, Request } from '../_models/requests';
import { ScheduleEntry } from '../_models/scheduleEntry';
import { Shift } from '../_models/shift';
import { TaskCreationDTO } from '../_models/tasks';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.baseUrl;
  constructor(private http: HttpClient) { }

  makePost(post: makePostDto) {
    return this.http.post(this.baseUrl + 'admin/dashboard', post);
  }

  getPosts() {
    return this.http.get(this.baseUrl + 'admin/dashboard');
  }

  register(user: AppUser) {
    return this.http.post(this.baseUrl + 'account/register', user);
  }

  createDepartment(department: Department) {
    return this.http.post(this.baseUrl + 'admin/department', department);
  }

  getDepartments(): Observable<Department[]> {
    return this.http.get<Department[]>(this.baseUrl + 'admin/departments');
  }

  removeDepartment(id: number) {
    return this.http.delete(this.baseUrl + "admin/department/" + id);
  }

  addRole(id: number, rolename: string) {
    return this.http.post(this.baseUrl + "admin/role/" + id +'/' + rolename, {});
  }

  removeRole(id: number) {
    return this.http.delete(this.baseUrl + "admin/role/" + id);
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + "admin/users/" + id);
  }

  updateRequest(id: number, status: RequestStatus) {
    return this.http.post(this.baseUrl + "requests/" + id + "/" + status, {});
  }

  searchRequests(requestSearch: RequestSearch): Observable<Request[]> {
    return this.http.post<Request[]>(this.baseUrl + "requests/search", requestSearch);
  }

  getUsersWithPending(): Observable<AppUser[]> {
    return this.http.get<AppUser[]>(this.baseUrl + 'users/pending');
  }

  changeUserDepartment(userId: number, departmentId: number) {
    return this.http.put(this.baseUrl + 'users/department/' + userId  + '/' + departmentId, {});
  }

  changeUserRole(userId: number, roleId: number) {
    return this.http.put(this.baseUrl + 'users/role/' + userId  + '/' + roleId, {});
  }

  createWorkShift(shift: Shift) {
    return this.http.post(this.baseUrl + 'admin/shift', shift);
  }

  getWorkShifts(): Observable<Shift[]> {
    return this.http.get<Shift[]>(this.baseUrl + 'admin/shift');
  }

  deleteWorkShift(id: number) {
    return this.http.delete(this.baseUrl + 'admin/shift/' + id);
  }

  addCalendarEntry(calendarEntry: ScheduleEntry) {
    return this.http.post(this.baseUrl + 'calendar', calendarEntry);
  }

  getCVs() : Observable<CV[]> {
    return this.http.get<CV[]>(this.baseUrl + 'cv');
  }

  updateCV(id, notes) {
    return this.http.put(this.baseUrl + 'cv', {id: id, adminNotes: notes});
  }

  deleteCV(id) {
    return this.http.delete(this.baseUrl + 'cv/' + id);
  }

  uploadCV(cv: CV) {
    let formData = new FormData();
    formData.append('fName', cv.fName);
    formData.append('lName', cv.lName);
    formData.append('email', cv.email);
    formData.append('coverLetter', cv.coverLetter);
    formData.append('cvFile', cv.cvFile);
    return this.http.post(this.baseUrl + 'cv', formData);
  }
}
