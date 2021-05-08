using System;
using System.Collections.Generic;
using API.Entities;

namespace API.DTOs
{
    public class TaskCreationDto
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime StartTime { get; set; }
        public int Duration { get; set; }

        public int EmployeeId { get; set; }

    }
}