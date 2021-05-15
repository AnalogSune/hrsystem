using API.Entities;

namespace API.DTOs
{
    public class MeetingSearchDto
    {
        public int? DepartmentId { get; set; }
        public int? MeetingType { get; set; }
    }
}