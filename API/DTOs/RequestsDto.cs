using System;
using API.Entities;

namespace API.DTOs
{
    public class RequestsDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public int Duration { get; set; }
        public int Status { get; set; }
        public RequestType requestType { get; set; }
        public int ShiftId {get; set;} = 1;
        public DateTime EndDate
        {
            get => Date.AddDays(Duration);
            set => Duration = (value - Date).Days;
        }
    }
}