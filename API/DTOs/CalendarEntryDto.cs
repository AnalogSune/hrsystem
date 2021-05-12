using System;
using System.ComponentModel.DataAnnotations.Schema;
using API.Entities;

namespace API.DTOs
{
    public class CalendarEntryDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EmployeeId { get; set; }        
        public CalendarType Type { get; set; }
        public bool CreateNewEntry { get; set; }
        public int? ShiftId { get; set; }
    }
}