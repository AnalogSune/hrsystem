import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appuser';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) { }


getUser(id: number) {
  return this.http.get<AppUser>(this.baseUrl + 'users/' + id);
}
}
