using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class PersonalFile
    {
        public int Id { get; set; }

        public int FileOwnerId { get; set; }

        public AppUser FileOwner { get; set; }
        public string FileUrl { get; set; }
        public string FileType { get; set; }
        public string OriginalFileName { get; set; }
        public string FileId { get; set; }
    }
}