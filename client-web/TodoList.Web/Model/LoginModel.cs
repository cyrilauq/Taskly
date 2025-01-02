using System.ComponentModel.DataAnnotations;

namespace TodoList.Web.Model
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
