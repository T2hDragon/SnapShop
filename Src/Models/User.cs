using System.ComponentModel.DataAnnotations;

namespace SnapShop.Models
{
    public class User : Entity
    {
        public User()
        {
        }

        public User(string username, string email)
        {
            this.Username = username;
            this.Email = email;
        }

        [Required]
        [MaxLength(63)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(63)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(63)]
        public required string Email { get; set; }
    }
}
