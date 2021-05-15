using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ITasksRepository
    {
        Task<TaskReturnDto> AddTask(TaskCreationDto taskDto);
        Task<SubTask> AddSubTask(SubTaskCreationDto subTask);
        Task<bool> DeleteTask(int taskId);
        Task<IEnumerable<TaskReturnDto>> GetTasks(TaskSearchDto taskDto);
        Task<bool> CompleteSubTask(int taskId);
    }
}