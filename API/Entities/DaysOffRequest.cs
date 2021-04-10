using System;

namespace API.Entities
{
    public class DaysOffRequest
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public AppUser Employee { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int Status { get; set; }
        
    }
}