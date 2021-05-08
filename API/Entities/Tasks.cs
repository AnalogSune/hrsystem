using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{

    public enum TaskStatus{
        InProgress, Completed
    }

    public class Tasks
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [Column(TypeName="Date")]
        public DateTime StartTime { get; set; }
        
        public int EmployeeId { get; set; }
        
        public AppUser Employee { get; set; }

        public int Duration { get; set; }

        public ICollection<SubTask> SubTasks { get; set; }

        public TaskStatus Status { get; set; }

        public DateTime EndTime 
        {
            get => StartTime.AddDays(Duration);
        }

        public Tasks()
        {
            StartTime = DateTime.Today;
        }
    }
}