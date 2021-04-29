using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class PersonalFiles
    {
        public int Id { get; set; }

        public int FileOwnerId { get; set; }

        public AppUser FileOwner { get; set; }

        [MaxLength(500)]
        public string FileUrl { get; set; }
        
        [MaxLength(50)]
        public string OriginalFileName { get; set; }

        [MaxLength(500)]
        public string FileId { get; set; }
    }
}