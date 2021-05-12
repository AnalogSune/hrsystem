import { Component, OnInit } from '@angular/core';
import { SubTaskCreationDto, Task, TaskCreationDTO } from '../_models/tasks';
import { AlertifyService } from '../_services/alertify.service';
import { AuthService } from '../_services/auth.service';
import { TaskService } from '../_services/task.service';
import { UserService } from '../_services/user.service';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit {
  taskCreation: TaskCreationDTO = {};
  subTaskCreation: SubTaskCreationDto = {};
  tasks: Task[] = undefined;
  constructor(private userService: AuthService, private taskService: TaskService,private alertifyService: AlertifyService) { }

  ngOnInit() {
  }

  getTasks() {
    this.taskService.getTasks().subscribe(t => {
      this.tasks = t;
    }, error => {
      this.alertifyService.error('Unable to retrieve tasks!', error);
    })
  }

  isAdmin() {
    return this.userService.isAdmin();
  }

  userSelected(event) {
    this.taskCreation.employeeId = event;
  }

  submit() {
    if (!this.taskCreation.employeeId) {this.alertifyService.error('Select an employee!'); return;}
    this.taskService.createTask(this.taskCreation).subscribe(t => {
      this.alertifyService.success('Task Created!');
    }, error => {
      this.alertifyService.error('Unable to create task', error);
    })
  }

}
