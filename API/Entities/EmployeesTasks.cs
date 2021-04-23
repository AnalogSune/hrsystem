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
        public AppUser Employee { get; set; }

        [ForeignKey("tasks")]
        public int TaskId { get; set; }
        public Tasks tasks { get; set; }

        public TaskStatus Status { get; set; }
    }
}