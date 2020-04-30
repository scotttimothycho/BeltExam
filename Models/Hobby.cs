using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeltExam.Models
{
    public class Hobby
    {
        [Key] // denotes PK, not needed if named ModelNameId
        public int HobbyId { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least {1} characters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least {1} characters")]
        [Display(Name = "Description")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Enthusiast> Enthusiasts { get; set; }
    }
}