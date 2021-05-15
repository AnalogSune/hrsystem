using System;
using API.Entities;

namespace API.DTOs
{
    public class MeetingDto
    {
        public string Description { get; set; }
        public int DepartmentId { get; set; }
        public DateTime Date { get; set; }
        public int DurationHours { get; set; }
        public MeetingType MeetingType { get; set; }
        public string MeetingLink { get; set; }
    }
}