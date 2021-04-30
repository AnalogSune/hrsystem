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
    [Authorize]
    public class TaskController : BaseApiController
    {
        private readonly ITasksRepository _tasksRepository;
        public TaskController(ITasksRepository tasksRepository)
        {
            _tasksRepository = tasksRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddTask(TaskDto taskDto)
        {
            return Ok(await _tasksRepository.AddTask(taskDto));
        }

        [HttpPost("addemployee/{taskid}/{employeeid}")]
        public async Task<IActionResult> AddEmployeeToTask(int taskId, int employeeId)
        {
            return Ok(await _tasksRepository.AddEmployeeToTask(taskId, employeeId));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (await _tasksRepository.DeleteTask(id))
                return Ok();
            
            return BadRequest("Unable to delete the task!");
        }
        
        [HttpPut("{taskid}/{employeeid}/{statusid}")]
        public async Task<IActionResult> UpdateTask(int taskid, int employeeid, int statusid)
        {
            if (await _tasksRepository.UpdateTaskStatus(taskid, employeeid, statusid))
                return Ok();
                
            return BadRequest("Unable to update the task!");
        }
        
        [HttpGet]
        public async Task<IActionResult> GetTasks(TaskSearchDto taskSearchDto)
        {
            return Ok(await _tasksRepository.GetTasks(taskSearchDto));
        }
    }
}