import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { SubTaskCreationDto, Task, TaskCreationDTO, TaskSearchDto } from '../_models/tasks';

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

  completeSubTask(subTaskId: number) {
    return this.http.put(this.baseUrl + 'task/' + subTaskId, {});
  }

  getTasks(searchDto: TaskSearchDto = {}) {
    return this.http.post<Task[]>(this.baseUrl + 'task/search', searchDto);
  }

  deleteTask(id: number) {
    return this.http.delete(this.baseUrl + 'task/' + id);
  }
}
