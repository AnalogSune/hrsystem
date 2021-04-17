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

        public int EmployeeId { get; set; }

        public AppUser Employee { get; set; }
        
        [Column(TypeName="Date")]
        public DateTime StartDate { get; set; }
        
        [Column(TypeName="Date")]
        public DateTime EndDate { get; set; }

        public CalendarType Type { get; set; }

    }
}