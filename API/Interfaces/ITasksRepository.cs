using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ITasksRepository
    {
        Task<Tasks> AddTask(TaskDto taskDto);
        Task<IEnumerable<TaskReturnDto>> GetTasks(TaskSearchDto taskDto);

        Task<bool> UpdateTaskStatus(int taskId, int employeeId, int taskStatus);

        Task<bool> DeleteTask(int taskId);

        Task<bool> AddEmployeeToTask(int taskId, int employeeId);
    }
}