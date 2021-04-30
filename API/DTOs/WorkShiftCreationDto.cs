using System;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs 
{
    public class WorkShiftCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public int Duration { get; set; }
    }
}