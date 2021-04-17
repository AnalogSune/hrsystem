using System;
using API.Entities;

namespace API.DTOs
{
    public class RequestsDto
    {
        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }
        public RequestType requestType { get; set; }
        public DateTime EndDate
        {
            get => Date.AddDays(Duration);
        }
    }
}