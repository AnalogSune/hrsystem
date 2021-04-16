using System;

namespace API.Entities
{
    public enum RequestStatus
    {
        Pending, Accepted, Declined
    }
    public enum RequestType
    {
        DayOff, WorkHome
    }
    public class Request
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }
        public AppUser Employee { get; set; }
        public DateTime Date { get; set; }

        public DateTime DateCreated { get; set; }

        public int Duration { get; set; }

        public RequestStatus Status { get; set; }
        public RequestType requestType { get; set; }
        public DateTime EndDate
        {
            get => Date.AddDays(Duration);
        }

        Request()
        {
            DateCreated = DateTime.Now;
            Status = RequestStatus.Pending;
        }
    }
}