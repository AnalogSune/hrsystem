using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface ITasksRepository
    {
        Task<Tasks> AddTask(TaskDto taskDto);
        Task<IEnumerable<Tasks>> GetTasks(TaskSearchDto taskDto);

        Task<bool> UpdateTaskStatus(int employeeId, int taskId, int taskStatus);

        Task<bool> DeleteTask(int taskId);

        Task<bool> AddEmployeeToTask(int taskId, int employeeId);
    }
}