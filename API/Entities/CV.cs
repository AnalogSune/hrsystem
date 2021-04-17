namespace API.Entities
{
    public class CV
    {
        public int Id { get; set; }

        public string Fname { get; set; }

        public string Lname { get; set; }

        public string Email { get; set; }

        public string CoverLetter { get; set; }

        public string FileUrl { get; set; }

        public string FileId { get; set; }

        public int Status { get; set; }

        public string AdminNote { get; set; }
    }
}