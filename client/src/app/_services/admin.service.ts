import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appuser';
import { Department } from '../_models/department';
import { makePostDto } from '../_models/makePostDto';
import { RequestSearch, RequestStatus, Request } from '../_models/requests';

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

  changePassword(id: number, password: string) {
    return this.http.post(this.baseUrl + "account/password/" + id + "/" + password, {});
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
}
