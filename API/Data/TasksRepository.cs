using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TaskStatus = API.Entities.TaskStatus;

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

        public async Task<TaskReturnDto> AddTask(TaskCreationDto taskDto)
        {
            var newTask = _mapper.Map<Tasks>(taskDto);
            var taskAdded = await _context.Tasks.AddAsync(newTask);
            if (await _context.SaveChangesAsync() > 0)
                return _mapper.Map<TaskReturnDto>(await _context.Tasks.Where(t => t.Id == taskAdded.Entity.Id).Include(t => t.Employee).FirstOrDefaultAsync());
            return null;
        }

        public async Task<SubTask> AddSubTask(SubTaskCreationDto subTask)
        {
            var subtask = await _context.SubTasks.AddAsync(_mapper.Map<SubTask>(subTask));
            var task = await _context.Tasks.Where(t => t.Id == subTask.TasksId).FirstOrDefaultAsync();
            task.Status = TaskStatus.InProgress;
            if (await _context.SaveChangesAsync() > 0)
                return subtask.Entity;
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
           return await _context.Tasks
            .Include(t => t.SubTasks)
            .Where(t => taskDto.employeeId == null || t.EmployeeId == taskDto.employeeId)
            .Where(t => taskDto.taskId == null || t.Id == taskDto.taskId)
            .Where(t => taskDto.status == null || t.Status == taskDto.status)
            .Where(t => taskDto.isOverdue == null? true:
                        taskDto.isOverdue == false? t.StartTime.AddDays(t.Duration) >= DateTime.Now:
                        t.StartTime.AddDays(t.Duration) < DateTime.Now
                    )
            .ProjectTo<TaskReturnDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
        }

        public async Task<bool> CompleteSubTask(int taskId)
        {
            var newTask = await _context.SubTasks
                .Where(t => t.Id == taskId)
                .FirstOrDefaultAsync();

            newTask.Status = TaskStatus.Completed;

            var tsk = await _context.Tasks
                .Where(t => t.Id == newTask.TasksId)
                .Include(t => t.SubTasks).FirstOrDefaultAsync();

            TaskStatus status = TaskStatus.Completed;

            foreach (var st in tsk.SubTasks)
            {
                if (st.Status == TaskStatus.InProgress)
                {
                    status = TaskStatus.InProgress;
                    break;
                }
            }
            
            tsk.Status = status;
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}