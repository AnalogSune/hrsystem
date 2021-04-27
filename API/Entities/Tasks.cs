using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{

    public enum TaskType{
        Training, Task
    }

    public class Tasks
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Title { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        public DateTime StartTime { get; set; }

        public int Duration { get; set; }

        public TaskType type { get; set; }

        // public ICollection<EmployeesTasks> ETasks{ get; set; }

        public DateTime EndTime 
        {
            get => StartTime.AddDays(Duration);
        }
    }
}