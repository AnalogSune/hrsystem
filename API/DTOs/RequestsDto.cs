using System;

namespace API.DTOs
{
    public class RequestsDto
    {
        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }
    }
}