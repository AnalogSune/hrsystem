using System;
using API.Entities;

namespace API.DTOs
{
    public class CalendarSearchDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int? EmployeeId { get; set; }
        
        public CalendarType? Type { get; set; }

    }
}