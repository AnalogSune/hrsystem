using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class WorkShift
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public int Duration { get; set; }
        public DateTime EndTime { get =>  StartTime.AddHours(Duration); }
    }
}