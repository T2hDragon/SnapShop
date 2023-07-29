using System.ComponentModel.DataAnnotations;

namespace SnapShop.Models
{
    public class Assignment : Entity
    {
        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }
    }
}
