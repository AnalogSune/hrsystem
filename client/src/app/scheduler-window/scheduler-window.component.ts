import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-scheduler-window',
  templateUrl: './scheduler-window.component.html',
  styleUrls: ['./scheduler-window.component.css']
})
export class SchedulerWindowComponent implements OnInit {

  constructor() { }

  @Input() isVisible: boolean = true;

  startDate: Date = new Date();
  endDate: Date = new Date();

  ngOnInit() {
  }

  activate(date: Date) {
    this.isVisible = true;
    this.startDate = date;
    if (this.endDate ==undefined || this.startDate > this.endDate)
      this.endDate = date;
  }

}
