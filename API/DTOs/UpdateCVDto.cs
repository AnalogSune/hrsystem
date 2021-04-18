using API.Entities;

namespace API.DTOs
{
    public class UpdateCVDto
    {
        public int Id { get; set; }
        public string AdminNotes { get; set; }
        public CVStatus Status { get; set; }
    }
}