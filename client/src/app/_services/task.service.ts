import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SubTaskCreationDto, Task, TaskCreationDTO } from '../_models/tasks';

@Injectable({
  providedIn: 'root'
})
export class TaskService {

  baseUrl: string = environment.baseUrl;
  constructor(private http: HttpClient) {

  }

  createTask(task: TaskCreationDTO) {
    return this.http.post(this.baseUrl + 'task', task);
  }

  addSubTask(subTask: SubTaskCreationDto) {
    return this.http.post(this.baseUrl + 'task/subtask', subTask);
  }

  getTasks() {
    return this.http.post<Task[]>(this.baseUrl + 'task/search', {});
  }
}
