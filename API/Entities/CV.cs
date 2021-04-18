namespace API.Entities
{
    public enum CVStatus 
    {
        Pending, Accepted, Declined, MaybeLater
    }

    public class CV
    {
        public int Id { get; set; }

        public string Fname { get; set; }

        public string Lname { get; set; }

        public string Email { get; set; }

        public string CoverLetter { get; set; }

        public string FileUrl { get; set; }

        public string FileId { get; set; }

        public CVStatus Status { get; set; }

        public string AdminNote { get; set; }
    }
}