using System.ComponentModel.DataAnnotations;

namespace Taskly.Web.Application.Model
{
    public class RegisterModel
    {
        [Required]
        [MinLength(3)]
        public string Firstname { get; set; }
        [Required]
        [MinLength(3)]
        public string Lastname { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string Pseudo { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateOnly BirthDate { get; set; }
    }
}
