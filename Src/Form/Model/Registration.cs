using System.ComponentModel.DataAnnotations;
using SnapShop.Form.Validation;

namespace SnapShop.Form.Model
{
    public class Registration
    {
        [MinLength(4)]
        [MaxLength(63)]
        public required string Username { get; set; }

        [MinLength(4)]
        [MaxLength(63)]
        [Password(MinimumLowerCases = 1, MinimumUpperCases = 1, MinimumNumbers = 2, MinimumSpecialCharacters = 1)]
        public required string Password { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(127)]
        public required string Email { get; set; }
    }
}
