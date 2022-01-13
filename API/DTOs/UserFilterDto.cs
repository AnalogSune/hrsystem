using System;

namespace API.DTOs
{
    public class UserFilterDto
    {
        public string Email { get; set; }

        public string FName { get; set; }

        public string LName { get; set; }      

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        protected bool Equals(UserFilterDto other)
        {
            return Email == other.Email && FName == other.FName && LName == other.LName && Address == other.Address && PhoneNumber == other.PhoneNumber;
        }

        
        public override bool Equals(object obj)
        
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserFilterDto) obj);
        }
        
        public override int GetHashCode()
        {
            return HashCode.Combine(Email, FName, LName, Address, PhoneNumber);
        }
    }
}