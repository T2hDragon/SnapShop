using System.ComponentModel.DataAnnotations;

namespace SnapShop.Form.Model
{
    public class Login
    {
        [MinLength(4)]
        [MaxLength(63)]
        [Display(Name = "form.model.login.username")]
        public required string Username { get; set; }

        [MinLength(4)]
        [MaxLength(63)]
        public required string Password { get; set; }
    }
}
