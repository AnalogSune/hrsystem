import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { config } from 'rxjs';
import { CreateTaskComponent } from '../create-task/create-task.component';
import { SubTask, SubTaskCreationDto, Task, TaskCreationDTO, TaskSearchDto } from '../_models/tasks';
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
  subTaskCreation: SubTaskCreationDto = {};
  tasks: Task[] = undefined;
  searchDto: TaskSearchDto = {status:null, isOverdue:null};

  constructor(private authService: AuthService, private taskService: TaskService,private alertifyService: AlertifyService, private dialog: MatDialog) { }

  ngOnInit() {
    this.getTasks();
  }

  getTasks() {
    if (!this.isAdmin())
    {
      this.searchDto.employeeId = this.authService.decodedToken.nameid;
    }

    this.taskService.getTasks(this.searchDto).subscribe(t => {
      this.tasks = t;
    }, error => {
      this.alertifyService.error('Unable to retrieve tasks!', error);
    })
  }

  isTaskCompleted(task: Task) : boolean {
    let isComplete = true; 
    for (let st of task.subTasks)
    {
      if (st.status === 0)
      {
        isComplete = false;
        break;
      }
    }

    return isComplete;
  }

  taskComletedClass(task: Task): string {
    if(this.isTaskCompleted(task) == true) return "fa fa-check-circle";
    else return "fa fa-circle";
  }

  isOverdue(task: Task): string {
    let now = new Date();
    if ((new Date(task.endTime)) < now && !this.isTaskCompleted(task)) return "Overdue"
     else return ""
  }

  isAdmin() {
    return this.authService.isAdmin();
  }

  completeSubTask(subtask: SubTask) {
    console.log(subtask.id);
    if (subtask.status != 0) return;
    this.taskService.completeSubTask(subtask.id).subscribe(r => {
      this.alertifyService.success('Subtask updated!');
      subtask.status = 1;
    }, error => {
      this.alertifyService.error('Error updated task!', error);
    });
  }

  deleteTask(task: Task) {
    this.taskService.deleteTask(task.id).subscribe(r => {
      this.alertifyService.success("Deleted task!");
      this.tasks.splice(this.tasks.indexOf(task), 1);
    }, error => {
      this.alertifyService.error("Couldn't delete task!", error);
    });
  }

  addSubtask(text: string, task: Task) {
    let st: SubTaskCreationDto = {description: text, tasksId: task.id};

    this.taskService.addSubTask(st).subscribe(subtask => {
      this.alertifyService.success("Subtask Created!");
      task.subTasks.push(subtask);
    }, error => {
      this.alertifyService.error("Unable to create subtask!", error);
    })
  }

  submit(task: TaskCreationDTO) {
    if (!task.employeeId) {this.alertifyService.error('Select an employee!'); return;}
    this.taskService.createTask(task).subscribe(t => {
      this.alertifyService.success('Task Created!');
      this.tasks.push(t);
    }, error => {
      this.alertifyService.error('Unable to create task', error);
    })
  }

  openDialog() {
    const ref = this.dialog.open(CreateTaskComponent, {width: 'auto'});
    ref.afterClosed().subscribe(r => {
      this.submit(r);
    });
  }

  daysFilter = (d: Date | null): boolean => {
    if (d == null) return false;
    return new Date(d.getFullYear(), d.getMonth(), d.getDate() + 1) >= (new Date());
  }

}
