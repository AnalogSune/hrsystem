using API.Entities;

namespace API.DTOs
{
    public class TaskSearchDto
    {
        public int? employeeId { get; set; }
        public int? taskId { get; set; }
        public TaskStatus? status { get; set; }
    }
}