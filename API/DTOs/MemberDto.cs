using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        public string Username { get; set; }

        public string ProfilePicture { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public Departments Department { get; set; }

        public Role Role { get; set; }
    }
}