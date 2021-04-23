using System;
using API.Entities;

namespace API.DTOs
{
    public class TaskDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public TaskType Type { get; set; }
    }
}