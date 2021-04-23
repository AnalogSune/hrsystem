using API.Entities;

namespace API.DTOs
{
    public class TaskReturnDto
    {
        public Tasks Task { get; set; }
        public int status { get; set; }
    }
}