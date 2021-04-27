import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appuser';
import { Department } from '../_models/department';
import { makePostDto } from '../_models/makePostDto';

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

updateDepartment(department: Department) {
  return this.http.put(this.baseUrl + "admin/department/" + department.id, department);
}

}
