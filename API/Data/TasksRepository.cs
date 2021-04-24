using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class TasksRepository : ITasksRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public TasksRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<bool> AddEmployeeToTask(int taskId, int employeeId)
        {
            var et = new EmployeesTasks { EmployeeId = employeeId, TaskId = taskId };
            await _context.EmployeesTasks.AddAsync(et);
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<Tasks> AddTask(TaskDto taskDto)
        {
            var newTask = _mapper.Map<Tasks>(taskDto);
            await _context.Tasks.AddAsync(newTask);
            if (await _context.SaveChangesAsync() > 0)
                return newTask;
            return null;
        }

        public async Task<bool> DeleteTask(int taskId)
        {
            var newTask = await _context.Tasks.Where(t => t.Id == taskId).FirstOrDefaultAsync();
            _context.Tasks.Remove(newTask);
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<IEnumerable<TaskReturnDto>> GetTasks(TaskSearchDto taskDto)
        {
           return (await _context.EmployeesTasks
           .Where(t => taskDto.employeeId == null? true: t.EmployeeId == taskDto.employeeId)
           .Where(t => taskDto.taskId == null? true: t.TaskId == taskDto.taskId)
           .Where(t => taskDto.status == null? true: t.Status == taskDto.status)
           .Include(t => t.Task)
           .ProjectTo<TaskReturnDto>(_mapper.ConfigurationProvider)
           .ToListAsync())
           .Where(t => taskDto.taskType == null? true: t.Task.type == taskDto.taskType);
        }

        public async Task<bool> UpdateTaskStatus(int taskId, int employeeId, int taskStatus)
        {
            var newTask = await _context.EmployeesTasks.Where(t => t.EmployeeId == employeeId && t.TaskId == taskId).FirstOrDefaultAsync();
            newTask.Status = (API.Entities.TaskStatus)taskStatus;
            return (await _context.SaveChangesAsync() > 0);
        }
    }
}