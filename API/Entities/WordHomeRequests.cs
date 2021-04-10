using System;

namespace API.Entities
{
    public class WordHomeRequests
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        public DateTime Date { get; set; }

        public int Status { get; set; }
    }
}