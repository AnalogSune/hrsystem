import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
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

}
