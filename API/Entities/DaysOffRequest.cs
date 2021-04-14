using System;

namespace API.Entities
{
    public class DaysOffRequest
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public AppUser Employee { get; set; }

        public DateTime Date { get; set; }

        public DateTime DateCreated { get; set; }

        public int Duration { get; set; }

        public int Status { get; set; }
        
        public DateTime EndDate
        {
            get => Date.AddDays(Duration);
        }

        public DaysOffRequest()
        {
            DateCreated = DateTime.Now;
        }
    }
}