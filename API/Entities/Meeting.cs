using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    public enum MeetingType{
        Training, Meeting
    }

    public class Meeting
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [ForeignKey("departments")]
        public int DepartmentId { get; set; }

        public DateTime Date { get; set; }

        public int DurationHours { get; set; }

        public DateTime EndTime { get => Date.AddHours(DurationHours); }

        public MeetingType MeetingType { get; set; }

        public string MeetingLink { get; set; }
    }
}