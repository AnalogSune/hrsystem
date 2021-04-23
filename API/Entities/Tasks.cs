using System;
using System.Collections.Generic;

namespace API.Entities
{

    public enum TaskType{
        Training, Task
    }

    public class Tasks
    {
        public int Id { get; set; }

        public string Title { get; set; }

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