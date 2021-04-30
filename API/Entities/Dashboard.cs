using System;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class Dashboard
    {
        public int Id { get; set; }

        public int PublisherId { get; set; }
        public AppUser Publisher { get; set; }

        public string Content { get; set; }

        public DateTime TimeCreated { get; set; }

        public bool isAdmin { get; set; }

        Dashboard()
        {
            TimeCreated = DateTime.Now;
        }

    }
}