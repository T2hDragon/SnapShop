using System.ComponentModel.DataAnnotations;

namespace SnapShop.Models
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
