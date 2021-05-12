using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{

    public enum CalendarType 
    {
        Working, DayOff, WorkFromHome, SickDay
    }

    public class CalendarEntry
    {
        public int Id { get; set; }

        [ForeignKey("users")]
        public int EmployeeId { get; set; }

        
        [Column(TypeName="Date")]
        public DateTime StartDate { get; set; }
        
        [Column(TypeName="Date")]
        public DateTime EndDate { get; set; }

        public CalendarType Type { get; set; }

        [ForeignKey("WorkShift")]
        public int? ShiftId { get; set; }
        
        public WorkShift Shift { get; set; }

    }
}