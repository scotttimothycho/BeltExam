using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{
    public class User
    {
        [Key] // denotes PK, not needed if named ModelNameId
        public int UserId { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least {1} characters")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(2, ErrorMessage = "must be at least {1} characters")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(3, ErrorMessage = "must be at least {1} characters")]
        [MaxLength(15, ErrorMessage = "must not be more than {1} characters")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(8, ErrorMessage = "must be at least {1} characters")]
        [DataType(DataType.Password)] // auto fills input type attr
        public string Password { get; set; }

        [NotMapped] // don't add to DB
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "doesn't match password")]
        [Display(Name = "Confirm Password")]
        public string PasswordConfirm { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Enthusiast> Hobbies { get; set; }


        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }
    }
}