using Microsoft.AspNetCore.Http;

namespace API.DTOs
{
    public class CVDto
    {
        public string Fname { get; set; }

        public string Lname { get; set; }

        public string Email { get; set; }

        public string CoverLetter { get; set; }

        public IFormFile CvFile { get; set; }
    }
}