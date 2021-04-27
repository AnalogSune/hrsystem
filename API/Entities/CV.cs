using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public enum CVStatus 
    {
        Pending, Accepted, Declined, MaybeLater
    }

    public class CV
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Fname { get; set; }

        [MaxLength(50)]
        public string Lname { get; set; }

        [MaxLength(30)]
        public string Email { get; set; }

        [MaxLength(200)]
        public string CoverLetter { get; set; }

        [MaxLength(50)]
        public string FileUrl { get; set; }

        [MaxLength(50)]
        public string FileId { get; set; }

        public CVStatus Status { get; set; }

        [MaxLength(50)]
        public string AdminNote { get; set; }
    }
}