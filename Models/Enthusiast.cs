using System;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class Enthusiast
    {
        [Key]
        public int EnthusiastId { get; set; }
        public int UserId { get; set; }
        public int HobbyId { get; set; }
        public User User { get; set; }
        public Hobby Hobby { get; set; }
        public string Proficiency { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}