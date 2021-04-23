using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TaskController : BaseApiController
    {
        private readonly ITasksRepository _tasksRepository;
        public TaskController(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Tasks>> AddTask(TaskDto taskDto)
        {
            return await _tasksRepository.AddTask(taskDto);
        }

        [Authorize]
        [HttpPost("addemployee/{taskid}/{employeeid}")]
        public async Task<ActionResult<bool>> AddEmployeeToTask(int taskId, int employeeId)
        {
            return await _tasksRepository.AddEmployeeToTask(taskId, employeeId);
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteTask(int id)
        {
            return await _tasksRepository.DeleteTask(id);
        }
        
        [Authorize]
        [HttpPut("{taskid}/{employeeid}/{statusid}")]
        public async Task<ActionResult<bool>> UpdateTask(int taskid, int employeeid, int statusid)
        {
            return await _tasksRepository.UpdateTaskStatus(taskid, employeeid, statusid);
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeesTasks>>> GetTasks(TaskSearchDto taskSearchDto)
        {
            return Ok(await _tasksRepository.GetTasks(taskSearchDto));
        }
    }
}