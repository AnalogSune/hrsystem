/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MeetingService } from './meeting.service';

describe('Service: Meeting', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MeetingService]
    });
  });

  it('should ...', inject([MeetingService], (service: MeetingService) => {
    expect(service).toBeTruthy();
  }));
});
