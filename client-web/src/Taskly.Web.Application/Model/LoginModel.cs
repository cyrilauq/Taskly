using System.ComponentModel.DataAnnotations;

namespace Taskly.Web.Application.Model
{
    public class LoginModel
    {
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(5)]
        public string Login { get; set; }
    }
}
