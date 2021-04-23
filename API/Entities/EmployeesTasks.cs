using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public enum TaskStatus {
        NotComplete, Complete
    }

    public class EmployeesTasks
    {
        [ForeignKey("users")]
        public int EmployeeId { get; set; }

        [ForeignKey("tasks")]
        public int TaskId { get; set; }
        public Tasks Task { get; set; }

        public TaskStatus Status { get; set; }
    }
}