using API.Entities;

namespace API.DTOs
{
    public class RequestSearchDto
    {
        public int? EmployeeId { get; set; }
        public RequestStatus? requestStatus { get; set; }
        public RequestType? requestType { get; set; }
    }
}