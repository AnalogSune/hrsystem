using System;
using API.Entities;

namespace API.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string PictureUrl { get; set; }
        public string PictureId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }

        public string Address { get; set; }

        public string Country { get; set; }

        public string Nationality { get; set; }

        public string PhoneNumber { get; set; }

        public double DaysOffLeft { get; set; }

        public Department InDepartment { get; set; }

        public Role Role { get; set; }

        public int DepartmentId { get; set; }
        public int RoleId { get; set; }

        public bool IsAdmin { get; set; }

        private bool Equals(MemberDto other)
        {
            return Id == other.Id && 
                   Email == other.Email && 
                   PictureUrl == other.PictureUrl && 
                   PictureId == other.PictureId && 
                   DateOfBirth.Equals(other.DateOfBirth) && 
                   FName == other.FName && 
                   LName == other.LName && 
                   Address == other.Address && 
                   Country == other.Country && 
                   Nationality == other.Nationality && 
                   PhoneNumber == other.PhoneNumber && 
                   DaysOffLeft.Equals(other.DaysOffLeft) && 
                   Equals(InDepartment, other.InDepartment) && 
                   Equals(Role, other.Role) && 
                   DepartmentId == other.DepartmentId && 
                   RoleId == other.RoleId && 
                   IsAdmin == other.IsAdmin;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MemberDto) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Email);
            hashCode.Add(PictureUrl);
            hashCode.Add(PictureId);
            hashCode.Add(DateOfBirth);
            hashCode.Add(FName);
            hashCode.Add(LName);
            hashCode.Add(Address);
            hashCode.Add(Country);
            hashCode.Add(Nationality);
            hashCode.Add(PhoneNumber);
            hashCode.Add(DaysOffLeft);
            hashCode.Add(InDepartment);
            hashCode.Add(Role);
            hashCode.Add(DepartmentId);
            hashCode.Add(RoleId);
            hashCode.Add(IsAdmin);
            return hashCode.ToHashCode();
        }
    }
}