using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class TaskReturnDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
                
        public MemberDto Employee { get; set; }

        public int Duration { get; set; }

        public ICollection<SubTask> SubTasks { get; set; }

        public DateTime EndTime { get => StartTime.AddDays(Duration); }
    }
}