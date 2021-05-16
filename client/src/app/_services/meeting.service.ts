import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Meeting, MeetingSearchDto } from '../_models/meetings';

@Injectable({
  providedIn: 'root'
})
export class MeetingService {
  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) {

  }

  createMeeting(meeting: Meeting) {
    return this.http.post<Meeting>(this.baseUrl + 'meeting', meeting);
  }

  getMeetings(meetingSearch: MeetingSearchDto) {
    return this.http.post<Meeting[]>(this.baseUrl + 'meeting/search', meetingSearch);
  }

}
