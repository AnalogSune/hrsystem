namespace API.DTOs
{
    public class PersonalFilesDto
    {
        public int Id { get; set; }

        public int FileOwnerId { get; set; }
        public string FileUrl { get; set; }
        public string OriginalFileName { get; set; }
    }
}