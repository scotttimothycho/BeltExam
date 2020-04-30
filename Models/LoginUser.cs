using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeltExam.Models
{

    [NotMapped]
    public class LoginUser
    {
        [Required(ErrorMessage = "is required.")]
        [MinLength(3, ErrorMessage = "must be at least {1} characters")]
        [MaxLength(15, ErrorMessage = "must not be more than {1} characters")]
        [Display(Name = "User Name")]
        public string LoginName { get; set; }

        [Required(ErrorMessage = "is required.")]
        [MinLength(8, ErrorMessage = "must be at least 8 characters")]
        [DataType(DataType.Password)] // auto fills input type attr
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }
    }
}
