using System;

namespace API.DTOs
{
    public class DashboardReturnDto
    {
        public int Id { get; set; }

        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public DateTime TimeCreated { get; set; }

        public string Content { get; set; }
    }
}