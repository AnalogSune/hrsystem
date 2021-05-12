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
        public async Task<IActionResult> AddTask(TaskCreationDto taskDto)
        {
            return Ok(await _tasksRepository.AddTask(taskDto));
        }

        [HttpPost("subtask")]
        public async Task<IActionResult> AddSubTask(SubTaskCreationDto subTask)
        {
            return Ok(await _tasksRepository.AddSubTask(subTask));
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (await _tasksRepository.DeleteTask(id))
                return Ok();
            
            return BadRequest("Unable to delete the task!");
        }
        
        [HttpPut("{taskid}")]
        public async Task<IActionResult> CompleteSubTask(int taskid)
        {
            if (await _tasksRepository.CompleteSubTask(taskid))
                return Ok();
                
            return BadRequest("Unable to update the task!");
        }
        
        [HttpPost("search")]
        public async Task<IActionResult> GetTasks(TaskSearchDto taskSearchDto)
        {
            return Ok(await _tasksRepository.GetTasks(taskSearchDto));
        }
    }
}